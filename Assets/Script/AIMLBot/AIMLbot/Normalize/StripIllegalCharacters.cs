using System;
using System.Text.RegularExpressions;
using System.Text;

namespace AIMLbot.Normalize
{
    /// <summary>
    /// Strips any illegal characters found within the input string. Illegal characters are referenced from
    /// the bot's Strippers regex that is defined in the setup XML file.
    /// </summary>
    public class StripIllegalCharacters : AIMLbot.Utils.TextTransformer
    {
        public StripIllegalCharacters(AIMLbot.Bot bot, string inputString) : base(bot, inputString)
        { }

        public StripIllegalCharacters(AIMLbot.Bot bot)
            : base(bot) 
        { }

        protected override string ProcessChange()
        {
            return this.bot.Strippers.Replace(this.inputString, " ");
        }
    }
}
