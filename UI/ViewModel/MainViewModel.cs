using Business;
using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Models;
using UI.ViewModel.Commands;

namespace UI.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ICommand OpenTourHandlerCommand { get; set; }
        public ICommand OpenTourLogHandlerCommand { get; set; }

        #region uh test??
        private testmodel model = new testmodel();

        private string testtext;
        public string Testtext
        {
            get { return model.output; }
            set
            {
                testtext = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainViewModel()
        {
            OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
            OpenTourLogHandlerCommand = new RelayCommand(OpenTourLogHandlerWindow);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenTourHandlerWindow(object parameter)
        {
            TourHandlerWindow tourHandlerWindow = new TourHandlerWindow();
            tourHandlerWindow.Show();
        }

        private void OpenTourLogHandlerWindow(object parameter)
        {
            TourLogHandlerWindow tourLogHandlerWindow = new TourLogHandlerWindow();
            tourLogHandlerWindow.Show();
        }
    }
}
