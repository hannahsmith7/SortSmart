using SortSmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SortSmart.Models.DeweyDecimalModel;

namespace SortSmart.ViewModels
{
    internal class FindingCallNumbersViewModel : INotifyPropertyChanged
    {
        private readonly DeweyDecimalModel _model;
        private ClassificationNode _currentNode;
        private Random _random = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to raise the PropertyChanged event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties
        private ObservableCollection<ClassificationOption> _options = new ObservableCollection<ClassificationOption>();
        public ObservableCollection<ClassificationOption> Options
        {
            get => _options;
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }

        public ClassificationNode CurrentQuestion
        {
            get => _currentNode;
            set
            {
                _currentNode = value;
                OnPropertyChanged();
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCorrectAnswer;
        public bool IsCorrectAnswer
        {
            get => _isCorrectAnswer;
            set
            {
                if (_isCorrectAnswer != value)
                {
                    _isCorrectAnswer = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isQuizActive;
        public bool IsQuizActive
        {
            get => _isQuizActive;
            set
            {
                if (_isQuizActive != value)
                {
                    _isQuizActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public FindingCallNumbersViewModel()
        {
            _model = new DeweyDecimalModel();
            // Get the path to the directory where the executable is running
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the relative path to the JSON file
            string jsonFilePath = Path.Combine(baseDirectory, "Files", "DeweyDecimalList.json");

            // Use jsonFilePath to load the JSON file
            _model.LoadData(jsonFilePath);
            StartNewQuestion();
        }

        // Resets the quiz and generates a new question
        public void StartNewQuestion()
        {
            // Get a random top-level node to start
            _currentNode = _model.Root.Children[_random.Next(_model.Root.Children.Count)];
            GenerateOptions();
            IsQuizActive = true;
        }

        // Generates options for the current question
        private void GenerateOptions()
        {
            Options.Clear();

            // Insert the correct answer
            Options.Add(new ClassificationOption { Number = _currentNode.Number, Description = _currentNode.Description });

            // Insert random wrong answers
            while (Options.Count < 4)
            {
                var randomOption = _model.Root.Children[_random.Next(_model.Root.Children.Count)];
                if (!Options.Any(o => o.Number == randomOption.Number))
                {
                    Options.Add(new ClassificationOption { Number = randomOption.Number, Description = randomOption.Description });
                }
            }

            // Shuffle the options to randomize their order
            Options = new ObservableCollection<ClassificationOption>(Options.OrderBy(x => _random.Next()));
        }

        // Processes the user's answer and advances the quiz
        public void SelectAnswer(string number)
        {
            if (_currentNode.Number == number)
            {
                IsCorrectAnswer = true;
                Score++;

                // If there are child nodes, set up the next question
                if (_currentNode.Children.Any())
                {
                    _currentNode = _currentNode.Children[_random.Next(_currentNode.Children.Count)];
                    GenerateOptions();
                }
                else
                {
                    // No more child nodes, the quiz for this branch is complete
                    IsQuizActive = false;
                }
            }
            else
            {
                IsCorrectAnswer = false;
                StartNewQuestion(); // Optionally, start a new question or end the quiz
            }
        }
    }

    // Helper class to represent an option in the view
    public class ClassificationOption
    {
        public string Number { get; set; }
        public string Description { get; set; }
    }
}
