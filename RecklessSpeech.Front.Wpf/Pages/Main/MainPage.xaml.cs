using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Pages.Main
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
