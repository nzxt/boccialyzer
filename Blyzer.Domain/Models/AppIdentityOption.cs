namespace Blyzer.Domain.Models
{
    /// <summary>
    /// Represents all the options you can use to configure the identity system.
    /// </summary>
    public class AppIdentityOption
    {
        /// <summary>
        /// Expire TimeSpan
        /// </summary>
        public int ExpireTimeSpan { get; set; }
        /// <summary>
        /// Specifies options for password requirements.
        /// </summary>
        public AppIdentityPassword Password { get; set; }
        /// <summary>
        /// Gets or sets the LockoutOptions for the identity system.
        /// </summary>
        public AppIdentityLockout Lockout { get; set; }
        /// <summary>
        /// Gets or sets the UserOptions for the identity system.
        /// </summary>
        public AppIdentityUser User { get; set; }
        /// <summary>
        /// Gets or sets the SignInOptions for the identity system.
        /// </summary>
        public AppIdentitySignIn SignIn { get; set; }
    }
}
