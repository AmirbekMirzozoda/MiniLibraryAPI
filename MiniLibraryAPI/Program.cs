using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MiniLibraryAPI.AutoMapper;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.Services;
using MiniLibraryAPI.Workers;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:7000");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(conf => conf.UseNpgsql(connection));
builder.Services.AddScoped<ICategoryService, CategoryService>();

// builder.Services.AddHostedService<WorkerWithTimer>();
// builder.Services.AddHostedService<SimpleWorker>();

var app = builder.Build();

var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

var workerWithQuartz = JobBuilder.Create<WorkerWithQuartz>()
    .WithIdentity("WorkerWithQuartz")
    .Build();

// каждый 100 миллисекунд
var workerWithQuartzTrigger = TriggerBuilder.Create()
    .WithIdentity("WorkerWithQuartzTrigger")
    .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
        .WithInterval(TimeSpan.FromMilliseconds(100))
        .RepeatForever())
    // .WithSchedule(CronScheduleBuilder.CronSchedule("*/10 * * * *"))
    // .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(19, 05))
    // .WithSimpleSchedule(scheduleBuilder => scheduleBuilder.WithIntervalInSeconds(100).RepeatForever())
    .ForJob(workerWithQuartz)
    .Build();

scheduler.ScheduleJob(workerWithQuartz, workerWithQuartzTrigger)
    .GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// app.UseHttpsRedirection();


using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync();
app.Run();