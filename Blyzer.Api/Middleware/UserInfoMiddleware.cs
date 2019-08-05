using Blyzer.Domain.Entities;
using Blyzer.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Serilog;

namespace Blyzer.Api.Middleware
{
    /// <summary>
    /// Інформація про користувача та його налаштування
    /// </summary>
    public class UserInfoMiddleware
    {
        #region # Local variables

        private readonly RequestDelegate _next;

        #endregion
        #region # UserInfoMiddleware конструктор

        /// <summary>
        /// UserInfoMiddleware constructor
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="userInfo">UserInfo</param>
        /// <param name="userManager">UserManager</param>
        public async Task Invoke(HttpContext httpContext, IUserInfo userInfo, UserManager<AppUser> userManager)
        {
            try
            {

                var tryGetValue = httpContext.Request.Headers.TryGetValue("Locale", out var locale);
                if (tryGetValue) userInfo.Locale = locale;
                else userInfo.Locale = "UA";

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    userInfo.UserName = httpContext.User?.Identity?.Name;
                    userInfo.IpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    if (Guid.TryParse(httpContext.User.FindFirstValue("id"), out Guid id)) userInfo.AppUserId = id;
                    if (bool.TryParse(httpContext.User.FindFirstValue("is_admin"), out bool isAdmin)) userInfo.IsAdmin = isAdmin;
                    userInfo.Role = httpContext.User.FindFirstValue(ClaimTypes.Role);
                }
#if DEBUG
                userInfo.Locale = "UA";
                        userInfo.UserName = "admin";
                userInfo.IpAddress = "192.168.1.1";

                userInfo.AppUserId = new Guid("8f666c33-f0c1-41a9-bc70-8628bfe521b5");
                userInfo.IsAdmin = true;
                userInfo.Role = "Administrator";

#endif
                await _next.Invoke(httpContext);

            }
            catch (Exception exception)
            {
                Log.Error("{SeedError}", exception.Message);
            }

        }
    }
}
