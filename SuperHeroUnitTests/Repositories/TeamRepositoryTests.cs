namespace TeamUnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        private readonly DbContextOptions<SuperHeroDbContext> _options;
        private readonly SuperHeroDbContext _context;
        private readonly TeamRepository _teamRepository;

        public TeamRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<SuperHeroDbContext>()
                .UseInMemoryDatabase(databaseName: "TeamRepository")
                .Options;

            _context = new(_options);

            _teamRepository = new(_context);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTeams_WhenTeamsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Team.Add(new()
            {
                Id = 1,
                Name = "Justice League"
            });

            _context.Team.Add(new()
            {
                Id = 2,
                Name = "Iron Man"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _teamRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Team>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfTeams_WhenNoTeamsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _teamRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Team>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetById_ShouldReturnTeam_WhenTeamExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int teamId = 1;

            _context.Team.Add(new()
            {
                Id = teamId,
                Name = "Justice League"
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _teamRepository.GetById(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Team>(result);
            Assert.Equal(teamId, result.Id);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _teamRepository.GetById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldAddNewIdToTeam_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Team superHero = new()
            {
                Name = "Justice League"
            };

            // Act
            var result = await _teamRepository.Create(superHero);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Team>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void Create_ShouldFailToAddNewTeam_WhenTeamIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Team superHero = new()
            {
                Id = 1,
                Name = "Justice League"
            };

            await _teamRepository.Create(superHero);

            // Act
            async Task action() => await _teamRepository.Create(superHero);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnTeam_WhenTeamExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;
            Team newTeam = new()
            {
                Id = teamId,
                Name = "Justice League"
            };
            _context.Team.Add(newTeam);
            await _context.SaveChangesAsync();
            Team updateTeam = new()
            {
                Id = teamId,
                Name = "new Justice League"
            };

            // Act
            var result = await _teamRepository.Update(teamId, updateTeam);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Team>(result);
            Assert.Equal(teamId, result.Id);
            Assert.Equal(updateTeam.Name, result.Name);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;

            Team updateTeam = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            // Act
            var result = await _teamRepository.Update(teamId, updateTeam);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedTeam_WhenTeamIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int teamId = 1;
            Team newTeam = new()
            {
                Id = teamId,
                Name = "new Justice League"
            };

            _context.Team.Add(newTeam);
            await _context.SaveChangesAsync();

            // Act
            var result = await _teamRepository.Delete(teamId);
            var superHero = await _teamRepository.GetById(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Team>(result);
            Assert.Equal(teamId, result.Id);

            Assert.Null(superHero);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _teamRepository.Delete(1);

            // Assert
            Assert.Null(result);
        }
    }
}
