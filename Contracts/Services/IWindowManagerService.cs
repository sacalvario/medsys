using System.Windows;

namespace ECN.Contracts.Services
{
    public interface IWindowManagerService
    {
        Window MainWindow { get; }

        void OpenInNewWindow(string pageKey, object parameter = null);

        bool? OpenInDialog(string pageKey, object parameter);

        Window GetWindow(string pageKey);
    }
}
