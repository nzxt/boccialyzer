using System;

namespace Blyzer.Domain.Models
{
    public class AppError : Exception
    {
        public int StatusCode { get; set; }

        //public ValidationErrorCollection Errors { get; set; }
    }
}
