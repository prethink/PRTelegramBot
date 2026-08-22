using System.ComponentModel.DataAnnotations.Schema;

namespace FastBotTemplateConsole.Models
{
    /// <summary>
    /// Custom registration link
    /// For Entity Framework
    /// </summary>
    [Table("register_links")]
    public class RegLink
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Tracking link
        /// </summary>
        [Column("link")]
        public string Link { get; set; }

        /// <summary>
        /// Describes where the person came to the bot from
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Number of registrations
        /// </summary>
        [Column("reg_count")]
        public long RegCount { get; set; }
    }
}
