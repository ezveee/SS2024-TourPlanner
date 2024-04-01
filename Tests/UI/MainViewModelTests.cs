using global::UI.View.MainWindowComponents.TourDetails.TourData.Options;
using UI.ViewModel;

namespace Tests.UI;
[TestFixture]
public class MainViewModelTests
{
	private MainViewModel _viewModel;

	[SetUp]
	public void Setup()
	{
		_viewModel = new MainViewModel();
	}

	[Test]
	[Apartment(ApartmentState.STA)]
	public void SwitchToGeneral_Command_CorrectView()
	{
		_viewModel.SwitchToGeneral(null);
		Assert.IsInstanceOf(typeof(GeneralControls), _viewModel.CurrentView);
	}

	[Test]
	[Apartment(ApartmentState.STA)]
	public void SwitchToMap_Command_CorrectView()
	{
		_viewModel.SwitchToMap(null);
		Assert.IsInstanceOf(typeof(MapDisplayControls), _viewModel.CurrentView);
	}

	[Test]
	[Apartment(ApartmentState.STA)]
	public void SwitchToMisc_Command_CorrectView()
	{
		_viewModel.SwitchToMisc(null);
		Assert.IsInstanceOf(typeof(MiscControls), _viewModel.CurrentView);
	}

	[Test]
	public void OnPropertyChanged_PropertyChangedInvoked()
	{
		bool eventRaised = false;
		_viewModel.PropertyChanged += (sender, args) => eventRaised = true;

		_viewModel.OnPropertyChanged(nameof(_viewModel.CurrentView));

		Assert.IsTrue(eventRaised);
	}

	[Test]
	[Apartment(ApartmentState.STA)]
	public void OpenTourHandlerWindow_Command_TourHandlerWindowOpened()
	{
		// This test assumes that window opening is handled correctly
		Assert.DoesNotThrow(() => _viewModel.OpenTourHandlerWindow(null));
	}

	[Test]
	[Apartment(ApartmentState.STA)]
	public void OpenTourLogHandlerWindow_Command_TourLogHandlerWindowOpened()
	{
		// This test assumes that window opening is handled correctly
		Assert.DoesNotThrow(() => _viewModel.OpenTourLogHandlerWindow(null));
	}
}

