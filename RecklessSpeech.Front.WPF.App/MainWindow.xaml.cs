using Microsoft.Win32;
using RecklessSpeech.Front.WPF.App.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace RecklessSpeech.Front.WPF.App
{
    public partial class MainWindow : Window
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public SequencePageViewModel ViewModel => (SequencePageViewModel)this.DataContext;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new SequencePageViewModel(new BackEndGateway());
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

        private void ContextMenu_Enrich_Click(object sender, RoutedEventArgs e)
        {
            int total = SequenceListView.SelectedItems.Count;
            int count = 0;

            this.ViewModel.Progress = 0;
            foreach (SequenceDto sequence in this.SequenceListView.SelectedItems)
            {
                this.ViewModel.EnrichSequenceCommand.Execute(sequence);
                this.ViewModel.Progress = ++count / total * 100;
            }
        }

        private void ContextMenu_Send_to_Anki_Click(object sender, RoutedEventArgs e)
        {
            int total = SequenceListView.SelectedItems.Count;
            int count = 0;

            this.ViewModel.Progress = 0;
            foreach (SequenceDto sequence in this.SequenceListView.SelectedItems)
            {
                this.ViewModel.SendSequenceToAnkiCommand.Execute(sequence);
                this.ViewModel.Progress = ++count / total * 100;
            }
        }
    }
}