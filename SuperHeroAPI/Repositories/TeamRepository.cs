namespace SuperHeroAPI.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAll();
        Task<Team> GetById(int teamId);
        Task<Team> Create(Team newTeam);
        Task<Team> Update(int teamId, Team updateTeam);
        Task<Team> Delete(int teamId);
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly SuperHeroDbContext _context;

        public TeamRepository(SuperHeroDbContext SuperHeroDbContext)
        {
            _context = SuperHeroDbContext;
        }

        public async Task<List<Team>> GetAll()
        {
            return await _context.Team
                .Include(t => t.Members)
                .ToListAsync();
        }

        public async Task<Team> GetById(int id)
        {
            return await _context.Team
                .Include(t => t.Members)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Team> Create(Team newTeam)
        {
            _context.Team.Add(newTeam);
            await _context.SaveChangesAsync();
            return newTeam;
        }

        public async Task<Team> Update(int teamId, Team character)
        {
            Team updateTeam = await _context.Team
                .Include(t => t.Members)
                .FirstOrDefaultAsync(c => c.Id == teamId);

            if (updateTeam != null)
            {
                updateTeam.Name = character.Name;

                await _context.SaveChangesAsync();
            }

            return updateTeam;
        }

        public async Task<Team> Delete(int teamId)
        {
            Team deleteTeam = await _context.Team
                .Include(t => t.Members)
                .FirstOrDefaultAsync(c => c.Id == teamId);

            if (deleteTeam != null)
            {
                _context.Team.Remove(deleteTeam);
                await _context.SaveChangesAsync();
            }

            return deleteTeam;
        }
    }
}
