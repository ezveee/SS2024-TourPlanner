using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using HttpServer.Controllers;
using Moq;
using System.Linq.Expressions;
using System.Text.Json;

namespace Tests.Controllers;

[TestFixture]
public class TourLogControllerTests
{
	private TourLogController _testController;
	private Dictionary<int, TourLog> _tourLogs = [];

	public static void AreEqualByJson(object expected, object actual)
	{
		string expectedJson = JsonSerializer.Serialize(expected);
		string actualJson = JsonSerializer.Serialize(actual);
		Assert.That(expectedJson, Is.EqualTo(actualJson));
	}

	[SetUp]
	public void SetUp()
	{
		Mock<IService<TourLog>> mockTourLogService = new();

		_ = mockTourLogService.Setup(service => service.Create(It.IsAny<TourLog>()))
					.Returns<TourLog>(tourLog =>
					{
						_tourLogs.Add(tourLog.LogId, tourLog);
						return tourLog;
					});

		_ = mockTourLogService.Setup(service => service.Update(It.IsAny<TourLog>()))
					.Returns<TourLog>(tourLog =>
					{
						if (!_tourLogs.ContainsKey(tourLog.LogId))
						{
							throw new InvalidOperationException("Tour does not exist.");
						}

						_tourLogs[tourLog.LogId] = tourLog;
						return tourLog;
					});

		_ = mockTourLogService.Setup(service => service.Delete(It.IsAny<int>()))
					.Callback<int>(id =>
					{
						if (!_tourLogs.ContainsKey(id))
						{
							throw new InvalidOperationException("Tour does not exist.");
						}

						_ = _tourLogs.Remove(id);
					});

		_ = mockTourLogService.Setup(service => service.GetById(It.IsAny<int>()))
					.Returns<int>(id => _tourLogs.ContainsKey(id) ? _tourLogs[id] : null);

		_ = mockTourLogService.Setup(service => service.GetAll())
					.Returns(() => _tourLogs.Values);

		_ = mockTourLogService.Setup(service => service.Find(It.IsAny<Expression<Func<TourLog, bool>>>()))
					.Returns<Expression<Func<TourLog, bool>>>(predicate =>
					{
						return _tourLogs.Values.AsQueryable().Where(predicate.Compile());
					});

		_testController = new TourLogController(mockTourLogService.Object);
	}

	[Test]
	public void CanCallGetWithNoParameters()
	{
		// Arrange
		_tourLogs = [];

		TourLog tourLog1 = new() { LogId = 1 };
		TourLog tourLog2 = new() { LogId = 2 };
		TourLog tourLog3 = new() { LogId = 3 };

		_ = _testController.Post(tourLog1);
		_ = _testController.Post(tourLog2);
		_ = _testController.Post(tourLog3);

		// Act
		IEnumerable<TourLog>? allTours = _testController.Get();

		// Assert
		Assert.That(allTours.Count(), Is.EqualTo(3));
	}

	[Test]
	public void CanCallGetWithInt()
	{
		// Arrange
		_tourLogs = [];
		int id = 1539046787;

		// Act
		IEnumerable<TourLog>? result = _testController.Get(id);

		// Assert
		Assert.That(result, Is.Empty);
	}

	[Test]
	public void CanCallPost()
	{
		// Arrange
		TourLog entity = new()
		{
			LogId = 1046990382,
			TourId = 1234576551,
			Date = DateTime.UtcNow,
			Comment = "TestValue689665582",
			Difficulty = 1159203374,
			Distance = 6497.31934F,
			Time = 1332819267.12,
			Rating = 1405850949
		};

		// Act
		TourLog? result = _testController.Post(entity).Value;

		// Assert
		AreEqualByJson(result, entity);
		AreEqualByJson(_tourLogs[entity.LogId], result.MapLogToDataAccess());
	}

	[Test]
	public void CanCallPut()
	{
		// Arrange
		TourLog updatedTourLog = new()
		{
			LogId = 1046990382,
			TourId = 1234576551,
			Date = DateTime.UtcNow,
			Comment = "TestValue689665582",
			Difficulty = 1159203374,
			Distance = 6497.31934F,
			Time = 1332819267.12,
			Rating = 1405850949
		};

		_ = _testController.Post(updatedTourLog);

		updatedTourLog.Comment = "UpdatedComment";

		// Act
		TourLog? result = _testController.Put(updatedTourLog).Value;

		// Assert
		AreEqualByJson(updatedTourLog, _tourLogs[result.LogId]);
	}

	[Test]
	public void CanCallDelete()
	{
		// Arrange
		_tourLogs = [];

		int id = 517703432;

		TourLog entity = new()
		{
			LogId = id,
			TourId = 1234576551,
			Date = DateTime.UtcNow,
			Comment = "TestValue689665582",
			Difficulty = 1159203374,
			Distance = 6497.31934F,
			Time = 1332819267.12,
			Rating = 1405850949
		};
		_ = _testController.Post(entity);

		// Act
		_ = _testController.Delete(id);

		// Assert
		Assert.That(!_tourLogs.ContainsKey(id));
	}
}
