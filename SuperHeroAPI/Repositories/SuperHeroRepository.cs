namespace SuperHeroAPI.Repositories
{
    public interface ISuperHeroRepository
    {
        Task<List<SuperHero>> GetAll();
        Task<SuperHero> GetById(int superHeroId);
        Task<SuperHero> Create(SuperHero newSuperHero);
        Task<SuperHero> Update(int superHeroId, SuperHero updateSuperHero);
        Task<SuperHero> Delete(int superHeroId);
    }

    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly SuperHeroDbContext _context;

        public SuperHeroRepository(SuperHeroDbContext context)
        {
            _context = context;
        }

        public async Task<List<SuperHero>> GetAll()
        {
            return await _context.SuperHero.Include(s => s.Team).ToListAsync();
        }

        public async Task<SuperHero> GetById(int superHeroId)
        {
            return await _context.SuperHero
                .Include(s => s.Team)
                .FirstOrDefaultAsync(s => s.Id == superHeroId);
        }

        public async Task<SuperHero> Create(SuperHero newSuperHero)
        {
            _context.SuperHero.Add(newSuperHero);
            await _context.SaveChangesAsync();
            // get the SuperHero AND the team
            newSuperHero = await GetById(newSuperHero.Id);
            return newSuperHero;
        }

        public async Task<SuperHero> Update(int superHeroId, SuperHero updateSuperHero)
        {
            SuperHero superHero = await GetById(superHeroId);
            if (superHero != null)
            {
                superHero.Name = updateSuperHero.Name;
                superHero.FirstName = updateSuperHero.FirstName;
                superHero.LastName = updateSuperHero.LastName;
                superHero.Place = updateSuperHero.Place;
                superHero.DebutYear = updateSuperHero.DebutYear;
                superHero.TeamId = updateSuperHero.TeamId;

                await _context.SaveChangesAsync();
                // incase the team was changed, get the hero and the correct team
                superHero = await GetById(superHero.Id);
            }
            return superHero;
        }

        public async Task<SuperHero> Delete(int superHeroId)
        {
            SuperHero superHero = await GetById(superHeroId);
            if (superHero != null)
            {
                _context.SuperHero.Remove(superHero);
                await _context.SaveChangesAsync();
            }
            return superHero;
        }
    }
}
