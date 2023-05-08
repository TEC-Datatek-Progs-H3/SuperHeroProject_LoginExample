namespace SuperHeroAPI.DTOs.DTOInterfaces;

public interface IMapRequestToEntity<TEntity> where TEntity : class
{
    TEntity ToEntity();
}