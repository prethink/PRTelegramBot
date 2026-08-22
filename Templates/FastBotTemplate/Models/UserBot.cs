using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleExample.Models
{
    /// <summary>
    /// User
    /// For Entity Framework
    /// </summary>
    [Table("users")]
    public class UserBot
    {
        /// <summary>
        /// Telegram identifier
        /// </summary>
        [Key]
        [Column("telegram_id")]
        public long TelegramId { get; set; }

        /// <summary>
        /// Reference to the user who brought this one to the bot
        /// </summary>
        [Column("parent_user_id")]
        public long? ParentUserId { get; set; }
        public UserBot? ParentUser { get; set; }

        /// <summary>
        /// Registration date
        /// </summary>
        [Column("registered_date")]
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// Date of the last activity
        /// </summary>
        [Column("last_activity")]
        public DateTime LastActivity { get; set; }

        /// <summary>
        /// Login 
        /// </summary>
        [Column("login")]
        public string? Login { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Column("firstname")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Column("lastname")]
        public string? LastName { get; set; }

        /// <summary>
        /// Whether the user is banned
        /// </summary>
        [Column("is_ban")]
        public bool IsBan { get; set; }

        /// <summary>
        /// Whether the account is activated
        /// </summary>
        [Column("is_active")]
        public bool IsActivate { get; set; }

        /// <summary>
        /// The user's activity points
        /// </summary>
        [Column("activity")]
        public long Activity { get; set; }

        /// <summary>
        /// Personal link
        /// </summary>
        [Column("link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets the user name
        /// </summary>
        /// <returns>User name</returns>
        public string GetName()
        {
            if (!string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName))
            {
                string tempName = string.Empty;
                tempName += FirstName + " ";
                tempName += LastName;
                return tempName;
            }
            else if (!string.IsNullOrEmpty(Login))
            {
                return Login;
            }
            return "Name is not set";
        }

        /// <summary>
        /// Adds activity points
        /// </summary>
        /// <param name="activity">Activity points</param>
        public void AddActivity(long activity)
        {
            Activity += activity;
            LastActivity = DateTime.Now;
        }
    }
}
