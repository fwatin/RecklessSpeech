using RecklessSpeech.Front.Wpf.Contracts.Services;
using RecklessSpeech.Front.Wpf.Contracts.Views;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Pages.Shell
{
    public partial class ShellWindow : ChromelessWindow, IShellWindow
    {
        public string themeName = App.Current.Properties["Theme"]?.ToString()!= null? App.Current.Properties["Theme"]?.ToString(): "Windows11Light";
        public ShellWindow(IPageService pageService, ShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            SfSkinManager.SetTheme(this, new Theme(this.themeName));
        }

        public Frame GetNavigationFrame()
            => this.shellFrame;

        public void ShowWindow()
            => this.Show();

        public void CloseWindow()
            => this.Close();
    }
}
