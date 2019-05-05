using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boccialyzer.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    CreatedBy = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    UpdatedBy = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    Caption = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Опис ролі"),
                    IsDefault = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "За замовчуванням"),
                    IsSystem = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Системна?"),
                    IsAdministrator = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Адміністратор?"),
                    IsSuperUser = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Суперюзер?"),
                    IsExpert = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Експерт?"),
                    IsManager = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Менеджер?"),
                    IsOwner = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Наш співробітник?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
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
                    Name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Код країни"),
                    Alpha2 = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Alpha2"),
                    Alpha3 = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Alpha3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                })
                .Annotation("Npgsql:Comment", "Країни");

            migrationBuilder.CreateTable(
                name: "LogDbEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час створення"),
                    UserName = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи"),
                    IpAddress = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "IP-адреса"),
                    EventDate = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час сповіщення"),
                    EventLevel = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рівень сповіщення"),
                    Message = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Текст сповіщення"),
                    Exception = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Помилка"),
                    EventType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип сповіщення"),
                    TableName = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Сутність"),
                    KeyValues = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор запису"),
                    OldValues = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Старе значення"),
                    NewValues = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Нове значення"),
                    OperationType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип операції"),
                    OperationResult = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Результат виконання операції")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDbEvent", x => x.Id);
                })
                .Annotation("Npgsql:Comment", "Сповіщення операцій з БД");

            migrationBuilder.CreateTable(
                name: "Players",
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
                    FullName = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ім'я та прізвище"),
                    PlayerClassification = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Класифікація"),
                    CountryId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Країна"),
                    IsBisFed = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Чи є гравцем BISFed?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                })
                .Annotation("Npgsql:Comment", "Гравці");

            migrationBuilder.CreateTable(
                name: "TournamentTypes",
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
                    Name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    Abbr = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Абревіатура"),
                    IsBisFed = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Чи є офіційним турніром BISFed?"),
                    Icon = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Іконка")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTypes", x => x.Id);
                })
                .Annotation("Npgsql:Comment", "Тип турніру");

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    UpdatedOn = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    CreatedBy = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    UpdatedBy = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    CountryId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Національність"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true)
                        .Annotation("Npgsql:Comment", "Ім'я"),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                        .Annotation("Npgsql:Comment", "Прізвище"),
                    DateOfBirth = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата народження"),
                    Gender = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Стать"),
                    PlayerId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор гравця"),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
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
                    Name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    TournamentTypeId = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип турниру"),
                    DateFrom = table.Column<DateTime>(type: "Date", nullable: false)
                        .Annotation("Npgsql:Comment", "Дата початку"),
                    DateTo = table.Column<DateTime>(type: "Date", nullable: false)
                        .Annotation("Npgsql:Comment", "Дата завершення")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentTypes_TournamentTypeId",
                        column: x => x.TournamentTypeId,
                        principalTable: "TournamentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Турніри");

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
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
                    DateTimeStamp = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час тренування"),
                    AppUserId = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Тренування");

            migrationBuilder.CreateTable(
                name: "Matches",
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
                    DateTimeStamp = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час проведення"),
                    MatchType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип матчу"),
                    CompetitionEvent = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Competition Event"),
                    PoolStage = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Етап пулу"),
                    EliminationStage = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Етап на вибування"),
                    ScoreRed = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок червоних"),
                    ScoreBlue = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок синіх"),
                    AvgPointRed = table.Column<int>(nullable: false),
                    AvgPointBlue = table.Column<int>(nullable: false),
                    FlagRed = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор прапору для червоних"),
                    FlagBlue = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор прапору для синіх"),
                    AppUserId = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор Користувача системи"),
                    TrainingId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Тренування"),
                    TournamentId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Турнір")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Матчі");

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
                    ScoreRed = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок червоних"),
                    ScoreBlue = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок синіх"),
                    AvgPointRed = table.Column<int>(nullable: false),
                    AvgPointBlue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Періоди гри");

            migrationBuilder.CreateTable(
                name: "Balls",
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
                    Rating = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Оцінка"),
                    IsJack = table.Column<bool>(nullable: false),
                    IsPenalty = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Штрафний м'яч?"),
                    IsDeadBall = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "М'яч поза грою?"),
                    DeadBallType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Типи м'ячів поза грою"),
                    ShotType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип кидка"),
                    Box = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ігрова зона"),
                    Distance = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дистанція"),
                    StageId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Період гри"),
                    PlayerId = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Гравець"),
                    TrainingId = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Тренування")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balls_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Balls_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Balls_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "М'ячі");

            migrationBuilder.CreateTable(
                name: "LinkToPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    Bib = table.Column<int>(nullable: false),
                    Box = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<int>(nullable: false),
                    IsSubstitutePlayer = table.Column<bool>(nullable: true),
                    MatchId = table.Column<Guid>(nullable: true),
                    StageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkToPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkToPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkToPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkToPlayers_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CountryId",
                table: "AppUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserName",
                table: "AppUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_Box",
                table: "Balls",
                column: "Box");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_DeadBallType",
                table: "Balls",
                column: "DeadBallType");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_Distance",
                table: "Balls",
                column: "Distance");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_PlayerId",
                table: "Balls",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_ShotType",
                table: "Balls",
                column: "ShotType");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_StageId",
                table: "Balls",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Balls_TrainingId",
                table: "Balls",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LinkToPlayers_MatchId",
                table: "LinkToPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkToPlayers_PlayerId",
                table: "LinkToPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkToPlayers_StageId",
                table: "LinkToPlayers",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_LogDbEvent_EventLevel",
                table: "LogDbEvent",
                column: "EventLevel");

            migrationBuilder.CreateIndex(
                name: "IX_LogDbEvent_EventType",
                table: "LogDbEvent",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AppUserId",
                table: "Matches",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionEvent",
                table: "Matches",
                column: "CompetitionEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_EliminationStage",
                table: "Matches",
                column: "EliminationStage");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchType",
                table: "Matches",
                column: "MatchType");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PoolStage",
                table: "Matches",
                column: "PoolStage");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TrainingId",
                table: "Matches",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_MatchId",
                table: "Stages",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Name",
                table: "Tournaments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentTypeId",
                table: "Tournaments",
                column: "TournamentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTypes_Name",
                table: "TournamentTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_AppUserId",
                table: "Trainings",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Balls");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "LinkToPlayers");

            migrationBuilder.DropTable(
                name: "LogDbEvent");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "TournamentTypes");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
