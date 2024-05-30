using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using UI.HttpHelpers;
using UI.Interfaces;
using UI.Models;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel;

public class TourLogHandlerViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
{
	public ICommand CreationCommand { get; set; }
	public ICommand QuitCreationCommand { get; set; }

	private readonly Window _window;

	public event PropertyChangedEventHandler? PropertyChanged;

	// you know the gist
	private readonly IHttpHelper<TourLogModel> _tourLogHelper = new HttpTourLogHelper();

	public TourLogHandlerViewModel(Window window)
	{
		_window = window;

		CreationCommand = new RelayCommand(Create);
		QuitCreationCommand = new RelayCommand(Quit);
	}

	#region Create a Tourlog
	#region Tourlog details
	private int _tourId;
	public int TourId
	{
		get => _tourId;
		set
		{
			_tourId = value;
			OnPropertyChanged(nameof(TourId));
		}
	}

	private DateTime _date;
	public DateTime Date
	{
		get => _date;
		set
		{
			_date = value;
			OnPropertyChanged(nameof(Date));
		}
	}

	private string _comment;
	public string Comment
	{
		get => _comment;
		set
		{
			_comment = value;
			OnPropertyChanged(nameof(Comment));
		}
	}

	private int _difficulty;
	public int Difficulty
	{
		get => _difficulty;
		set
		{
			_difficulty = value;
			OnPropertyChanged(nameof(Difficulty));
		}
	}

	private float _distance;
	public float Distance
	{
		get => _distance;
		set
		{
			_distance = value;
			OnPropertyChanged(nameof(Distance));
		}
	}

	private double _time;
	public double Time
	{
		get => _time;
		set
		{
			_time = value;
			OnPropertyChanged(nameof(Time));
		}
	}

	private int _rating;
	public int Rating
	{
		get => _rating;
		set
		{
			_rating = value;
			OnPropertyChanged(nameof(Rating));
		}
	}
	#endregion

	public void Create(object parameter)
	{
		TourId = MainViewModel.SelectedTourData.TourId;

		TourLogModel tourLogModel = new()
		{
			TourId = _tourId,
			Date = _date,
			Comment = _comment,
			Difficulty = _difficulty,
			Distance = _distance,
			Time = _time,
			Rating = _rating
		};

		_ = _tourLogHelper.CreateDataAsync(tourLogModel);
		CloseWindow(_window);
	}
	#endregion

	public void Quit(object parameter)
	{
		CloseWindow(_window);
	}

	public void CloseWindow(Window window)
	{
		window.Close();
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
