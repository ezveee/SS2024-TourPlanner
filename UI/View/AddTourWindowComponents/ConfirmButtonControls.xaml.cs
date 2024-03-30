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

namespace UI.View.AddTourWindowComponents
{
    /// <summary>
    /// Interaction logic for ConfirmButtonControls.xaml
    /// </summary>
    public partial class ConfirmButtonControls : UserControl
    {
        public event EventHandler ConfirmButtonClicked;
        public ConfirmButtonControls()
        {
            InitializeComponent();
        }

        private void CreateTour(object sender, RoutedEventArgs e)
        {
            // code to create tours

            ConfirmButtonClicked?.Invoke(this, e);
        }
    }
}
