using System.Windows;
using UI.Models;
using UI.ViewModel;

namespace UI;

/// <summary>
/// Interaction logic for TourLogHandlerWindow.xaml
/// </summary>
public partial class TourLogHandlerWindow : Window
{
	public TourLogHandlerWindow()
	{
		InitializeComponent();
		DataContext = new TourLogHandlerViewModel(this);
	}

	public TourLogHandlerWindow(TourLogModel tourLogModel)
	{
		InitializeComponent();
		DataContext = new TourLogEditorViewModel(this, tourLogModel);
	}
}
