using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    public partial class apparelAndClosetsTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Closet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Closet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apparel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apparel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apparel_Closet_ClosetId",
                        column: x => x.ClosetId,
                        principalTable: "Closet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Closet",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Det store", "Skab 1" });

            migrationBuilder.InsertData(
                table: "Closet",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Det lille", "Skab 2" });

            migrationBuilder.InsertData(
                table: "Apparel",
                columns: new[] { "Id", "ClosetId", "Description", "Name" },
                values: new object[] { 1, 1, "De sorte wrangler", "Sorte jeans" });

            migrationBuilder.InsertData(
                table: "Apparel",
                columns: new[] { "Id", "ClosetId", "Description", "Name" },
                values: new object[] { 2, 2, "De blå lewis", "Blå jeans" });

            migrationBuilder.CreateIndex(
                name: "IX_Apparel_ClosetId",
                table: "Apparel",
                column: "ClosetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apparel");

            migrationBuilder.DropTable(
                name: "Closet");
        }
    }
}
