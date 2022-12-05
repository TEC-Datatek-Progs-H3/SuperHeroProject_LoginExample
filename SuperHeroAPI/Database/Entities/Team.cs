namespace SuperHeroAPI.Database.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Name { get; set; }

        public List<SuperHero> Members { get; set; } = new();
    }
}
