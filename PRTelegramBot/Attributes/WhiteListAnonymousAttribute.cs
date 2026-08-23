﻿using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// The attribute makes the white list settings be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class WhiteListAnonymousAttribute
        : Attribute, IBotIdentificatorAttribute
    {
        #region IBaseQueryAttribute

        /// <inheritdoc />
        public List<long> BotIds { get; set; } = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        public WhiteListAnonymousAttribute(long botId)
        {
            this.BotIds.Add(botId);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        public WhiteListAnonymousAttribute(List<long> botIds)
        {
            this.BotIds.AddRange(botIds);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public WhiteListAnonymousAttribute() : this(0) { }


        #endregion
    }
}
