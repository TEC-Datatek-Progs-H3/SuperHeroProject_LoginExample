namespace SuperHeroAPI.Database
{
    public class SuperHeroDbContext : DbContext
    {
        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHero { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Apparel> Apparel { get; set; }
        public DbSet<Closet> Closet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Closet>().HasData(
                new Closet
                {
                    Id = 1,
                    Name = "Skab 1",
                    Description = "Det store"
                },
                new Closet
                {
                    Id = 2,
                    Name = "Skab 2",
                    Description = "Det lille"
                });
            modelBuilder.Entity<Apparel>().HasData(
                new Apparel
                {
                    Id = 1,
                    Name = "Sorte jeans",
                    Description = "De sorte wrangler",
                    ClosetId = 1
                },
                new Apparel
                {
                    Id = 2,
                    Name = "Blå jeans",
                    Description = "De blå lewis",
                    ClosetId = 2
                });

            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 1,
                  Username = "Albert",
                  Email = "albert@mail.dk",
                  Password = "Test1234",
                  Role = Role.Admin
              },
              new User
              {
                  Id = 2,
                  Username = "Benny",
                  Email = "benny@mail.dk",
                  Password = "Test1234",
                  Role = Role.User
              });

            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Justice League"
                },
                new Team
                {
                    Id = 2,
                    Name = "Avengers"
                });


            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero
                {
                    Id = 1,
                    Name = "Superman",
                    FirstName = "Clark",
                    LastName = "Kent",
                    Place = "Metropolis",
                    DebutYear = 1938,
                    TeamId = 1
                },
                new SuperHero
                {
                    Id = 2,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Malibu",
                    DebutYear = 1963,
                    TeamId = 2
                });
        }
    }
}
