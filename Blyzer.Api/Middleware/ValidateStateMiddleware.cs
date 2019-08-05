using Blyzer.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blyzer.Api.Middleware
{
    public class ValidateStateMiddleware
    {
        #region # Local variables

        private readonly RequestDelegate _next;

        #endregion
        #region # UserInfoMiddleware конструктор

        /// <summary>
        /// ValidateStateMiddleware constructor
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        public ValidateStateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        public async Task Invoke(HttpContext httpContext)
        {
            if (AppState.FatalError)
            {
                var result = JsonConvert.SerializeObject(new { error = "Startup: Fatal error!" });
                httpContext.Response.ContentType = "application/json";
                //httpContext.Response.StatusCode = 500;

                var res = new ApiResponse(statusCode: 222, message: "message", result: null, errors: new List<string>(new string[] { "element1", "element2", "element3" }));
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(res));
                //await httpContext.Response.WriteAsync(res);

                //await HandleExceptionAsync(httpContext, new Exception("Startup: Fatal error!"));
                Log.Fatal("{FatalError}", "Startup: Fatal error!");
            }
            else
                await _next.Invoke(httpContext);
        }

        //private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        //{
        //    var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        //    if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
        //    else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
        //    else if (ex is MyException) code = HttpStatusCode.BadRequest;

        //    var result = JsonConvert.SerializeObject(new { error = ex.Message });
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)code;
        //    return context.Response.WriteAsync(result);
        //}
    }
}
