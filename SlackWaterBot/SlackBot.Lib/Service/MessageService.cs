using SlackBot.Lib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlackBot.Lib.Service
{
    public class MessageService
    {
        private string _rawMessage { get; set; }

        private string[] _message { get; set; }

        public string BotId { get; private set; }

        public MessageService(string message, string botId)
        {
            _rawMessage = message.ToLower();
            _message = _rawMessage.RemoveManySpaces().Split(' ');
            BotId = botId;
        }
        public string MessageWithoutBotMention()
        {
            return string.Join(" ", _message.Where(m => !m.ContainsIgnoringCase(BotId)));
        }

        public string MessageWithoutBotMention(string actionToRemove)
        {
            return string.Join(" ", _message.Where(m => !m.ContainsIgnoringCase(BotId) && !m.ContainsIgnoringCase(actionToRemove)));
        }

        public bool HasMentionedBot()
        {
            return Regex.Match(_rawMessage, BotId, RegexOptions.IgnoreCase).Success;
        }

        public bool HasAnyCommand()
        {
            return MessageWithoutBotMention().Count() > 0;
        }

    }
}
