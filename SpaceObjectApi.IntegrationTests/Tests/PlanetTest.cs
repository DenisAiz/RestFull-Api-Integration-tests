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
    public class PlanetTest
    {
        private PlanetController _controller;
        private DatabaseCreator _db;
        private ISpaceObjectRepository<Planet> _repository;
        private Planet _planet;

        [SetUp]
        public void Setup()
        {
            _db = new DatabaseCreator();
            Utilities.InitializeDbForTests(_db.Context);
            _repository = new SpaceObjectRepository<Planet>(_db.Context);
            _controller = new PlanetController(_repository);
            _planet = new Planet();
        }

        [OneTimeTearDown]
        public void Сlean()
        {
            Utilities.ReinitializeDbForTests(_db.Context);
        }

        [Test]
        public async Task Get_ShouldGetPlanet()
        {
            // Arrange    
            var planet = await _controller.Get();
            var spaceObject = _repository.Get();

            //Assert
            Assert.AreEqual(spaceObject, planet);
        }

        [Test]
        public async Task GetPlanetIdAsync_ShouldReturnOkAndGetOnePlanet()
        {
            // Arrange
            var planet = _db.Context.Planets.FirstOrDefault();
            var propertyPlanet = await _controller.GetId(planet.Id);

            //Act
            var resultReply = (OkObjectResult)propertyPlanet;
            var reasultPlanet = resultReply.Value;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(planet, reasultPlanet);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task GetPlanetIdAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var propertyPlanet = await _controller.GetId(Id);

            //Act
            var resultReply = (StatusCodeResult)propertyPlanet;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task CreatePlanetAsync_ShouldReturnOkAndCreatePlanet()
        {
            // Arrange
            var spaceObject = await _controller.Create(_planet);
            var planet = _db.Context.Planets.FirstOrDefault(a => a.Name == _planet.Name);

            //Act
            var resultReply = (OkObjectResult)spaceObject;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(planet, _planet);
        }

        [Test]
        public async Task UpdatePlanetAsync_ShouldReturnOkAndUpdatePlanet()
        {
            // Arrange
            var planet = _db.Context.Planets.FirstOrDefault();
            var updatePlanet = await _controller.Update(planet);

            //Act
            var resultReply = (OkObjectResult)updatePlanet;
            var propertyPlanet = await _repository.GetAsync(planet.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(planet.Id, propertyPlanet.Id);
        }

        [Test]
        public async Task UpdatePlanetAsync_ShouldReturnNotFound()
        {
            // Arrange
            var updatePlanet = await _controller.Update(_planet);

            //Act
            var resultReply = (StatusCodeResult)updatePlanet;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task DeletePlanetAsync_ShouldReturnOkAndDeletePlanet()
        {
            // Arrange
            var planet = _db.Context.Planets.FirstOrDefault();
            var deletePlanet = await _controller.Delete(planet.Id);

            //Act           
            var resultReply = (OkObjectResult)deletePlanet;
            var propertyPlanet = await _repository.GetAsync(planet.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(propertyPlanet, null);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task DeletePlanetAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var updatePlanet = await _controller.Delete(Id);

            //Act
            var resultReply = (StatusCodeResult)updatePlanet;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }
    }
}
