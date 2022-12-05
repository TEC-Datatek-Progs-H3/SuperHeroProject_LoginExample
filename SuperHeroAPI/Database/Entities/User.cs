﻿namespace SuperHeroAPI.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
