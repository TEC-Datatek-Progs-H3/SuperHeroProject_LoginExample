namespace SuperHeroAPI.Database.EntityInterfaces;

public interface IUpdateById<TEntity> where TEntity : class
{
    Task<TEntity> UpdateByIdAsync(int entityId, TEntity updateEntity);
}