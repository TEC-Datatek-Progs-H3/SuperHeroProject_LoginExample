namespace SuperHeroAPI.Repositories
{
    public interface IClosetsRepository
    {
        Task<Closet> CreateAsync(Closet newEntity);
        Task<Closet> DeleteByIdAsync(int entityId);
        Task<Closet> FindById(int entityId);
        Task<List<Apparel>> GetAllApparelsAsync();
        Task<List<Closet>> GetAllAsync();
        Task<List<Closet>> GetThreeRandomClosets();
        Task<Closet> UpdateByIdAsync(int entityId, Closet updateEntity);
    }
}