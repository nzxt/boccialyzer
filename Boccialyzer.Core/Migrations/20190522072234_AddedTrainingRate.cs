using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class AddedTrainingRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Trainings");
        }
    }
}
