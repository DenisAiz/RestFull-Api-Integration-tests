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
    public class BlackHoleTest
    {
        private BlackHoleController _controller;
        private DatabaseCreator _db;
        private ISpaceObjectRepository<BlackHole> _repository;
        private BlackHole _blackHole;

        [SetUp]
        public void Setup()
        {
            _db = new DatabaseCreator();
            Utilities.InitializeDbForTests(_db.Context);
            _repository = new SpaceObjectRepository<BlackHole>(_db.Context);
            _controller = new BlackHoleController(_repository);
            _blackHole = new BlackHole();
        }

        [OneTimeTearDown]
        public void Сlean()
        {
            Utilities.ReinitializeDbForTests(_db.Context);
        }

        [Test]
        public async Task Get_ShouldGetBlackHole()
        {
            // Arrange    
            var blackHole = await _controller.Get();
            var spaceObject = _repository.Get();

            //Assert
            Assert.AreEqual(spaceObject, blackHole);
        }

        [Test]
        public async Task GetBlackHoleIdAsync_ShouldReturnOkAndGetOneBlackHole()
        {
            // Arrange
            var blackHole = _db.Context.BlackHoles.FirstOrDefault();
            var propertyBlackHole = await _controller.GetId(blackHole.Id);

            //Act
            var resultReply = (OkObjectResult)propertyBlackHole;
            var reasultBlackHole = resultReply.Value;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(blackHole, reasultBlackHole);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task GetBlackHoleIdAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var propertyBlackHole = await _controller.GetId(Id);

            //Act
            var resultReply = (StatusCodeResult)propertyBlackHole;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task CreateBlackHoleAsync_ShouldReturnOkAndCreateBlackHole()
        {
            // Arrange
            var spaceObject = await _controller.Create(_blackHole);
            var blackHole = _db.Context.BlackHoles.FirstOrDefault(a => a.Name == _blackHole.Name);

            //Act
            var resultReply = (OkObjectResult)spaceObject;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(blackHole, _blackHole);
        }

        [Test]
        public async Task UpdateBlackHoleAsync_ShouldReturnOkAndUpdateBlackHole()
        {
            // Arrange
            var blackHole = _db.Context.BlackHoles.FirstOrDefault();
            var updateBlackHole = await _controller.Update(blackHole);

            //Act
            var resultReply = (OkObjectResult)updateBlackHole;
            var propertyBlackHole = await _repository.GetAsync(blackHole.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(blackHole.Id, propertyBlackHole.Id);
        }

        [Test]
        public async Task UpdateBlackHoleAsync_ShouldReturnNotFound()
        {
            // Arrange
            var updateBlackHole = await _controller.Update(_blackHole);

            //Act
            var resultReply = (StatusCodeResult)updateBlackHole;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task DeleteBlackHoleAsync_ShouldReturnOkAndDeleteBlackHole()
        {
            // Arrange
            var blackHole = _db.Context.BlackHoles.FirstOrDefault();
            var deleteBlackHole = await _controller.Delete(blackHole.Id);

            //Act           
            var resultReply = (OkObjectResult)deleteBlackHole;
            var propertyBlackHole = await _repository.GetAsync(blackHole.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(propertyBlackHole, null);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task DeleteBlackHoleAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var updateBlackHole = await _controller.Delete(Id);

            //Act
            var resultReply = (StatusCodeResult)updateBlackHole;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }
    }
}
