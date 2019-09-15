using AutoMapper;
using Blyzer.Api.Extensions;
using Blyzer.Api.Middleware;
using Blyzer.Dal.Context;
using Blyzer.Dal.Extensions;
using Blyzer.Domain.Entities;
using Blyzer.Domain.Mappers;
using Blyzer.Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;

namespace Blyzer.Api
{
    /// <summary>
    /// Main class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application configuration options
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Hosting Environment
        /// </summary>
        public static IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            AppState.FatalError = false;
            var host = CreateWebHostBuilder(args).Build();
            var isDevelopment = HostingEnvironment.IsDevelopment();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                services.GetService<AppDbContext>().ApplyMigrationsAsync().Wait();
                services.GetService<AppDbContext>().EnsureSeededAsync(services.GetService<RoleManager<AppRole>>()
                    , services.GetService<UserManager<AppUser>>()).Wait();
                if (isDevelopment)
                    services.GetService<AppDbContext>().EnsureSeededDevAsync().Wait();
            }
            Log.Information("{Info}", "Running service...");
            host.Run();
        }

        /// <summary>
        /// IWebHost Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())

                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    HostingEnvironment = hostingContext.HostingEnvironment;
                    Configuration = config.Build();
                })

                .ConfigureServices((hostingContext, services) =>
                {
                    try
                    {
                        services.AddOptions();
                        services.ConfigureSerilog(Configuration);
                        services.ConfigureAppConfiguration(Configuration);
                        services.ConfigureDbContext(Configuration);
                        services.ConfigureIdentity(Configuration);
                        services.ConfigureCors(Configuration);
                        services.ConfigureSwagger(Configuration);


                        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                            .AddFluentValidation();

                        services.AddAutoMapper(typeof(BallMapper));

                        //services.AddTransient<IValidator<RequestParameterValidator>, RequestParametersModel>();

                        services.ConfigureAppServices();
                        services.ConfigureCompression();
                    }
                    catch (Exception ex)
                    {
                        AppState.FatalError = true;
                        //Console.WriteLine(ex);
                        //throw;
                    }
                })
                .Configure(app =>
                {
                    app.UseAppMiddleware();

                    var env = app.ApplicationServices.GetService<IHostingEnvironment>();

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        app.UseExceptionHandler(errorApp =>
                        {
                            errorApp.Run(async context =>
                            {
                                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                                var exception = exceptionHandlerPathFeature.Error;

                                Log.Error("{AppError}", exception.Message);

                                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(result);
                            });
                        });
                        app.UseHsts();
                    }
                    app.UseStaticFiles();
                    app.UseCors("AllowAll");
                    app.UseAuthentication();
                    app.UseMiddleware<UserInfoMiddleware>();
                    app.UseSwaggerMiddleware(Configuration);
                    app.UseHttpsRedirection();
                    app.UseResponseCompression();
                    app.UseMvc();

                })
                .UseKestrel((context, options) =>
                {
                    options.Configure(context.Configuration.GetSection("Kestrel"));
                    //options.Listen(IPAddress.Loopback, context.Configuration.GetValue<int>("Kestrel:Port"));
                })
                .UseSerilog();
    }
}
