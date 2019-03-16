using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class ModifiedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Matches_MatchId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Trainings_TrainingId",
                table: "Balls");

            migrationBuilder.DropIndex(
                name: "IX_Balls_MatchId",
                table: "Balls");

            migrationBuilder.DropIndex(
                name: "IX_Balls_TrainingId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "Box1PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box1PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box2PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box2PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box3PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box3PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box4PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box4PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box5PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box5PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box6PlayerBib",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Box6PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Balls");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchToPlayerId",
                table: "Balls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StageId",
                table: "Balls",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MatchToPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    IsSubstitutePlayer = table.Column<bool>(nullable: false),
                    Bib = table.Column<int>(nullable: false),
                    Box = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    TrainingId = table.Column<Guid>(nullable: true),
                    MatchId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchToPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchToPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchToPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchToPlayers_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    MatchId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDisrupted = table.Column<bool>(nullable: false),
                    IsTieBreak = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stage_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balls_MatchToPlayerId",
                table: "Balls",
                column: "MatchToPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_StageId",
                table: "Balls",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchToPlayers_MatchId",
                table: "MatchToPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchToPlayers_PlayerId",
                table: "MatchToPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchToPlayers_TrainingId",
                table: "MatchToPlayers",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_MatchId",
                table: "Stage",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_MatchToPlayers_MatchToPlayerId",
                table: "Balls",
                column: "MatchToPlayerId",
                principalTable: "MatchToPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Stage_StageId",
                table: "Balls",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balls_MatchToPlayers_MatchToPlayerId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Stage_StageId",
                table: "Balls");

            migrationBuilder.DropTable(
                name: "MatchToPlayers");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropIndex(
                name: "IX_Balls_MatchToPlayerId",
                table: "Balls");

            migrationBuilder.DropIndex(
                name: "IX_Balls_StageId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "MatchToPlayerId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Balls");

            migrationBuilder.AddColumn<int>(
                name: "Box1PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box1PlayerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Box2PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box2PlayerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Box3PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box3PlayerId",
                table: "Matches",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Box4PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box4PlayerId",
                table: "Matches",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Box5PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box5PlayerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Box6PlayerBib",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Box6PlayerId",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discriminator",
                table: "Balls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MatchId",
                table: "Balls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Balls",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balls_MatchId",
                table: "Balls",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_TrainingId",
                table: "Balls",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Matches_MatchId",
                table: "Balls",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Trainings_TrainingId",
                table: "Balls",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
