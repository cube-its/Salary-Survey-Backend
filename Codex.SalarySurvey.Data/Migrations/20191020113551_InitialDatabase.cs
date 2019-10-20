using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codex.SalarySurvey.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    Placeholder = table.Column<string>(nullable: true),
                    ToolTip = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    RegEx = table.Column<string>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    StartsOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndsOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 78, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    SmsCode = table.Column<string>(maxLength: 20, nullable: true),
                    SmsCodeExpiredOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SmsCodePassedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsCodexUser = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    AutocompleteOnly = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Page = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Questions",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    StartedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PageCompleted = table.Column<int>(nullable: false),
                    JoiningPlatform = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSurveys_Surveys",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSurveys_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SurveyQuestionId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_SurveyQuestions",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyQuestionId",
                table: "QuestionAnswers",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "QuestionAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveys_SurveyId",
                table: "UserSurveys",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveys_UserId",
                table: "UserSurveys",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "UserSurveys");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
