namespace SuperHeroAPI.Database.EntityInterfaces;

public interface ICreate<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity newEntity);
}
