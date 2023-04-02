namespace Sovos.Invoicing.BackgroundTasks.Absractions.Tasks;

public interface IBackgroundTask
{
    Task HandleAsync();
}