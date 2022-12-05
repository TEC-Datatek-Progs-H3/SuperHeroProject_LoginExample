namespace SuperHeroUnitTests.Services
{
    public class SuperHeroServiceTests
    {
        private readonly SuperHeroService _superHeroService;
        private readonly Mock<ISuperHeroRepository> _superHeroRepositoryMock = new();

        public SuperHeroServiceTests()
        {
            _superHeroService = new(_superHeroRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfSuperHeroResponses_WhenSuperHerosExists()
        {
            // Arrange
            List<SuperHero> superHeros = new();

            superHeros.Add(new()
            {
                Id = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            });

            superHeros.Add(new()
            {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Malibu",
                DebutYear = 1963,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            });

            _superHeroRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(superHeros);

            // Act
            var result = await _superHeroService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<SuperHeroResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfSuperHeroResponses_WhenNoSuperHerosExists()
        {
            // Arrange
            List<SuperHero> superHeros = new();

            _superHeroRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(superHeros);

            // Act
            var result = await _superHeroService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<SuperHeroResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnSuperHeroResponse_WhenSuperHeroExists()
        {
            // Arrange
            int superHeroId = 1;

            SuperHero superHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            };

            _superHeroRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(superHero);

            // Act
            var result = await _superHeroService.GetById(superHeroId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHeroResponse>(result);
            Assert.Equal(superHero.Id, result.Id);
            Assert.Equal(superHero.FirstName, result.FirstName);
            Assert.Equal(superHero.LastName, result.LastName);
            Assert.Equal(superHero.Name, result.Name);
            Assert.Equal(superHero.DebutYear, result.DebutYear);
            Assert.Equal(superHero.Place, result.Place);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            int superHeroId = 1;

            _superHeroRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _superHeroService.GetById(superHeroId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnSuperHeroResponse_WhenCreateIsSuccess()
        {
            // Arrange
            SuperHeroRequest newSuperHero = new()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = 1
            };
            int superHeroId = 1;
            SuperHero superHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            };

            _superHeroRepositoryMock
                .Setup(x => x.Create(It.IsAny<SuperHero>()))
                .ReturnsAsync(superHero);

            // Act
            var result = await _superHeroService.Create(newSuperHero);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHeroResponse>(result);
            Assert.Equal(superHero.Id, result.Id);
            Assert.Equal(superHero.Name, result.Name);
            Assert.Equal(superHero.FirstName, result.FirstName);
            Assert.Equal(superHero.LastName, result.LastName);
            Assert.Equal(superHero.Place, result.Place);
            Assert.Equal(superHero.DebutYear, result.DebutYear);
        }

        [Fact]
        public async void Create_ShouldReturnNull_WhenRepositoryReturnsNull()
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

            _superHeroRepositoryMock
                .Setup(x => x.Create(It.IsAny<SuperHero>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _superHeroService.Create(newSuperHero);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldReturnSuperHeroResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test if anything actually changed on the DB,
            // we only test that the returned values match the submitted values
            // Arrange
            SuperHeroRequest superHeroRequest = new()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = 1
            };
            int superHeroId = 1;
            SuperHero superHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            };
            _superHeroRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<SuperHero>()))
                .ReturnsAsync(superHero);

            // Act
            var result = await _superHeroService.Update(superHeroId, superHeroRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHeroResponse>(result);
            Assert.Equal(superHero.Id, result.Id);
            Assert.Equal(superHeroRequest.Name, result.Name);
            Assert.Equal(superHeroRequest.FirstName, result.FirstName);
            Assert.Equal(superHeroRequest.LastName, result.LastName);
            Assert.Equal(superHeroRequest.Place, result.Place);
            Assert.Equal(superHeroRequest.DebutYear, result.DebutYear);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            SuperHeroRequest superHeroRequest = new()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = 1
            };

            int superHeroId = 1;

            _superHeroRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<SuperHero>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _superHeroService.Update(superHeroId, superHeroRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnSuperHeroResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int superHeroId = 1;

            SuperHero superHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                Team = new()
                {
                    Id = 1,
                    Name = "Justice League"
                }
            };

            _superHeroRepositoryMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(superHero);

            // Act
            var result = await _superHeroService.Delete(superHeroId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHeroResponse>(result);
            Assert.Equal(superHero.Id, result.Id);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenSuperHeroDoesNotExists()
        {
            // Arrange
            int superHeroId = 1;

            _superHeroRepositoryMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _superHeroService.Delete(superHeroId);

            // Assert
            Assert.Null(result);
        }
    }
}
