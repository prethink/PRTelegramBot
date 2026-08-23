﻿using FastBotTemplateConsole.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FastBotTemplateConsole.Models
{

    /// <summary>
    /// Ads / announcements.
    /// Entity framework
    /// </summary>
    [Table("Announcements")]
    public class Announcement
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Link to the media — a photo or a video.
        /// </summary>
        [Column("media")]
        public string? Media { get; set; }

        /// <summary>
        /// Text of the ad message.
        /// </summary>
        [Column("text")]
        public string Text { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Whether it is active.
        /// </summary>
        [Column("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Data for the menu.
        /// </summary>
        [Column("menu_data")]
        public string? MenuData { get; set; }

        /// <summary>
        /// Type of the ad menu.
        /// </summary>
        [Column("menu_type")]
        public MenuType MenuType { get; set; }

        /// <summary>
        /// Type of the ad message.
        /// </summary>
        [Column("message_type")]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Minimum age the ad is shown to.
        /// </summary>
        [Column("start_age")]
        public long? StartAge { get; set; }

        /// <summary>
        /// Maximum age the ad is shown to.
        /// </summary>
        [Column("end_age")]
        public long? EndAge { get; set; }

        /// <summary>
        /// The list of tags, separated by ";".
        /// </summary>
        [Column("tags")]
        public string? Tags { get; set; }

        /// <summary>
        /// Number of views.
        /// </summary>
        [Column("viewed")]
        public long Viewed { get; set; }

        /// <summary>
        /// Generates an inline menu with links if MenuData holds menu data.
        /// </summary>
        /// <returns>The menu, or an empty list.</returns>
        public List<InlineURL> GetMenu()
        {
            try
            {
                return JsonSerializer.Deserialize<List<InlineURL>>(MenuData ?? string.Empty);
            }
            catch
            {
                return new List<InlineURL>();
            }

        }

        /// <summary>
        /// Serialization of the inline menu.
        /// </summary>
        /// <param name="menu">Menu</param>
        /// <returns></returns>
        public static string WriteMenu(List<IInlineContent> menu)
        {
            try
            {
                return JsonSerializer.Serialize(menu);
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
