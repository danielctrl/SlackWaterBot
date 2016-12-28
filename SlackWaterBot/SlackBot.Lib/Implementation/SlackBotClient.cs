using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;
using SlackBot.Lib.Model;
using SlackBot.Lib.API;
using SlackBot.Lib.Resources;

namespace SlackBot.Lib.Implementation
{
    public abstract class SlackBotClient
    {
        //Current chat's token
        private string _token
        {
            get
            {
                return SlackBotAPI.SlackToken;
            }
        }

        //Current used socket
        private WebSocket _socket;

        //Is running flag
        private bool _running = true;

        //Current bot's id
        public string BotId { get; private set; }

        //Current bot's tag
        public string BotTagName { get { return $"<@{BotId}>"; } private set { } }

        private SlackUser botUser { get; set; }

        //Current bot's name in chat
        public SlackUser BotUser
        {
            get
            {
                //If the user has different id, the id was changed manually and we need to load user again
                //If the user is null we must load it for the first time
                if ((botUser != null && BotId != botUser.Id) || botUser == null)
                    botUser = SlackBotAPI.GetUserFromId(BotId);

                return botUser;
            }

            set
            {
                BotId = value.Id;
                botUser = value;
            }
        }

        public UserIdentity Identity { get; set; }

        public delegate void ChannelJoinedHandler(object sender, SlackChannel channel);
        public event ChannelJoinedHandler OnChannelJoined;

        public delegate void MessageHandler(object sender, SlackMessage message);
        public event MessageHandler OnMessage;

        /// <summary>
        /// Starts the slack api
        /// </summary>
        /// <param name="token">Generated token from slack which allows the client to connect to a team</param>
        /// <param name="name">Bot's name on slack</param>
        public SlackBotClient(string token)
        {
            //Register the current token and name
            SlackBotAPI.SetupToken(token);

            //Start the bot
            _start();
        }

        /// <summary>
        /// Starts the slack api
        /// </summary>
        /// <param name="token">Generated token from slack which allows the client to connect to a team</param>
        /// <param name="name">Bot's name on slack</param>
        public SlackBotClient(string token, UserIdentity identity)
        {
            this.Identity = identity;

            //Register the current token and name
            SlackBotAPI.SetupToken(token);

            //Start the bot
            _start();
        }

        /// <summary>
        /// Execute the first run for the api
        /// </summary>
        private void _start()
        {
            Console.WriteLine("Starting...");
            JObject response = SlackBotAPI.Call(SlackAPICalls.RTMStart, null);

            _socket = new WebSocket(response[SlackJSONProperties.Url].ToString());
            _socket.OnMessage += _parseRequest;

            _socket.Connect();

            //When connected, set the bot's user and id.
            BotUser = response["self"].ToObject<SlackUser>();

            //Creates an identity
            if(Identity == null)
                Identity = new UserIdentity(BotUser.Name);
        }

        /// <summary>
        /// Every time slack calls an action it calls this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _parseRequest(object sender, MessageEventArgs e)
        {            
            string jsonString = e.Data;

            JObject message = JObject.Parse(jsonString);

            if (message[SlackJSONProperties.Type].ToString() == SlackRTMEventTypes.Hello)
                Connected();

            if (message[SlackJSONProperties.Type].ToString() == SlackRTMEventTypes.Message)
                UserMessage(message);

            if (message[SlackJSONProperties.Type].ToString() == SlackRTMEventTypes.ChannelJoined)
                JoinedChannel(message);
        }

        private void Connected()
        {
            Console.WriteLine("Conected. \r\n");
        }

        private void JoinedChannel(JObject message)
        {
            var channel = SlackBotAPI.GetChannelFromId((string)message[SlackMessageJSONProps.Channel]);
            OnChannelJoined?.Invoke(this, channel);
        }

        private void UserMessage(JObject message)
        {
            if (message["username"] != null && message["username"].ToString() == Identity.Username)
                return;

            Console.WriteLine("Received message: ");
            Console.WriteLine(message);

            //Converts what we received in a message object
            var slackMessage = message.ToObject<SlackMessage>();

            var isBot = false;

            //We don't use message.IsBot() here because it'll lazy call the api to load user and we want this to be fast
            if (message[SlackMessageJSONProps.BotId] != null && (message["subtype"] != null && message["subtype"].ToString() == "bot_message"))
            {
                //If its a bot, set its id to the message's user id
                slackMessage.UserID = message[SlackMessageJSONProps.BotId].ToString();
                isBot = true;
            }

            var attachments = message["attachments"];
            
            if (isBot && string.IsNullOrEmpty(slackMessage.Text) && attachments != null)
            {
                var fallback = (attachments[0]["fallback"] as JValue).Value;
                slackMessage.Text = fallback.ToString();
            }

            if(slackMessage.IsValid())
                OnMessage?.Invoke(this, slackMessage);
        }

        /// <summary>
        /// Sends a message to a given slack channel
        /// </summary>
        /// <param name="channel">Slack channel to send the message to</param>
        /// <param name="message">Message to be sent. It can have slack's formatting</param>
        public bool SendMessage(SlackChannel channel, string message)
        {
            JObject parameters = new JObject
            {
                {"channel", channel.Id},
                {"text", WebUtility.HtmlDecode(message).Replace('&', 'E')}
            };

            //Adds the current identity to the message
            parameters = Identity.AddIdentityParameters(parameters);

            var result = SlackBotAPI.Call(SlackAPICalls.ChatPostMessage, parameters);

            bool retorno;
            bool.TryParse(result["ok"].ToString(), out retorno);

            return retorno;
        }

        /// <summary>
        /// Sends a message to a given slack channel
        /// </summary>
        /// <param name="channel">Slack channel to send the message to</param>
        /// <param name="message">Message to be sent. It can have slack's formatting</param>
        public bool SendMessage(SlackMessage message)
        {
            JObject parameters = JObject.FromObject(message);

            //Adds the current identity to the message
            parameters = Identity.AddIdentityParameters(parameters);

            var result = SlackBotAPI.Call(SlackAPICalls.ChatPostMessage, parameters);

            bool retorno;
            bool.TryParse(result["ok"].ToString(), out retorno);

            return retorno;
        }

        /// <summary>
        /// Sends each string in a list as a new slack message
        /// </summary>
        /// <param name="channel">Slack channel to send the message to</param>
        /// <param name="message">Message list to be sent. Messages can have slack's formatting</param>
        public bool SendMessage(IEnumerable<SlackMessage> messages)
        {
            var sucess = true;

            //Foreach item in the array
            foreach (var msg in messages)
            {
                //Send the message and if it didn't sent, sucess = false
                if (!SendMessage(msg))
                    sucess = false;
            }

            return sucess;
        }

        /// <summary>
        /// Sends each string in a list as a new slack message
        /// </summary>
        /// <param name="channel">Slack channel to send the message to</param>
        /// <param name="message">Message list to be sent. Messages can have slack's formatting</param>
        public bool SendMessage(SlackChannel channel, IEnumerable<string> messages)
        {
            var sucess = true;

            //Foreach item in the array
            foreach (var msg in messages)
            {
                //Send the message and if it didn't sent, sucess = false
                if (!SendMessage(channel, msg))
                    sucess = false;
            }

            return sucess;
        }

        /// <summary>
        /// Send a reaction to some message
        /// </summary>
        /// <param name="channel">Slack channel to send the message to</param>
        /// <param name="message">Message list to be sent. Messages can have slack's formatting</param>
        /// <param name="reactions">Array of current channel's emojis (without ":") to react</param>
        public void SendReaction(SlackChannel channel, SlackMessage message, params string[] reactions)
        {
            foreach (var reaction in reactions)
            {
                JObject parameters = new JObject
                {
                    {"channel", channel.Id},
                    {"name", reaction},
                    {"timestamp", message.TimeStamp}
                };

                SlackBotAPI.Call(SlackAPICalls.ReactionsAdd, parameters);
            }
        }

        /// <summary>
        /// Send a file to the given channel
        /// </summary>
        public JObject SendFile(SlackChannel channel, string filePath)
        {
            JObject parameters = new JObject
            {
                {"channel", channel.Id},
                {"filetype", "text"},
                {"filename", filePath}
            };

            var retorno = SlackBotAPI.Call(SlackAPICalls.FilesUpload, parameters, filePath);
            return retorno;
        }

        public SlackUser GetBot(string id)
        {
            var param = new JObject();
            param.Add("bot", id);

            JObject response = SlackBotAPI.Call(SlackAPICalls.BotsInfo, param);

            if (!((bool)response["ok"]))
                throw new Exception(response["error"].ToString());

            var bot = (JObject)response["bot"];
            SlackUser user = bot.ToObject<SlackUser>();

            return user;
        }
    }
}
