using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.ViewModel.Commands;

namespace UI.ViewModel
{
    internal class MainViewModel
    {
        public ICommand OpenTourHandlerCommand { get; set; }
        public ICommand OpenTourDetailHandlerCommand { get; set; }

        public MainViewModel()
        {
            OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
            OpenTourDetailHandlerCommand = new RelayCommand(OpenTourDetailHandlerWindow);
        }

        private void OpenTourHandlerWindow(object parameter)
        {
            TourHandlerWindow tourHandlerWindow = new TourHandlerWindow();
            tourHandlerWindow.Show();
        }

        private void OpenTourDetailHandlerWindow(object parameter)
        {
            TourDetailHandlerWindow tourDetailHandlerWindow = new TourDetailHandlerWindow();
            tourDetailHandlerWindow.Show();
        }
    }
}
