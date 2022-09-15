using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecklessSpeech.Front.WPF.App.ViewModels
{
    public class SequenceDto: INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Word { get; set; }

        private string explanation;
        public string? Explanation
        {
            get
            {
                return this.explanation;
            }
            set
            {
                this.explanation = value;
                OnPropertyChanged("explanation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}