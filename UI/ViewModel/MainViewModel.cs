using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using UI.HttpHelpers;
using UI.Interfaces;
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

	public ICommand GenerateTourReportCommand { get; set; }
	public ICommand GenerateSummaryCommand { get; set; }

	public ICommand RefreshTourCommand { get; set; }

	public ICommand ToggleThemeCommand { get; set; }

	public static TourModel? tourToModify = new();
	public static TourLogModel? tourLogToModify = new();

	// again, im aware this is ugly
	private readonly IHttpHelper<TourModel> _tourHelper = new HttpTourHelper();
	private readonly IHttpHelper<TourLogModel> _tourLogHelper = new HttpTourLogHelper();

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
		GenerateTourReportCommand = new RelayCommand(GenerateTourReport);
		GenerateSummaryCommand = new RelayCommand(GenerateSummaryReport);
		RefreshTourCommand = new RelayCommand(RefreshTourList);
		ToggleThemeCommand = new RelayCommand(ToggleTheme);

		IsDarkMode = false;
		BackgroundBrush = Brushes.White;
		ForegroundBrush = Brushes.Black;

		GetTourNames();

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

	public void RefreshTourList(object obj)
	{
		GetTourNames();
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

	public async void GenerateTourReport(object parameter)
	{
		if (_selectedTour == null)
		{
			return;
		}

		ReportViewModel reportViewModel = new ();
		await reportViewModel.PostDataToApiAsync(_selectedTour.Item1, "tour");

	}

	public async void GenerateSummaryReport(object parameter)
	{
		if (_selectedTour == null)
		{
			return;
		}

		ReportViewModel reportViewModel = new();
		await reportViewModel.PostDataToApiAsync(_selectedTour.Item1, "summary");
	}

	private void OnTourCreated(object sender, EventArgs e)
	{
		GetTourNames();
	}

	private async void ModifyTour(object parameter)
	{
		if (_selectedTour is null)
		{
			return;
		}

		tourToModify = await _tourHelper.GetDataAsync(_selectedTour.Item1);

		if (tourToModify is null)
		{
			return;
		}

		TourHandlerWindow tourHandlerWindow = new(tourToModify);
		tourHandlerWindow.Show();
	}

	private async void ModifyTourLog(object parameter)
	{
		if (_selectedTourLog is null)
		{
			return;
		}

		tourLogToModify = await _tourLogHelper.GetDataAsync(_selectedTourLog.LogId);

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

		_ = _tourHelper.DeleteDataAsync(_selectedTour.Item1);
		if (_tourLogs.Count > 0)
		{
			_ = _tourLogHelper.DeleteDataAsync(_selectedTour.Item1);
		}

		_selectedTour = null;
		GetTourNames();
	}

	public void DeleteTourLog(object parameter)
	{
		if (_selectedTourLog is null)
		{
			return;
		}

		_ = _tourLogHelper.DeleteDataAsync(_selectedTourLog.LogId);

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
			GetTourNames();
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

	private async void GetTourNames()
	{
		_tourNames = [];
		List<TourModel> tours = await _tourHelper.GetDataAsync();

		foreach (TourModel name in tours)
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

	private static TourModel _selectedTourData;

	public static TourModel SelectedTourData
	{
		get => _selectedTourData;
		set
		{
			if (_selectedTourData != value)
			{
				_selectedTourData = value;
				//OnPropertyChanged(nameof(SelectedTourData));
			}
		}
	}

	public async void SelectTour(object parameter)
	{
		if (parameter == null)
		{
			return;
		}

		int tourId = (int)parameter;
		GetTourLogData(tourId);
		GetImage(tourId);

		SelectedTourData = await _tourHelper.GetDataAsync(tourId);
		CurrentView = new MapDisplayControls();
	}

	private async void GetTourLogData(int id)
	{
		_ = new TourLogModel();
		_tourLogs = [];

		List<TourLogModel> tourLogs = await _tourLogHelper.GetDataAsync();

		foreach (TourLogModel item in tourLogs)
		{
			if (item.TourId == id)
			{
				_tourLogs.Add(item);
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
		// TODO: fix image loading
		//ImageLoader imageLoader = new();
		//Image = imageLoader.GetImagePath(tourId);
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
		((TourHandlerViewModel)tourHandlerWindow.DataContext).TourCreated += OnTourCreated;
		tourHandlerWindow.Show();
	}

	public void OpenTourLogHandlerWindow(object parameter)
	{
		if (_selectedTour is null)
		{
			return;
		}

		TourLogHandlerWindow tourLogHandlerWindow = new();
		tourLogHandlerWindow.Show();
	}

	private bool _isDarkMode;
	public bool IsDarkMode
	{
		get => _isDarkMode;
		set
		{
			_isDarkMode = value;
			OnPropertyChanged();
			ApplyTheme(value);
		}
	}

	private Brush _backgroundBrush;
	public Brush BackgroundBrush
	{
		get => _backgroundBrush;
		set
		{
			_backgroundBrush = value;
			OnPropertyChanged();
		}
	}

	private Brush _foregroundBrush;	
	public Brush ForegroundBrush
	{
		get => _foregroundBrush;
		set
		{
			_foregroundBrush = value;
			OnPropertyChanged();
		}
	}

	public void ToggleTheme(object parameter)
	{
		IsDarkMode = !IsDarkMode;
	}

	private void ApplyTheme(bool isDarkMode)
	{
		if (isDarkMode)
		{
			BackgroundBrush = new SolidColorBrush(Color.FromArgb(230, 34, 31, 47));
			ForegroundBrush = Brushes.White;
		}
		else
		{
			BackgroundBrush = Brushes.White;
			ForegroundBrush = Brushes.Black;
		}
	}
}
