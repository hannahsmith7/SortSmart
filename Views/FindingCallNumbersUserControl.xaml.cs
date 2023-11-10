using SortSmart.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FindingCallNumbersUserControl.xaml
    /// </summary>
    public partial class FindingCallNumbersUserControl : UserControl
    {
        public FindingCallNumbersUserControl()
        {
            InitializeComponent();
            this.DataContext = new FindingCallNumbersViewModel();
        }
    }
}
