namespace SuperHeroAPI.DTOs
{
    public class TeamResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamSuperHeroResponse> Members { get; set; } = new();
    }

    public class TeamSuperHeroResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public short DebutYear { get; set; } = 0;
    }
}
