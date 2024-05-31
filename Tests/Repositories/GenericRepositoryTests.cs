using DataAccess.Interfaces;
using Moq;

namespace Tests.Repositories;

public class TestEntity
{
	public int Id { get; set; }
	public required string Name { get; set; }
}


[TestFixture]
public class GenericRepositoryTests
{
	private Mock<IRepository<TestEntity>> _repositoryMock;
	private List<TestEntity> _data;

	[SetUp]
	public void Setup()
	{
		_data =
		[
			new TestEntity { Id = 1, Name = "Test1" },
			new TestEntity { Id = 2, Name = "Test2" }
		];

		_repositoryMock = new Mock<IRepository<TestEntity>>();

		_ = _repositoryMock.Setup(r => r.GetAll()).Returns(_data);
		_ = _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns<int>(id => _data.FirstOrDefault(e => e.Id == id));

	}

	[Test]
	public void GetAll_ReturnsAllEntities()
	{
		// Arrange
		IRepository<TestEntity> repository = _repositoryMock.Object;

		// Act
		IEnumerable<TestEntity>? result = repository.GetAll();

		Assert.Multiple(() =>
		{
			// Assert
			Assert.That(result.Count(), Is.EqualTo(_data.Count));
			Assert.That(_data.First().Name, Is.EqualTo(result.First().Name));
		});
	}
}
