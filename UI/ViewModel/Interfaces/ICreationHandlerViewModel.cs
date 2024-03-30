using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.ViewModel.Interfaces
{
    public interface ICreationHandlerViewModel
    {
        void Create(object parameter);
        void Quit(object parameter);
        void CloseWindow(Window window);
    }
}
