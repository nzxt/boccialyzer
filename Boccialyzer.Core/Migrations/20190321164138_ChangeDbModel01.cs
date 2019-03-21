using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boccialyzer.Core.Migrations
{
    public partial class ChangeDbModel01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_TournamentType_TournamentTypeId",
                table: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "Tournament");

            migrationBuilder.RenameIndex(
                name: "IX_Tournaments_TournamentTypeId",
                table: "Tournament",
                newName: "IX_Tournament_TournamentTypeId");

            migrationBuilder.AlterTable(
                name: "Tournament")
                .Annotation("Npgsql:Comment", "Турнири");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Tournament",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "Дата та час редагування");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Tournament",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

            migrationBuilder.AlterColumn<Guid>(
                name: "TournamentTypeId",
                table: "Tournament",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "Тип турниру");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tournament",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("Npgsql:Comment", "Назва");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "Tournament",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .Annotation("Npgsql:Comment", "Дата завершення");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "Tournament",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .Annotation("Npgsql:Comment", "Дата початку");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tournament",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "Дата та час внесення");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Tournament",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true)
                .Annotation("Npgsql:Comment", "Користувач системи, що створив запис");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tournament",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Npgsql:Comment", "Ідентифікатор");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_Name",
                table: "Tournament",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournament_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_TournamentType_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId",
                principalTable: "TournamentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournament_TournamentId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_TournamentType_TournamentTypeId",
                table: "Tournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournament",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_Name",
                table: "Tournament");

            migrationBuilder.RenameTable(
                name: "Tournament",
                newName: "Tournaments");

            migrationBuilder.RenameIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournaments",
                newName: "IX_Tournaments_TournamentTypeId");

            migrationBuilder.AlterTable(
                name: "Tournaments")
                .OldAnnotation("Npgsql:Comment", "Турнири");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .OldAnnotation("Npgsql:Comment", "Дата та час редагування");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true)
                .OldAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

            migrationBuilder.AlterColumn<Guid>(
                name: "TournamentTypeId",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(Guid))
                .OldAnnotation("Npgsql:Comment", "Тип турниру");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(string))
                .OldAnnotation("Npgsql:Comment", "Назва");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "Tournaments",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .OldAnnotation("Npgsql:Comment", "Дата завершення");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "Tournaments",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date")
                .OldAnnotation("Npgsql:Comment", "Дата початку");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true)
                .OldAnnotation("Npgsql:Comment", "Дата та час внесення");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true)
                .OldAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(Guid))
                .OldAnnotation("Npgsql:Comment", "Ідентифікатор");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_TournamentType_TournamentTypeId",
                table: "Tournaments",
                column: "TournamentTypeId",
                principalTable: "TournamentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
