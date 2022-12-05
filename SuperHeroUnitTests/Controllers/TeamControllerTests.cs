namespace TeamUnitTests.Controllers
{
    public class TeamControllerTests
    {
        private readonly TeamController _teamController;
        private Mock<ITeamService> _teamServiceMock = new();

        public TeamControllerTests()
        {
            _teamController = new TeamController(_teamServiceMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenTeamsExists()
        {
            // Arrange
            List<TeamResponse> teams = new();

            teams.Add(new TeamResponse
            {
                Id = 1,
                Name = "Justice League"
            });

            teams.Add(new TeamResponse
            {
                Id = 2,
                Name = "Avengers"
            });

            _teamServiceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(teams);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetAll();

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoTeamsExists()
        {
            // Arrange
            List<TeamResponse> teams = new();

            _teamServiceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(teams);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetAll();

            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            _teamServiceMock
                 .Setup(x => x.GetAll())
                 .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetAll();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _teamServiceMock
                 .Setup(x => x.GetAll())
                 .ReturnsAsync(() => throw new Exception("This is an Exception"));

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetAll();

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int teamId = 1;

            TeamResponse Team = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(Team);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetById(teamId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenTeamDoesNotExists()
        {
            // Arrange
            int teamId = 1;

            _teamServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetById(teamId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _teamServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _teamController.GetById(1);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenTeamIsSuccessfullyCreated()
        {
            // Arrange
            TeamRequest newTeam = new()
            {
                Name = "Justice League"
            };

            int teamId = 1;

            TeamResponse teamResponse = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamServiceMock
                .Setup(x => x.Create(It.IsAny<TeamRequest>()))
                .ReturnsAsync(teamResponse);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Create(newTeam);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            TeamRequest newTeam = new()
            {
                Name = "Justice League"
            };

            _teamServiceMock
                .Setup(x => x.Create(It.IsAny<TeamRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Create(newTeam);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenTeamIsSuccessfullyUpdated()
        {
            // Arrange
            TeamRequest updateTeam = new()
            {
                Name = "Justice League"
            };

            int teamId = 1;

            TeamResponse teamResponse = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TeamRequest>()))
                .ReturnsAsync(teamResponse);


            // Act
            var result = (IStatusCodeActionResult)await _teamController.Update(1, updateTeam);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTeamDoesNotExists()
        {
            // Arrange
            TeamRequest updateTeam = new()
            {
                Name = "Justice League"
            };

            int teamId = 1;

            _teamServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TeamRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Update(teamId, updateTeam);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            TeamRequest updateTeam = new()
            {
                Name = "Justice League"
            };

            int teamId = 1;

            _teamServiceMock
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<TeamRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Update(teamId, updateTeam);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenTeamIsDeleted()
        {
            // Arrange
            int teamId = 1;

            TeamResponse teamResponse = new()
            {
                Id = teamId,
                Name = "Justice League"
            };

            _teamServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(teamResponse);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Delete(teamId);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTeamDoesNotExists()
        {
            // Arrange
            int teamId = 1;
            _teamServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Delete(teamId);

            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int teamId = 1;

            _teamServiceMock
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _teamController.Delete(teamId);

            // Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
