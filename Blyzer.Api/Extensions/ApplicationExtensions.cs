using Blyzer.Api.Middleware;
using Blyzer.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Blyzer.Api.Extensions
{
    /// <summary>
    /// Application Extensions
    /// </summary>
    public static class ApplicationExtensions
    {
        #region UseSwaggerMiddleware

        /// <summary>
        /// UseSwaggerMiddleware
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        /// <param name="configuration">Configuration</param>
        public static void UseSwaggerMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = configuration.GetSection("Swagger").Get<SwaggerOption>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(options.EndpointPath, options.EndpointDescription);
                c.RoutePrefix = options.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DisplayOperationId();
            });
        }

        #endregion
        #region UseAppMiddleware

        /// <summary>
        /// UseAppMiddleware
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        public static void UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ValidateStateMiddleware>();
            app.UseMiddleware<UserInfoMiddleware>();
        }

        #endregion
    }
}
