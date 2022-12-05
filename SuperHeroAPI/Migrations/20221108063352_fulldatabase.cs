using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class fulldatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperHero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    DebutYear = table.Column<short>(type: "smallint", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperHero_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Justice League" },
                    { 2, "Avengers" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "albert@mail.dk", "Test1234", 0, "Albert" },
                    { 2, "benny@mail.dk", "Test1234", 1, "Benny" }
                });

            migrationBuilder.InsertData(
                table: "SuperHero",
                columns: new[] { "Id", "DebutYear", "FirstName", "LastName", "Name", "Place", "TeamId" },
                values: new object[] { 1, (short)1938, "Clark", "Kent", "Superman", "Metropolis", 1 });

            migrationBuilder.InsertData(
                table: "SuperHero",
                columns: new[] { "Id", "DebutYear", "FirstName", "LastName", "Name", "Place", "TeamId" },
                values: new object[] { 2, (short)1963, "Tony", "Stark", "Iron Man", "Malibu", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_SuperHero_TeamId",
                table: "SuperHero",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperHero");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
