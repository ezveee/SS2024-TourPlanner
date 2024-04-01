using System.Windows;
using UI.Models;
using UI.ViewModel;

namespace UI;

/// <summary>
/// Interaction logic for AddTourWindow.xaml
/// </summary>
public partial class TourHandlerWindow : Window
{
	public TourHandlerWindow()
	{
		InitializeComponent();
		DataContext = new TourHandlerViewModel(this);
	}

	public TourHandlerWindow(TourModel tourModel)
	{
		InitializeComponent();
		DataContext = new TourEditorViewModel(this, tourModel);
	}
}
