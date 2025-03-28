using System.Windows.Controls;

using RecklessSpeech.Front.Wpf.Contracts.Views;
using RecklessSpeech.Front.Wpf.ViewModels;

using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;

namespace RecklessSpeech.Front.Wpf.Views
{
    public partial class ShellDialogWindow : ChromelessWindow, IShellDialogWindow
    {
        public ShellDialogWindow(ShellDialogViewModel viewModel)
        {
            InitializeComponent();
            viewModel.SetResult = OnSetResult;
            DataContext = viewModel;
        }

        public Frame GetDialogFrame()
            => dialogFrame;

        private void OnSetResult(bool? result)
        {
            DialogResult = result;
            Close();
        }
    }
}
