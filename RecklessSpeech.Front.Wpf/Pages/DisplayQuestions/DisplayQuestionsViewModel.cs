using RecklessSpeech.Front.Wpf.Helpers;
using System.Collections.ObjectModel;

namespace RecklessSpeech.Front.Wpf.Pages.DisplayQuestions
{
    public class DisplayQuestionsViewModel: Observable
    {
        public ObservableCollection<QuestionModel> Questions { get; set; }

        public DisplayQuestionsViewModel()
        {
            Questions = new ObservableCollection<QuestionModel>
            {
                new QuestionModel { Question = "What is the capital of France?", Answer = "Paris" },
                new QuestionModel { Question = "What is the capital of Germany?", Answer = "Berlin" },
                new QuestionModel { Question = "What is the capital of Italy?", Answer = "Rome" },
                new QuestionModel { Question = "What is the capital of Spain?", Answer = "Madrid" },
                new QuestionModel { Question = "What is the capital of Portugal?", Answer = "Lisbon" },
                new QuestionModel { Question = "What is the capital of the United Kingdom?", Answer = "London" },
                new QuestionModel { Question = "What is the capital of the United States?", Answer = "Washington, D.C." },
                new QuestionModel { Question = "What is the capital of Canada?", Answer = "Ottawa" },
                new QuestionModel { Question = "What is the capital of Mexico?", Answer = "Mexico City" },
                new QuestionModel { Question = "What is the capital of Brazil?", Answer = "Brasília" }
            };
        }
    }
}