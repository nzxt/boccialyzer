using Blyzer.Domain.Enums;
using Serilog;

namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Service execution result
    /// </summary>
    public class ApiResult
    {
        public ApiResult(ApiResultStatus status, object result = null, string error = null)
        {
            Status = status;
            Result = result;
            Error = error;
            if (status == ApiResultStatus.Error)
                Log.Error("{ApiError}", error);
            if (status == ApiResultStatus.Warning)
                Log.Error("{ApiWarning}", error);
        }
        /// <summary>
        /// Execution status
        /// </summary>
        public ApiResultStatus Status { get; set; }
        /// <summary>
        /// Execution result
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}
