

namespace SuperHeroAPI.Repositories
{

    public interface IClosetRepository :  ICreate<Closet>, IGetAll<Closet>, IFindById<Closet>, IUpdateById<Closet>, IDeleteById<Closet>
    {
        Task<List<Closet>> GetThreeRandomClosets();
        Task<List<Apparel>> GetAllApparelsAsync();
    }

    public class ClosetsRepository : IClosetRepository, IClosetsRepository
    {
        private readonly SuperHeroDbContext _context;

        public ClosetsRepository(SuperHeroDbContext context)
        {
            _context = context;
        }
        public Task<Closet> CreateAsync(Closet newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Closet> DeleteByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<Closet> FindById(int entityId)
        {
            throw new NotImplementedException();
        }



        public async Task<List<Closet>> GetAllAsync()
        {
            var c = await _context.Closet.Include(c => c.Apparels).ToListAsync();
            return c;
        }

        public async Task<List<Apparel>> GetAllApparelsAsync()
        {
            var a = await _context.Apparel.ToListAsync();
            return a;
        }
        public Task<List<Closet>> GetThreeRandomClosets()
        {
            throw new NotImplementedException();
        }

        public Task<Closet> UpdateByIdAsync(int entityId, Closet updateEntity)
        {
            throw new NotImplementedException();
        }


    }
}
