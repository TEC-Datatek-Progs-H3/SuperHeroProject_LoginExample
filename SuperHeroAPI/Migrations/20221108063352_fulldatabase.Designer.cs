// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperHeroAPI.Database;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    [DbContext(typeof(SuperHeroDbContext))]
    [Migration("20221108063352_fulldatabase")]
    partial class fulldatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SuperHeroAPI.Database.Entities.SuperHero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<short>("DebutYear")
                        .HasColumnType("smallint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("SuperHero");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DebutYear = (short)1938,
                            FirstName = "Clark",
                            LastName = "Kent",
                            Name = "Superman",
                            Place = "Metropolis",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 2,
                            DebutYear = (short)1963,
                            FirstName = "Tony",
                            LastName = "Stark",
                            Name = "Iron Man",
                            Place = "Malibu",
                            TeamId = 2
                        });
                });

            modelBuilder.Entity("SuperHeroAPI.Database.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Team");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Justice League"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Avengers"
                        });
                });

            modelBuilder.Entity("SuperHeroAPI.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "albert@mail.dk",
                            Password = "Test1234",
                            Role = 0,
                            Username = "Albert"
                        },
                        new
                        {
                            Id = 2,
                            Email = "benny@mail.dk",
                            Password = "Test1234",
                            Role = 1,
                            Username = "Benny"
                        });
                });

            modelBuilder.Entity("SuperHeroAPI.Database.Entities.SuperHero", b =>
                {
                    b.HasOne("SuperHeroAPI.Database.Entities.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SuperHeroAPI.Database.Entities.Team", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
