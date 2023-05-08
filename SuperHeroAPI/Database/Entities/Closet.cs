using SuperHeroAPI.DTOs.DTOInterfaces;

namespace SuperHeroAPI.Database.Entities
{
    public class Closet : IMapEntityToResponse<ClosetResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Apparel> Apparels { get; set; } = new();

        public ClosetResponse ToResponse()
        {
            ClosetResponse response = new ClosetResponse
            {
                Id = Id,
                Name = Name,
                Description = Description
            };

            if (Apparels != null)
            {
                response.Apparels = Apparels.Select(a => a.ToResponse()).ToList();
            }

            return response;
        }
    }
}
