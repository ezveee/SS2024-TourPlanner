using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Models;
using UI.ViewModel.Commands;
using UI.ViewModel.Interfaces;

namespace UI.ViewModel;
public class TourEditorViewModel : ICreationHandlerViewModel, INotifyPropertyChanged
{
	public ICommand CreationCommand { get; set; }
	public ICommand QuitCreationCommand { get; set; }

	private readonly Window _window;

	public event PropertyChangedEventHandler? PropertyChanged;

	TourModel _tourToModify;

	public TourEditorViewModel(Window window, TourModel tourToModify)
	{
		_window = window;
		_tourToModify = tourToModify;

		SetTourToModifyValues();

		CreationCommand = new RelayCommand(Create);
		QuitCreationCommand = new RelayCommand(Quit);
	}

	private void SetTourToModifyValues()
	{
		if (_tourToModify == null) 
		{
			return;
		}

		_name = _tourToModify.Name;
		_desc = _tourToModify.Description;
		_from = _tourToModify.From;
		_to = _tourToModify.To;
		_transportType = _tourToModify.TransportType;
		_distance = _tourToModify.Distance;
		_time = _tourToModify.EstimatedTime;
		_info = _tourToModify.RouteInformation;
	}

	public void CloseWindow(Window window)
	{
		_window.Close();
	}

	private string _name;
	public string Name
	{
		get { return _name; }
		set
		{
			_name = value;
			OnPropertyChanged(nameof(Name));
		}
	}

	private string _desc;
	public string Desc
	{
		get { return _desc; }
		set
		{
			_desc = value;
			OnPropertyChanged(nameof(Desc));
		}
	}

	private string _from;
	public string From
	{
		get { return _from; }
		set
		{
			_from = value;
			OnPropertyChanged(nameof(From));
		}
	}

	private string _to;
	public string To
	{
		get { return _to; }
		set
		{
			_to = value;
			OnPropertyChanged(nameof(To));
		}
	}

	private string _transportType;
	public string TransportType
	{
		get { return _transportType; }
		set
		{
			_transportType = value;
			OnPropertyChanged(nameof(TransportType));
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

	private string _info;
	public string Info
	{
		get { return _info; }
		set
		{
			_info = value;
			OnPropertyChanged(nameof(Info));
		}
	}

	public void Create(object parameter)
	{
		_tourToModify.Name = _name;
		_tourToModify.Description = _desc;
		_tourToModify.From = _from;
		_tourToModify.To = _to;
		_tourToModify.Distance = _distance;
		_tourToModify.TransportType = _transportType;
		_tourToModify.EstimatedTime = _time;
		_tourToModify.RouteInformation = _info;

		TourModel.UpdateTour(_tourToModify);
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
