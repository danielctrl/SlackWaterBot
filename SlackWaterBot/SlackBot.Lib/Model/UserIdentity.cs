using Newtonsoft.Json.Linq;
using SlackBot.Lib.API;
using SlackBot.Lib.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Lib.Model
{
    public class UserIdentity
    {
        public string Username { get; set; }

        private string userIcon { get; set; }

        /// <summary>
        /// An emoji or image URL to the sender profile picture. OBS: Emoji cannot contain ":"
        /// </summary>
        public string UserIcon
        {
            get
            {
                return userIcon;
            }

            set
            {
                //Set IsEmoji every time user icon is changed
                JObject response = SlackBotAPI.Call(SlackAPICalls.EmojiList, new JObject());
                var emojis = response["emoji"].ToList();

                IsEmoji = emojis.Any(x => (":"+(x as JProperty).Name+":").Contains(value));
                userIcon = value;
            }
        }

        /// <summary>
        /// Returns if the given user icon is an existing emoji. If not, it is an url
        /// </summary>
        public bool IsEmoji { get; set; }

        public bool IsBot { get; set; }

        public UserIdentity(string username)
        {
            this.Username = username;
            IsBot = true;
        }

        public JObject AddIdentityParameters(JObject parameters)
        {
            parameters.Add("username", Username);
            parameters.Add("as_user", (!IsBot).ToString());

            if(userIcon != null)
            {
                parameters.Add(IsEmoji ? "icon_emoji" : "icon_url", UserIcon);
            }

            return parameters;
        }
    }
}
