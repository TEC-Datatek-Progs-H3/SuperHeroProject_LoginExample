namespace SuperHeroAPI.Services
{
    public interface ITeamService
    {
        Task<List<TeamResponse>> GetAll();
        Task<TeamResponse> GetById(int id);
        Task<TeamResponse> Create(TeamRequest newTeam);
        Task<TeamResponse> Update(int teamId, TeamRequest updateTeam);
        Task<TeamResponse> Delete(int teamId);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<TeamResponse>> GetAll()
        {
            List<Team> teams = await _teamRepository.GetAll();

            if (teams != null)
            {
                return teams.Select(team => MapTeamToTeamResponse(team)).ToList();
            }
            return new List<TeamResponse>();
        }

        public async Task<TeamResponse> GetById(int teamId)
        {
            Team? team = await _teamRepository.GetById(teamId);

            if (team != null)
            {
                return MapTeamToTeamResponse(team);
            }

            return null;
        }

        public async Task<TeamResponse> Create(TeamRequest teamRequest)
        {
            Team team = MapTeamRequestToTeam(teamRequest);

            Team insertedTeam = await _teamRepository.Create(team);

            if (insertedTeam != null)
            {
                return MapTeamToTeamResponse(insertedTeam);
            }

            return null;
        }

        public async Task<TeamResponse> Update(int teamId, TeamRequest teamRequest)
        {
            Team team = MapTeamRequestToTeam(teamRequest);

            Team updatedTeam = await _teamRepository.Update(teamId, team);

            if (updatedTeam != null)
            {
                return MapTeamToTeamResponse(updatedTeam);
            }

            return null;
        }

        public async Task<TeamResponse> Delete(int teamId)
        {
            Team deletedTeam = await _teamRepository.Delete(teamId);

            if (deletedTeam != null)
            {
                return MapTeamToTeamResponse(deletedTeam);
            }

            return null;
        }

        private static Team MapTeamRequestToTeam(TeamRequest team)
        {
            return new Team
            {
                Name = team.Name
            };
        }

        private static TeamResponse MapTeamToTeamResponse(Team team)
        {
            return new TeamResponse
            {
                Id = team.Id,
                Name = team.Name,
                Members = team.Members.Select(character => new TeamSuperHeroResponse
                {
                    Id = character.Id,
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    Name = character.Name,
                    DebutYear = character.DebutYear,
                    Place = character.Place
                }).ToList()
            };
        }
    }
}
