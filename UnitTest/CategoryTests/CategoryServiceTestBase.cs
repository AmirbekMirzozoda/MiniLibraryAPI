using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;
using Moq;

namespace UnitTest.CategoryTests;

public abstract class CategoryServiceTestBase
{
    protected readonly ApplicationDbContext Context;
    protected readonly IMapper Mapper;
    protected readonly Mock<ILogger<CategoryService>> LoggerMock;

    protected CategoryServiceTestBase()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=mini_library_test;Username=postgres;Password=postgres"
            )
            .Options;

        Context = new ApplicationDbContext(options);
        
        //Context.Database.EnsureDeleted();
        Context.Database.Migrate();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AddCategoryDto, Category>();
        });

        Mapper = mapperConfig.CreateMapper();

        LoggerMock = new Mock<ILogger<CategoryService>>();
    }

    protected CategoryService CreateService()
        => new CategoryService(Context, Mapper, LoggerMock.Object);
}