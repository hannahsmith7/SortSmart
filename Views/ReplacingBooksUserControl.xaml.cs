using SortSmart;
using SortSmart.Models;
using SortSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SortSmart.Views
{
    /// <summary>
    /// Interaction logic for ReplacingBooksUserControl.xaml
    /// </summary>

    public partial class ReplacingBooksUserControl : UserControl
    {
        private ReplacingBooksViewModel viewModel;
        internal ReplacingBooksViewModel ViewModel => DataContext as ReplacingBooksViewModel;

        //----------------------------------------------------------------------------------------------------------------------//
        public ReplacingBooksUserControl()
        {
            InitializeComponent();

            // Instantiate the ViewModel and set the DataContext for this UserControl.
            viewModel = new ReplacingBooksViewModel();
            this.DataContext = viewModel;
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Handler for when an item in the ListView is clicked.
        // It initiates the drag operation if an item is selected.
        // Code done with the help of ChatGPT
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var draggedItem = listView.SelectedItem as string;

            if (draggedItem != null)
            {
                // Begin the drag operation.
                DragDrop.DoDragDrop(listView, draggedItem, DragDropEffects.Move);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Handler for when an item is dropped onto the ListView.
        // This will be used to reorder the list based on the drag-and-drop operation.
        // Code done with the help of ChatGPT
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            // Get the dragged item from the data.
            var droppedData = e.Data.GetData(typeof(string));
            var droppedItem = droppedData as string;

            // Get the target item where the dragged item will be dropped.
            var targetItem = (e.OriginalSource as FrameworkElement)?.DataContext as string;

            if (targetItem != null)
            {
                // Notify the ViewModel to reorder the list based on the drag-and-drop.
                viewModel.ReorderList(droppedItem, targetItem);

                // Check the partial order after reordering.
                CheckPartialOrder();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
        // Code done with the help of ChatGPT
        private void CheckPartialOrder()
        {
            // Counter for the number of call numbers in their correct position.
            int correctPositions = 0;

            try
            {
                // Loop through the call numbers to compare each one with the next.
                for (int i = 0; i < ViewModel.CallNumbers.Count - 1; i++)
                {
                    // Extract the numeric portions of the current and next call number.
                    double currentNumeric = ViewModel.ExtractNumericPart(ViewModel.CallNumbers[i]);
                    double nextNumeric = ViewModel.ExtractNumericPart(ViewModel.CallNumbers[i + 1]);

                    // If the current call number is less than or equal to the next one, it's in the correct order.
                    if (currentNumeric <= nextNumeric)
                    {
                        correctPositions++; // Increment the counter for correct positions.
                    }
                }
                // Update the ViewModel's progress value with the number of correct positions.
                ViewModel.ProgressValue = correctPositions;
            }
            catch (FormatException)
            {
                // Handle or log the error as needed.
                ViewModel.ProgressValue = 0; // Reset progress.
            }
            //----------------------------------------------------------------------------------------------------------------------//
        }
    }
}
//---------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//



