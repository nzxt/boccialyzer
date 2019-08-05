namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Gets or sets the SignInOptions for the identity system.
    /// </summary>
    public class AppIdentitySignIn
    {
        /// <summary>
        /// Gets or sets a flag indicating whether a confirmed email address is required to sign in. Defaults to false.
        /// </summary>
        public bool RequireConfirmedEmail { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating whether a confirmed telephone number is required to sign in. Defaults to false.
        /// </summary>
        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}
