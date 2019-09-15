using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blyzer.Dal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "app_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    caption = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Опис ролі"),
                    is_default = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "За замовчуванням"),
                    is_administrator = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Адміністратор?"),
                    is_super_user = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Суперюзер?"),
                    is_manager = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Менеджер?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auto_history",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    row_id = table.Column<string>(maxLength: 50, nullable: false),
                    table_name = table.Column<string>(maxLength: 128, nullable: false),
                    changed = table.Column<string>(nullable: true),
                    kind = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auto_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Код країни"),
                    alpha2 = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Alpha2"),
                    alpha3 = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Alpha3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Країни");

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    full_name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ім'я та прізвище"),
                    player_classification = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Класифікація"),
                    country_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Країна"),
                    is_bis_fed = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Чи є гравцем BISFed?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Гравці");

            migrationBuilder.CreateTable(
                name: "tournament_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    abbr = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Абревіатура"),
                    is_bis_fed = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Чи є офіційним турніром BISFed?"),
                    icon = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Іконка")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournament_types", x => x.id);
                })
                .Annotation("Npgsql:Comment", "Типи турнірів");

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    role_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_app_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "app_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    country_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Національність"),
                    first_name = table.Column<string>(maxLength: 50, nullable: true)
                        .Annotation("Npgsql:Comment", "Ім'я"),
                    last_name = table.Column<string>(maxLength: 50, nullable: false)
                        .Annotation("Npgsql:Comment", "Прізвище"),
                    date_of_birth = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата народження"),
                    gender = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Стать"),
                    player_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор гравця"),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_app_users_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<Guid>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false),
                    discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_app_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "app_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true),
                    discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: false)
                        .Annotation("Npgsql:Comment", "Назва"),
                    tournament_type_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип турниру"),
                    date_from = table.Column<DateTime>(type: "Date", nullable: false)
                        .Annotation("Npgsql:Comment", "Дата початку"),
                    date_to = table.Column<DateTime>(type: "Date", nullable: false)
                        .Annotation("Npgsql:Comment", "Дата завершення"),
                    app_user_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournaments", x => x.id);
                    table.ForeignKey(
                        name: "fk_tournaments_app_users_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tournaments_tournament_types_tournament_type_id",
                        column: x => x.tournament_type_id,
                        principalTable: "tournament_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Турніри");

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    date_time_stamp = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час проведення"),
                    match_type = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип матчу"),
                    rate = table.Column<int>(nullable: false),
                    is_public = table.Column<bool>(nullable: false),
                    competition_event = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Competition Event"),
                    pool_stage = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Етап пулу"),
                    elimination_stage = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Етап на вибування"),
                    score_red = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок червоних"),
                    score_blue = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок синіх"),
                    avg_point_red = table.Column<int>(nullable: false),
                    avg_point_blue = table.Column<int>(nullable: false),
                    flag_red = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор прапору для червоних"),
                    flag_blue = table.Column<string>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор прапору для синіх"),
                    tournament_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Турнір"),
                    app_user_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches", x => x.id);
                    table.ForeignKey(
                        name: "fk_matches_app_users_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_matches_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Npgsql:Comment", "Матчі");

            migrationBuilder.CreateTable(
                name: "ends",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    match_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор матчу"),
                    index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Порядковий номер у грі"),
                    is_disrupted = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "З порушенням?"),
                    is_tie_break = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тай-брейк?"),
                    score_red = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок червоних"),
                    score_blue = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Рахунок синіх"),
                    avg_point_red = table.Column<int>(nullable: false),
                    avg_point_blue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ends", x => x.id);
                    table.ForeignKey(
                        name: "fk_ends_matches_match_id",
                        column: x => x.match_id,
                        principalTable: "matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "Періоди гри");

            migrationBuilder.CreateTable(
                name: "balls",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ідентифікатор"),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    index = table.Column<int>(nullable: false),
                    rating = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Оцінка"),
                    is_jack = table.Column<bool>(nullable: false),
                    is_penalty = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "Штрафний м'яч?"),
                    is_dead_ball = table.Column<bool>(nullable: false)
                        .Annotation("Npgsql:Comment", "М'яч поза грою?"),
                    dead_ball_type = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Типи м'ячів поза грою"),
                    shot_type = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Тип кидка"),
                    from_box = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Ігрова зона"),
                    distance = table.Column<float>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дистанція"),
                    end_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Період гри"),
                    player_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Гравець"),
                    coordinate_x = table.Column<float>(nullable: false)
                        .Annotation("Npgsql:Comment", "Координата X"),
                    coordinate_y = table.Column<float>(nullable: false)
                        .Annotation("Npgsql:Comment", "Координата Y")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balls", x => x.id);
                    table.ForeignKey(
                        name: "fk_balls_ends_end_id",
                        column: x => x.end_id,
                        principalTable: "ends",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_balls_players_player_id",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("Npgsql:Comment", "М'ячі");

            migrationBuilder.CreateTable(
                name: "link_to_player",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false)
                        .Annotation("Npgsql:Comment", "Дата та час внесення"),
                    updated_on = table.Column<DateTime>(nullable: true)
                        .Annotation("Npgsql:Comment", "Дата та час редагування"),
                    created_by = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Користувач системи, що створив запис"),
                    updated_by = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Користувач системи, що модифікував запис"),
                    is_public = table.Column<bool>(nullable: false),
                    bib = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "BIB"),
                    box = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Comment", "Номер боксу"),
                    player_id = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:Comment", "Гравець"),
                    discriminator = table.Column<int>(nullable: false),
                    end_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор періоду гри"),
                    is_substitute_player = table.Column<bool>(nullable: true)
                        .Annotation("Npgsql:Comment", "Гравець для заміни?"),
                    match_id = table.Column<Guid>(nullable: true)
                        .Annotation("Npgsql:Comment", "Ідентифікатор матчу")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_link_to_player", x => x.id);
                    table.ForeignKey(
                        name: "fk_link_to_player_ends_end_id",
                        column: x => x.end_id,
                        principalTable: "ends",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_link_to_player_matches_match_id",
                        column: x => x.match_id,
                        principalTable: "matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_link_to_player_players_player_id",
                        column: x => x.player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "app_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_app_users_country_id",
                table: "app_users",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "app_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "app_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_app_users_user_name",
                table: "app_users",
                column: "user_name");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_balls_end_id",
                table: "balls",
                column: "end_id");

            migrationBuilder.CreateIndex(
                name: "ix_balls_player_id",
                table: "balls",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "ix_ends_match_id",
                table: "ends",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "ix_link_to_player_end_id",
                table: "link_to_player",
                column: "end_id");

            migrationBuilder.CreateIndex(
                name: "ix_link_to_player_match_id",
                table: "link_to_player",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "ix_link_to_player_player_id",
                table: "link_to_player",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_app_user_id",
                table: "matches",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_tournament_id",
                table: "matches",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_app_user_id",
                table: "tournaments",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_tournament_type_id",
                table: "tournaments",
                column: "tournament_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "auto_history");

            migrationBuilder.DropTable(
                name: "balls");

            migrationBuilder.DropTable(
                name: "link_to_player");

            migrationBuilder.DropTable(
                name: "app_roles");

            migrationBuilder.DropTable(
                name: "ends");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "app_users");

            migrationBuilder.DropTable(
                name: "tournament_types");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
