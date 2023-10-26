using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using static SortSmart.ViewModels.ReplacingBooksViewModel;

namespace SortSmart.ViewModels
{
    internal class IdentifyingAreasViewModel : INotifyPropertyChanged
    {
        // Dictionary to store the call numbers and descriptions
        private readonly Dictionary<string, string> _callNumberDescriptionMapping;

        // Command to generate new question list
        public RelayCommand GenerateNewQuestionCommand { get; }

        // Command to check answers
        public RelayCommand CheckAnswersCommand { get; }

        private ObservableCollection<string> _leftColumnItems;
        private ObservableCollection<string> _rightColumnItems;
        private bool _isMatchingDescriptionToCallNumber;  // starts with descriptions to call numbers
        private Random _random = new Random();

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------------------------------------------------------------------------------------------------//
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        public IdentifyingAreasViewModel()
        {
            _isMatchingDescriptionToCallNumber = false;
            _callNumberDescriptionMapping = new Dictionary<string, string>
        {
             {"000", "Computer science, information & general works"},
            {"100", "Philosophy and psychology"},
            {"200", "Religion"},
            {"300", "Social sciences"},
            {"400", "Language"},
            {"500", "Natural sciences & mathematics"},
            {"600", "Technology"},
            {"700", "Arts & recreation"},
            {"800", "Literature"},
            {"900", "History & geography"}
        };
            // Initialisation of commands
            GenerateNewQuestionCommand = new RelayCommand(GenerateNewQuestion);

            CheckAnswersCommand = new RelayCommand(CheckAnswers);

            GenerateNewQuestion();
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        // This property represents a collection of strings for items in the left column.
        // ObservableCollection is a collection that can notify when items are added, removed, or the whole list is refreshed.
        public ObservableCollection<string> LeftColumnItems
        {
            // This is a getter for the LeftColumnItems property. When you access this property, 
            // it returns the value of the private field _leftColumnItems.
            get => _leftColumnItems;

            // This is a setter for the LeftColumnItems property. 
            // When you set a value to this property, this block of code is executed.
            set
            {
                // Assigns the provided value to the private field _leftColumnItems.
                _leftColumnItems = value;

                // Calls the OnPropertyChanged method (usually used in context of INotifyPropertyChanged interface) 
                // to notify any subscribers (like a UI) that the value of LeftColumnItems has changed. 
                // This can trigger mechanisms like UI updates in MVVM design pattern.
                OnPropertyChanged(nameof(LeftColumnItems));
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        public ObservableCollection<string> RightColumnItems
        {
            get => _rightColumnItems;
            set
            {
                _rightColumnItems = value;
                OnPropertyChanged(nameof(RightColumnItems));
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        // This method will generate a new question by populating LeftColumnItems and RightColumnItems accordingly
        public void GenerateNewQuestion()
        {
            // Step 1: Randomly select four items
            var selectedItems = _isMatchingDescriptionToCallNumber
                ? _callNumberDescriptionMapping.Values.OrderBy(x => _random.Next()).Take(4).ToList()  // Select Descriptions
                : _callNumberDescriptionMapping.Keys.OrderBy(x => _random.Next()).Take(4).ToList();    // Select Call Numbers

            LeftColumnItems = new ObservableCollection<string>(selectedItems);

            // Step 2: Randomly select seven possible answers
            var correctAnswers = _isMatchingDescriptionToCallNumber
                ? selectedItems.Select(d => _callNumberDescriptionMapping.First(kv => kv.Value == d).Key).ToList()  // Find corresponding Call Numbers
                : selectedItems.Select(c => _callNumberDescriptionMapping[c]).ToList();   // Find corresponding Descriptions

            var incorrectAnswers = _isMatchingDescriptionToCallNumber
                ? _callNumberDescriptionMapping.Keys.Except(correctAnswers).OrderBy(x => _random.Next()).Take(3).ToList()  // Incorrect Call Numbers
                : _callNumberDescriptionMapping.Values.Except(correctAnswers).OrderBy(x => _random.Next()).Take(3).ToList();  // Incorrect Descriptions

            var allPossibleAnswers = correctAnswers.Concat(incorrectAnswers).OrderBy(x => _random.Next()).ToList();
            RightColumnItems = new ObservableCollection<string>(allPossibleAnswers);
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        // This property represents a boolean flag indicating whether the current matching task 
        // is to match a description to a call number.
        public bool IsMatchingDescriptionToCallNumber
        {
            // This is a getter for the IsMatchingDescriptionToCallNumber property. 
            // When this property is accessed, it returns the value of the private field 
            // _isMatchingDescriptionToCallNumber.
            get => _isMatchingDescriptionToCallNumber;
            // This is a setter for the IsMatchingDescriptionToCallNumber property. 
            // When you set a value to this property, this block of code is executed.
            set
            {
                // Assigns the provided value to the private field _isMatchingDescriptionToCallNumber.
                _isMatchingDescriptionToCallNumber = value;

                // Calls the OnPropertyChanged method (commonly used in the context of the INotifyPropertyChanged interface) 
                // to notify any subscribers (like a UI) that the value of IsMatchingDescriptionToCallNumber has changed. 
                // This can activate mechanisms like UI updates in the MVVM design pattern.
                OnPropertyChanged();
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------//
        private string _matchStatusColor = "Black"; // Default color

        public string MatchStatusColor
        {
            get => _matchStatusColor;
            set
            {
                _matchStatusColor = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        private string _feedback;
        public string Feedback
        {
            get => _feedback;
            set
            {
                _feedback = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Checks if the given item at the specified index is correctly matched.
        /// </summary>
        /// <param name="itemIndex">The index of the item to check.</param>
        /// <returns>True if the item is correctly matched; otherwise, false.</returns>
        public bool CheckMatch(int itemIndex)
        {
            // This variable will store whether the item has been matched correctly or not.
            bool isMatchCorrect = false;
            // Output to the debug console the items from the LeftColumnItems for troubleshooting purposes.
            Debug.WriteLine("Left items:");
            foreach (var item in LeftColumnItems)
            {
                Debug.WriteLine(item);
            }
            // Output to the debug console the items from the RightColumnItems for troubleshooting purposes.
            Debug.WriteLine("Right items:");
            foreach (var item in RightColumnItems)
            {
                Debug.WriteLine(item);
            }
            // If we're not matching descriptions to call numbers, then we're matching call numbers to descriptions.
            if (!_isMatchingDescriptionToCallNumber)
            {
                // Check if the left column item at the given index exists in the mapping dictionary and 
                // if its corresponding right column item matches the one in the dictionary.
                if (_callNumberDescriptionMapping.ContainsKey(LeftColumnItems[itemIndex])
                    && RightColumnItems[itemIndex] == _callNumberDescriptionMapping[LeftColumnItems[itemIndex]])
                {
                    isMatchCorrect = true;
                }
            }
            else // Otherwise, we are indeed matching descriptions to call numbers.
            {
                // Check if the right column item at the given index exists in the mapping dictionary and 
                // if its corresponding left column item matches the one in the dictionary.
                if (_callNumberDescriptionMapping.ContainsKey(RightColumnItems[itemIndex])
                    && LeftColumnItems[itemIndex] == _callNumberDescriptionMapping[RightColumnItems[itemIndex]])
                {
                    isMatchCorrect = true;
                }
            }
            // Provide feedback based on whether the item was matched correctly or not.
            if (isMatchCorrect)
            {
               // If matched correctly, increase the score by 1.
                Score += 1; // Increase the score
                Feedback = "Correct!";
            }
            else
            {
                Feedback = "Incorrect. Try again!";
            }

            // Update the color based on match correctness
            MatchStatusColor = isMatchCorrect ? "Green" : "Red";

            return isMatchCorrect;
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Checks if all the answers are correct and provides feedback accordingly.
        /// </summary>
        public void CheckAnswers()
        {
            try
            {
                // Variable to store the count of correct answers.
                int correctAnswers = 0;

                // If we're not matching descriptions to call numbers, then we're matching call numbers to descriptions.
                if (!_isMatchingDescriptionToCallNumber)
                {
                    // Loop through all the items in the left column.
                    for (int i = 0; i < LeftColumnItems.Count; i++)
                    {
                        // Check if the left column item at the current index exists in the mapping dictionary.
                        if (_callNumberDescriptionMapping.ContainsKey(LeftColumnItems[i]))
                        {
                            // If its corresponding right column item matches the one in the dictionary, it's a correct answer.
                            if (RightColumnItems[i] == _callNumberDescriptionMapping[LeftColumnItems[i]])
                            {
                                correctAnswers++;
                            }
                        }
                    }
                }
                else // Otherwise, we are indeed matching descriptions to call numbers.
                {
                    // Loop through all the items in the left column.
                    for (int i = 0; i < LeftColumnItems.Count; i++)
                    {
                        // Check if the right column item at the current index exists in the mapping dictionary.
                        if (_callNumberDescriptionMapping.ContainsKey(RightColumnItems[i]))
                        {
                            // If its corresponding left column item matches the one in the dictionary, it's a correct answer.
                            if (LeftColumnItems[i] == _callNumberDescriptionMapping[RightColumnItems[i]])
                            {
                                correctAnswers++;
                            }
                        }
                    }
                }
                // Check if all the answers are correct (assuming there are 4 items to match).
                if (correctAnswers == 4)
                {
                    Score = 0;
                    Feedback = "Correct Answer!";
                    // Toggle the matching direction for the next question.
                    _isMatchingDescriptionToCallNumber = !_isMatchingDescriptionToCallNumber;
                    // Generate a new question after correctly answering the current one.
                    GenerateNewQuestion();
                }
                else
                {
                    // If not all answers are correct, provide a feedback to try again.
                    Feedback = "Incorrect. Try again!";
                }
            }
            catch (Exception ex)
            {
                // If any error occurs during the process, show an error message box.
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Moves an item within the RightColumnItems collection from one position to another.
        /// </summary>
        /// <param name="oldIndex">The original index of the item to move.</param>
        /// <param name="newIndex">The index to where the item should be moved.</param>
        public void MoveItem(int oldIndex, int newIndex)
        {
            // Check if the old index is out of bounds and return if so.
            if (oldIndex < 0 || oldIndex >= RightColumnItems.Count) return;

            // Check if the new index is out of bounds and return if so.
            if (newIndex < 0 || newIndex >= RightColumnItems.Count) return;

            // If the old and new indexes are the same, there's no need to move. Return early.
            if (oldIndex == newIndex) return;

            // Retrieve the item from the original position.
            var item = RightColumnItems[oldIndex];

            // Remove the item from its original position.
            RightColumnItems.RemoveAt(oldIndex);

            // Insert the item at its new position.
            RightColumnItems.Insert(newIndex, item);

            // Debugging: Print out the items in RightColumnItems after the move operation.
            foreach (var desc in RightColumnItems)
            {
                Debug.WriteLine(desc);
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------//
        // Private field to store the current item to be matched.
        private string _currentItemToMatch;

        /// <summary>
        /// Gets or sets the current item that the user should match.
        /// When set, it also raises the property changed event to notify UI for potential updates.
        /// </summary>
        public string CurrentItemToMatch
        {
            // Return the value stored in the private field when accessed.
            get => _currentItemToMatch;
            set
            {
                // Update the private field with the provided value.
                _currentItemToMatch = value;
                // Notify any subscribers (e.g., the UI) that the property has changed.
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------//
    }
}
//-----------------------------------------------...ooo000 END OF FILE 000ooo...-----------------------------------------------------//