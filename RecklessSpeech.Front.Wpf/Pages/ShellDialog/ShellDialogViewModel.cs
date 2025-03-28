using RecklessSpeech.Front.Wpf.Helpers;
using System;
using System.Windows.Input;

namespace RecklessSpeech.Front.Wpf.Pages.ShellDialog
{
    public class ShellDialogViewModel : Observable
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand => this._closeCommand ?? (this._closeCommand = new RelayCommand(this.OnClose));

        public Action<bool?> SetResult { get; set; }

        public ShellDialogViewModel()
        {
        }

        private void OnClose()
        {
            bool result = true;
            this.SetResult(result);
        }
    }
}
