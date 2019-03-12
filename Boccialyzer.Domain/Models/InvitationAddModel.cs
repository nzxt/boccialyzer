using System.Collections.Generic;
using System.ComponentModel;

namespace Boccialyzer.Domain.Models
{
    /// <summary>
    /// Модель створення запрошень
    /// </summary>
    public class InvitationAddModel
    {
        /// <summary>
        /// Зберегти та надіслати
        /// </summary>
        [DefaultValue(false)]
        public bool SaveAndSend { get; set; }
        /// <summary>
        /// Адреса електропошти
        /// </summary>
        public IEnumerable<string> Emails { get; set; }
    }
}
