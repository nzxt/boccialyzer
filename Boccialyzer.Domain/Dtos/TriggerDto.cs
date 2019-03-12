namespace Boccialyzer.Domain.Dtos
{
    /// <summary>
    /// Тригери БД
    /// </summary>
    public class TriggerDto
    {
        /// <summary>
        /// Назва тригера
        /// </summary>
        public string Tgname { get; set; }
        /// <summary>
        /// Код тригера
        /// </summary>
        public string Sql { get; set; }
    }
}
