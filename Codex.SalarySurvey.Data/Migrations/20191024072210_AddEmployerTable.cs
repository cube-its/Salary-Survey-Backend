using Microsoft.EntityFrameworkCore.Migrations;

namespace Codex.SalarySurvey.Data.Migrations
{
    public partial class AddEmployerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerID = table.Column<int>(nullable: true),
                    EmployerName = table.Column<string>(maxLength: 500, nullable: true),
                    EmployerActive = table.Column<bool>(nullable: true),
                    EmployerOriginalNameHeb = table.Column<string>(maxLength: 500, nullable: true),
                    Popularity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employers_ID",
                table: "Employers",
                column: "EmployerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employers");
        }
    }
}
