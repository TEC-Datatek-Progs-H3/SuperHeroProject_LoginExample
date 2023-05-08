namespace SuperHeroAPI.Services
{
    public interface ISuperHeroService
    {
        Task<List<SuperHeroResponse>> GetAll();
        Task<SuperHeroResponse> GetById(int superHeroId);
        Task<SuperHeroResponse> Create(SuperHeroRequest newSuperHero);
        Task<SuperHeroResponse> Update(int superHeroId, SuperHeroRequest updateSuperHero);
        Task<SuperHeroResponse> Delete(int superHeroId);
    }

    public class SuperHeroService : ISuperHeroService
    {
        private readonly ISuperHeroRepository _superHeroRepository;

        public SuperHeroService(ISuperHeroRepository superHeroRepository)
        {
            _superHeroRepository = superHeroRepository;
        }

        public async Task<List<SuperHeroResponse>> GetAll()
        {
            List<SuperHero> superHeroes = await _superHeroRepository.GetAll();

            if (superHeroes != null)
            {
                return superHeroes.Select(superHero => MapSuperHeroToSuperHeroResponse(superHero)).ToList();
            }

            return null;
        }

        public async Task<SuperHeroResponse> GetById(int superHeroId)
        {
            SuperHero superHero = await _superHeroRepository.GetById(superHeroId);

            if (superHero != null)
            {
                return MapSuperHeroToSuperHeroResponse(superHero);
            }

            return null;
        }

        public async Task<SuperHeroResponse> Create(SuperHeroRequest superHeroRequest)
        {
            SuperHero superHero = MapSuperHeroRequestToSuperHero(superHeroRequest);

            SuperHero insertedSuperHero = await _superHeroRepository.Create(superHero);

            if (insertedSuperHero != null)
            {
                return MapSuperHeroToSuperHeroResponse(insertedSuperHero);
            }

            return null;
        }

        public async Task<SuperHeroResponse> Update(int superHeroId, SuperHeroRequest superHeroRequest)
        {
            SuperHero superHero = MapSuperHeroRequestToSuperHero(superHeroRequest);

            SuperHero updatedSuperHero = await _superHeroRepository.Update(superHeroId, superHero);

            if (updatedSuperHero != null)
            {
                return MapSuperHeroToSuperHeroResponse(updatedSuperHero);
            }

            return null;
        }

        public async Task<SuperHeroResponse> Delete(int superHeroId)
        {
            SuperHero deletedSuperHero = await _superHeroRepository.Delete(superHeroId);

            if (deletedSuperHero != null)
            {
                return MapSuperHeroToSuperHeroResponse(deletedSuperHero);
            }

            return null;
        }

        private static SuperHero MapSuperHeroRequestToSuperHero(SuperHeroRequest superHero) => new SuperHero
        {
            FirstName = superHero.FirstName,
            LastName = superHero.LastName,
            Name = superHero.Name,
            Place = superHero.Place,
            DebutYear = superHero.DebutYear,
            TeamId = superHero.TeamId
        };


        private static SuperHeroResponse MapSuperHeroToSuperHeroResponse(SuperHero superHero) => new SuperHeroResponse
        {
            Id = superHero.Id,
            FirstName = superHero.FirstName,
            LastName = superHero.LastName,
            Name = superHero.Name,
            DebutYear = superHero.DebutYear,
            Place = superHero.Place,
            Team = new SuperHeroTeamResponse
            {
                Id = superHero.Team.Id,
                Name = superHero.Team.Name
            }
        };


        //private static SuperHeroResponse MapSuperHeroToSuperHeroResponse(SuperHero superHero)
        //{
        //    SuperHeroResponse response = new SuperHeroResponse
        //    {
        //        Id = superHero.Id,
        //        FirstName = superHero.FirstName,
        //        LastName = superHero.LastName,
        //        Name = superHero.Name,
        //        DebutYear = superHero.DebutYear,
        //        Place = superHero.Place
        //    };

        //    if (superHero.Team != null)
        //    {
        //        response.Team.Id = superHero.Team.Id;
        //        response.Team.Name = superHero.Team.Name;
        //    };
        //    return response;

        //}
    }
}
