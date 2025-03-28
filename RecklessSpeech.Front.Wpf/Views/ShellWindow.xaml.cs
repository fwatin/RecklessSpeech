using System;
using System.Windows;
using System.Windows.Controls;

using RecklessSpeech.Front.Wpf.Contracts.Services;
using RecklessSpeech.Front.Wpf.Contracts.Views;
using RecklessSpeech.Front.Wpf.ViewModels;

using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;

namespace RecklessSpeech.Front.Wpf.Views
{
    public partial class ShellWindow : ChromelessWindow, IShellWindow
    {
        public string themeName = App.Current.Properties["Theme"]?.ToString()!= null? App.Current.Properties["Theme"]?.ToString(): "Windows11Light";
        public ShellWindow(IPageService pageService, ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            SfSkinManager.SetTheme(this, new Theme(themeName));
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
