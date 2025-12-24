
namespace MiniLibraryAPI.Workers;

public class SimpleWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Console.WriteLine("SimpleWorker running at: " + DateTime.Now);
                // выполнение фоновой задачи
            }
            catch (Exception ex)
            {
                // обработка ошибки однократного неуспешного выполнения фоновой задачи
            }
 
            await Task.Delay(1000);
        }
    }
}