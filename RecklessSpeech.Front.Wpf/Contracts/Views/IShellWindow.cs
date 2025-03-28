using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
