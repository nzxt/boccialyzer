using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class ChangeDbModel04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stages_MatchId",
                table: "Stages",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Matches_MatchId",
                table: "Stages",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Matches_MatchId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_MatchId",
                table: "Stages");
        }
    }
}
