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
public class TourLogServiceTests
{
	private TourLogService _testService;
	private Dictionary<int, DataAccess.Models.TourLog> _tourLogs = [];

	public static void AreEqualByJson(object expected, object actual)
	{
		string expectedJson = JsonSerializer.Serialize(expected);
		string actualJson = JsonSerializer.Serialize(actual);
		Assert.That(expectedJson, Is.EqualTo(actualJson));
	}

	[SetUp]
	public void SetUp()
	{
		Mock<IRepository<DataAccess.Models.TourLog>> mockRepository = new();

		_ = mockRepository.Setup(repo => repo.Add(It.IsAny<DataAccess.Models.TourLog>()))
					.Returns<DataAccess.Models.TourLog>(tourLog =>
					{
						_tourLogs.Add(tourLog.LogId, tourLog);
						return tourLog;
					});

		_ = mockRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
					.Returns<int>(id => _tourLogs.ContainsKey(id) ? _tourLogs[id] : null);

		_ = mockRepository.Setup(repo => repo.Delete(It.IsAny<int>()))
					.Callback<int>(id =>
					{
						if (!_tourLogs.ContainsKey(id))
						{
							throw new DeleteNonExistingEntityException();
						}

						_ = _tourLogs.Remove(id);
					});

		_ = mockRepository.Setup(repo => repo.GetAll())
					.Returns(() => _tourLogs.Values);

		_ = mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<DataAccess.Models.TourLog, bool>>>()))
					.Returns<Expression<Func<DataAccess.Models.TourLog, bool>>>(predicate =>
					{
						return _tourLogs.Values.Where(predicate.Compile());
					});

		_ = mockRepository.Setup(repo => repo.SaveChanges())
					.Callback(() => { /* Do nothing */ });

		_ = mockRepository.Setup(repo => repo.Update(It.IsAny<DataAccess.Models.TourLog>()))
					.Returns<DataAccess.Models.TourLog>(tourLog =>
					{
						if (!_tourLogs.ContainsKey(tourLog.LogId))
						{
							throw new InvalidOperationException("TourLog does not exist.");
						}

						_tourLogs[tourLog.LogId] = tourLog;
						return tourLog;
					});

		_testService = new TourLogService(mockRepository.Object);
	}

	[Test]
	public void CanCallCreate()
	{
		// Arrange
		_tourLogs = [];

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
		TourLog? result = _testService.Create(entity);

		// Assert
		AreEqualByJson(result, entity);
		AreEqualByJson(_tourLogs[entity.LogId], result.MapLogToDataAccess());
	}

	[Test]
	public void CanCallUpdate()
	{
		// Arrange
		_tourLogs = [];

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

		_ = _testService.Create(entity);
		entity.Comment = "UpdatedComment";

		// Act
		TourLog? result = _testService.Update(entity);

		// Assert
		AreEqualByJson(entity, _tourLogs[result.LogId]);
	}

	[Test]
	public void CanCallDelete()
	{
		// Arrange
		_tourLogs = [];

		int id = 1636648681;
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
		_ = _testService.Create(entity);

		// Act
		_testService.Delete(id);

		// Assert
		Assert.That(!_tourLogs.ContainsKey(id));
	}

	[Test]
	public void CanCallGetById_ExistingId()
	{
		// Arrange
		_tourLogs = [];

		int id = 1629047330;
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
		_ = _testService.Create(entity);

		// Act
		TourLog? result = _testService.GetById(id);

		// Assert
		AreEqualByJson(entity, result);
	}

	[Test]
	public void CanCallGetById_NonExistingId()
	{
		// Arrange
		_tourLogs = [];
		int id = 1629047330;

		// Act
		TourLog? result = _testService.GetById(id);

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void CanCallGetAll()
	{
		// Arrange
		_tourLogs = [];

		TourLog tourLog1 = new() { LogId = 1 };
		TourLog tourLog2 = new() { LogId = 2 };
		TourLog tourLog3 = new() { LogId = 3 };

		_ = _testService.Create(tourLog1);
		_ = _testService.Create(tourLog2);
		_ = _testService.Create(tourLog3);

		// Act
		IEnumerable<TourLog>? allTours = _testService.GetAll();

		// Assert
		Assert.That(allTours.Count(), Is.EqualTo(3));
	}

	[Test]
	public void CanCallFind()
	{
		// Arrange
		_tourLogs = [];

		TourLog tourLog = new() { LogId = 1 };
		_ = _testService.Create(tourLog);
		Expression<Func<TourLog, bool>> predicate = x => true;

		// Act
		IEnumerable<TourLog>? result = _testService.Find(predicate);

		// Assert
		Assert.That(result.Any(), Is.EqualTo(true));
	}
}
