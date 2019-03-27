using Boccialyzer.Core;
using Boccialyzer.Core.Context;
using Boccialyzer.Core.Repository;
using Boccialyzer.Core.SeedData;
using Boccialyzer.Domain.Entities;
using Boccialyzer.Domain.Models;
using Boccialyzer.Web;
using Boccialyzer.Web.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer
{
    /// <summary>
    /// Точка входу
    /// </summary>
    public class Program
    {
        #region # Static Var

        /// <summary>
        ///     Провайдер змінних оточення
        /// </summary>
        public static IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        ///     Провайдер налаштувань
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        #endregion
        #region # Main

        /// <summary>
        ///     Головний метод
        /// </summary>
        /// <param name="args">Аргументи запуску</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var isDevelopment = HostingEnvironment.IsDevelopment();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                Log.Information("{Info}", "Застосування міграцій");
                services.GetService<ApplicationDbContext>().ApplyMigrationsAsync().Wait();
                Log.Information("{Info}", "Додавання данних");
                AppRoleData.Seed(services).Wait();
                AppUserData.Seed(services, isDevelopment).Wait();
                services.GetService<ApplicationDbContext>().EnsureSeededAsync(isDevelopment).Wait();
                services.GetService<ApplicationDbContext>().EnsureSeededAfterAsync(isDevelopment).Wait();
            }
            Log.Information("{Info}", "Запуск сервісу");
            host.Run();
        }

        #endregion

        #region # CreateWebHostBuilder

        /// <summary>
        ///     Налаштування хоста
        /// </summary>
        /// <param name="args">Аргументи</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                HostingEnvironment = env;

                #region # Read configs

                config
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables("ASPNETCORE_")
                .AddCommandLine(args);
                Configuration = config.Build();

                #endregion

                #region # Serilog config

                Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                //.Enrich.With<EventTypeEnricher>()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole(
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

                #endregion

                #region # ConnectionString config

                AppState.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(AppState.ConnectionString))
                {
                    AppState.FatalError = true;
                    Log.Fatal("Помилка отримання рядка підключення до БД!");
                }

                #endregion
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(AppState.ConnectionString));
                services.Configure<AppAuthOption>(options => Configuration.GetSection("AppAuthOption").Bind(options));

                #region # Identity config

                services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

                var cookiesOption = Configuration.GetSection("AppCookiesOption").Get<AppCookiesOption>();
                services.Configure<IdentityOptions>(options =>
                {
                    #region # Password settings

                    options.Password.RequireDigit = cookiesOption.Password.RequireDigit;
                    options.Password.RequiredLength = cookiesOption.Password.RequiredLength;
                    options.Password.RequireNonAlphanumeric = cookiesOption.Password.RequireNonAlphanumeric;
                    options.Password.RequireUppercase = cookiesOption.Password.RequireUppercase;
                    options.Password.RequireLowercase = cookiesOption.Password.RequireLowercase;
                    options.Password.RequiredUniqueChars = cookiesOption.Password.RequiredUniqueChars;

                    #endregion
                    #region # Lockout settings

                    options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(cookiesOption.Lockout.DefaultLockoutTimeSpan);
                    options.Lockout.MaxFailedAccessAttempts = cookiesOption.Lockout.MaxFailedAccessAttempts;
                    options.Lockout.AllowedForNewUsers = cookiesOption.Lockout.AllowedForNewUsers;

                    #endregion
                    #region # AppUser settings

                    options.User.RequireUniqueEmail = cookiesOption.User.RequireUniqueEmail;

                    #endregion
                    #region # SignIn setting

                    options.SignIn.RequireConfirmedEmail = cookiesOption.SignIn.RequireConfirmedEmail;
                    options.SignIn.RequireConfirmedPhoneNumber = cookiesOption.SignIn.RequireConfirmedPhoneNumber;

                    #endregion
                });

                #endregion
                #region # Cookie config

                services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(cookiesOption.ExpireTimeSpan);
                    options.SlidingExpiration = true;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                                ctx.Response.StatusCode = 401;
                            else
                                ctx.Response.Redirect(ctx.RedirectUri);

                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                                ctx.Response.StatusCode = 403;
                            else
                                ctx.Response.Redirect(ctx.RedirectUri);

                            return Task.CompletedTask;
                        }
                    };
                });

                #endregion
                #region # Config CORS

                var origins = Configuration["CorsOrigins"].Split(',').ToArray();
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder
                                .WithOrigins(origins)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                });

                #endregion
                #region # Set options

                services.Configure<AppOptionModel>(options =>
                {
                    try
                    {
                        #region option: ConnectionString

                        var tempValue = Configuration.GetConnectionString("DefaultConnection");
                        if (!string.IsNullOrEmpty(tempValue))
                        {
                            options.ConnectionString = tempValue;
                            AppState.FatalError = false;
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
                        Log.Fatal("{Fatal}{ErrorMessage}", "Помилка отримання налаштувань імпортера (AppOptions)",
                            ex.Message);
                    }
                });

                #endregion

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

                #region # Config reposirtories

                services.AddScoped<IUserInfo, UserInfo>();
                services.AddTransient<IAccountRepository, AccountRepository>();
                services.AddTransient<IAppRoleRepository, AppRoleRepository>();
                services.AddTransient<IAppUserRepository, AppUserRepository>();
                services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
                services.AddTransient<ICountryRepository, CountryRepository>();
                services.AddTransient<ISendGridRepository, SendGridRepository>();
                services.AddTransient<ISystemJobRepository, SystemJobRepository>();
                services.AddTransient<ITournamentRepository, TournamentRepository>();
                services.AddTransient<ITournamentTypeRepository, TournamentTypeRepository>();
                services.AddTransient<IMatchRepository, MatchRepository>();
                services.AddTransient<IStageRepository, StageRepository>();
                services.AddTransient<IBallRepository, BallRepository>();
                services.AddTransient<IPlayerRepository, PlayerRepository>();
                services.AddTransient<ILogRepository, LogRepository>();

                #endregion

                #region # Config Swagger Generator

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = $"Boccialyzer API Version 1 Build {DateTime.UtcNow:yyyy.MM.dd}",
                        Description = "Boccialyzer Web API"
                    });
                    //c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Boccialyzer.Web.xml"));
                    //c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Boccialyzer.Domain.xml"));

                    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    //c.IncludeXmlComments(xmlPath);
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Boccialyzer.Web.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Boccialyzer.Domain.xml"));

                    c.DescribeAllEnumsAsStrings();
                    c.DescribeStringEnumsInCamelCase();
                    c.IgnoreObsoleteProperties();
                    //c.CustomOperationIds(_ => $"{_.ActionDescriptor.RouteValues["controller"]}{_.ActionDescriptor.RouteValues["action"]}{_.HttpMethod.ToCamelCase()}");
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

                #endregion

                //services.AddAuthorization(options =>
                //{
                //    options.AddPolicy("ElevatedRights", policy =>
                //policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator"));
                //});

                services.AddOptions();
            })
            .Configure(app =>
            {
                app.UseStaticFiles();
                app.UseCors("AllowAll");
                app.UseAuthentication();
                app.UseMiddleware<UserInfoMiddleware>();

                #region # Enable middleware to Swagger and SwaggerUI

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
                    c.RoutePrefix = "api-docs";
                    c.DocExpansion(DocExpansion.None);
                    c.DefaultModelRendering(ModelRendering.Model);
                    c.DisplayOperationId();
                });

                #endregion

                app.UseMvc();
            })
            .UseKestrel((builderContext, options) =>
            {
                options.Configure(builderContext.Configuration.GetSection("Kestrel"));
            })
            .UseSerilog();

        #endregion
    }
}
