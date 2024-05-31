using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using HttpServer.Controllers;
using Moq;
using System.Linq.Expressions;
using System.Text.Json;

namespace Tests.Controllers;

[TestFixture]
public class TourControllerTests
{
	private TourController _testController;
	private Dictionary<int, Tour> _tours = [];

	public static void AreEqualByJson(object expected, object actual)
	{
		string expectedJson = JsonSerializer.Serialize(expected);
		string actualJson = JsonSerializer.Serialize(actual);
		Assert.That(expectedJson, Is.EqualTo(actualJson));
	}

	[SetUp]
	public void SetUp()
	{
		Mock<IService<Tour>> mockTourService = new();

		_ = mockTourService.Setup(service => service.Create(It.IsAny<Tour>()))
					.Returns<Tour>(tour =>
					{
						_tours.Add(tour.TourId, tour);
						return tour;
					});

		_ = mockTourService.Setup(service => service.Update(It.IsAny<Tour>()))
					.Returns<Tour>(tour =>
					{
						if (!_tours.ContainsKey(tour.TourId))
						{
							throw new InvalidOperationException("Tour does not exist.");
						}

						_tours[tour.TourId] = tour;
						return tour;
					});

		_ = mockTourService.Setup(service => service.Delete(It.IsAny<int>()))
					.Callback<int>(id =>
					{
						if (!_tours.ContainsKey(id))
						{
							throw new InvalidOperationException("Tour does not exist.");
						}

						_ = _tours.Remove(id);
					});

		_ = mockTourService.Setup(service => service.GetById(It.IsAny<int>()))
					.Returns<int>(id => _tours.ContainsKey(id) ? _tours[id] : null);

		_ = mockTourService.Setup(service => service.GetAll())
					.Returns(() => _tours.Values);

		_ = mockTourService.Setup(service => service.Find(It.IsAny<Expression<Func<Tour, bool>>>()))
					.Returns<Expression<Func<Tour, bool>>>(predicate =>
					{
						return _tours.Values.AsQueryable().Where(predicate.Compile());
					});

		_testController = new TourController(mockTourService.Object);
	}

	[Test]
	public void CanCallGetWithNoParameters()
	{
		// Arrange
		_tours = [];

		Tour tour1 = new() { TourId = 1 };
		Tour tour2 = new() { TourId = 2 };
		Tour tour3 = new() { TourId = 3 };

		_ = _testController.Post(tour1);
		_ = _testController.Post(tour2);
		_ = _testController.Post(tour3);

		// Act
		IEnumerable<Tour>? allTours = _testController.Get();

		// Assert
		Assert.That(allTours.Count(), Is.EqualTo(3));
	}

	[Test]
	public void CanCallGetWithInt()
	{
		// Arrange
		_tours = [];
		int id = 1539046787;

		// Act
		Tour? result = _testController.Get(id).Value;

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void CanCallPost()
	{
		// Arrange
		Tour tour = new()
		{
			TourId = 989317127,
			Name = "TestValue1304070950",
			Description = "TestValue927007853",
			From = "TestValue1887134603",
			To = "TestValue1140295235",
			TransportType = "TestValue1146286518",
			Distance = 21195.6465F,
			EstimatedTime = 1320092882.46,
			RouteInformation = "TestValue114990506"
		};

		// Act
		Tour? result = _testController.Post(tour).Value;

		// Assert
		AreEqualByJson(result, tour);
		AreEqualByJson(_tours[tour.TourId], result.MapTourToDataAccess());
	}

	[Test]
	public void CanCallPut()
	{
		// Arrange
		Tour updatedTour = new()
		{
			TourId = 1626773551,
			Name = "TestValue2061020759",
			Description = "TestValue163397771",
			From = "TestValue1966528622",
			To = "TestValue2073718487",
			TransportType = "TestValue645336389",
			Distance = 22287.17F,
			EstimatedTime = 1438973760.51,
			RouteInformation = "TestValue1816591746"
		};

		_ = _testController.Post(updatedTour);

		updatedTour.Name = "UpdatedName";

		// Act
		Tour? result = _testController.Put(updatedTour).Value;

		// Assert
		AreEqualByJson(updatedTour, _tours[result.TourId]);
	}

	[Test]
	public void CanCallDelete()
	{
		// Arrange
		_tours = [];

		int id = 517703432;

		Tour entity = new()
		{
			TourId = id,
			Name = "TestValue2007399243",
			Description = "TestValue132821423",
			From = "TestValue1358489351",
			To = "TestValue2136692841",
			TransportType = "TestValue69859409",
			Distance = 17516.2031F,
			EstimatedTime = 1579479408.54,
			RouteInformation = "TestValue524436639"
		};
		_ = _testController.Post(entity);

		// Act
		_ = _testController.Delete(id);

		// Assert
		Assert.That(!_tours.ContainsKey(id));
	}
}
