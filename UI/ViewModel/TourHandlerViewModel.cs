using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Models;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel
{
    internal class TourHandlerViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
    {
        public ICommand CreationCommand { get; set; }
        public ICommand QuitCreationCommand { get; set; }

        private readonly Window _window;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TourHandlerViewModel(Window window)
        {
            _window = window;

            CreationCommand = new RelayCommand(Create);
            QuitCreationCommand = new RelayCommand(Quit);
        }

        public void Create(object parameter)
        {
            // code to create the tour

            CloseWindow(_window);
        }

        public void Quit(object parameter)
        {
            CloseWindow(_window);
        }

        public void CloseWindow(Window window)
        {
            _window.Close();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
