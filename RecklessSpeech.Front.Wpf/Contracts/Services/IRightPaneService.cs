using System;
using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Contracts.Services
{
    public interface IRightPaneService
    {
        event EventHandler PaneOpened;

        event EventHandler PaneClosed;

        void OpenInRightPane(string pageKey, object parameter = null);

        void Initialize(Frame rightPaneFrame);

        void CleanUp();
    }
}
