using RecklessSpeech.Front.Wpf.Contracts.Views;
using Syncfusion.Windows.Shared;
using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Pages.ShellDialog
{
    public partial class ShellDialogWindow : ChromelessWindow, IShellDialogWindow
    {
        public ShellDialogWindow(ShellDialogViewModel viewModel)
        {
            InitializeComponent();
            viewModel.SetResult = this.OnSetResult;
            this.DataContext = viewModel;
        }

        public Frame GetDialogFrame()
            => this.dialogFrame;

        private void OnSetResult(bool? result)
        {
            this.DialogResult = result;
            this.Close();
        }
    }
}
