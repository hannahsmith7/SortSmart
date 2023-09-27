using SortSmart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace SortSmart.View_Models
{
    // The ReplacingBooksViewModel manages the functionality of the "Replacing Books" feature.
    internal class ReplacingBooksViewModel : INotifyPropertyChanged
    {
        // Event and method for INotifyPropertyChanged implementation.This is essential for data binding.
        // Implementing INotifyPropertyChanged to update the UI when the underlying data changes
        //----------------------------------------------------------------------------------------------------------------------//
        public event PropertyChangedEventHandler PropertyChanged;

        // Invokes the PropertyChanged event to inform the UI of property changes.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Represents the list of call numbers being managed and displayed in the UI.
        private ObservableCollection<string> _callNumbers;
        public ObservableCollection<string> CallNumbers
        {
            get { return _callNumbers; }
            set
            {
                _callNumbers = value;
                OnPropertyChanged("CallNumbers"); // Notify UI of change.
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
        // Represents the progress made by the user in sorting books.
        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(ProgressValue)); // Notify UI of change.
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Commands linked to UI actions: Checking the order of books and shuffling the order.
        public ICommand CheckOrderCommand { get; private set; }
        public ICommand ShuffleBooksCommand { get; private set; }

        //----------------------------------------------------------------------------------------------------------------------//
        public ReplacingBooksViewModel()
        {
            // Populate the list of call numbers and initialize UI commands.
            CallNumbers = new ObservableCollection<string>();
            GenerateRandomCallNumbers(); // Generate initial random call numbers.

            // Link commands to their respective action methods.
            CheckOrderCommand = new RelayCommand(CheckOrder);
            ShuffleBooksCommand = new RelayCommand(ShuffleBooks);
        }
        //----------------------------------------------------------------------------------------------------------------------//
  
        // Populates the CallNumbers list with 10 random call numbers for demonstration.
        private void GenerateRandomCallNumbers()
        {
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var numberPart = (random.Next(1, 999) + random.NextDouble()).ToString("0.00");
                var charPart = ((char)random.Next(65, 91)).ToString() + ((char)random.Next(65, 91)).ToString() + ((char)random.Next(65, 91)).ToString();
                CallNumbers.Add(numberPart + " " + charPart);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Reorders the CallNumbers list based on a drag-and-drop operation in the UI.
        // Code done with the help of ChatGPT
        public void ReorderList(string droppedItem, string targetItem)
        {
            int removedIdx = CallNumbers.IndexOf(droppedItem);
            int targetIdx = CallNumbers.IndexOf(targetItem);

            if (removedIdx < 0 || targetIdx < 0 || removedIdx >= CallNumbers.Count || targetIdx >= CallNumbers.Count) return;

            CallNumbers.RemoveAt(removedIdx);

            if (removedIdx < targetIdx)
                CallNumbers.Insert(targetIdx, droppedItem);
            else
                CallNumbers.Insert(targetIdx, droppedItem);
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Command method to check if the call numbers are in the correct order
        private void CheckOrder()
        {
            // Check if the numeric portion of the call numbers are in ascending order
            for (int i = 1; i < CallNumbers.Count; i++)
            {
                // Extract numeric portion from the current and previous call number
                double currentNumeric = ExtractNumericPart(CallNumbers[i]);
                double previousNumeric = ExtractNumericPart(CallNumbers[i - 1]);

                if (previousNumeric > currentNumeric)
                {
                    // They are not in order based on the numeric value
                    MessageBox.Show("The order is incorrect. Please try again.");
                    return;
                }
            }

            MessageBox.Show("The order is correct. Well done!");

            // If the loop completed without returning early, the order was correct:
            ProgressValue++; // Increment the progress

            if (ProgressValue >= 10)
            {
                // Reset the progress bar and provide feedback
                ProgressValue = 0;
                MessageBox.Show("Congratulations! You've mastered this round of book sorting!");
            }
            else
            {
                // Provide feedback for correct ordering
                MessageBox.Show("Great job! The order is correct.");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Method to extract numeric portion from a call number
        // Code done with the help of ChatGPT
        public double ExtractNumericPart(string callNumber)
        {
            // Split the call number by space and consider the first part as numeric
            var parts = callNumber.Split(' ');
            if (parts.Length > 0 && double.TryParse(parts[0], out double numericPart))
            {
                return numericPart;
            }
            else
            {
                // Throw an exception for unexpected formats
                throw new FormatException($"Unexpected format for call number: {callNumber}");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Command method to shuffle the call numbers randomly
        private void ShuffleBooks()
        {
            var random = new Random();
            var shuffledList = CallNumbers.OrderBy(item => random.Next()).ToList();

            CallNumbers.Clear();
            foreach (var item in shuffledList)
            {
                CallNumbers.Add(item);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
        // RelayCommand is a basic implementation of the ICommand interface
        // It simplifies command creation by encapsulating the execute action.
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public event EventHandler CanExecuteChanged;

            //----------------------------------------------------------------------------------------------------------------------//
            public RelayCommand(Action execute)
            {
                _execute = execute;
            }
            //----------------------------------------------------------------------------------------------------------------------//
            public bool CanExecute(object parameter) => true;
            //----------------------------------------------------------------------------------------------------------------------//
            public void Execute(object parameter)
            {
                try
                {
                    _execute();
                }
                catch (Exception)
                {
                    // Handle or log the error as needed.
                    MessageBox.Show("An error occurred. Please try again.");
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
    }
}
//---------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//


