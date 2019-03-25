namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Типи м'ячів поза грою
    /// </summary>
    public enum DeadBallType
    {
        /// <summary>
        /// Не визначено
        /// </summary>
        None,
        /// <summary>
        /// Balls Not Thrown (the athlete elect not to throw)
        /// </summary>
        BallsNotThrown,
        /// <summary>
        /// Violation
        /// </summary>
        Violation,
        /// <summary>
        /// Time is out
        /// </summary>
        TimeIsOut
    }
}