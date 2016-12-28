using Newtonsoft.Json.Linq;
using SlackBot.Lib.API;
using SlackBot.Lib.Extensions;
using SlackBot.Lib.Implementation;
using SlackBot.Lib.Model;
using SlackBot.Lib.Resources;
using SlackBot.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SlackBot.Bots
{
    public class WaterBot : SlackBotClient
    {
        public WaterBot(string token) : base(token)
        {
            Identity.UserIcon = ":droplet:";
            Identity.Username = "waterBot |BETA|";
            OnChannelJoined += _joinedChannel;
            OnMessage += _parseMessage;
        }
        
        private Dictionary<string, Action<SlackMessage>> Actions {
            get
            {
                var dic = new Dictionary<string, Action<SlackMessage>>();
                //dic.Add("Start", (sM) => Start(sM));
                dic.Add("+1", (sM) => AddOne(sM));
                dic.Add("-1", (sM) => RemoveOne(sM));
                return dic;
            }
        }

        private const string endLineMark = "\n ";

        private MessageService MessageService { get; set; }

        private List<WaterMember> Members { get; set; }

        private void _joinedChannel(object sender, SlackChannel channel)
        {
            SendMessage(channel,
                "Oi, eu sou o `" + BotUser.Name + "`." +
                "Estou aqui para ajudar.\n" +
                "Para obter a lista de comandos, digite: `<@" + BotId + "|" + BotUser.Name + "> ajuda`");
        }

        private void _parseMessage(object sender, SlackMessage slackMessage)
        {
            try
            {
                //If its not a bot(most common case) continues already
                if (!slackMessage.IsBot())
                    SendSlackMessage(slackMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO NA EXECUÇÃO: ");
                Console.WriteLine(ex.Message);
            }
        }

        private void SendSlackMessage(SlackMessage slackMessage)
        {
            // returns void if message is empty
            if (string.IsNullOrWhiteSpace(slackMessage.Text))
                return;

            MessageService = new MessageService(slackMessage.Text, BotUser.Id);

            // Bot only works in groups
            if (!slackMessage.IsDirectMessage())
            {
                // returns void if there are only mention
                if (!MessageService.HasAnyCommand())
                    return;

                RunsActionMethod(slackMessage);
            }
        }

        private void RunsActionMethod(SlackMessage slackMessage)
        {
            // Check with actions should be called
            var actionsInMsg = Actions.Where(x => MessageService.MessageWithoutBotMention().Contains(x.Key.ToLower()));

            // Call action
            foreach (var actionDictionary in actionsInMsg)
                actionDictionary.Value(slackMessage);
        }

        private void Start(SlackMessage slackMessage)
        {
            var param = MessageService.MessageWithoutBotMention("start");

            // This action only works with bot metion and start command with params
            if (!MessageService.HasAnyCommand() || !MessageService.HasMentionedBot() || string.IsNullOrWhiteSpace(param))
                return;

            var membersNamesParam = param.Split(',');

            Members = new List<WaterMember>();

            foreach (var item in membersNamesParam)
                Members.Add(new WaterMember(item.Trim()));

            SendMessage(slackMessage.Channel, PrintMembersCount());
        }

        private void AddOne(SlackMessage slackMessage)
        {
            var member = GetSenderMember(slackMessage);

            if (slackMessage.Text.Contains(EmoctionsEnum.Bottle))
                member.BottleCount++;
            else if (slackMessage.Text.Contains(EmoctionsEnum.Glass))
                member.GlassCount++;
            else
                member.BottleCount++;


            SendMessage(slackMessage.Channel, PrintMembersCount());
        }
        private void RemoveOne(SlackMessage slackMessage)
        {
            var member = GetSenderMember(slackMessage);

            if (member.BottleCount > 0)
                if (slackMessage.Text.Contains(EmoctionsEnum.Bottle))
                    member.BottleCount--;
                else if (slackMessage.Text.Contains(EmoctionsEnum.Glass))
                    member.GlassCount--;
                else
                    member.BottleCount--;

            SendMessage(slackMessage.Channel, PrintMembersCount());
        }



        private WaterMember GetSenderMember(SlackMessage slackMessage)
        {
            Members = GetWaterMembersListBasedInLastBotMsgInGroup(slackMessage.ChannelID);

            if (Members == null)
                Members = new List<WaterMember>();

            var member = Members.FirstOrDefault(x => string.Equals(x.Name, slackMessage.User.Name, StringComparison.OrdinalIgnoreCase));

            if (member == null)
            {
                member = new WaterMember(slackMessage.User.Name);
                Members.Add(member);
            }

            return member;
        }

        private string PrintMembersCount()
        {
            var sBuilder = new StringBuilder();
            foreach (var member in Members)
            {
                sBuilder.AppendFormat("{0}: ", member.Name, EmoctionsEnum.NonPortableWater);

                for (int i = 0; i < member.BottleCount; i++)
                    sBuilder.AppendFormat("{0} ", EmoctionsEnum.Bottle);
                for (int i = 0; i < member.GlassCount; i++)
                    sBuilder.AppendFormat("{0} ", EmoctionsEnum.Glass);

                if (member.GlassCount + member.BottleCount == 0)
                    sBuilder.AppendFormat("{0} ", EmoctionsEnum.NonPortableWater);

                sBuilder.AppendFormat("{0}", endLineMark);
            }

            return sBuilder.ToString();
        }

        private List<WaterMember> GetWaterMembersListBasedInLastBotMsgInGroup(string groupdId)
        {
            var lastMsg = GetTodayLastBotMessageInGroup(groupdId);

            if (lastMsg == null)
                return null;

            string[] splitedLastMsg = lastMsg.Split(new string[] { endLineMark }, StringSplitOptions.RemoveEmptyEntries);
            
            Members = new List<WaterMember>();

            foreach (var msgLine in splitedLastMsg)
            {
                var splitedNameAndQuantity = msgLine.Split(new string[] {":"}, 2, StringSplitOptions.RemoveEmptyEntries);
                if (splitedNameAndQuantity.Count() < 2)
                    continue;

                var name = splitedNameAndQuantity.First();
                var bottle = Regex.Matches(splitedNameAndQuantity[1], EmoctionsEnum.Bottle).Count;
                var glass = Regex.Matches(splitedNameAndQuantity[1], EmoctionsEnum.Glass).Count;

                var member = new WaterMember(name);
                member.BottleCount = bottle;
                member.GlassCount = glass;

                Members.Add(member);
            }


            return Members;
        }

        private string GetTodayLastBotMessageInGroup(string groupdId)
        {
            var request = new JObject();
            request.Add("channel", groupdId);
            request.Add("oldest", DateTime.Now.Date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            JArray channelArray = (JArray)SlackBotAPI.Call(SlackAPICalls.GroupsHistory, request)["messages"];

            //#ToDo: Filtrar pelo Id
            var lastMsg = channelArray.ToObject<IEnumerable<SlackMessage>>().FirstOrDefault(x => x.Subtype == SlackMessage.SubTypeEnum.botMessage);

            return lastMsg == null
                ? null
                : lastMsg.Text;
        }
    }

    public class WaterMember
    {
        public string Name { get; set; }

        public int BottleCount { get; set; }

        public int GlassCount { get; set; }

        public WaterMember(string name)
        {
            Name = name;
        }
    }
}
