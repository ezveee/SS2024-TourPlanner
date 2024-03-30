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
    internal class TourHandlerViewModel : ICreationHandlerViewModel
    {
        public ICommand TourCreationCommand { get; set; }
        public ICommand QuitTourCreationCommand { get; set; }

        private readonly Window _window;

        public TourHandlerViewModel(Window window)
        {
            _window = window;

            TourCreationCommand = new RelayCommand(Create);
            QuitTourCreationCommand = new RelayCommand(Quit);
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
