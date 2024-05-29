//using global::UI.View.MainWindowComponents.TourDetails.TourData.Options;
//using System.Windows.Controls;
//using UI.ViewModel;
//using NUnit;

//namespace Tests.UI;
//[TestFixture]
//public class MainViewModelTests
//{
//	private MainViewModel _viewModel;

//	[SetUp]
//	public void Setup()
//	{
//		_viewModel = new MainViewModel();
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void SwitchToGeneral_Command_CorrectView()
//	{
//		_viewModel.SwitchToGeneral(null);
//		Assert.IsInstanceOf(typeof(GeneralControls), _viewModel.CurrentView);
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void SwitchToMap_Command_CorrectView()
//	{
//		_viewModel.SwitchToMap(null);
//		Assert.IsInstanceOf(typeof(MapDisplayControls), _viewModel.CurrentView);
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void SwitchToMisc_Command_CorrectView()
//	{
//		_viewModel.SwitchToMisc(null);
//		Assert.IsInstanceOf(typeof(MiscControls), _viewModel.CurrentView);
//	}

//	[Test]
//	public void OnPropertyChanged_PropertyChangedInvoked()
//	{
//		bool eventRaised = false;
//		_viewModel.PropertyChanged += (sender, args) => eventRaised = true;

//		_viewModel.OnPropertyChanged(nameof(_viewModel.CurrentView));

//		Assert.IsTrue(eventRaised);
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void OpenTourHandlerWindow_Command_TourHandlerWindowOpened()
//	{
//		Assert.DoesNotThrow(() => _viewModel.OpenTourHandlerWindow(null));
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void OpenTourLogHandlerWindow_Command_TourLogHandlerWindowOpened()
//	{
//		Assert.DoesNotThrow(() => _viewModel.OpenTourLogHandlerWindow(null));
//	}

//	[Test]
//	[Apartment(ApartmentState.STA)]
//	public void CurrentView_Set_RaisesPropertyChanged()
//	{
//		// Arrange
//		bool propertyChangedRaised = false;
//		_viewModel.PropertyChanged += (s, e) =>
//		{
//			if (e.PropertyName == nameof(_viewModel.CurrentView))
//			{
//				propertyChangedRaised = true;
//			}
//		};

//		// Act
//		_viewModel.CurrentView = new UserControl();

//		// Assert
//		Assert.That(propertyChangedRaised, Is.True);
//	}
//}

// TODO: fix whatever happened here