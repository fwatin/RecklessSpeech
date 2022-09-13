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

        private int progress;
        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }



        //commands
        public ICommand AddSequencesCommand { get; }
        public ICommand EnrichSequenceCommand { get; }
        public ICommand SendSequenceToAnkiCommand { get; }


        public FlowPageViewModel()
        {
            this.Sequences = new ObservableCollection<SequenceDto>();

            this.AddSequencesCommand = new DelegateCommand<string>(async s => await AddSequences(s));
            this.EnrichSequenceCommand = new DelegateCommand<SequenceDto>(async s => await EnrichSequence(s));
            this.SendSequenceToAnkiCommand = new DelegateCommand<SequenceDto>(async s => await SendSequenceToAnki(s));
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
        
        private async Task EnrichSequence(SequenceDto sequence)
        {
            await BackEndGateway.EnrichSequence(sequence.Id);
        }
        
        private async Task SendSequenceToAnki(SequenceDto sequence)
        {
            await BackEndGateway.SendSequenceToAnki(sequence.Id);
        }
    }
}