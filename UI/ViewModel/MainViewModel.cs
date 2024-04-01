using Business;
using Business.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Models;
using UI.View.MainWindowComponents.TourDetails.TourData.Options;
using UI.ViewModel.Commands;

namespace UI.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
	public ICommand OpenTourHandlerCommand { get; set; }
	public ICommand OpenTourLogHandlerCommand { get; set; }
	public ICommand DeleteTourCommand { get; set; }
	public ICommand DeleteTourLogCommand { get; set; }
	public ICommand SelectTourCommand { get; set; }
	public ICommand ModifyTourCommand { get; set; }
	public ICommand ModifyTourLogCommand { get; set; }

	public ICommand GeneralCommand { get; set; }
	public ICommand MapCommand { get; set; }
	public ICommand MiscCommand { get; set; }

	public static TourModel? tourToModify = new();
	public static TourLogModel? tourLogToModify = new();

	public MainViewModel()
	{
		OpenTourHandlerCommand = new RelayCommand(OpenTourHandlerWindow);
		OpenTourLogHandlerCommand = new RelayCommand(OpenTourLogHandlerWindow);
		SelectTourCommand = new RelayCommand(SelectTour);
		DeleteTourCommand = new RelayCommand(DeleteTour);
		DeleteTourLogCommand = new RelayCommand(DeleteTourLog);
		ModifyTourCommand = new RelayCommand(ModifyTour);
		ModifyTourLogCommand = new RelayCommand(ModifyTourLog);
		GeneralCommand = new RelayCommand(SwitchToGeneral);
		MapCommand = new RelayCommand(SwitchToMap);
		MiscCommand = new RelayCommand(SwitchToMisc);

		TourHandlerViewModel tourHandler = new();
		tourHandler.TourCreated += OnTourCreated;

		TourModel m = new();
		GetTourNames(m);

		ImageModel imageModel = new();
		//GetImage(imageModel);
	}

	private UserControl _currentView;

	public UserControl CurrentView
	{
		get => _currentView;
		set
		{
			_currentView = value;
			OnPropertyChanged(nameof(CurrentView));
		}
	}

	public void SwitchToGeneral(object obj)
	{
		CurrentView = new GeneralControls();
	}

	public void SwitchToMap(object obj)
	{
		CurrentView = new MapDisplayControls();
	}

	public void SwitchToMisc(object obj)
	{
		CurrentView = new MiscControls();
	}

	private void OnTourCreated(object sender, EventArgs e)
	{
		TourModel m = new();
		GetTourNames(m);
	}

	private void ModifyTour(object parameter)
	{
		tourToModify = TourModel.GetTour(_selectedTour.Item1);

		if (tourToModify == null)
		{
			return;
		}

		TourHandlerWindow tourHandlerWindow = new(tourToModify);
		tourHandlerWindow.Show();
	}

	private void ModifyTourLog(object parameter)
	{
		tourLogToModify = TourLogModel.GetTourLog(_selectedTourLog.LogId);

		if (tourLogToModify == null)
		{
			return;
		}

		TourLogHandlerWindow tourlogHandlerWindow = new(tourLogToModify);
		tourlogHandlerWindow.Show();
	}

	public void DeleteTour(object parameter)
	{
		if (_selectedTour is null)
		{
			return;
		}

		// TODO: fix image deletion logic
		// could not delte image as it was opened
		// close image once tour is deleted

		// TODO: change so ViewModel doesnt directly access BL function
		//ImageLoader imageLoader = new();
		//string imagePath = imageLoader.GetImagePath(_selectedTour.Item1);
		//Image = null;
		//imageLoader.DeleteImage(imagePath);

		TourModel.DeleteTour(_selectedTour.Item1);
		if (_tourLogs.Count > 0)
		{
			TourLogModel tourLogModel = new();
			tourLogModel.DeleteLog(_selectedTour.Item1);
		}

		_selectedTour = null;

		OnPropertyChanged(nameof(TourNames));
		TourModel m = new();
		GetTourNames(m);
	}

	public void DeleteTourLog(object parameter)
	{
		if (_selectedTourLog is null)
		{
			return;
		}

		TourLogModel tourLogModel = new();
		tourLogModel.DeleteLog(_selectedTourLog.LogId);

		OnPropertyChanged(nameof(TourLogs));
		GetTourLogData(_selectedTour.Item1);
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
			TourModel m = new();
			GetTourNames(m);
		}
	}

	private Tuple<int, string>? _selectedTour;
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

	private TourLogModel _selectedTourLog;
	public TourLogModel SelectedTourLog
	{
		get => _selectedTourLog;
		set
		{
			_selectedTourLog = value;
			OnPropertyChanged(nameof(SelectedTourLog));
		}
	}

	private TourModel _selectedTourData;

	public TourModel SelectedTourData
	{
		get => _selectedTourData;
		set
		{
			if (_selectedTourData != value)
			{
				_selectedTourData = value;
				OnPropertyChanged(nameof(SelectedTourData));
			}
		}
	}

	public void SelectTour(object parameter)
	{
		if (parameter == null)
		{
			return;
		}

		int tourId = (int)parameter;
		GetTourLogData(tourId);
		GetImage(tourId);

		SelectedTourData = TourModel.GetTour(tourId);
		CurrentView = new MapDisplayControls();
	}

	private void GetTourLogData(int id)
	{
		TourLogModel model = new();
		_tourLogs = [];

		foreach (TourLog item in model.TourLogs)
		{
			TourLogModel logModel = new()
			{
				LogId = item.LogId,
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
	private string? _image;
	public string? Image
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
	public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public void OpenTourHandlerWindow(object parameter)
	{
		TourHandlerWindow tourHandlerWindow = new();
		tourHandlerWindow.Show();
	}

	public void OpenTourLogHandlerWindow(object parameter)
	{
		TourLogHandlerWindow tourLogHandlerWindow = new();
		tourLogHandlerWindow.Show();
	}
}
