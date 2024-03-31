using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    internal class TourLogHandlerViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
    {
        public ICommand CreationCommand { get; set; }
        public ICommand QuitCreationCommand { get; set; }

        private readonly Window _window;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TourLogHandlerViewModel(Window window)
        {
            _window = window;

            CreationCommand = new RelayCommand(Create);
            QuitCreationCommand = new RelayCommand(Quit);
        }

        #region Create a Tourlog
        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            { 
                _tourId = value; 
                OnPropertyChanged(nameof(TourId));
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get { return _difficulty; }
            set
            {
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        private float _distance;
        public float Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private double _time;
        public double Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public void Create(object parameter)
        {
            TourLogModel model = new TourLogModel(_tourId, _date, _comment, _difficulty, _distance, _time, _rating);

            // TODO: same as tour view

            CloseWindow(_window);
        }
        #endregion

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
