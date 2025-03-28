using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Pages.DisplayQuestions
{
    public partial class DisplayQuestionsPage : Page
    {
        public DisplayQuestionsViewModel ViewModel => (DisplayQuestionsViewModel)this.DataContext;

        public DisplayQuestionsPage(DisplayQuestionsViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}