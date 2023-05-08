namespace SuperHeroAPI.Database.EntityInterfaces;

public interface IDeleteById<TEntity> where TEntity : class
{
    Task<TEntity> DeleteByIdAsync(int entityId);
}
