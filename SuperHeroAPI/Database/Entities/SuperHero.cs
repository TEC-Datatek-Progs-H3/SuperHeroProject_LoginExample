namespace SuperHeroAPI.Database.Entities
{
    public class SuperHero
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string Place { get; set; } = string.Empty;

        [Column(TypeName = "smallint")]
        public short DebutYear { get; set; } = 0;

        [ForeignKey("Team.Id")]
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}
