using SuperHeroAPI.DTOs.DTOInterfaces;

namespace SuperHeroAPI.Database.Entities
{
    public class Apparel : IMapEntityToResponse<ApparelResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClosetId { get; set; }

        public Closet? Closet { get; set; }

        public ApparelResponse ToResponse()
        {
            ApparelResponse response = new ApparelResponse
            {
                Id = Id,
                Name = Name,
                Description = Description,
                ClosetId = ClosetId,
            };
            
            if (Closet != null)
                response.ClosetName = Closet.Name;
            else
                response.ClosetName = null;
            return response;
        }
    }
}
