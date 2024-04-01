using System.Windows.Controls;
using System.Windows.Input;
using UI.ViewModel;

namespace UI.View.MainWindowComponents.TourList;

/// <summary>
/// Interaction logic for TourListControls.xaml
/// </summary>
public partial class TourListControls : UserControl
{
	public TourListControls()
	{
		InitializeComponent();
	}

	private void TourItem_Click(object sender, MouseButtonEventArgs e)
	{
		if (sender is TextBlock textBlock && textBlock.Tag is int tourId)
		{
			((MainViewModel)DataContext).SelectTourCommand.Execute(tourId);
		}
	}
}
