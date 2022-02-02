using Loans.Frontend.Service.Contracts;

namespace Loans.Frontend.Service;

public class NotificationService : INotificationService
{
    public event Action<string, bool> OnShow;
    public event Action OnHide;

    public async Task ShowErrorNotification(string message)
    {
        OnShow?.Invoke(message, false);
        await Task.Delay(5000);
        OnHide?.Invoke();
    }

    public async Task ShowSuccessNotification(string message)
    {
        OnShow?.Invoke(message, true);
        await Task.Delay(5000);
        OnHide?.Invoke();
    }
}