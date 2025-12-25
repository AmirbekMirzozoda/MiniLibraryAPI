using Quartz;

namespace MiniLibraryAPI.Workers;

public class WorkerWithQuartz : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("WorkerWithQuartz running at: " + DateTime.Now);

        // Implement your job logic here
        return Task.CompletedTask;
    }
}