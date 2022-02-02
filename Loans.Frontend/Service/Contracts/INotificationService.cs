namespace Loans.Frontend.Service.Contracts;

public interface INotificationService
{
    public event Action<string, bool> OnShow;
    public event Action OnHide;
    public Task ShowErrorNotification(string message);

    public Task ShowSuccessNotification(string message);
}