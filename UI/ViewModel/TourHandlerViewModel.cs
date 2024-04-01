using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using UI.Models;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel;

public class TourHandlerViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
{
	public ICommand CreationCommand { get; set; }
	public ICommand QuitCreationCommand { get; set; }

	private readonly Window _window;

	public event PropertyChangedEventHandler? PropertyChanged;

	public event EventHandler TourCreated;

	public TourHandlerViewModel(Window window)
	{
		_window = window;

		CreationCommand = new RelayCommand(Create);
		QuitCreationCommand = new RelayCommand(Quit);
	}

	public TourHandlerViewModel()
	{

	}

	#region Create new Tour
	#region Tour details
	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			_name = value;
			OnPropertyChanged(nameof(Name));
		}
	}

	private string _desc;
	public string Desc
	{
		get => _desc;
		set
		{
			_desc = value;
			OnPropertyChanged(nameof(Desc));
		}
	}

	private string _from;
	public string From
	{
		get => _from;
		set
		{
			_from = value;
			OnPropertyChanged(nameof(From));
		}
	}

	private string _to;
	public string To
	{
		get => _to;
		set
		{
			_to = value;
			OnPropertyChanged(nameof(To));
		}
	}

	private string _transportType;
	public string TransportType
	{
		get => _transportType;
		set
		{
			_transportType = value;
			OnPropertyChanged(nameof(TransportType));
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

	private string _info;
	public string Info
	{
		get => _info;
		set
		{
			_info = value;
			OnPropertyChanged(nameof(Info));
		}
	}
	#endregion

	public void Create(object parameter)
	{
		TourModel.CreateTour(_name, _desc, _from, _to, _transportType, _distance, _time, _info);
		CloseWindow(_window);

		TourCreated?.Invoke(this, EventArgs.Empty);
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
