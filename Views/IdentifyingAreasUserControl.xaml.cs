using SortSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for IdentifyingAreasUserControl.xaml
    /// </summary>
    public partial class IdentifyingAreasUserControl : UserControl
    {
        private string _draggingItem;
        //---------------------------------------------------------------------------------------------------------------------------//
        public IdentifyingAreasUserControl()
        {
            InitializeComponent();
            this.DataContext = new IdentifyingAreasViewModel();
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var data = GetDataFromListView(listView, e.GetPosition(listView));
            var listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            _draggingItem = (string)listViewItem.Content;

            if (data != null)
                DragDrop.DoDragDrop(listView, data, DragDropEffects.Move);
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Retrieves data from a ListView based on a specific point (typically from mouse events).
        /// </summary>
        /// <param name="source">The ListView source from which data is to be retrieved.</param>
        /// <param name="point">The point in the ListView used to identify the item.</param>
        /// <returns>The data from the ListView corresponding to the given point or null if not found.</returns>
        private static object GetDataFromListView(ListView source, Point point)
        {
            // Use InputHitTest to determine which UIElement within the ListView was hit.
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                // Initialize data with a default unset value.
                object data = DependencyProperty.UnsetValue;

                // Loop to navigate up the visual tree until we find the data corresponding to the UIElement.
                while (data == DependencyProperty.UnsetValue)
                {
                    // Try getting the item from the ListView's ItemContainer.
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    // If data is still unset, move up to the parent in the visual tree.
                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;

                    // If we have reached the ListView itself, exit the loop without finding any item.
                    if (element == source)
                        return null;
                }
                // Return the found data if it's set.
                if (data != DependencyProperty.UnsetValue)
                    return data;
            }
            // If no item was found, return null.
            return null;
        }
        //---------------------------------------------------------------------------------------------------------------------------//
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            var targetListView = sender as ListView;
            var droppedPosition = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            var droppedData = e.Data.GetData(typeof(string)) as string;

            int oldIndex = rightColumnItems.Items.IndexOf(_draggingItem);
            int newIndex = rightColumnItems.Items.IndexOf(droppedPosition.Content);

            if (targetListView != null && !string.IsNullOrEmpty(droppedData))
            {
                // Assuming you have access to your ViewModel here. If not, adjust as needed.
                var viewModel = DataContext as IdentifyingAreasViewModel;
                viewModel.MoveItem(oldIndex, newIndex);

                // Check if the dropped item is correct.
                if (viewModel != null && viewModel.CheckMatch(newIndex))
                {
                    // Handle correct match. For example:
                    MessageBox.Show("Correct Match!");
                }
                else
                {
                    // Handle incorrect match. For example:
                    MessageBox.Show("Incorrect Match. Try Again.");
                }
            }
        }

        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T) return (T)current;
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        //---------------------------------------------------------------------------------------------------------------------------//

    }
}
//------------------------------------------------------...ooo000 END OF FILE 000ooo...----------------------------------------------//