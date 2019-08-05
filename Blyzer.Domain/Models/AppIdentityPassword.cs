namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Specifies options for password requirements.
    /// </summary>
    public class AppIdentityPassword
    {
        /// <summary>
        /// Gets or sets a flag indicating if passwords must contain a digit. Defaults to true.
        /// </summary>
        public bool RequireDigit { get; set; }
        /// <summary>
        /// Gets or sets the minimum length a password must be. Defaults to 6.
        /// </summary>
        public int RequiredLength { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating if passwords must contain a lower case ASCII character. Defaults to true.
        /// </summary>
        public bool RequireLowercase { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating if passwords must contain a non-alphanumeric character. Defaults to true.
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; }
        /// <summary>
        /// Gets or sets the minimum number of unique chars a password must comprised of. Defaults to 1.
        /// </summary>
        public int RequiredUniqueChars { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating if passwords must contain a upper case ASCII character. Defaults to true.
        /// </summary>
        public bool RequireUppercase { get; set; }
    }
}
