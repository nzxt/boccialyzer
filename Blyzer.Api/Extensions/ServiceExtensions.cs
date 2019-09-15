using Blyzer.Dal.Context;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Extensions;
using Blyzer.Domain.Models;
using Blyzer.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Blyzer.Domain.Filtering;

namespace Blyzer.Api.Extensions
{
    /// <summary>
    /// Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        #region Configure Application`s Configuration
        /// <summary>
        /// Configure Application`s Configuration
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration from appsetting.json</param>
        public static void ConfigureAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppConfiguration>(options=>
            {
                try
                {
                    #region option: ConnectionString

                    var tempValue = configuration.GetConnectionString("DefaultConnection");
                    if (!string.IsNullOrEmpty(tempValue))
                    {
                        AppState.FatalError = false;
                        options.ConnectionString = tempValue;
                    }
                    else
                    {
                        AppState.FatalError = true;
                        Log.Fatal("{Fatal}",
                            "Не вказано рядок підключення до БД (ConnectionStrings: DefaultConnection)");
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    Log.Fatal("{Fatal}{ErrorMessage}", "Помилка отримання налаштувань (AppOptions)",
                        ex.Message);
                }
            });
            Log.Information("{Info}", "Configure Configuration ... OK");
        }

        #endregion
        #region Configure CORS
        /// <summary>
        /// Configure CORS
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration from appsetting.json</param>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                        builder =>
                        {
                            builder
                                .WithOrigins(configuration["CorsOrigins"].Split(',').ToArray())
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
            });
            Log.Information("{Info}", "Configure CORS ............ OK");
        }

        #endregion
        #region Configure Swagger

        /// <summary>
        /// Configure Swagger
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration from appsetting.json</param>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("Swagger").Get<SwaggerOption>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Version, new Info
                {
                    Version = options.Version,
                    Title = $"{options.Title} {DateTime.UtcNow:yyyy.MM.dd}",
                    Description = options.Description
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Authorization format : Bearer {token}",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", new string[] { } }
                    });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Blyzer.Api.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Blyzer.Domain.xml"));
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.IgnoreObsoleteProperties();
                c.CustomOperationIds(_ =>
                {
                    var path = _.RelativePath.Split("/");
                    string operationId = "";
                    foreach (var s in path)
                    {
                        operationId += (s.Contains("{")
                                    ? $"By{s.Replace("{", "").Replace("}", "").ToFirstUpperCase()}"
                                    : s.ToFirstUpperCase());
                    }
                    operationId += $"{_.HttpMethod.ToCamelCase()}";
                    return operationId;
                });
                //c.EnableAnnotations();
            });
            Log.Information("{Info}", "Configure Swagger ......... OK");
        }

        #endregion
        #region Configure Identity

        /// <summary>
        /// Configure Identity
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration</param>
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("IdentityOption").Get<AppIdentityOption>();
            services.Configure<IdentityOptions>(identityOptions =>
            {
                #region # Password settings

                identityOptions.Password.RequireDigit = options.Password.RequireDigit;
                identityOptions.Password.RequiredLength = options.Password.RequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = options.Password.RequireNonAlphanumeric;
                identityOptions.Password.RequireUppercase = options.Password.RequireUppercase;
                identityOptions.Password.RequireLowercase = options.Password.RequireLowercase;
                identityOptions.Password.RequiredUniqueChars = options.Password.RequiredUniqueChars;

                #endregion
                #region # Lockout settings

                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(options.Lockout.DefaultLockoutTimeSpan);
                identityOptions.Lockout.MaxFailedAccessAttempts = options.Lockout.MaxFailedAccessAttempts;
                identityOptions.Lockout.AllowedForNewUsers = options.Lockout.AllowedForNewUsers;

                #endregion
                #region # AppUser settings

                identityOptions.User.RequireUniqueEmail = options.User.RequireUniqueEmail;

                #endregion
                #region # SignIn setting

                identityOptions.SignIn.RequireConfirmedEmail = options.SignIn.RequireConfirmedEmail;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = options.SignIn.RequireConfirmedPhoneNumber;

                #endregion
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            //.AddTokenProvider("RefreshToken", typeof(DataProtectorTokenProvider<AppUser>));

            Log.Information("{Info}", "Configure Identity ........ OK");
        }

        #endregion
        #region Configure DbContext

        /// <summary>
        /// Configure DbContext
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration</param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //if (!AppState.FatalError)
            //{
                services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            //}
            Log.Information("{Info}", "Configure DbContext ....... OK");
        }

        #endregion
        #region Configure Serilog
        /// <summary>
        /// Configure Serilog
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration from appsetting.json</param>
        public static void ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole(
                    outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Information("Configure Serilog ......... OK");
        }

        #endregion
        #region Configure Compression

        /// <summary>
        /// Configure Compression
        /// </summary>
        /// <param name="services">Service Collection</param>
        public static void ConfigureCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            Log.Information("{Info}", "Configure Compression ..... OK");
        }

        #endregion
        #region Configure App Services
        /// <summary>
        /// Configure App Services
        /// </summary>
        /// <param name="services">Service Collection</param>
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserInfo, UserInfo>();

            services.AddTransient<IValueRepository, ValueRepository>();


            //services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<IAppRoleRepository, AppRoleRepository>();
            //services.AddTransient<IAppUserRepository, AppUserRepository>();
            //services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            //services.AddTransient<ICountryRepository, CountryRepository>();
            //services.AddTransient<ITrainingRepository, TrainingRepository>();
            //services.AddTransient<ISystemJobRepository, SystemJobRepository>();
            //services.AddTransient<ITournamentRepository, TournamentRepository>();
            //services.AddTransient<ITournamentTypeRepository, TournamentTypeRepository>();
            //services.AddTransient<IMatchRepository, MatchRepository>();
            //services.AddTransient<IMatchToPlayerRepository, MatchToPlayerRepository>();
            //services.AddTransient<IStageRepository, StageRepository>();
            //services.AddTransient<IStageToPlayerRepository, StageToPlayerRepository>();
            //services.AddTransient<IBallRepository, BallRepository>();
            //services.AddTransient<IPlayerRepository, PlayerRepository>();
            //services.AddTransient<ILogRepository, LogRepository>();
            //services.AddTransient<IStatRepository, StatRepository>();
            Log.Information("{Info}", "Configure App Services .... OK");
        }

        #endregion

    }
}