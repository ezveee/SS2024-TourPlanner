using Moq;
using System.Windows.Controls;
using System.Windows.Input;
using UI.ViewModel;

namespace Tests.UI;
public class MainViewModelTests
{
	private MainViewModel _viewModel;
	private Mock<ICommand> _mockCommand;

	[SetUp]
	public void Setup()
	{
		_mockCommand = new Mock<ICommand>();
		_viewModel = new MainViewModel();
	}

	[Test]
	public void CurrentView_Set_RaisesPropertyChanged()
	{
		// Arrange
		bool propertyChangedRaised = false;
		_viewModel.PropertyChanged += (s, e) =>
		{
			if (e.PropertyName == nameof(_viewModel.CurrentView))
			{
				propertyChangedRaised = true;
			}
		};

		// Act
		_viewModel.CurrentView = new UserControl();

		// Assert
		Assert.That(propertyChangedRaised, Is.True);
	}

	[Test]
	public void SelectTour_WithValidParameter_UpdatesSelectedTourData()
	{
		// Arrange
		int testTourId = 18;
		_viewModel.SelectedTour = new Tuple<int, string>(testTourId, "Test Tour");

		// Act
		_viewModel.SelectTour(testTourId);

		// Assert
		Assert.IsNotNull(_viewModel.SelectedTourData);
		Assert.That(_viewModel.SelectedTourData.TourId, Is.EqualTo(testTourId));
	}
}
