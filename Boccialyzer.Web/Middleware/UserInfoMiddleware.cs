using Boccialyzer.Core.Repository;
using Boccialyzer.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Boccialyzer.Web.Middleware
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
        /// UserInfoMiddleware конструктор
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
        /// <param name="userInfo"></param>
        /// <param name="userManager"></param>
        public async Task Invoke(HttpContext httpContext, IUserInfo userInfo, UserManager<AppUser> userManager)
        {
            var tryGetValue = httpContext.Request.Headers.TryGetValue("Locale", out var locale);
            if (tryGetValue) userInfo.Locale = locale;
            else userInfo.Locale = "UA";

            if (httpContext.User.Identity.IsAuthenticated)
            {
                userInfo.UserName = httpContext.User?.Identity?.Name;
                userInfo.IpAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                var user = await userManager.FindByNameAsync(userInfo.UserName);
                userInfo.AppUserId = user.Id;
                userInfo.Roles = await userManager.GetRolesAsync(user);
            }
            await _next.Invoke(httpContext);
        }
    }
}
