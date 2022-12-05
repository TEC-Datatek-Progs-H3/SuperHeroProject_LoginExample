namespace TeamUnitTests.Services
{
    public class TeamServiceTests
    {
        private readonly TeamService _teamService;
        private readonly Mock<ITeamRepository> _teamRepositoryMock = new();

        public TeamServiceTests()
        {
            _teamService = new(_teamRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTeamResponses_WhenTeamsExists()
        {
            // Arrange
            List<Team> teams = new();

            teams.Add(new()
            {
                Id = 1,
                Name = "Justice League"
            });

            teams.Add(new()
            {
                Id = 2,
                Name = "Avengers"
            });

            _teamRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(teams);

            // Act
            var result = await _teamService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<TeamResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfTeamResponses_WhenNoTeamsExists()
        {
            // Arrange
            List<Team> teams = new();

            _teamRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(teams);

            // Act
            var result = await _teamService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<TeamResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnTeamResponse_WhenTeamExists()
        {
            // Arrange
            int teamId = 1;

            Team team = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(team);

            // Act
            var result = await _teamService.GetById(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TeamResponse>(result);
            Assert.Equal(team.Id, result.Id);
            Assert.Equal(team.Name, result.Name);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenTeamDoesNotExists()
        {
            // Arrange
            int teamId = 1;

            _teamRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _teamService.GetById(teamId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnTeamResponse_WhenCreateIsSuccess()
        {
            // Arrange
            TeamRequest newTeam = new()
            {
                Name = "Justice League"
            };
            int teamId = 1;
            Team team = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamRepositoryMock
                .Setup(x => x.Create(It.IsAny<Team>()))
                .ReturnsAsync(team);

            // Act
            var result = await _teamService.Create(newTeam);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TeamResponse>(result);
            Assert.Equal(team.Id, result.Id);
            Assert.Equal(team.Name, result.Name);
        }

        [Fact]
        public async void Create_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            TeamRequest newTeam = new()
            {
                Name = "Justice League"
            };

            _teamRepositoryMock
                .Setup(x => x.Create(It.IsAny<Team>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _teamService.Create(newTeam);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldReturnTeamResponse_WhenUpdateIsSuccess()
        {
            // NOTICE, we do not test if anything actually changed on the DB,
            // we only test that the returned values match the submitted values
            // Arrange
            TeamRequest teamRequest = new()
            {
                Name = "Justice League"
            };
            int teamId = 1;
            Team team = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Team>()))
                .ReturnsAsync(team);

            // Act
            var result = await _teamService.Update(teamId, teamRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TeamResponse>(result);
            Assert.Equal(team.Id, result.Id);
            Assert.Equal(teamRequest.Name, result.Name);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenTeamDoesNotExists()
        {
            // Arrange
            TeamRequest teamRequest = new()
            {
                Name = "Justice League"
            };

            int teamId = 1;

            _teamRepositoryMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Team>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _teamService.Update(teamId, teamRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTeamResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int teamId = 1;

            Team team = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamRepositoryMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(team);

            // Act
            var result = await _teamService.Delete(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TeamResponse>(result);
            Assert.Equal(team.Id, result.Id);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenTeamDoesNotExists()
        {
            // Arrange
            int teamId = 1;

            _teamRepositoryMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _teamService.Delete(teamId);

            // Assert
            Assert.Null(result);
        }
    }
}
