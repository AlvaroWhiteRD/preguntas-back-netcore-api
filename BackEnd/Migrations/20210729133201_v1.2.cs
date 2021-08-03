using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerQuestionnaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    QuestionnairesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerQuestionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerQuestionnaires_Questionnaires_QuestionnairesId",
                        column: x => x.QuestionnairesId,
                        principalTable: "Questionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerQuestionnaireDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerQuestionnairesId = table.Column<int>(type: "int", nullable: false),
                    AnswersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerQuestionnaireDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerQuestionnaireDetails_AnswerQuestionnaires_AnswerQuestionnairesId",
                        column: x => x.AnswerQuestionnairesId,
                        principalTable: "AnswerQuestionnaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnswerQuestionnaireDetails_Answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestionnaireDetails_AnswerQuestionnairesId",
                table: "AnswerQuestionnaireDetails",
                column: "AnswerQuestionnairesId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestionnaireDetails_AnswersId",
                table: "AnswerQuestionnaireDetails",
                column: "AnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerQuestionnaires_QuestionnairesId",
                table: "AnswerQuestionnaires",
                column: "QuestionnairesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerQuestionnaireDetails");

            migrationBuilder.DropTable(
                name: "AnswerQuestionnaires");
        }
    }
}
