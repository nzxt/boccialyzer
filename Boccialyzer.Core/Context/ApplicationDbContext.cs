using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.LogEntities;
using Boccialyzer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boccialyzer.Core.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        /// <summary>
        /// Керування логуванням
        /// </summary>
        public bool LoggingDisable { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region # DbSets

        /// <summary>
        /// Користувачі
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }
        /// <summary>
        /// Ролі коричтувачів
        /// </summary>
        public DbSet<AppRole> AppRoles { get; set; }
        /// <summary>
        /// Налаштування
        /// </summary>
        public DbSet<Configuration> Configurations { get; set; }
        /// <summary>
        /// Національності
        /// </summary>
        public DbSet<Country> Countries { get; set; }
        /// <summary>
        /// Тип турнирів
        /// </summary>
        public DbSet<TournamentType> TournamentTypes { get; set; }
        /// <summary>
        /// Турніри
        /// </summary>
        public DbSet<Tournament> Tournaments { get; set; }
        /// <summary>
        /// М'ячі
        /// </summary>
        public DbSet<Ball> Balls { get; set; }
        /// <summary>
        /// Гравці
        /// </summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>
        /// Матчі
        /// </summary>
        public DbSet<Match> Matches { get; set; }
        /// <summary>
        /// Тренування
        /// </summary>
        public DbSet<Training> Trainings { get; set; }
        /// <summary>
        /// Зв'язок гравців з матчами
        /// </summary>
        public DbSet<MatchToPlayer> MatchToPlayers { get; set; }
        /// <summary>
        /// Періоди гри
        /// </summary>
        public DbSet<Stage> Stages { get; set; }
        /// <summary>
        /// Зв'язок гравців з етапами
        /// </summary>
        public DbSet<StageToPlayer> StageToPlayers { get; set; }


        /// <summary>
        /// Сповіщення операцій з БД
        /// </summary>
        public DbSet<LogDbEvent> LogDbEvents { get; set; }

        #endregion
        #region # DbQueries

        public DbQuery<MatchExtended> MatchesExtended { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("hstore");

            modelBuilder.Query<ViewStoredFunction>();
            modelBuilder.Query<ViewStoredView>();
            modelBuilder.Query<ViewTrigger>();
            modelBuilder.Query<MatchExtended>().ToView("bl_get_matches_list", "public");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens");


            modelBuilder.Entity<LinkToPlayers>()
                .HasDiscriminator<int>("Discriminator")
                .HasValue<MatchToPlayer>(1)
                .HasValue<StageToPlayer>(2);

            //modelBuilder.Entity<Stage>()
            //    .Property(u => u.AvgPointRed)
            //    .HasComputedColumnSql("public.bl_get_avg_point_red_by_stage('[Id]')");


            #region # AppRole Mapping

            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppRole>().HasKey(x => x.Id);

            modelBuilder.Entity<AppRole>().Property(x => x.Caption).HasColumnName(@"Caption").ForNpgsqlHasComment("Опис ролі").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsDefault).HasColumnName(@"IsDefault").ForNpgsqlHasComment("За замовчуванням").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsSystem).HasColumnName(@"IsSystem").ForNpgsqlHasComment("Системна?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsAdministrator).HasColumnName(@"IsAdministrator").ForNpgsqlHasComment("Адміністратор?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsSuperUser).HasColumnName(@"IsSuperUser").ForNpgsqlHasComment("Суперюзер?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsExpert).HasColumnName(@"IsExpert").ForNpgsqlHasComment("Експерт?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsManager).HasColumnName(@"IsManager").ForNpgsqlHasComment("Менеджер?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsOwner).HasColumnName(@"IsOwner").ForNpgsqlHasComment("Наш співробітник?").ValueGeneratedNever();

            modelBuilder.Entity<AppRole>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # AppUser Mapping

            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppUser>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName);

            modelBuilder.Entity<AppUser>().Property(x => x.CountryId).HasColumnName(@"CountryId").ForNpgsqlHasComment("Національність").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.FirstName).HasColumnName(@"FirstName").ForNpgsqlHasComment("Ім'я").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.LastName).HasColumnName(@"LastName").ForNpgsqlHasComment("Прізвище").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.DateOfBirth).HasColumnName(@"DateOfBirth").ForNpgsqlHasComment("Дата народження").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.Gender).HasColumnName(@"Gender").ForNpgsqlHasComment("Стать").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.PlayerId).HasColumnName(@"PlayerId").ForNpgsqlHasComment("Ідентифікатор гравця").ValueGeneratedNever();

            modelBuilder.Entity<AppUser>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Ball Mapping

            modelBuilder.Entity<Ball>().ToTable(@"Balls").ForNpgsqlHasComment("М'ячі");
            modelBuilder.Entity<Ball>().HasKey(x => x.Id);
            modelBuilder.Entity<Ball>().HasIndex(x => x.Box);
            modelBuilder.Entity<Ball>().HasIndex(x => x.ShotType);
            modelBuilder.Entity<Ball>().HasIndex(x => x.Distance);
            modelBuilder.Entity<Ball>().HasIndex(x => x.DeadBallType);

            modelBuilder.Entity<Ball>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.Rating).HasColumnName(@"Rating").ForNpgsqlHasComment("Оцінка").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.IsPenalty).HasColumnName(@"IsPenalty").ForNpgsqlHasComment("Штрафний м'яч?").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.IsDeadBall).HasColumnName(@"IsDeadBall").ForNpgsqlHasComment("М'яч поза грою?").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.DeadBallType).HasColumnName(@"DeadBallType").ForNpgsqlHasComment("Типи м'ячів поза грою").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.ShotType).HasColumnName(@"ShotType").ForNpgsqlHasComment("Тип кидка").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.Box).HasColumnName(@"Box").ForNpgsqlHasComment("Ігрова зона").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.Distance).HasColumnName(@"Distance").ForNpgsqlHasComment("Дистанція").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.StageId).HasColumnName(@"StageId").ForNpgsqlHasComment("Період гри").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.PlayerId).HasColumnName(@"PlayerId").ForNpgsqlHasComment("Гравець").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.TrainingId).HasColumnName(@"TrainingId").ForNpgsqlHasComment("Тренування").ValueGeneratedNever();

            modelBuilder.Entity<Ball>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Country Mapping

            modelBuilder.Entity<Country>().ToTable(@"Countries").ForNpgsqlHasComment("Країни");
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().HasIndex(x => x.Code);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name);

            modelBuilder.Entity<Country>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Code).HasColumnName(@"Code").ForNpgsqlHasComment("Код країни").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Alpha2).HasColumnName(@"Alpha2").ForNpgsqlHasComment("Alpha2").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Alpha3).HasColumnName(@"Alpha3").ForNpgsqlHasComment("Alpha3").ValueGeneratedNever();

            modelBuilder.Entity<Country>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Match Mapping

            modelBuilder.Entity<Match>().ToTable(@"Matches").ForNpgsqlHasComment("Матчі");
            modelBuilder.Entity<Match>().HasKey(x => x.Id);
            modelBuilder.Entity<Match>().HasIndex(x => x.MatchType);
            modelBuilder.Entity<Match>().HasIndex(x => x.CompetitionEvent);
            modelBuilder.Entity<Match>().HasIndex(x => x.PoolStage);
            modelBuilder.Entity<Match>().HasIndex(x => x.EliminationStage);

            modelBuilder.Entity<Match>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.DateTimeStamp).HasColumnName(@"DateTimeStamp").ForNpgsqlHasComment("Дата та час проведення").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.MatchType).HasColumnName(@"MatchType").ForNpgsqlHasComment("Тип матчу").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.CompetitionEvent).HasColumnName(@"CompetitionEvent").ForNpgsqlHasComment("Competition Event").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.PoolStage).HasColumnName(@"PoolStage").ForNpgsqlHasComment("Етап пулу").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.EliminationStage).HasColumnName(@"EliminationStage").ForNpgsqlHasComment("Етап на вибування").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.ScoreRed).HasColumnName(@"ScoreRed").ForNpgsqlHasComment("Рахунок червоних").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.ScoreBlue).HasColumnName(@"ScoreBlue").ForNpgsqlHasComment("Рахунок синіх").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.FlagRed).HasColumnName(@"FlagRed").ForNpgsqlHasComment("Ідентифікатор прапору для червоних").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.FlagBlue).HasColumnName(@"FlagBlue").ForNpgsqlHasComment("Ідентифікатор прапору для синіх").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.AppUserId).HasColumnName(@"AppUserId").ForNpgsqlHasComment("Ідентифікатор Користувача системи").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.TrainingId).HasColumnName(@"TrainingId").ForNpgsqlHasComment("Тренування").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.TournamentId).HasColumnName(@"TournamentId").ForNpgsqlHasComment("Турнір").ValueGeneratedNever();

            modelBuilder.Entity<Match>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Player Mapping

            modelBuilder.Entity<Player>().ToTable(@"Players").ForNpgsqlHasComment("Гравці");
            modelBuilder.Entity<Player>().HasKey(x => x.Id);

            modelBuilder.Entity<Player>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.FullName).HasColumnName(@"FullName").ForNpgsqlHasComment("Ім'я та прізвище").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.PlayerClassification).HasColumnName(@"PlayerClassification").ForNpgsqlHasComment("Класифікація").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.CountryId).HasColumnName(@"CountryId").ForNpgsqlHasComment("Країна").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.CountryId).HasColumnName(@"CountryId").ForNpgsqlHasComment("Країна").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.IsBisFed).HasColumnName(@"IsBisFed").ForNpgsqlHasComment("Чи є гравцем BISFed?").ValueGeneratedNever();

            modelBuilder.Entity<Player>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Stages Mapping

            modelBuilder.Entity<Stage>().ToTable(@"Stages").ForNpgsqlHasComment("Періоди гри");
            modelBuilder.Entity<Stage>().HasKey(x => x.Id);

            modelBuilder.Entity<Stage>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.MatchId).HasColumnName(@"MatchId").ForNpgsqlHasComment("Ідентифікатор матчу").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.Index).HasColumnName(@"Index").ForNpgsqlHasComment("Порядковий номер у грі").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.IsDisrupted).HasColumnName(@"IsDisrupted").ForNpgsqlHasComment("З порушенням?").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.IsTieBreak).HasColumnName(@"IsTieBreak").ForNpgsqlHasComment("Тай-брейк?").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.ScoreRed).HasColumnName(@"ScoreRed").ForNpgsqlHasComment("Рахунок червоних").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.ScoreBlue).HasColumnName(@"ScoreBlue").ForNpgsqlHasComment("Рахунок синіх").ValueGeneratedNever();

            modelBuilder.Entity<Stage>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Stage>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Training Mapping

            modelBuilder.Entity<Training>().ToTable(@"Trainings").ForNpgsqlHasComment("Тренування");
            modelBuilder.Entity<Training>().HasKey(x => x.Id);

            modelBuilder.Entity<Training>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Training>().Property(x => x.DateTimeStamp).HasColumnName(@"DateTimeStamp").ForNpgsqlHasComment("Дата та час тренування").ValueGeneratedNever();
            modelBuilder.Entity<Training>().Property(x => x.AppUserId).HasColumnName(@"AppUserId").ForNpgsqlHasComment("Користувач системи").ValueGeneratedNever();

            modelBuilder.Entity<Training>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Training>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Training>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Training>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # TournamentType Mapping

            modelBuilder.Entity<TournamentType>().ToTable(@"TournamentTypes").ForNpgsqlHasComment("Тип турніру");
            modelBuilder.Entity<TournamentType>().HasKey(x => x.Id);
            modelBuilder.Entity<TournamentType>().HasIndex(x => x.Name);

            modelBuilder.Entity<TournamentType>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Abbr).HasColumnName(@"Abbr").IsRequired().ForNpgsqlHasComment("Абревіатура").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.IsBisFed).HasColumnName(@"IsBisFed").IsRequired().ForNpgsqlHasComment("Чи є офіційним турніром BISFed?").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Icon).HasColumnName(@"Icon").ForNpgsqlHasComment("Іконка").ValueGeneratedNever();

            modelBuilder.Entity<TournamentType>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Tournament Mapping

            modelBuilder.Entity<Tournament>().ToTable(@"Tournaments").ForNpgsqlHasComment("Турніри");
            modelBuilder.Entity<Tournament>().HasKey(x => x.Id);
            modelBuilder.Entity<Tournament>().HasIndex(x => x.Name);

            modelBuilder.Entity<Tournament>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.TournamentTypeId).HasColumnName(@"TournamentTypeId").IsRequired().ForNpgsqlHasComment("Тип турниру").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.DateFrom).HasColumnName(@"DateFrom").IsRequired().ForNpgsqlHasComment("Дата початку").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.DateTo).HasColumnName(@"DateTo").ForNpgsqlHasComment("Дата завершення").ValueGeneratedNever();

            modelBuilder.Entity<Tournament>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion

            #region # LogDbEvent

            modelBuilder.Entity<LogDbEvent>().ToTable(@"LogDbEvent").ForNpgsqlHasComment("Сповіщення операцій з БД");
            modelBuilder.Entity<LogDbEvent>().HasKey(x => x.Id);
            modelBuilder.Entity<LogDbEvent>().HasIndex(x => x.EventType);
            modelBuilder.Entity<LogDbEvent>().HasIndex(x => x.EventLevel);

            modelBuilder.Entity<LogDbEvent>().Property(x => x.TableName).HasColumnName(@"TableName").ForNpgsqlHasComment("Сутність").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.KeyValues).HasColumnName(@"KeyValues").ForNpgsqlHasComment("Ідентифікатор запису").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.OldValues).HasColumnName(@"OldValues").ForNpgsqlHasComment("Старе значення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.NewValues).HasColumnName(@"NewValues").ForNpgsqlHasComment("Нове значення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.OperationType).HasColumnName(@"OperationType").ForNpgsqlHasComment("Тип операції").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.OperationResult).HasColumnName(@"OperationResult").ForNpgsqlHasComment("Результат виконання операції").ValueGeneratedNever();

            modelBuilder.Entity<LogDbEvent>().Property(x => x.Id).HasColumnName(@"Id").IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час створення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.UserName).HasColumnName(@"UserName").ForNpgsqlHasComment("Користувач системи").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.IpAddress).HasColumnName(@"IpAddress").ForNpgsqlHasComment("IP-адреса").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.EventDate).HasColumnName(@"EventDate").ForNpgsqlHasComment("Дата та час сповіщення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.EventType).HasColumnName(@"EventType").ForNpgsqlHasComment("Тип сповіщення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.EventLevel).HasColumnName(@"EventLevel").ForNpgsqlHasComment("Рівень сповіщення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.Message).HasColumnName(@"Message").ForNpgsqlHasComment("Текст сповіщення").ValueGeneratedNever();
            modelBuilder.Entity<LogDbEvent>().Property(x => x.Exception).HasColumnName(@"Exception").ForNpgsqlHasComment("Помилка").ValueGeneratedNever();

            #endregion

        }

        //public override int SaveChanges()
        //{
        //    if (LoggingDisable)
        //    {
        //        var result = base.SaveChanges();
        //        return result;
        //    }
        //    else
        //    {
        //        var auditEntries = ActionBeforeSaveChanges();
        //        var result = base.SaveChanges();
        //        ActionAfterSaveChanges(auditEntries);
        //        return result;
        //    }
        //}

        //public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    if (LoggingDisable)
        //    {
        //        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //        return result;
        //    }
        //    else
        //    {
        //        var auditEntries = ActionBeforeSaveChanges();
        //        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //        if (auditEntries.Any())
        //            await ActionAfterSaveChanges(auditEntries);
        //        return result;
        //    }
        //}

        //private List<AuditEntry> ActionBeforeSaveChanges()
        //{
        //    ChangeTracker.DetectChanges();

        //    var entriesChanges = ChangeTracker.Entries().Where(x =>
        //        x.State == EntityState.Added ||
        //        x.State == EntityState.Modified ||
        //        x.State == EntityState.Deleted).ToList();

        //    if (entriesChanges.Count > 0)
        //    {
        //        var entries = new List<AuditEntry>();
        //        try
        //        {
        //            foreach (var entry in entriesChanges)
        //            {
        //                var auditEntry = new AuditEntry(tableName: entry.Metadata.Relational().TableName);
        //                #region # Set OperationType

        //                switch (entry.State)
        //                {
        //                    case EntityState.Added:
        //                        auditEntry.OperationType = OperationType.Create;
        //                        auditEntry.Message = "Запис додано";
        //                        break;
        //                    case EntityState.Deleted:
        //                        auditEntry.OperationType = OperationType.Delete;
        //                        auditEntry.Message = "Запис видалено";
        //                        break;
        //                    case EntityState.Modified:
        //                        auditEntry.OperationType = OperationType.Update;
        //                        auditEntry.Message = "Запис модифіковано";
        //                        break;
        //                }

        //                #endregion

        //                foreach (var property in entry.Properties)
        //                {
        //                    if (property.Metadata.PropertyInfo.CustomAttributes.Any(_ =>
        //                        _.AttributeType.Name == "AuditIgnoreAttribute"))
        //                        continue;

        //                    if (property.IsTemporary)
        //                    {
        //                        auditEntry.TemporaryProperties.Add(property);
        //                        continue;
        //                    }

        //                    var propertyName = property.Metadata.Name;

        //                    if (property.Metadata.IsPrimaryKey())
        //                    {
        //                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
        //                        continue;
        //                    }

        //                    switch (entry.State)
        //                    {
        //                        case EntityState.Added:
        //                            auditEntry.NewValues[propertyName] = property.CurrentValue;
        //                            auditEntry.EventType = LogEventTypeDb.DbAddOk;
        //                            break;

        //                        case EntityState.Deleted:
        //                            entry.State = EntityState.Modified;
        //                            if (entry.Properties.Any(_ => _.Metadata.Name.Equals("IsDeleted")))
        //                            {
        //                                entry.Property("IsDeleted").CurrentValue = true;
        //                                auditEntry.EventType = LogEventTypeDb.DbDeleteOk;
        //                            }
        //                            break;

        //                        case EntityState.Modified:
        //                            if (property.OriginalValue.Equals(property.CurrentValue))
        //                                continue;
        //                            if (property.IsModified)
        //                            {
        //                                auditEntry.OldValues[propertyName] = property.OriginalValue;
        //                                if (auditEntry.OperationType == OperationType.Update)
        //                                {
        //                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
        //                                }
        //                                auditEntry.EventType = LogEventTypeDb.DbUpdateOk;
        //                            }
        //                            break;
        //                        case EntityState.Detached:
        //                            break;
        //                        case EntityState.Unchanged:
        //                            break;
        //                    }
        //                }
        //                if (auditEntry.OldValues != null
        //                    && auditEntry.OldValues.Count > 0
        //                    && auditEntry.NewValues != null
        //                    && auditEntry.NewValues.Count > 0) entries.Add(auditEntry);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("{SaveChangesError}", ex.Message);
        //        }

        //        return entries;
        //    }

        //    return null;
        //}

        //private Task ActionAfterSaveChanges(List<AuditEntry> auditEntries)
        //{
        //    if (auditEntries == null || auditEntries.Count == 0)
        //        return Task.CompletedTask;

        //    foreach (var entry in auditEntries)
        //    {

        //        if (entry.HasTemporaryProperties)
        //        {
        //            foreach (var prop in entry.TemporaryProperties)
        //            {
        //                if (prop.Metadata.IsPrimaryKey())
        //                {
        //                    entry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
        //                }
        //                else
        //                {
        //                    entry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
        //                }
        //            }
        //        }

        //        //_log.DbLog(auditEntries);
        //    }

        //    return Task.CompletedTask;
        //}
    }
}
