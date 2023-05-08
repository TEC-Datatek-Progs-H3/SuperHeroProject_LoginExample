namespace SuperHeroAPI.DTOs
{
    public class ApparelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int ClosetId { get; set; }
        public string? ClosetName { get; set; }
    }
}
