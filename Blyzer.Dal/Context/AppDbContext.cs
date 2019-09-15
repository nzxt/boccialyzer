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

        #region # DbSets definitions

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }
        /// <summary>
        /// Roles
        /// </summary>
        public DbSet<AppRole> AppRoles { get; set; }
        /// <summary>
        /// Balls
        /// </summary>
        public DbSet<Ball> Balls { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public DbSet<Country> Countries { get; set; }
        /// <summary>
        /// Tournament Types
        /// </summary>
        public DbSet<TournamentType> TournamentTypes { get; set; }
        /// <summary>
        /// Tournaments
        /// </summary>
        public DbSet<Tournament> Tournaments { get; set; }
        /// <summary>
        /// Players
        /// </summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>
        /// Matches
        /// </summary>
        public DbSet<Match> Matches { get; set; }
        /// <summary>
        /// Relation between match and players
        /// </summary>
        public DbSet<MatchToPlayer> MatchToPlayers { get; set; }
        /// <summary>
        /// Ends
        /// </summary>
        public DbSet<End> Ends { get; set; }
        /// <summary>
        /// Relation between end and players
        /// </summary>
        public DbSet<EndToPlayer> EndToPlayers { get; set; }

        #endregion
        //#region # DbQueries

        //public DbQuery<MatchExtended> MatchesExtended { get; set; }

        //#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.EnableAutoHistory(int.MaxValue);

            modelBuilder.Query<StoredFunction>();
            //modelBuilder.Query<MatchExtended>().ToView("bl_get_matches_list", "public");

            #region # Tables definition

            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<AppRoleClaim>().ToTable("AppRoleClaims");
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogins");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRoles");
            modelBuilder.Entity<AppUserToken>().ToTable("AppUserTokens");
            modelBuilder.Entity<Ball>().ToTable(@"Balls");
            modelBuilder.Entity<Country>().ToTable(@"Countries");
            modelBuilder.Entity<End>().ToTable(@"Ends");
            modelBuilder.Entity<Match>().ToTable(@"Matches");
            modelBuilder.Entity<Player>().ToTable(@"Players");
            modelBuilder.Entity<TournamentType>().ToTable(@"TournamentTypes");
            modelBuilder.Entity<Tournament>().ToTable(@"Tournaments");

            #endregion

            #region # Relations definition

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

            #endregion

            #region # Keys definition

            modelBuilder.Entity<AppRole>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUser>().HasKey(x => x.Id);
            modelBuilder.Entity<Ball>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<End>().HasKey(x => x.Id);
            modelBuilder.Entity<Match>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<TournamentType>().HasKey(x => x.Id);
            modelBuilder.Entity<Tournament>().HasKey(x => x.Id);

            #endregion

            #region # Indexes definition

            modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName);
            
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