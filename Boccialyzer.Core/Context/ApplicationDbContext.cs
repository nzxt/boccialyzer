using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Enums;
using Boccialyzer.Domain.LogEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boccialyzer.Domain;
using Boccialyzer.Domain.Models;

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

        public DbSet<Ball> Balls { get; set; }

        public DbSet<Player> Players { get; set; }



        /// <summary>
        /// Матчі
        /// </summary>
        public DbSet<Match> Matches { get; set; }
        /// <summary>
        /// М'ячі у матчі
        /// </summary>
        public DbSet<MatchBall> MatchBalls { get; set; }
        /// <summary>
        /// Тренування
        /// </summary>
        public DbSet<Training> Trainings { get; set; }
        /// <summary>
        /// М'ячі тренування
        /// </summary>
        public DbSet<TrainingBall> TrainingBalls { get; set; }



        /// <summary>
        /// Сповіщення операцій з БД
        /// </summary>
        public DbSet<LogDbEvent> LogDbEvents { get; set; }

        #endregion
        #region # DbQueries

        //public DbQuery<InitiatorIssue> InitiatorIssues { get; set; }
        //public DbQuery<InitiatorSession> InitiatorSessions { get; set; }
        //public DbQuery<BaseStationDto> BaseStationsDto { get; set; }
        //public DbQuery<IssuesForAccess> IssuesForAccess { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("hstore");



            modelBuilder.Entity<Ball>()
                .HasDiscriminator<int>("Discriminator")
                .HasValue<Ball>(0)
                .HasValue<TrainingBall>(1)
                .HasValue<MatchBall>(2);




            modelBuilder.Query<ViewStoredFunction>();
            modelBuilder.Query<ViewStoredView>();
            modelBuilder.Query<ViewTrigger>();
            //modelBuilder.Query<InitiatorIssue>().ToView("Boccialyzer_view_issues_for_initiators", "public");
            //modelBuilder.Query<InitiatorSession>().ToView("Boccialyzer_view_sessions_for_initiator", "public");
            //modelBuilder.Query<BaseStationDto>().ToView("Boccialyzer_view_basestation", "public");
            //modelBuilder.Query<IssuesForAccess>().ToView("Boccialyzer_view_issues_for_access", "public");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens");

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

            modelBuilder.Entity<AppUser>().Property(x => x.NationalityId).HasColumnName(@"NationalityId").ForNpgsqlHasComment("Національність").ValueGeneratedNever();

            modelBuilder.Entity<AppUser>().Property(x => x.CreatedOn).HasColumnName(@"CreatedOn").ForNpgsqlHasComment("Дата та час внесення").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedOn).HasColumnName(@"UpdatedOn").ForNpgsqlHasComment("Дата та час редагування").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").ForNpgsqlHasComment("Користувач системи, що створив запис").ValueGeneratedNever();
            modelBuilder.Entity<AppUser>().Property(x => x.UpdatedBy).HasColumnName(@"UpdatedBy").ForNpgsqlHasComment("Користувач системи, що модифікував запис").ValueGeneratedNever();

            #endregion
            #region # Country Mapping

            modelBuilder.Entity<Country>().ToTable(@"Countries").ForNpgsqlHasComment("Громадянство");
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
            #region # TournamentType Mapping

            modelBuilder.Entity<TournamentType>().ToTable(@"TournamentType").ForNpgsqlHasComment("Тип турниру");
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

        public override int SaveChanges()
        {
            if (LoggingDisable)
            {
                var result = base.SaveChanges();
                return result;
            }
            else
            {
                var auditEntries = ActionBeforeSaveChanges();
                var result = base.SaveChanges();
                ActionAfterSaveChanges(auditEntries);
                return result;
            }
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (LoggingDisable)
            {
                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                return result;
            }
            else
            {
                var auditEntries = ActionBeforeSaveChanges();
                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                if (auditEntries.Any())
                    await ActionAfterSaveChanges(auditEntries);
                return result;
            }
        }

        private List<AuditEntry> ActionBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var entriesChanges = ChangeTracker.Entries().Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified ||
                x.State == EntityState.Deleted).ToList();

            if (entriesChanges.Count > 0)
            {
                var entries = new List<AuditEntry>();
                try
                {
                    foreach (var entry in entriesChanges)
                    {
                        var auditEntry = new AuditEntry(tableName: entry.Metadata.Relational().TableName);
                        #region # Set OperationType

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.OperationType = OperationType.Create;
                                auditEntry.Message = "Запис додано";
                                break;
                            case EntityState.Deleted:
                                auditEntry.OperationType = OperationType.Delete;
                                auditEntry.Message = "Запис видалено";
                                break;
                            case EntityState.Modified:
                                auditEntry.OperationType = OperationType.Update;
                                auditEntry.Message = "Запис модифіковано";
                                break;
                        }

                        #endregion

                        foreach (var property in entry.Properties)
                        {
                            if (property.Metadata.PropertyInfo.CustomAttributes.Any(_ =>
                                _.AttributeType.Name == "AuditIgnoreAttribute"))
                                continue;

                            if (property.IsTemporary)
                            {
                                auditEntry.TemporaryProperties.Add(property);
                                continue;
                            }

                            var propertyName = property.Metadata.Name;

                            if (property.Metadata.IsPrimaryKey())
                            {
                                auditEntry.KeyValues[propertyName] = property.CurrentValue;
                                continue;
                            }

                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                    auditEntry.EventType = LogEventTypeDb.DbAddOk;
                                    break;

                                case EntityState.Deleted:
                                    entry.State = EntityState.Modified;
                                    if (entry.Properties.Any(_ => _.Metadata.Name.Equals("IsDeleted")))
                                    {
                                        entry.Property("IsDeleted").CurrentValue = true;
                                        auditEntry.EventType = LogEventTypeDb.DbDeleteOk;
                                    }
                                    break;

                                case EntityState.Modified:
                                    if (property.OriginalValue.Equals(property.CurrentValue))
                                        continue;
                                    if (property.IsModified)
                                    {
                                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                                        if (auditEntry.OperationType == OperationType.Update)
                                        {
                                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                                        }
                                        auditEntry.EventType = LogEventTypeDb.DbUpdateOk;
                                    }
                                    break;
                                case EntityState.Detached:
                                    break;
                                case EntityState.Unchanged:
                                    break;
                            }
                        }
                        if (auditEntry.OldValues != null
                            && auditEntry.OldValues.Count > 0
                            && auditEntry.NewValues != null
                            && auditEntry.NewValues.Count > 0) entries.Add(auditEntry);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("{SaveChangesError}", ex.Message);
                }

                return entries;
            }

            return null;
        }

        private Task ActionAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var entry in auditEntries)
            {

                if (entry.HasTemporaryProperties)
                {
                    foreach (var prop in entry.TemporaryProperties)
                    {
                        if (prop.Metadata.IsPrimaryKey())
                        {
                            entry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                        }
                        else
                        {
                            entry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                        }
                    }
                }

                //_log.DbLog(auditEntries);
            }

            return Task.CompletedTask;
        }
    }
}
