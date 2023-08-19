using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Interface;
using PRTelegramBot.Models.InlineButtons;
using ConsoleExample.Models.Enums;

namespace ConsoleExample.Models.DataBase
{

    /// <summary>
    /// Реклама / объявления
    /// Entity framework
    /// </summary>
    [Table("Announcements")]
    public class Announcement
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на медиа, фото, видео
        /// </summary>
        [Column("media")]
        public string? Media { get; set; }

        /// <summary>
        /// Текст сообщения рекламы
        /// </summary>
        [Column("text")]
        public string Text { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Активна или нет
        /// </summary>
        [Column("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Данные для меню
        /// </summary>
        [Column("menu_data")]
        public string? MenuData { get; set; }

        /// <summary>
        /// Тип меню рекламы
        /// </summary>
        [Column("menu_type")]
        public MenuType MenuType { get; set; }

        /// <summary>
        /// Тип сообщения рекламы
        /// </summary>
        [Column("message_type")]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Минимальный возраст для показа рекламы
        /// </summary>
        [Column("start_age")]
        public long? StartAge { get; set; }

        /// <summary>
        /// Максимальный возраст для показа рекламы
        /// </summary>
        [Column("end_age")]
        public long? EndAge { get; set; }

        /// <summary>
        /// Список тегов, перечесление через ;
        /// </summary>
        [Column("tags")]
        public string? Tags { get; set; }

        /// <summary>
        /// Количество просмотров
        /// </summary>
        [Column("viewed")]
        public long Viewed { get; set; }

        /// <summary>
        /// Генерирует inline меню со ссылками есть есть данные для меню в MenuData
        /// </summary>
        /// <returns>Возращает меню или пустой список</returns>
        public List<InlineURL> GetMenu()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<InlineURL>>(MenuData ?? "");
            }
            catch (Exception ex)
            {
                return new List<InlineURL>();
            }

        }

        /// <summary>
        /// Сериази
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static string WriteMenu(List<IInlineContent> menu)
        {
            try
            {
                return JsonConvert.SerializeObject(menu);
            }
            catch(Exception ex)
            {
                return "";
            }
        }

    }
}
