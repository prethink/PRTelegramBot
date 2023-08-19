using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleExample.Models
{
    /// <summary>
    /// Пользователь
    /// Для Entity framework
    /// </summary>
    [Table("users")]
    public class UserBot
    {
        /// <summary>
        /// Идентфикатор телеграм
        /// </summary>
        [Key]
        [Column("telegram_id")]
        public long TelegramId { get; set; }

        /// <summary>
        /// Ссылка на пользователя который привел в бот
        /// </summary>
        [Column("parent_user_id")]
        public long? ParentUserId { get; set; }
        public UserBot? ParentUser { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [Column("registered_date")]
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// Дата последней активности
        /// </summary>
        [Column("last_activity")]
        public DateTime LastActivity { get; set; }

        /// <summary>
        /// Логин 
        /// </summary>
        [Column("login")]
        public string? Login { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Column("firstname")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("lastname")]
        public string? LastName { get; set; }

        /// <summary>
        /// Забанен или нет
        /// </summary>
        [Column("is_ban")]
        public bool IsBan { get; set; }

        /// <summary>
        /// Активированная учетка или нет
        /// </summary>
        [Column("is_active")]
        public bool IsActivate { get; set; }

        /// <summary>
        /// Очки активности пользователя
        /// </summary>
        [Column("activity")]
        public long Activity { get; set; }

        /// <summary>
        /// Персональная ссылка
        /// </summary>
        [Column("link")]
        public string Link { get; set; }

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        /// <returns>Имя пользователя</returns>
        public string GetName()
        {
            if (!string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(LastName))
            {
                string tempName = "";
                tempName += FirstName + " ";
                tempName += LastName;
                return tempName;
            }
            else if (!string.IsNullOrEmpty(Login))
            {
                return Login;
            }
            return "Имя не определено";
        }

        /// <summary>
        /// Добавляет очки активности
        /// </summary>
        /// <param name="activity">Очки активности</param>
        public void AddActivity(long activity)
        {
            Activity += activity;
            LastActivity = DateTime.Now;
        }
    }
}
