using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class ChangeDbModel03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balls_MatchToPlayers_MatchToPlayerId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchToPlayers_Matches_MatchId",
                table: "MatchToPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchToPlayers_Players_PlayerId",
                table: "MatchToPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Balls_MatchToPlayerId",
                table: "Balls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchToPlayers",
                table: "MatchToPlayers");

            migrationBuilder.DropColumn(
                name: "MatchToPlayerId",
                table: "Balls");

            migrationBuilder.RenameTable(
                name: "MatchToPlayers",
                newName: "LinkToPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_MatchToPlayers_PlayerId",
                table: "LinkToPlayers",
                newName: "IX_LinkToPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchToPlayers_MatchId",
                table: "LinkToPlayers",
                newName: "IX_LinkToPlayers_MatchId");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerId",
                table: "Balls",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsSubstitutePlayer",
                table: "LinkToPlayers",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "Discriminator",
                table: "LinkToPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StageId",
                table: "LinkToPlayers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkToPlayers",
                table: "LinkToPlayers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_PlayerId",
                table: "Balls",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkToPlayers_StageId",
                table: "LinkToPlayers",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_Players_PlayerId",
                table: "Balls",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkToPlayers_Matches_MatchId",
                table: "LinkToPlayers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkToPlayers_Players_PlayerId",
                table: "LinkToPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkToPlayers_Stages_StageId",
                table: "LinkToPlayers",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balls_Players_PlayerId",
                table: "Balls");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkToPlayers_Matches_MatchId",
                table: "LinkToPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkToPlayers_Players_PlayerId",
                table: "LinkToPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkToPlayers_Stages_StageId",
                table: "LinkToPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Balls_PlayerId",
                table: "Balls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkToPlayers",
                table: "LinkToPlayers");

            migrationBuilder.DropIndex(
                name: "IX_LinkToPlayers_StageId",
                table: "LinkToPlayers");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "LinkToPlayers");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "LinkToPlayers");

            migrationBuilder.RenameTable(
                name: "LinkToPlayers",
                newName: "MatchToPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_LinkToPlayers_PlayerId",
                table: "MatchToPlayers",
                newName: "IX_MatchToPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkToPlayers_MatchId",
                table: "MatchToPlayers",
                newName: "IX_MatchToPlayers_MatchId");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchToPlayerId",
                table: "Balls",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSubstitutePlayer",
                table: "MatchToPlayers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchToPlayers",
                table: "MatchToPlayers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_MatchToPlayerId",
                table: "Balls",
                column: "MatchToPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balls_MatchToPlayers_MatchToPlayerId",
                table: "Balls",
                column: "MatchToPlayerId",
                principalTable: "MatchToPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchToPlayers_Matches_MatchId",
                table: "MatchToPlayers",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchToPlayers_Players_PlayerId",
                table: "MatchToPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
