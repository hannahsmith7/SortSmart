using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SortSmart.Models
{
    // Implementing INotifyPropertyChanged allows properties of the model to notify the UI when they are changed.
    //----------------------------------------------------------------------------------------------------------------------//
    internal class CallNumber : INotifyPropertyChanged
    {
        private string _number; // initiating variables
        private string _authorInitials;

        // Represents the number part of the call number.
        //----------------------------------------------------------------------------------------------------------------------//
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(); // Notify the UI of the change.
            }
        }

        // Represents the initials part of the call number.
        //----------------------------------------------------------------------------------------------------------------------//
        public string AuthorInitials
        {
            get => _authorInitials;
            set
            {
                _authorInitials = value;
                OnPropertyChanged(); // Notify the UI of the change.
            }
        }
        //----------------------------------------------------------------------------------------------------------------------//

        // Event to notify when a property has changed.
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise the above event.
        //----------------------------------------------------------------------------------------------------------------------//
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ToString method to represent how CallNumber should be displayed.
        public override string ToString()
        {
            return $"{Number} {AuthorInitials}";
        }
    }

}
//---------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//


