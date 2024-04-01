using Business.Interfaces;
using Business.Models;
using Moq;
using UI.Models;

namespace Tests.UI;
[TestFixture]
public class TourLogModelTests
{
	private Mock<IManager<TourLog>> _mockManager;
	private TourLogModel _tourLogModel;

	[SetUp]
	public void Setup()
	{
		_mockManager = new Mock<IManager<TourLog>>();
		_tourLogModel = new TourLogModel();
	}

	[Test]
	public void Constructor_WithoutParameters_InitializesTourLogs()
	{
		// Arrange
		List<TourLog> expectedTourLogs = new() { new TourLog() };
		_ = _mockManager.Setup(m => m.GetAll()).Returns(expectedTourLogs);

		// Act
		TourLogModel tourLogModel = new();

		// Assert
		Assert.That(tourLogModel.TourLogs, Is.EqualTo(expectedTourLogs));
	}

	[Test]
	public void Constructor_WithIdParameter_InitializesTourLogsWithLogsByTourId()
	{
		// Arrange
		int testId = 1;
		List<TourLog> expectedTourLogs = new() { new TourLog() };
		_ = _mockManager.Setup(m => m.GetLogsByTourId(testId)).Returns(expectedTourLogs);

		// Act
		TourLogModel tourLogModel = new(testId);

		// Assert
		Assert.That(tourLogModel.TourLogs, Is.EqualTo(expectedTourLogs));
	}

	// Add more tests for other methods and properties as needed
}
