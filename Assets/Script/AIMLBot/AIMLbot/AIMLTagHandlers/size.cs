using System;
using System.Xml;
using System.Text;

namespace AIMLbot.AIMLTagHandlers
{
    /// <summary>
    /// The size element tells the AIML interpreter that it should substitute the number of 
    /// categories currently loaded.
    /// 
    /// The size element does not have any content. 
    /// </summary>
    public class size : AIMLbot.Utils.AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public size(AIMLbot.Bot bot,
                        AIMLbot.User user,
                        AIMLbot.Utils.SubQuery query,
                        AIMLbot.Request request,
                        AIMLbot.Result result,
                        XmlNode templateNode)
            : base(bot, user, query, request, result, templateNode)
        {
        }

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "size")
            {
                return Convert.ToString(this.bot.Size);
            }
            return string.Empty;
        }
    }
}
