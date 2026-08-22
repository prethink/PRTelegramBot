using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Attribute that enables slash (/) commands.
    /// </summary>
    public sealed class SlashHandlerAttribute : StringQueryAttribute
    {

        #region Fields and properties

        /// <summary>
        /// Separator character.
        /// </summary>
        public char SplitChar { get; private set; } = default;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(char splitChar, params string[] commands)
            : this(0, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) {  }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(params string[] commands)
            : this(0, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, params string[] commands)
            : this(botId, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, char splitChar, params string[] commands)
            : this(botId, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, params string[] commands)
            : this(botIds, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, default, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, char splitChar, params string[] commands)
            : this(botIds, CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(0, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(botId, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, default, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, char splitChar, params string[] commands)
            : this(botIds, commandComparison, StringComparison.OrdinalIgnoreCase, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(StringComparison stringComparison, params string[] commands)
            : this(0, CommandComparison.Contains, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(StringComparison stringComparison, char splitChar, params string[] commands)
            : this(0, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
            : this(botId, CommandComparison.Contains, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(botId, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
            : this(botIds, CommandComparison.Contains, stringComparison, default, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(botIds, CommandComparison.Contains, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this(0, commandComparison, stringComparison, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : this(0, commandComparison, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
            : this([botId], commandComparison, stringComparison, default, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botId">Bot identifier.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : this([botId], commandComparison, stringComparison, splitChar, commands) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="commandComparison">How to compare the command.</param>
        /// <param name="stringComparison">How to compare the string.</param>
        /// <param name="splitChar">Separator character.</param>
        /// <param name="commands">Commands.</param>
        public SlashHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, char splitChar, params string[] commands)
            : base(botIds, commandComparison, stringComparison)
        {
            this.SplitChar = splitChar;

            foreach (var command in commands)
            {
                var formatedCommand = command.StartsWith('/') 
                    ? command 
                    : "/" + command;

                this.commands.Add(formatedCommand);
            }
        }

        #endregion
    }
}
