using Business;
using Business.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UI.Models;
using UI.ViewModel.Commands;

namespace UI.ViewModel;

internal class MainViewModel : INotifyPropertyChanged
{
	public ICommand OpenTourHandlerCommand { get; set; }
	public ICommand OpenTourLogHandlerCommand { get; set; }
	public ICommand DeleteTourCommand { get; set; }
	public ICommand SelectTourCommand { get; set; }

	public ICommand ModifyTourCommand { get; set; }

	public MainViewModel()
	{
		OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
		OpenTourLogHandlerCommand = new RelayCommand(OpenTourLogHandlerWindow);
		SelectTourCommand = new RelayCommand(SelectTour);
		DeleteTourCommand = new RelayCommand(DeleteTour);
		ModifyTourCommand = new RelayCommand(ModifyTour);

		TourHandlerViewModel tourHandler = new TourHandlerViewModel();
		tourHandler.TourCreated += OnTourCreated;

		TourModel m = new TourModel();
		GetTourNames(m);

		ImageModel imageModel = new();
		//GetImage(imageModel);
	}

	private void OnTourCreated(object sender, EventArgs e)
	{
		TourModel m = new TourModel();
		GetTourNames(m);
	}

	public static TourModel? tourToModify = new TourModel();

	private void ModifyTour(object parameter)
	{
		tourToModify = TourModel.GetTour(_selectedTour.Item1);

		if (tourToModify == null) 
		{
			return;
		}

		TourHandlerWindow tourHandlerWindow = new TourHandlerWindow(tourToModify);
		tourHandlerWindow.Show();
	}

	private void DeleteTour(object parameter)
	{
		foreach(var tour in _tourNames)
		{
			if(_selectedTour == null)
			{
				return;
			}

			if (tour.Item1 == _selectedTour.Item1)
			{
				// TODO: delete image function implemented here
				TourModel.DeleteTour(tour.Item1);
				TourLogModel tourLogModel = new TourLogModel();
				tourLogModel.DeleteLog(tour.Item1);
			}
		}

		OnPropertyChanged(nameof(TourNames));

		TourModel m = new TourModel();
		GetTourNames(m);
	}

	#region Display Tours
	private List<Tuple<int, string>> _tourNames;
	public List<Tuple<int, string>> TourNames
	{
		get => _tourNames ??= [];
		set
		{
			_tourNames = value;
			OnPropertyChanged(nameof(TourNames));
			TourModel m = new TourModel();
			GetTourNames(m);
		}
	}

	private Tuple<int, string> _selectedTour;
	public Tuple<int, string> SelectedTour
	{
		get => _selectedTour;
		set
		{
			_selectedTour = value;
			OnPropertyChanged(nameof(SelectedTour));
			SelectTour(value?.Item1);
		}
	}

	private void GetTourNames(TourModel model)
	{
		_tourNames = [];

		foreach (Tour name in model.Tours)
		{
			Tuple<int, string> temp = new(name.TourId, name.Name);

			_tourNames.Add(temp);
		}

		OnPropertyChanged(nameof(TourNames));
	}
	#endregion

	#region Display Tourlogs
	private List<TourLogModel> _tourLogs;
	public List<TourLogModel> TourLogs
	{
		get => _tourLogs ??= [];
		set
		{
			_tourLogs = value;
			OnPropertyChanged(nameof(TourLogs));
		}
	}
	private void SelectTour(object parameter)
	{
		if(parameter == null)
		{
			return;
		}

		int tourId = (int)parameter;
		GetTourLogData(tourId);
		GetImage(tourId);
	}

	private void GetTourLogData(int id)
	{
		TourLogModel model = new();

		_tourLogs = [];

		foreach (TourLog item in model.TourLogs)
		{
			TourLogModel logModel = new()
			{
				Date = item.Date,
				Comment = item.Comment,
				Difficulty = item.Difficulty,
				Distance = item.Distance,
				Time = item.Time,
				Rating = item.Rating
			};

			if (item.TourId == id)
			{
				_tourLogs.Add(logModel);
			}
		}

		OnPropertyChanged(nameof(TourLogs));
	}
	#endregion

	#region Displaying Map Image (semi functional)
	private string _image;
	public string Image
	{
		get => _image;
		set
		{
			_image = value;
			OnPropertyChanged(nameof(Image));
		}
	}

	private void GetImage(int tourId)
	{
		// TODO: change so UI ViewModel doesnt directly access BL functions?
		ImageLoader imageLoader = new();
		Image = imageLoader.GetImagePath(tourId);
	}
	#endregion


	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	private void OpenTourHandlerWindow(object parameter)
	{
		TourHandlerWindow tourHandlerWindow = new();
		tourHandlerWindow.Show();
	}

	private void OpenTourLogHandlerWindow(object parameter)
	{
		TourLogHandlerWindow tourLogHandlerWindow = new();
		tourLogHandlerWindow.Show();
	}
}
