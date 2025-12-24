namespace MiniLibraryAPI.Workers;

public class WorkerWithTimer : BackgroundService
{
    private Timer _timer;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(Work, null, 2000, Timeout.Infinite);
        return Task.CompletedTask;
    }

    private void Work(object state)
    {
        Console.WriteLine("WorkerWithTimer running at: " + DateTime.Now);
        
        _timer.Change(2000, Timeout.Infinite);
    }
}