namespace SuperHeroAPI.DTOs.DTOInterfaces;

public interface IMapEntityToResponse<TResponse> where TResponse : class
{
    TResponse ToResponse();
}