using Microsoft.EntityFrameworkCore.Migrations;

namespace Servidor.Migrations
{
    public partial class MigrationSqlite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Museum",
                columns: table => new
                {
                    Id_Museum = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Museum_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Theme = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museum", x => x.Id_Museum);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id_Article = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Article_Name = table.Column<string>(type: "TEXT", nullable: true),
                    isDamaged = table.Column<bool>(type: "INTEGER", nullable: false),
                    Id_RefMuseum = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id_Article);
                    table.ForeignKey(
                        name: "FK_Article_Museum_Id_RefMuseum",
                        column: x => x.Id_RefMuseum,
                        principalTable: "Museum",
                        principalColumn: "Id_Museum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_Id_RefMuseum",
                table: "Article",
                column: "Id_RefMuseum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Museum");
        }
    }
}
