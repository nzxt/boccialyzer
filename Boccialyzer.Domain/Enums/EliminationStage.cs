namespace Boccialyzer.Domain.Enums
{
    /// <summary>
    /// Етап на вибування
    /// </summary>
    public enum EliminationStage : int
    {
        /// <summary>
        /// Не визначено
        /// </summary>
        None,
        /// <summary>
        /// 1/8 фыналу
        /// </summary>
        Final8,
        /// <summary>
        /// Чвертьфінал
        /// </summary>
        Final4,
        /// <summary>
        /// Півфінал
        /// </summary>
        Final2,
        /// <summary>
        /// Bronze Final
        /// </summary>
        BronzeFinal,
        /// <summary>
        /// Final
        /// </summary>
        Final
    }
}