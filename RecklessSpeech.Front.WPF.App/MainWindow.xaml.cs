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
        public FlowPageVM ViewModel { get => (FlowPageVM)DataContext; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new FlowPageVM(); //todo renommer avec suffixe ViewModel
        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                ViewModel.AddFlowCommand.Execute(filePath);
            }
        }
    }
}
