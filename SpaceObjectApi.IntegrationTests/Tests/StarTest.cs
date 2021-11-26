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
    public class StarTest
    {
        private StarController _controller;
        private DatabaseCreator _db;
        private ISpaceObjectRepository<Star> _repository;
        private Star _star;

        [SetUp]
        public void Setup()
        {
            _db = new DatabaseCreator();
            Utilities.InitializeDbForTests(_db.Context);
            _repository = new SpaceObjectRepository<Star>(_db.Context);
            _controller = new StarController(_repository);
            _star = new Star();
        }

        [OneTimeTearDown]
        public void Сlean()
        {
            Utilities.ReinitializeDbForTests(_db.Context);
        }

        [Test]
        public async Task Get_ShouldGetStar()
        {
            // Arrange    
            var star = await _controller.Get();
            var spaceObject = _repository.Get();

            //Assert
            Assert.AreEqual(spaceObject, star);
        }

        [Test]
        public async Task GetStarIdAsync_ShouldReturnOkAndGetOneStar()
        {
            // Arrange
            var star = _db.Context.Stars.FirstOrDefault();
            var propertyStar = await _controller.GetId(star.Id);

            //Act
            var resultReply = (OkObjectResult)propertyStar;
            var reasultStar = resultReply.Value;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(star, reasultStar);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task GetStarIdAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var propertyStar = await _controller.GetId(Id);

            //Act
            var resultReply = (StatusCodeResult)propertyStar;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task CreateStarAsync_ShouldReturnOkAndCreateStar()
        {
            // Arrange
            var spaceObject = await _controller.Create(_star);
            var star = _db.Context.Stars.FirstOrDefault(a => a.Name == _star.Name);

            //Act
            var resultReply = (OkObjectResult)spaceObject;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(star, _star);
        }

        [Test]
        public async Task UpdateStarAsync_ShouldReturnOkAndUpdateStar()
        {
            // Arrange
            var star = _db.Context.Stars.FirstOrDefault();
            var updateStar = await _controller.Update(star);

            //Act
            var resultReply = (OkObjectResult)updateStar;
            var propertyStar = await _repository.GetAsync(star.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(star.Id, propertyStar.Id);
        }

        [Test]
        public async Task UpdateStarAsync_ShouldReturnNotFound()
        {
            // Arrange
            var updateStar = await _controller.Update(_star);

            //Act
            var resultReply = (StatusCodeResult)updateStar;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }

        [Test]
        public async Task DeleteStarAsync_ShouldReturnOkAndDeleteStar()
        {
            // Arrange
            var star = _db.Context.Stars.FirstOrDefault();
            var deleteStar = await _controller.Delete(star.Id);

            //Act           
            var resultReply = (OkObjectResult)deleteStar;
            var propertyStar = await _repository.GetAsync(star.Id);

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, resultReply.StatusCode);
            Assert.AreEqual(propertyStar, null);
        }

        [Test]
        [TestCase(int.MinValue)]
        public async Task DeleteStarAsync_ShouldReturnNotFound(int Id)
        {
            // Arrange
            var updateStar = await _controller.Delete(Id);

            //Act
            var resultReply = (StatusCodeResult)updateStar;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, resultReply.StatusCode);
        }
    }
}
