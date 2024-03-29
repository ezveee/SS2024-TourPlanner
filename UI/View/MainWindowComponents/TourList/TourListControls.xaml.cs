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

namespace UI.UIComponents.TourList
{
    /// <summary>
    /// Interaction logic for TourListControls.xaml
    /// </summary>
    public partial class TourListControls : UserControl
    {
        public TourListControls()
        {
            InitializeComponent();
        }

        private void OpenAddTourWindow(object sender, RoutedEventArgs e)
        {
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.Show();
        }
    }
}
