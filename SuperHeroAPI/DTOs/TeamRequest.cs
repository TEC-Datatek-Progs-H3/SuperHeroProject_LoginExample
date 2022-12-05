namespace SuperHeroAPI.DTOs
{
    public class TeamRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Name must not contain more than 32 chars")]
        public string Name { get; set; } = string.Empty;
    }
}
