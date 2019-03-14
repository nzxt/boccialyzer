namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Типи кидків
    /// </summary>
    public enum ShotType : int
    {
        /// <summary>
        /// First ball delivered after the jack
        /// </summary>
        FirstBall,
        /// <summary>
        /// A shot played to deliver a ball in a specific area
        /// </summary>
        Placement,
        /// <summary>
        /// A shot in which the player pushes own ball closer into a target area e.g. jack ball or scoring space
        /// </summary>
        PushOn,
        /// <summary>
        /// A shot in which the player pushes opposition ball away from the specific target
        /// </summary>
        PushOff,
        /// <summary>
        /// Predominently power shot along the ground
        /// </summary>
        Smash,
        /// <summary>
        /// A shot played to rebound off a ball into a scoring zone
        /// </summary>
        Ricochet,
        /// <summary>
        /// An aerial trajectory shot aimed at bouncing over a barrier ball to reach a specific target
        /// </summary>
        BounceOver,
        /// <summary>
        /// A shot with the intention of playing the ball and it remaining on top of another ball
        /// </summary>
        RollOnTop,
        /// <summary>
        /// A shot with the intention of the ball rolling up and over
        /// </summary>
        RollUpAndOver,
        /// <summary>
        /// An aerial shot played to move a target ball by attacking over a ball
        /// </summary>
        LobbingShot
    }
}