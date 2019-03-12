namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Функції БД
    /// </summary>
    public class StoredFunctionDto
    {
        /// <summary>
        /// Назва функції
        /// </summary>
        public string Proname { get; set; }
        /// <summary>
        /// Код функції
        /// </summary>
        public string Sql { get; set; }
    }
}
