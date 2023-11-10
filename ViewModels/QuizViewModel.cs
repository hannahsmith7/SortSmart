using SortSmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SortSmart.ViewModels.ReplacingBooksViewModel;
using System.Windows.Input;

namespace SortSmart.ViewModels
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<QuizItem> Questions { get; set; }
        private QuizItem currentQuestion;
        private int currentIndex = 0;
        private int score;

        public QuizItem CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        // Command for selecting an answer
        public ICommand SelectAnswerCommand { get; private set; }

        public QuizViewModel()
        {
            // Initialize the questions and commands here
            SelectAnswerCommand = new RelayCommand(SelectAnswer);
        }

        private void SelectAnswer(object obj)
        {
            if (obj is int index && index == CurrentQuestion.CorrectAnswerIndex)
            {
                Score++;
            }

            // Move to the next question
            if (currentIndex < Questions.Count - 1)
            {
                currentIndex++;
                CurrentQuestion = Questions[currentIndex];
            }
            else
            {
                // Handle the end of the quiz here
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
