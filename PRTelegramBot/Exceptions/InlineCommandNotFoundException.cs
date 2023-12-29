using PRTelegramBot.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Exceptions
{
    public class InlineCommandNotFoundException : Exception
    {
        public InlineCommandNotFoundException(Enum @enum) 
            : base($"{@enum.GetType().Name}.{@enum} Inline command not found in collection. " +
                   $"Required add attribute [{nameof(InlineCommandAttribute)}] to the enum {@enum.GetType().Name}.")
        {
        }
    }
}
