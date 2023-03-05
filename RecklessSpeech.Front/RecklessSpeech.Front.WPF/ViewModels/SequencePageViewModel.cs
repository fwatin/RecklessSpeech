using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecklessSpeech.Front.WPF.ViewModels
{
    public class SequencePageViewModel : INotifyPropertyChanged
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
        private readonly HttpBackEndGateway backEndGateway;

        public int Progress
        {
            get
            {
                return this.progress;
            }
            set
            {
                this.progress = value;
                OnPropertyChanged("Progress");
            }
        }



        //commands
        public ICommand AddSequencesCommand { get; }
        public ICommand ImportSequenceDetailsCommand { get; }
        public ICommand EnrichDutchSequenceCommand { get; }
        public ICommand EnrichEnglishSequenceCommand { get; }
        public ICommand SendSequenceToAnkiCommand { get; }


        public SequencePageViewModel(HttpBackEndGateway backEndGateway)
        {
            this.backEndGateway = backEndGateway;
            this.Sequences = new ObservableCollection<SequenceDto>();

            this.AddSequencesCommand = new DelegateCommand<string>(async s => await AddSequences(s));
            this.ImportSequenceDetailsCommand = new DelegateCommand<string>(async s => await ImportSequenceDetails(s));
            this.EnrichDutchSequenceCommand = new DelegateCommand<SequenceDto>(async s => await EnrichSequenceDutch(s));
            this.EnrichEnglishSequenceCommand = new DelegateCommand<SequenceDto>(async s => await EnrichSequenceEnglish(s));
            this.SendSequenceToAnkiCommand = new DelegateCommand<SequenceDto>(async s => await SendSequenceToAnki(s));
        }

        private async Task AddSequences(string filePath)
        {
            await this.backEndGateway.ImportSequencesFromCsvFile(filePath);

            IReadOnlyCollection<SequenceDto> newSequences = await this.backEndGateway.GetAllSequences();
            this.Sequences.Clear();

            foreach (SequenceDto newSequence in newSequences)
            {
                this.Sequences.Add(newSequence);
            }
        }

        private async Task ImportSequenceDetails(string filePath)
        {
            await this.backEndGateway.ImportSequencesDetailsFromJson(filePath);

            IReadOnlyCollection<SequenceDto> newSequences = await this.backEndGateway.GetAllSequences();
            this.Sequences.Clear();

            foreach (SequenceDto newSequence in newSequences)
            {
                this.Sequences.Add(newSequence);
            }
        }

        

        private async Task EnrichSequenceDutch(SequenceDto sequence)
        {
            await this.backEndGateway.EnrichSequenceDutch(sequence.Id);

            SequenceDto updatedSequence = await this.backEndGateway.GetOneSequence(sequence.Id);

            sequence.Explanation = updatedSequence.Explanation;
        }

        private async Task EnrichSequenceEnglish(SequenceDto sequence)
        {
            await this.backEndGateway.EnrichSequenceEnglish(sequence.Id);

            SequenceDto updatedSequence = await this.backEndGateway.GetOneSequence(sequence.Id);

            sequence.Explanation = updatedSequence.Explanation;
        }

        private async Task SendSequenceToAnki(SequenceDto sequence)
        {
            await this.backEndGateway.SendSequenceToAnki(sequence.Id);
        }
    }
}
