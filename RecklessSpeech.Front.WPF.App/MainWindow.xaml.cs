using Microsoft.Win32;
using RecklessSpeech.Front.WPF.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecklessSpeech.Front.WPF.App
{
    public partial class MainWindow : Window
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public FlowPageViewModel ViewModel => (FlowPageViewModel) this.DataContext;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new FlowPageViewModel();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();

            if (openFileDialog.ShowDialog() is false) return;

            string filePath = openFileDialog.FileName;

            this.ViewModel.AddSequencesCommand.Execute(filePath);
        }
    }
}