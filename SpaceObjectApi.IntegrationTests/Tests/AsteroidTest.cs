namespace SpaceObjectApi.IntegrationTests.Tests
{
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using SpaceObjectApi.Controllers;
    using SpaceObjectApi.Entities;
    using SpaceObjectApi.IntegrationTests.Data;
    using SpaceObjectApi.Repository;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    public class AsteroidTest
    {
        private AsteroidController _controller;
        private DatabaseCreator _db;
        private ISpaceObjectRepository<Asteroid> _repository;
        private Asteroid _asteroid;

        [SetUp]
        public void Setup()
        {
            _db = new DatabaseCreator();
            Utilities.InitializeDbForTests(_db.Context);
            _repository = new SpaceObjectRepository<Asteroid>(_db.Context);
            _controller = new AsteroidController(_repository);
            _asteroid = new Asteroid();
        }

        [OneTimeTearDown]
        public void Сlean()
        {
            Utilities.ReinitializeDbForTests(_db.Context);
        }

        [Test]
        public async Task Get_ShouldGetAsteroid()
        {
            // Arrange    
            var asteroid = await _controller.Get();
            var spaceObject = _repository.Get();

            //Assert
            Assert.AreEqual(spaceObject, asteroid);
        }

        [Test]
        public async Task GetAsteroidIdAsync_ShouldReturnOkAndGetOneAsteroid()
        {
            // Arrange
            var asteroid = _db.Context.Asteroids.FirstOrDefault();
            var propertyAsteroid = await _controller.GetId(asteroid.Id);

            //Act
            var resultReply = (OkObjectResult)propertyAsteroid;
            var reasultAsteroid = resultReply.Value;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(asteroid, reasultAsteroid);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task GetAsteroidIdAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var propertyAsteroid = await _controller.GetId(Id);

            //Act
            var resultReply = (StatusCodeResult)propertyAsteroid;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task CreateAsteroidAsync_ShouldReturnOkAndCreateAsteroid()
        {
            // Arrange
            var spaceObject = await _controller.Create(_asteroid);
            var asteroid = _db.Context.Asteroids.FirstOrDefault(a => a.Name == _asteroid.Name);

            //Act
            var resultReply = (OkObjectResult)spaceObject;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(asteroid, _asteroid);
        }

        [Test]
        public async Task UpdateAsteroidAsync_ShouldReturnOkAndUpdateAsteroid()
        {
            // Arrange            
            var asteroid = _db.Context.Asteroids.FirstOrDefault();
            var updateAsteroid = await _controller.Update(asteroid);

            //Act
            var resultReply = (OkObjectResult)updateAsteroid;
            var propertyAsteroid = await _repository.GetAsync(asteroid.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(asteroid.Id, propertyAsteroid.Id);
        }

        [Test]
        public async Task UpdateAsteroidAsync_ShouldReturnNotFound()
        {
            // Arrange
            var updateAsteroid = await _controller.Update(_asteroid);

            //Act
            var resultReply = (StatusCodeResult)updateAsteroid;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task DeleteAsteroidAsync_ShouldReturnOkAndDeleteAsteroid()
        {
            // Arrange
            var asteroid = _db.Context.Asteroids.FirstOrDefault();
            var deleteAsteroid = await _controller.Delete(asteroid.Id);

            //Act           
            var resultReply = (OkObjectResult)deleteAsteroid;
            var propertyAsteroid = await _repository.GetAsync(asteroid.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(propertyAsteroid, null);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task DeleteAsteroidAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var updateAsteroid = await _controller.Delete(Id);

            //Act
            var resultReply = (StatusCodeResult)updateAsteroid;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }
    }
}
