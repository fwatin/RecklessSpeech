using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecklessSpeech.Front.WPF.App.ViewModels
{
    public class FlowPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<SequenceDto> sequences;

        public ObservableCollection<SequenceDto> Sequences
        {
            get => this.sequences;
            init
            {
                this.sequences = value;
                OnPropertyChanged("Sequences");
            }
        }

        private readonly BackEndGateway backEndGateway;


        //commands
        public ICommand AddSequencesCommand { get; }


        public FlowPageViewModel()
        {
            this.backEndGateway = new BackEndGateway();

            this.Sequences = new ObservableCollection<SequenceDto>();

            //todo essayer une relaycommand plutot c'est natif wpf
            this.AddSequencesCommand = new DelegateCommand<string>(async s => await AddSequences(s));
        }

        private async Task AddSequences(string filePath)
        {
            await BackEndGateway.ImportSequencesFromCsvFile(filePath);

            IReadOnlyCollection<SequenceDto> newSequences = await BackEndGateway.GetAllSequences();
            foreach (SequenceDto newSequence in newSequences)
            {
                this.Sequences.Add(newSequence);
            }
        }
    }
}