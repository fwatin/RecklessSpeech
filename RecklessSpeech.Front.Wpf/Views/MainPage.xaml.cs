using System.Windows.Controls;

using RecklessSpeech.Front.Wpf.ViewModels;

namespace RecklessSpeech.Front.Wpf.Views
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
