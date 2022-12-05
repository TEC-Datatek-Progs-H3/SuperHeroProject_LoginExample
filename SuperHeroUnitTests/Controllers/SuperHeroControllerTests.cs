namespace SuperHeroUnitTests.Controllers
{
    public class SuperHeroControllerTests
    {
        private readonly SuperHeroController _superHeroController;
        private Mock<ISuperHeroService> _superHeroServiceMock = new();

        public SuperHeroControllerTests()
        {
            _superHeroController = new SuperHeroController(_superHeroServiceMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenSuperHeroesExists()
        {
            // Arrange
            List<SuperHeroResponse> superHeroes = new();

            superHeroes.Add(new SuperHeroResponse
            {
                Id = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938
            });

            _superHeroServiceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(superHeroes);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetAll();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoSuperHeroesExists()
        {
            // Arrange
            List<SuperHeroResponse> superHeroes = new();

            _superHeroServiceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(superHeroes);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetAll();

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            _superHeroServiceMock
                 .Setup(x => x.GetAll())
                 .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetAll();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _superHeroServiceMock
                 .Setup(x => x.GetAll())
                 .ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetAll();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int superHeroId = 1;

            SuperHeroResponse SuperHero = new()
            {
                Id = superHeroId,
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            _superHeroServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(SuperHero);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetById(superHeroId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            int superHeroId = 1;

            _superHeroServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetById(superHeroId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _superHeroServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.GetById(1);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenSuperHeroIsSuccessfullyCreated()
        {
            // Arrange
            SuperHeroRequest newSuperHero = new()
            {
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new()
            {
                Id = superHeroId,
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            _superHeroServiceMock
                .Setup(x => x.Create(It.IsAny<SuperHeroRequest>()))
                .ReturnsAsync(superHeroResponse);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Create(newSuperHero);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            SuperHeroRequest newSuperHero = new()
            {
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            _superHeroServiceMock
                .Setup(x => x.Create(It.IsAny<SuperHeroRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Create(newSuperHero);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenSuperHeroIsSuccessfullyUpdated()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new()
            {
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new()
            {
                Id = superHeroId,
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            _superHeroServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<SuperHeroRequest>()))
                .ReturnsAsync(superHeroResponse);


            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Update(1, updateSuperHero);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new()
            {
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            int superHeroId = 1;

            _superHeroServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<SuperHeroRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Update(superHeroId, updateSuperHero);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new()
            {
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            int superHeroId = 1;

            _superHeroServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<SuperHeroRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Update(superHeroId, updateSuperHero);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenSuperHeroIsDeleted()
        {
            // Arrange
            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new()
            {
                Id = superHeroId,
                FirstName = "Clark",
                LastName = "Kent",
                Name = "Superman",
                DebutYear = 1938,
                Place = "Metropolis"
            };

            _superHeroServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(superHeroResponse);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Delete(superHeroId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            int superHeroId = 1;
            _superHeroServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Delete(superHeroId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int superHeroId = 1;

            _superHeroServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _superHeroController.Delete(superHeroId);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
