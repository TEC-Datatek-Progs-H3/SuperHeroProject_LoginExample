namespace SuperHeroAPI.DTOs
{
    public class SuperHeroRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Name must not contain more than 32 chars")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "FirstName must not contain more than 32 chars")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "LastName must not contain more than 32 chars")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "Place must not contain more than 32 chars")]
        public string Place { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2500, ErrorMessage = "DebutYear must be between 1900 and 2500")]
        public short DebutYear { get; set; } = 1900;

        [Required(ErrorMessage = "Team is required")]
        [Range(1, int.MaxValue, ErrorMessage = "TeamId must not be 0")]
        public int TeamId { get; set; }
    }
}
