using System;
using System.Collections.Generic;
using System.Text;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SendGridEmailModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HtmlContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TextContent { get; set; }
    }
}
