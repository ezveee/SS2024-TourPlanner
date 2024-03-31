using Business;
using Business.Interfaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using UI.Models;
using UI.ViewModel.Commands;

namespace UI.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ICommand OpenTourHandlerCommand { get; set; }
        public ICommand OpenTourLogHandlerCommand { get; set; }
        public ICommand SelectTourCommand { get; set; }

        public MainViewModel()
        {
            OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
            OpenTourLogHandlerCommand = new RelayCommand(OpenTourLogHandlerWindow);
            SelectTourCommand = new RelayCommand(SelectTour);

            TourModel tourModel = new TourModel();
            GetTourNames(tourModel);

            ImageModel imageModel = new ImageModel();
            GetImage(imageModel);
        }

        #region Display Tours
        private List<Tuple<int, string>> _tourNames;
        public List<Tuple<int, string>> TourNames
        {
            get { return _tourNames ?? (_tourNames = new List<Tuple<int, string>>()); }
            set
            {
                _tourNames = value;
                OnPropertyChanged(nameof(TourNames));
            }
        }

        private void GetTourNames(TourModel model)
        {
            _tourNames = new List<Tuple<int, string>>();

            foreach (var name in model.Tours)
            {
                Tuple<int, string> temp = new Tuple<int, string>(name.TourId, name.Name);

                _tourNames.Add(temp);
            }
        }
        #endregion

        #region Display Tourlogs
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
        private void SelectTour(object parameter)
        {
            GetTourLogData((int)parameter);
        }

        private void GetTourLogData(int id)
        {
            TourLogModel model = new TourLogModel();

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

                if(item.TourId == id)
                {
                    _tourLogs.Add(logModel);
                }
            }
        }
        #endregion

        #region Displaying Map Image (semi functional)
        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private void GetImage(ImageModel imageModel)
        {
            _image = imageModel.img;
        }
        #endregion


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
