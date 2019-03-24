using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class ChangeDbModel02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchToPlayers_Trainings_TrainingId",
                table: "MatchToPlayers");

            migrationBuilder.DropIndex(
                name: "IX_MatchToPlayers_TrainingId",
                table: "MatchToPlayers");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "MatchToPlayers");

            migrationBuilder.AddColumn<int>(
                name: "MatchType",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreBlue",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScoreRed",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Balls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    CreatedBy = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    UpdatedBy = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    MatchId = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор матчу"),
                    Index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Порядковий номер у грі"),
                    IsDisrupted = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "З порушенням?"),
                    IsTieBreak = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тай-брейк?"),
                    ScoreRed = table.Column<int>(nullable: false),
                    ScoreBlue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                })
                .Annotation("Npgsql:Comment", "Періоди гри");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TrainingId",
                table: "Matches",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_StageId",
                table: "Balls",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_TrainingId",
                table: "Balls",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Stages_StageId",
                table: "Balls",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Trainings_TrainingId",
                table: "Balls",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Trainings_TrainingId",
                table: "Matches",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Stages_StageId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Trainings_TrainingId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Trainings_TrainingId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TrainingId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Balls_StageId",
                table: "Balls");

            migrationBuilder.DropIndex(
                name: "IX_Balls_TrainingId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "MatchType",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScoreBlue",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ScoreRed",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "AppUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "MatchToPlayers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchToPlayers_TrainingId",
                table: "MatchToPlayers",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchToPlayers_Trainings_TrainingId",
                table: "MatchToPlayers",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
