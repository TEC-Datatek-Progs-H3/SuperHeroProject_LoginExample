namespace SuperHeroAPI.DTOs
{



    public class ClosetRequest : IMapRequestToEntity<Closet>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Closet ToEntity()
        {
            return new Closet
            {
                Name = Name,
                Description = Description
            };
        }
    }

    public class ClosetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<ApparelResponse> Apparels { get; set; } = new();
    }
}
