namespace SuperHeroUnitTests.Repositories
{
    public class SuperHeroRepositoryTests
    {
        private readonly DbContextOptions<SuperHeroDbContext> _options;
        private readonly SuperHeroDbContext _context;
        private readonly SuperHeroRepository _superHeroRepository;

        public SuperHeroRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<SuperHeroDbContext>()
                .UseInMemoryDatabase(databaseName: "SuperHeroRepository")
                .Options;

            _context = new(_options);

            _superHeroRepository = new(_context);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfSuperHeroes_WhenSuperHeroesExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;
            _context.Team.Add(new Team { Id = teamId, Name = "Justice League" });
            await _context.SaveChangesAsync();

            _context.SuperHero.Add(new()
            {
                Id = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = teamId
            });

            _context.SuperHero.Add(new()
            {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Malibu",
                DebutYear = 1963,
                TeamId = teamId
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _superHeroRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SuperHero>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfSuperHeroes_WhenNoSuperHeroesExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _superHeroRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SuperHero>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetById_ShouldReturnSuperHero_WhenSuperHeroExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;
            _context.Team.Add(new Team { Id = teamId, Name = "Justice League" });
            await _context.SaveChangesAsync();

            int superHeroId = 1;

            _context.SuperHero.Add(new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = teamId
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _superHeroRepository.GetById(superHeroId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHero>(result);
            Assert.Equal(superHeroId, result.Id);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _superHeroRepository.GetById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToSuperHero_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // add a team to database...
            int teamId = 1;
            _context.Team.Add(new Team { Id = teamId, Name = "Justice League" });
            await _context.SaveChangesAsync();

            int expectedNewId = 1;

            SuperHero superHero = new()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = teamId
            };

            // Act
            var result = await _superHeroRepository.Create(superHero);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHero>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewSuperHero_WhenSuperHeroIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            SuperHero superHero = new()
            {
                Id = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938
            };

            await _superHeroRepository.Create(superHero);

            // Act
            async Task action() => await _superHeroRepository.Create(superHero);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnSuperHero_WhenSuperHeroExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;
            _context.Team.Add(new Team { Id = teamId, Name = "Justice League" });
            await _context.SaveChangesAsync();

            int superHeroId = 1;
            SuperHero newSuperHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938,
                TeamId = teamId
            };
            _context.SuperHero.Add(newSuperHero);
            await _context.SaveChangesAsync();
            SuperHero updateSuperHero = new()
            {
                Id = superHeroId,
                Name = "new Superman",
                FirstName = "new Clark",
                LastName = "new Kent",
                Place = "new Metropolis",
                DebutYear = 1999,
                TeamId = teamId
            };

            // Act
            var result = await _superHeroRepository.Update(superHeroId, updateSuperHero);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHero>(result);
            Assert.Equal(superHeroId, result.Id);
            Assert.Equal(updateSuperHero.Name, result.Name);
            Assert.Equal(updateSuperHero.FirstName, result.FirstName);
            Assert.Equal(updateSuperHero.LastName, result.LastName);
            Assert.Equal(updateSuperHero.Place, result.Place);
            Assert.Equal(updateSuperHero.DebutYear, result.DebutYear);
            Assert.Equal(updateSuperHero.TeamId, result.TeamId);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int superHeroId = 1;

            SuperHero updateSuperHero = new()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                DebutYear = 1938
            };

            // Act
            var result = await _superHeroRepository.Update(superHeroId, updateSuperHero);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedSuperHero_WhenSuperHeroIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            // add a team to database...
            int teamId = 1;
            _context.Team.Add(new Team { Id = teamId, Name = "Justice League" });
            await _context.SaveChangesAsync();

            int superHeroId = 1;
            SuperHero newSuperHero = new()
            {
                Id = superHeroId,
                Name = "new Superman",
                FirstName = "new Clark",
                LastName = "new Kent",
                Place = "new Metropolis",
                DebutYear = 1999,
                TeamId = teamId
            };

            _context.SuperHero.Add(newSuperHero);
            await _context.SaveChangesAsync();

            // Act
            var result = await _superHeroRepository.Delete(superHeroId);
            var superHero = await _superHeroRepository.GetById(superHeroId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SuperHero>(result);
            Assert.Equal(superHeroId, result.Id);

            Assert.Null(superHero);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _superHeroRepository.Delete(1);

            // Assert
            Assert.Null(result);
        }
    }
}
