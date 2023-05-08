namespace SuperHeroAPI.Database.EntityInterfaces;

public interface IFindById<TEntity> where TEntity : class
{
    Task<TEntity> FindById(int entityId);
}
