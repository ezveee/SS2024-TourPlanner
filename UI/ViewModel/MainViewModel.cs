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

        public MainViewModel()
        {
            OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
            OpenTourLogHandlerCommand = new RelayCommand(OpenTourLogHandlerWindow);

            TourModel tourModel = new TourModel();
            GetTourNames(tourModel);

            TourLogModel logModel = new TourLogModel();
            GetTourLogData(logModel);
        }

        private List<string> _tourNames;
        public List<string> TourNames
        {
            get { return _tourNames ?? (_tourNames = new List<string>()); }
            set
            {
                _tourNames = value;
                OnPropertyChanged(nameof(TourNames));
            }
        }

        private List<TourLogModel> _tourLogs;
        public List<TourLogModel> TourLogs
        {
            get { return _tourLogs ?? (_tourLogs = new List<TourLogModel>()); }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        private void GetTourLogData(TourLogModel model)
        {
            _tourLogs = new List<TourLogModel>();

            foreach (var item in model.TourLogs)
            {
                TourLogModel logModel = new TourLogModel()
                {
                    Date = item.Date,
                    Comment = item.Comment,
                    Difficulty = item.Difficulty,
                    Distance = item.Distance,
                    Time = item.Time,
                    Rating = item.Rating
                };

                _tourLogs.Add(logModel);
            }
        }

        private void GetTourNames(TourModel model)
        {
            _tourNames = new List<string>();

            foreach (var name in model.Tours)
            {
                _tourNames.Add(name.Name);
            }
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
