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
                var user = await userManager.FindByNameAsync(userInfo.UserName);//.Result;
                userInfo.AppUserId = user.Id;
                var roles = await userManager.GetRolesAsync(user);
                userInfo.RoleName = roles.FirstOrDefault();

                //var rrrr = RoleEnum.

                //if (Enum.TryParse(typeof(RoleEnum), userInfo.RoleName, true, out var roleId))
                //    userInfo.RoleId = (Guid)roleId;
                //else
                //    userInfo.RoleId = default(Guid);
            }
            await _next.Invoke(httpContext);
        }
    }
}
