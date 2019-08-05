using Blyzer.Domain.Entities;
using Blyzer.Domain.Extensions;
using Blyzer.Domain.QueryType;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blyzer.Dal.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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
        /// М'ячі
        /// </summary>
        public DbSet<Ball> Balls { get; set; }
        /// <summary>
        /// Країни
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
        /// Гравці
        /// </summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>
        /// Матчі
        /// </summary>
        public DbSet<Match> Matches { get; set; }
        /// <summary>
        /// Зв'язок гравців з матчами
        /// </summary>
        public DbSet<MatchToPlayer> MatchToPlayers { get; set; }
        /// <summary>
        /// Періоди гри
        /// </summary>
        public DbSet<End> Ends { get; set; }
        /// <summary>
        /// Зв'язок гравців з етапами
        /// </summary>
        public DbSet<EndToPlayer> EndToPlayers { get; set; }

        #endregion
        //#region # DbQueries

        //public DbQuery<MatchExtended> MatchesExtended { get; set; }

        //#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("hstore");
            modelBuilder.EnableAutoHistory(int.MaxValue);

            modelBuilder.Query<StoredFunction>();
            //modelBuilder.Query<MatchExtended>().ToView("bl_get_matches_list", "public");

            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogins");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRoles");
            modelBuilder.Entity<AppRoleClaim>().ToTable("AppRoleClaims");
            modelBuilder.Entity<AppUserToken>().ToTable("AppUserTokens");



            modelBuilder.Entity<AppUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                // Each RoleId can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each RoleId can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });


            modelBuilder.Entity<LinkToPlayer>()
                .HasDiscriminator<int>("Discriminator")
                .HasValue<MatchToPlayer>(1)
                .HasValue<EndToPlayer>(2);

            #region # AppRole Mapping

            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppRole>().HasKey(x => x.Id);

            modelBuilder.Entity<AppRole>().Property(x => x.Caption).ForNpgsqlHasComment("Опис ролі").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsDefault).ForNpgsqlHasComment("За замовчуванням").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsAdministrator).ForNpgsqlHasComment("Адміністратор?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsSuperUser).ForNpgsqlHasComment("Суперюзер?").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.IsManager).ForNpgsqlHasComment("Менеджер?").ValueGeneratedNever();

            modelBuilder.Entity<AppRole>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<AppRole>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # AppUser Mapping

            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppUser>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName);

            modelBuilder.Entity<AppUser>().Property(x => x.CountryId).ForNpgsqlHasComment("Національність").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.FirstName).ForNpgsqlHasComment("Ім'я").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.LastName).ForNpgsqlHasComment("Прізвище").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.DateOfBirth).ForNpgsqlHasComment("Дата народження").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.Gender).ForNpgsqlHasComment("Стать").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.PlayerId).ForNpgsqlHasComment("Ідентифікатор гравця").ValueGeneratedNever();

            modelBuilder.Entity<AppUser>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Ball Mapping

            modelBuilder.Entity<Ball>().ToTable(@"Balls").ForNpgsqlHasComment("М'ячі");
            modelBuilder.Entity<Ball>().HasKey(x => x.Id);

            modelBuilder.Entity<Ball>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.Rating).ForNpgsqlHasComment("Оцінка").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.IsPenalty).ForNpgsqlHasComment("Штрафний м'яч?").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.IsDeadBall).ForNpgsqlHasComment("М'яч поза грою?").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.DeadBallType).ForNpgsqlHasComment("Типи м'ячів поза грою").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.ShotType).ForNpgsqlHasComment("Тип кидка").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.FromBox).ForNpgsqlHasComment("Ігрова зона").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.Distance).ForNpgsqlHasComment("Дистанція").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.EndId).ForNpgsqlHasComment("Період гри").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.PlayerId).ForNpgsqlHasComment("Гравець").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.CoordinateX).ForNpgsqlHasComment("Координата X").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.CoordinateY).ForNpgsqlHasComment("Координата Y").ValueGeneratedNever();

            modelBuilder.Entity<Ball>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Ball>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Country Mapping

            modelBuilder.Entity<Country>().ToTable(@"Countries").ForNpgsqlHasComment("Країни");
            modelBuilder.Entity<Country>().HasKey(x => x.Id);

            modelBuilder.Entity<Country>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Code).HasColumnName(@"Code").ForNpgsqlHasComment("Код країни").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Alpha2).HasColumnName(@"Alpha2").ForNpgsqlHasComment("Alpha2").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.Alpha3).HasColumnName(@"Alpha3").ForNpgsqlHasComment("Alpha3").ValueGeneratedNever();

            modelBuilder.Entity<Country>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Country>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # End Mapping

            modelBuilder.Entity<End>().ToTable(@"Ends").ForNpgsqlHasComment("Періоди гри");
            modelBuilder.Entity<End>().HasKey(x => x.Id);

            modelBuilder.Entity<End>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.MatchId).ForNpgsqlHasComment("Ідентифікатор матчу").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.Index).ForNpgsqlHasComment("Порядковий номер у грі").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.IsDisrupted).ForNpgsqlHasComment("З порушенням?").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.IsTieBreak).ForNpgsqlHasComment("Тай-брейк?").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.ScoreRed).ForNpgsqlHasComment("Рахунок червоних").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.ScoreBlue).ForNpgsqlHasComment("Рахунок синіх").ValueGeneratedNever();

            modelBuilder.Entity<End>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<End>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # EndToPlayer Mapping

            //modelBuilder.Entity<EndToPlayer>().ToTable(@"EndToPlayers").ForNpgsqlHasComment("Зв'язок гравців з періодами гри");
            //modelBuilder.Entity<EndToPlayer>().HasKey(x => x.Id);

            //modelBuilder.Entity<EndToPlayer>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.Bib).ForNpgsqlHasComment("BIB").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.Box).ForNpgsqlHasComment("Номер боксу").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.PlayerId).ForNpgsqlHasComment("Гравець").ValueGeneratedNever();

            modelBuilder.Entity<EndToPlayer>().Property(x => x.EndId).ForNpgsqlHasComment("Ідентифікатор періоду гри").ValueGeneratedNever();

            modelBuilder.Entity<EndToPlayer>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<EndToPlayer>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Match Mapping

            modelBuilder.Entity<Match>().ToTable(@"Matches").ForNpgsqlHasComment("Матчі");
            modelBuilder.Entity<Match>().HasKey(x => x.Id);

            modelBuilder.Entity<Match>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.DateTimeStamp).ForNpgsqlHasComment("Дата та час проведення").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.MatchType).ForNpgsqlHasComment("Тип матчу").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.CompetitionEvent).ForNpgsqlHasComment("Competition Event").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.PoolStage).ForNpgsqlHasComment("Етап пулу").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.EliminationStage).ForNpgsqlHasComment("Етап на вибування").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.ScoreRed).ForNpgsqlHasComment("Рахунок червоних").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.ScoreBlue).ForNpgsqlHasComment("Рахунок синіх").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.FlagRed).ForNpgsqlHasComment("Ідентифікатор прапору для червоних").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.FlagBlue).ForNpgsqlHasComment("Ідентифікатор прапору для синіх").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.TournamentId).ForNpgsqlHasComment("Турнір").ValueGeneratedNever();

            modelBuilder.Entity<Match>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Match>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # MatchToPlayer Mapping

            //modelBuilder.Entity<MatchToPlayer>().ToTable(@"MatchToPlayers").ForNpgsqlHasComment("Зв'язок гравців з матчами");
            //modelBuilder.Entity<MatchToPlayer>().HasKey(x => x.Id);

            //modelBuilder.Entity<MatchToPlayer>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.Bib).ForNpgsqlHasComment("BIB").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.Box).ForNpgsqlHasComment("Номер боксу").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.PlayerId).ForNpgsqlHasComment("Гравець").ValueGeneratedNever();

            modelBuilder.Entity<MatchToPlayer>().Property(x => x.MatchId).ForNpgsqlHasComment("Ідентифікатор матчу").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.IsSubstitutePlayer).ForNpgsqlHasComment("Гравець для заміни?").ValueGeneratedNever();

            modelBuilder.Entity<MatchToPlayer>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<MatchToPlayer>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Player Mapping

            modelBuilder.Entity<Player>().ToTable(@"Players").ForNpgsqlHasComment("Гравці");
            modelBuilder.Entity<Player>().HasKey(x => x.Id);

            modelBuilder.Entity<Player>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.FullName).ForNpgsqlHasComment("Ім'я та прізвище").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.PlayerClassification).ForNpgsqlHasComment("Класифікація").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.CountryId).ForNpgsqlHasComment("Країна").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.IsBisFed).ForNpgsqlHasComment("Чи є гравцем BISFed?").ValueGeneratedNever();

            modelBuilder.Entity<Player>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Player>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # TournamentType Mapping

            modelBuilder.Entity<TournamentType>().ToTable(@"TournamentTypes").ForNpgsqlHasComment("Типи турнірів");
            modelBuilder.Entity<TournamentType>().HasKey(x => x.Id);

            modelBuilder.Entity<TournamentType>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Name).ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Abbr).ForNpgsqlHasComment("Абревіатура").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.IsBisFed).ForNpgsqlHasComment("Чи є офіційним турніром BISFed?").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.Icon).ForNpgsqlHasComment("Іконка").ValueGeneratedNever();

            modelBuilder.Entity<TournamentType>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<TournamentType>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Tournament Mapping

            modelBuilder.Entity<Tournament>().ToTable(@"Tournaments").ForNpgsqlHasComment("Турніри");
            modelBuilder.Entity<Tournament>().HasKey(x => x.Id);

            modelBuilder.Entity<Tournament>().Property(x => x.Id).IsRequired().ForNpgsqlHasComment("Ідентифікатор").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.Name).ForNpgsqlHasComment("Назва").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.TournamentTypeId).ForNpgsqlHasComment("Тип турниру").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.DateFrom).ForNpgsqlHasComment("Дата початку").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.DateTo).ForNpgsqlHasComment("Дата завершення").ValueGeneratedNever();

            modelBuilder.Entity<Tournament>().Property(x => x.CreatedOn).ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.UpdatedOn).ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.CreatedBy).ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<Tournament>().Property(x => x.UpdatedBy).ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion

            #region CamelCase names to snake-case

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();
                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase();
                }
                // Replace key names
                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }
                // Replace foreign key names
                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }
                // Replace index names
                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }

            #endregion
        }
    }
}