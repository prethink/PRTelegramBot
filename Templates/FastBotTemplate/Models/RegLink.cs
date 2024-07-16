using System.ComponentModel.DataAnnotations.Schema;

namespace FastBotTemplateConsole.Models
{
    /// <summary>
    /// Кастомные ссылка для регистрации
    /// Для Entity framework
    /// </summary>
    [Table("register_links")]
    public class RegLink
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Ссылка для отслеживания
        /// </summary>
        [Column("link")]
        public string Link { get; set; }

        /// <summary>
        /// Описание откуда пришел человек в бот
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Количество регистраций
        /// </summary>
        [Column("reg_count")]
        public long RegCount { get; set; }
    }
}
