using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel
{
    internal class TourDetailHandlerViewModel : ICreationHandlerViewModel
    {
        public ICommand TourDetailCreationCommand { get; set; }
        public ICommand QuitTourDetailCreationCommand { get; set; }

        private readonly Window _window;

        public TourDetailHandlerViewModel(Window window)
        {
            _window = window;

            TourDetailCreationCommand = new RelayCommand(Create);
            QuitTourDetailCreationCommand = new RelayCommand(Quit);
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
    }
}
