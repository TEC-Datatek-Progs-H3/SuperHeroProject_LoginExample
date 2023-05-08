namespace SuperHeroAPI.Database.EntityInterfaces;

public interface IGetAll<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
}