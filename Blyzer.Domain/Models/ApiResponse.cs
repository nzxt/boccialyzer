using System.Collections.Generic;
using Blyzer.Domain.Enums;
using Newtonsoft.Json;
using Serilog;

namespace Blyzer.Domain.Models
{
    /// <summary>
    /// ApiResponse
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; }
        /// <summary>
        /// Message
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        /// <summary>
        /// Result
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; }
        /// <summary>
        /// Errors
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// ApiResponse constructor
        /// </summary>
        /// <param name="statusCode">StatusCode</param>
        /// <param name="message">Message (optional)</param>
        /// <param name="result"></param>
        /// <param name="errors"></param>
        public ApiResponse(int statusCode, string message = null, object result = null, IEnumerable<string> errors = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Result = result;
            Errors = errors;
            if (errors != null)
                Log.Error("{ApiResponse}", JsonConvert.SerializeObject(errors));
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Successful execution";
                case 201:
                    return "Created successfully";
                case 202:
                    return "Modified successfully";
                case 204:
                    return "Deleted successfully";
                case 404:
                    return "Resource not found";
                case 422:
                    return "Missing parameters";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}
