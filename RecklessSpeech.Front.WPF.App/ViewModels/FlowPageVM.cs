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
    public class FlowPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<SequenceDto> sequences;
        public ObservableCollection<SequenceDto> Sequences
        {
            get
            {
                return sequences;
            }
            set
            {
                sequences = value;
                OnPropertyChanged("Sequences");
            }
        }

        private FacadeClient facadeClient;


        //commands
        public ICommand AddFlowCommand { get; set; } //todo renommer en AddSequencesCommand


        public FlowPageVM()
        {
            this.facadeClient = new FacadeClient();

            Sequences = new ObservableCollection<SequenceDto>();

            //todo essayer une relaycommand plutot c'est natif wpf
            AddFlowCommand = new DelegateCommand<string>(async s => await AddFlow(s));
        }

        private async Task AddFlow(string filePath)
        {
            await facadeClient.Flow_CreateAPI(filePath);

            //IReadOnlyCollection<SequenceDto> newSequences = await facadeClient.GetAllSequences();
            //foreach (var newSequence in newSequences)
            //{
            //    Sequences.Add(newSequence);
            //}
        }
    }
}
