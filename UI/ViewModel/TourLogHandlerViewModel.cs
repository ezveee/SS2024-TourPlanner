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
    internal class TourLogHandlerViewModel : ICreationHandlerViewModel
    {
        public ICommand CreationCommand { get; set; }
        public ICommand QuitCreationCommand { get; set; }

        private readonly Window _window;

        public TourLogHandlerViewModel(Window window)
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
    }
}
