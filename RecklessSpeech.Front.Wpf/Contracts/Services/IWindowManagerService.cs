using System.Windows;

namespace RecklessSpeech.Front.Wpf.Contracts.Services
{
    public interface IWindowManagerService
    {
        Window MainWindow { get; }

        void OpenInNewWindow(string pageKey, object parameter = null);

        bool? OpenInDialog(string pageKey, object parameter = null);

        Window GetWindow(string pageKey);
    }
}
