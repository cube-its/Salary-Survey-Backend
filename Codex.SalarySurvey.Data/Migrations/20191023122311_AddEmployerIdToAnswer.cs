using Microsoft.EntityFrameworkCore.Migrations;

namespace Codex.SalarySurvey.Data.Migrations
{
    public partial class AddEmployerIdToAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "QuestionAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "QuestionAnswers");
        }
    }
}
