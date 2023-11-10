using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortSmart.ViewModels;
using SortSmart.Views;
using System.Windows.Input;
using SortSmart.Commands;

namespace SortSmart.ViewModels
{
    // MainViewModel is the primary ViewModel that manages the main functionalities and navigation of the application.
    internal class MainViewModel : INotifyPropertyChanged

    {

        //----------------------------------------------------------------------------------------------------------------------//
        // The PropertyChanged event is used to notify that a property changed and the UI should update.
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called to raise the PropertyChanged event, notifying any UI elements that are bound to the property.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //----------------------------------------------------------------------------------------------------------------------//
        // The _currentView private field stores the current user control being shown in the main window.
        private object _currentView;

        // Public property to get or set the current user control to be displayed.
        // This drives the navigation of our application.
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//
        // This command will be bound to a button or menu item in the UI to show the view.
        public ICommand ShowReplacingBooksCommand { get; private set; }

        public ICommand ShowIdentifyingAreasCommand { get; private set; }
        public ICommand ShowFindingCallNumbersCommand { get; private set; }

        //----------------------------------------------------------------------------------------------------------------------//

        // MainViewModel constructor. Initializes the required commands and other setup.
        public MainViewModel()
        {
            // Initialize the command
            // Link the ShowReplacingBooksCommand to the ShowReplacingBooks method.
            ShowReplacingBooksCommand = new RelayCommand(ShowReplacingBooks);

            ShowIdentifyingAreasCommand = new RelayCommand(ShowIdentifyingAreas);

            ShowFindingCallNumbersCommand = new RelayCommand(ShowFindingCallNumbers);
        }
        //----------------------------------------------------------------------------------------------------------------------//
        // This method sets the current view to the "Replacing Books" user control.
        private void ShowReplacingBooks()
        {
            CurrentView = new ReplacingBooksUserControl();
        }
        //----------------------------------------------------------------------------------------------------------------------//
        private void ShowIdentifyingAreas()
        {
            CurrentView = new IdentifyingAreasUserControl();
        }

        //----------------------------------------------------------------------------------------------------------------------//
        private void ShowFindingCallNumbers()
        {
            CurrentView = new FindingCallNumbersUserControl();
        }
    }
}
//---------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//


