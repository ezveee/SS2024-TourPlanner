using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using UI.HttpHelpers;
using UI.Interfaces;
using UI.Models;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel;

public class TourLogEditorViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
{
	public ICommand CreationCommand { get; set; }
	public ICommand QuitCreationCommand { get; set; }

	private readonly Window _window;

	public event PropertyChangedEventHandler? PropertyChanged;

	private readonly TourLogModel _tourLogToModify;

	// is it too late now to say sorry?
	private readonly IHttpHelper<TourLogModel> _tourLogHelper = new HttpTourLogHelper();

	public TourLogEditorViewModel(Window window, TourLogModel logToModify)
	{
		_window = window;
		_tourLogToModify = logToModify;

		SetTourLogToModifyValues();

		CreationCommand = new RelayCommand(Create);
		QuitCreationCommand = new RelayCommand(Quit);
	}

	private void SetTourLogToModifyValues()
	{
		if (_tourLogToModify == null)
		{
			return;
		}

		_date = _tourLogToModify.Date;
		_comment = _tourLogToModify.Comment;
		_difficulty = _tourLogToModify.Difficulty;
		_distance = _tourLogToModify.Distance;
		_time = _tourLogToModify.Time;
		_rating = _tourLogToModify.Rating;
	}

	public void CloseWindow(Window window)
	{
		_window.Close();
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

	public void Create(object parameter)
	{
		_tourLogToModify.Date = _date;
		_tourLogToModify.Comment = _comment;
		_tourLogToModify.Difficulty = _difficulty;
		_tourLogToModify.Distance = _distance;
		_tourLogToModify.Time = _time;
		_tourLogToModify.Rating = _rating;

		_ = _tourLogHelper.UpdateDataAsync(_tourLogToModify);
		CloseWindow(_window);
	}

	public void Quit(object parameter)
	{
		CloseWindow(_window);
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}

