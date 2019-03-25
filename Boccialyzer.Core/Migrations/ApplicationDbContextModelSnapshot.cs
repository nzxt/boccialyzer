﻿// <auto-generated />
using System;
using Boccialyzer.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boccialyzer.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:hstore", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Boccialyzer.Domain.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnName("Caption")
                        .HasAnnotation("Npgsql:Comment", "Опис ролі");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnName("IsAdministrator")
                        .HasAnnotation("Npgsql:Comment", "Адміністратор?");

                    b.Property<bool>("IsDefault")
                        .HasColumnName("IsDefault")
                        .HasAnnotation("Npgsql:Comment", "За замовчуванням");

                    b.Property<bool>("IsExpert")
                        .HasColumnName("IsExpert")
                        .HasAnnotation("Npgsql:Comment", "Експерт?");

                    b.Property<bool>("IsManager")
                        .HasColumnName("IsManager")
                        .HasAnnotation("Npgsql:Comment", "Менеджер?");

                    b.Property<bool>("IsOwner")
                        .HasColumnName("IsOwner")
                        .HasAnnotation("Npgsql:Comment", "Наш співробітник?");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnName("IsSuperUser")
                        .HasAnnotation("Npgsql:Comment", "Суперюзер?");

                    b.Property<bool>("IsSystem")
                        .HasColumnName("IsSystem")
                        .HasAnnotation("Npgsql:Comment", "Системна?");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid?>("CountryId")
                        .HasColumnName("CountryId")
                        .HasAnnotation("Npgsql:Comment", "Національність");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<Guid?>("PlayerId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("UserName");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Ball", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Box");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("DeadBallType");

                    b.Property<int>("Distance");

                    b.Property<bool>("IsDeadBall");

                    b.Property<bool>("IsPenalty");

                    b.Property<Guid>("PlayerId");

                    b.Property<int>("Rating");

                    b.Property<int>("ShotType");

                    b.Property<Guid?>("StageId");

                    b.Property<Guid?>("TrainingId");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("StageId");

                    b.HasIndex("TrainingId");

                    b.ToTable("Balls");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Configuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор");

                    b.Property<string>("Alpha2")
                        .HasColumnName("Alpha2")
                        .HasAnnotation("Npgsql:Comment", "Alpha2");

                    b.Property<string>("Alpha3")
                        .HasColumnName("Alpha3")
                        .HasAnnotation("Npgsql:Comment", "Alpha3");

                    b.Property<int>("Code")
                        .HasColumnName("Code")
                        .HasAnnotation("Npgsql:Comment", "Код країни");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<string>("Icon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("Npgsql:Comment", "Назва");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.HasIndex("Name");

                    b.ToTable("Countries");

                    b.HasAnnotation("Npgsql:Comment", "Громадянство");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.LinkToPlayers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bib");

                    b.Property<int>("Box");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("Discriminator");

                    b.Property<Guid>("PlayerId");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("LinkToPlayers");

                    b.HasDiscriminator<int>("Discriminator");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppUserId");

                    b.Property<int>("CompetitionEvent");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime>("DateTimeStamp");

                    b.Property<int>("EliminationStage");

                    b.Property<int>("MatchType");

                    b.Property<int>("PoolStage");

                    b.Property<int>("ScoreBlue");

                    b.Property<int>("ScoreRed");

                    b.Property<Guid?>("TournamentId");

                    b.Property<Guid?>("TrainingId");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TournamentId");

                    b.HasIndex("TrainingId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CountryId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<int>("PlayerClassification");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Stage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<int>("Index")
                        .HasColumnName("Index")
                        .HasAnnotation("Npgsql:Comment", "Порядковий номер у грі");

                    b.Property<bool>("IsDisrupted")
                        .HasColumnName("IsDisrupted")
                        .HasAnnotation("Npgsql:Comment", "З порушенням?");

                    b.Property<bool>("IsTieBreak")
                        .HasColumnName("IsTieBreak")
                        .HasAnnotation("Npgsql:Comment", "Тай-брейк?");

                    b.Property<Guid>("MatchId")
                        .HasColumnName("MatchId")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор матчу");

                    b.Property<int>("ScoreBlue");

                    b.Property<int>("ScoreRed");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Stages");

                    b.HasAnnotation("Npgsql:Comment", "Періоди гри");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Tournament", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnName("DateFrom")
                        .HasColumnType("Date")
                        .HasAnnotation("Npgsql:Comment", "Дата початку");

                    b.Property<DateTime>("DateTo")
                        .HasColumnName("DateTo")
                        .HasColumnType("Date")
                        .HasAnnotation("Npgsql:Comment", "Дата завершення");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("Npgsql:Comment", "Назва");

                    b.Property<Guid>("TournamentTypeId")
                        .HasColumnName("TournamentTypeId")
                        .HasAnnotation("Npgsql:Comment", "Тип турниру");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("TournamentTypeId");

                    b.ToTable("Tournament");

                    b.HasAnnotation("Npgsql:Comment", "Турнири");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.TournamentType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор");

                    b.Property<string>("Abbr")
                        .IsRequired()
                        .HasColumnName("Abbr")
                        .HasAnnotation("Npgsql:Comment", "Абревіатура");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що створив запис");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час внесення");

                    b.Property<string>("Icon")
                        .HasColumnName("Icon")
                        .HasAnnotation("Npgsql:Comment", "Іконка");

                    b.Property<bool>("IsBisFed")
                        .HasColumnName("IsBisFed")
                        .HasAnnotation("Npgsql:Comment", "Чи є офіційним турніром BISFed?");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("Npgsql:Comment", "Назва");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnName("UpdatedBy")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи, що модифікував запис");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("UpdatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час редагування");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("TournamentType");

                    b.HasAnnotation("Npgsql:Comment", "Тип турниру");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppUserId");

                    b.Property<Guid?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime>("DateTimeStamp");

                    b.Property<Guid?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("Boccialyzer.Domain.LogEntities.LogDbEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasAnnotation("Npgsql:Comment", "Дата та час створення");

                    b.Property<DateTime>("EventDate")
                        .HasColumnName("EventDate")
                        .HasAnnotation("Npgsql:Comment", "Дата та час сповіщення");

                    b.Property<int>("EventLevel")
                        .HasColumnName("EventLevel")
                        .HasAnnotation("Npgsql:Comment", "Рівень сповіщення");

                    b.Property<int>("EventType")
                        .HasColumnName("EventType")
                        .HasAnnotation("Npgsql:Comment", "Тип сповіщення");

                    b.Property<string>("Exception")
                        .HasColumnName("Exception")
                        .HasAnnotation("Npgsql:Comment", "Помилка");

                    b.Property<string>("IpAddress")
                        .HasColumnName("IpAddress")
                        .HasAnnotation("Npgsql:Comment", "IP-адреса");

                    b.Property<string>("KeyValues")
                        .HasColumnName("KeyValues")
                        .HasAnnotation("Npgsql:Comment", "Ідентифікатор запису");

                    b.Property<string>("Message")
                        .HasColumnName("Message")
                        .HasAnnotation("Npgsql:Comment", "Текст сповіщення");

                    b.Property<string>("NewValues")
                        .HasColumnName("NewValues")
                        .HasAnnotation("Npgsql:Comment", "Нове значення");

                    b.Property<string>("OldValues")
                        .HasColumnName("OldValues")
                        .HasAnnotation("Npgsql:Comment", "Старе значення");

                    b.Property<int>("OperationResult")
                        .HasColumnName("OperationResult")
                        .HasAnnotation("Npgsql:Comment", "Результат виконання операції");

                    b.Property<int>("OperationType")
                        .HasColumnName("OperationType")
                        .HasAnnotation("Npgsql:Comment", "Тип операції");

                    b.Property<string>("TableName")
                        .HasColumnName("TableName")
                        .HasAnnotation("Npgsql:Comment", "Сутність");

                    b.Property<string>("UserName")
                        .HasColumnName("UserName")
                        .HasAnnotation("Npgsql:Comment", "Користувач системи");

                    b.HasKey("Id");

                    b.HasIndex("EventLevel");

                    b.HasIndex("EventType");

                    b.ToTable("LogDbEvent");

                    b.HasAnnotation("Npgsql:Comment", "Сповіщення операцій з БД");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.MatchToPlayer", b =>
                {
                    b.HasBaseType("Boccialyzer.Domain.Entities.LinkToPlayers");

                    b.Property<bool>("IsSubstitutePlayer");

                    b.Property<Guid>("MatchId");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.StageToPlayer", b =>
                {
                    b.HasBaseType("Boccialyzer.Domain.Entities.LinkToPlayers");

                    b.Property<Guid>("StageId");

                    b.HasIndex("StageId");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.AppUser", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.Country", "Country")
                        .WithMany("AppUsers")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Ball", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.Player")
                        .WithMany("Balls")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boccialyzer.Domain.Entities.Stage")
                        .WithMany("Balls")
                        .HasForeignKey("StageId");

                    b.HasOne("Boccialyzer.Domain.Entities.Training")
                        .WithMany("Balls")
                        .HasForeignKey("TrainingId");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Match", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppUser", "AppUser")
                        .WithMany("Matches")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boccialyzer.Domain.Entities.Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");

                    b.HasOne("Boccialyzer.Domain.Entities.Training")
                        .WithMany("Matches")
                        .HasForeignKey("TrainingId");
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Stage", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.Match", "Match")
                        .WithMany("Stages")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Tournament", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.TournamentType")
                        .WithMany("Tournaments")
                        .HasForeignKey("TournamentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.Training", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppUser")
                        .WithMany("Trainings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boccialyzer.Domain.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.MatchToPlayer", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.Match", "Match")
                        .WithMany("MatchToPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boccialyzer.Domain.Entities.Player")
                        .WithMany("MatchToPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boccialyzer.Domain.Entities.StageToPlayer", b =>
                {
                    b.HasOne("Boccialyzer.Domain.Entities.Stage", "Stage")
                        .WithMany("StageToPlayers")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
