using Business.Extensions;
using Business.Models;
using Business.Services;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using Moq;
using System.Linq.Expressions;
using System.Text.Json;

namespace Tests.Services;

[TestFixture]
public class TourServiceTests
{
	private TourService _testService;
	private Dictionary<int, DataAccess.Models.Tour> _tours = [];

	public static void AreEqualByJson(object expected, object actual)
	{
		string expectedJson = JsonSerializer.Serialize(expected);
		string actualJson = JsonSerializer.Serialize(actual);
		Assert.That(expectedJson, Is.EqualTo(actualJson));
	}

	[SetUp]
	public void SetUp()
	{
		Mock<IRepository<DataAccess.Models.Tour>> mockRepository = new();

		_ = mockRepository.Setup(repo => repo.Add(It.IsAny<DataAccess.Models.Tour>()))
					.Returns<DataAccess.Models.Tour>(tour =>
					{
						_tours.Add(tour.TourId, tour);
						return tour;
					});

		_ = mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
					.Returns<int>(id => _tours.ContainsKey(id) ? _tours[id] : null);

		_ = mockRepository.Setup(repo => repo.Delete(It.IsAny<int>()))
					.Callback<int>(id =>
					{
						if (!_tours.ContainsKey(id))
						{
							throw new DeleteNonExistingEntityException();
						}

						_ = _tours.Remove(id);
					});

		_ = mockRepository.Setup(repo => repo.GetAll())
					.Returns(() => _tours.Values);

		_ = mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<DataAccess.Models.Tour, bool>>>()))
					.Returns<Expression<Func<DataAccess.Models.Tour, bool>>>(predicate =>
					{
						return _tours.Values.Where(predicate.Compile());
					});

		_ = mockRepository.Setup(repo => repo.SaveChanges())
					.Callback(() => { /* Do nothing */ });

		_ = mockRepository.Setup(repo => repo.Update(It.IsAny<DataAccess.Models.Tour>()))
					.Returns<DataAccess.Models.Tour>(tour =>
					{
						if (!_tours.ContainsKey(tour.TourId))
						{
							throw new InvalidOperationException("Tour does not exist.");
						}

						_tours[tour.TourId] = tour;
						return tour;
					});

		Mock<IRepository<DataAccess.Models.TourLog>> mockLogRepository = new();
		_testService = new TourService(mockRepository.Object, mockLogRepository.Object);
	}

	[Test]
	public void CanCallCreate()
	{
		// Arrange
		_tours = [];

		Tour entity = new()
		{
			TourId = 1708625528,
			Name = "TestValue996594570",
			Description = "TestValue239573240",
			From = "TestValue1747671873",
			To = "TestValue1396600192",
			TransportType = "TestValue652687087",
			Distance = 5277.30371F,
			EstimatedTime = 1954818352.08,
			RouteInformation = "TestValue1258683528"
		};

		// Act
		Tour? result = _testService.Create(entity);

		// Assert
		AreEqualByJson(result, entity);
		AreEqualByJson(_tours[entity.TourId], result.MapTourToDataAccess());
	}

	[Test]
	public void CanCallUpdate()
	{
		// Arrange
		_tours = [];

		Tour entity = new()
		{
			TourId = 9873576,
			Name = "TestValue2007399243",
			Description = "TestValue132821423",
			From = "TestValue1358489351",
			To = "TestValue2136692841",
			TransportType = "TestValue69859409",
			Distance = 17516.2031F,
			EstimatedTime = 1579479408.54,
			RouteInformation = "TestValue524436639"
		};

		_ = _testService.Create(entity);
		entity.Name = "UpdatedName";

		// Act
		Tour? result = _testService.Update(entity);

		// Assert
		AreEqualByJson(entity, _tours[result.TourId]);
	}

	[Test]
	public void CanCallDelete()
	{
		// Arrange
		_tours = [];

		int id = 1636648681;
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
		_ = _testService.Create(entity);

		// Act
		_testService.Delete(id);

		// Assert
		Assert.That(!_tours.ContainsKey(id));
	}

	[Test]
	public void CanCallGetById_ExistingId()
	{
		// Arrange
		_tours = [];

		int id = 1629047330;
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
		_ = _testService.Create(entity);

		// Act
		Tour? result = _testService.GetById(id);

		// Assert
		AreEqualByJson(entity, result);
	}

	[Test]
	public void CanCallGetById_NonExistingId()
	{
		// Arrange
		_tours = [];
		int id = 1629047330;

		// Act
		Tour? result = _testService.GetById(id);

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void CanCallGetAll()
	{
		// Arrange
		_tours = [];

		Tour tour1 = new() { TourId = 1 };
		Tour tour2 = new() { TourId = 2 };
		Tour tour3 = new() { TourId = 3 };

		_ = _testService.Create(tour1);
		_ = _testService.Create(tour2);
		_ = _testService.Create(tour3);

		// Act
		IEnumerable<Tour>? allTours = _testService.GetAll();

		// Assert
		Assert.That(allTours.Count(), Is.EqualTo(3));
	}

	[Test]
	public void CanCallFind()
	{
		// Arrange
		_tours = [];

		Tour tour = new() { TourId = 1 };
		_ = _testService.Create(tour);
		Expression<Func<Tour, bool>> predicate = x => true;

		// Act
		IEnumerable<Tour>? result = _testService.Find(predicate);

		// Assert
		Assert.That(result.Any(), Is.EqualTo(true));
	}
}
