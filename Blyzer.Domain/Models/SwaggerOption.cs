namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Swagger Option
    /// </summary>
    public class SwaggerOption
    {
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Route Prefix
        /// </summary>
        public string RoutePrefix { get; set; }
        /// <summary>
        /// Endpoint Path
        /// </summary>
        public string EndpointPath { get; set; }
        /// <summary>
        /// Endpoint Description
        /// </summary>
        public string EndpointDescription { get; set; }
    }
}
