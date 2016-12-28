using Newtonsoft.Json;
using SlackBot.Lib.API;
using System.Collections;
using System.Collections.Generic;

namespace SlackBot.Lib.Model
{
    public class SlackMessageProps
    {
        //IMPORTANT! If adding here, remember to add the same on Resources.SlackMessageJSONProps
        public const string Text = "text";
        public const string TimeStamp = "ts";
        public const string Channel = "channel";
        public const string User = "user";
        public const string Team = "team";
        public const string BotId = "bot_id";
        public const string Attachments = "attachments";
        public const string Subtype = "subtype";
    }

    public class SlackMessageAttachmentProps
    {
        //IMPORTANT! If adding here, remember to add the same on Resources.SlackMessageJSONProps
        public const string Fallback = "fallback";
        public const string Pretext = "pretext";
        public const string Title = "title";
        public const string TitleLink = "title_link";
        public const string ImageURL = "image_url";
        public const string Text = "text";
        public const string Color = "color";
        public const string Type = "attachment_type";
        public const string Callback = "callback_id";
        public const string Fields = "fields";
        public const string Actions = "actions";
    }

    public class SlackMessageAttachmentFieldProps
    {
        public const string Title = "title";
        public const string Value = "value";
        public const string Short = "short";
    }

    public class SlackMessageAttachmentActionProps
    {
        //IMPORTANT! If adding here, remember to add the same on Resources.SlackMessageJSONProps
        public const string Name = "name";
        public const string Text = "text";
        public const string Type = "type";
        public const string Value = "value";
    }

    public class SlackMessage
    {
        [JsonProperty(SlackMessageProps.Text)]
        public string Text { get; set; }

        [JsonProperty(SlackMessageProps.TimeStamp)]
        public string TimeStamp { get; set; }

        [JsonProperty(SlackMessageProps.Team)]
        public string TeamID { get; set; }

        [JsonProperty(SlackMessageProps.Channel)]
        public string ChannelID { get; set; }

        [JsonProperty(SlackMessageProps.Subtype)]
        public string Subtype { get; set; }

        private SlackChannel _channel { get; set; }

        /// <summary>
        /// Returns the slack channel from the current ChannelID.
        /// It is lazy loaded but once loaded it'll not load again.
        /// If the ChannelID has changed it'll load it again.
        /// </summary>
        [JsonIgnore]
        public SlackChannel Channel
        {
            get
            {
                //If the channel has different id we need to load it again
                //If the channel is null we must load it for the first time
                if((_channel != null && ChannelID != _channel.Id) || _channel == null)
                    _channel = SlackBotAPI.GetChannelFromId(ChannelID);

                return _channel;
            }

            set
            {
                this.ChannelID = value.Id;
                this._channel = value;
            }
        }

        [JsonProperty(SlackMessageProps.User)]
        public string UserID { get; set; }

        private SlackUser _user { get; set; }

        /// <summary>
        /// Returns the slack user from the current UserID.
        /// It is lazy loaded but once loaded it'll not load again.
        /// If the UserID has changed it'll load it again.
        /// </summary>
        [JsonIgnore]
        public SlackUser User
        {
            get
            {
                //If the user has different id, the id was changed manually and we need to load user again
                //If the user is null we must load it for the first time
                if ((_user != null && UserID != _user.Id) || _user == null)
                    _user = SlackBotAPI.GetUserFromId(UserID);

                return _user;
            }

            set
            {
                this.UserID = value.Id;
                this._user = value;
            }
        }
        
        [JsonProperty(SlackMessageProps.Attachments)]        
        public IEnumerable<SlackAttachment> Attachments { get; set; }

        public class SlackAttachment
        {
            [JsonProperty(SlackMessageAttachmentProps.Fallback)]
            public string Fallback { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Pretext)]
            public string Pretext { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Title)]
            public string Title { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.TitleLink)]
            public string TitleLink { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.ImageURL)]
            public string ImageURL { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Text)]
            public string Text { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Color)]
            public string Color { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Type)]
            public string Type { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Callback)]
            public string Callback { get; set; }

            [JsonProperty(SlackMessageAttachmentProps.Fields)]
            public IEnumerable<SlackField> Fields { get; set; }

            public class SlackField
            {
                [JsonProperty(SlackMessageAttachmentFieldProps.Title)]
                public string Title { get; set; }

                [JsonProperty(SlackMessageAttachmentFieldProps.Value)]
                public string Value { get; set; }

                [JsonProperty(SlackMessageAttachmentFieldProps.Short)]
                public bool Short { get; set; }
            }

            [JsonProperty(SlackMessageAttachmentProps.Actions)]
            public IEnumerable<SlackAction> Actions { get; set; }

            public class SlackAction
            {
                [JsonProperty(SlackMessageAttachmentActionProps.Name)]
                public string Name { get; set; }

                [JsonProperty(SlackMessageAttachmentActionProps.Text)]
                public string Text { get; set; }

                [JsonProperty(SlackMessageAttachmentActionProps.Type)]
                public string Type { get; set; }

                [JsonProperty(SlackMessageAttachmentActionProps.Value)]
                public string Value { get; set; }
            }
        }

        /// <summary>
        /// Returns if this message is from a bot user or not.
        /// It calls the lazy loading of user.
        /// </summary>
        public bool IsBot()
        {
            //If there's an user ID, but this user is null, it's a bot
            //(because it couldn't get the user in the existing users list, and bot isn't an user)
            return (UserID != null && User == null);
        }

        /// <summary>
        /// Returns if this message is a direct message with the current bot, and not from any channel.
        /// It Calls the lazy loading of channel.
        /// </summary>
        public bool IsDirectMessage()
        {
            //If there's a channel ID, but this channel is null, it's a direct message 
            //(because it couldn't get the channel, so it doesn't exists)
            return (ChannelID != null && Channel == null);
        }

        /// <summary>
        /// Returns wether a message is valid or not.
        /// A valid message cannot have its Text, ChannelID and UserID with null values
        /// </summary>
        public bool IsValid()
        {
            return (Text != null || ChannelID != null || UserID != null);
        }

        public static class SubTypeEnum
        {
            public const string botMessage = "bot_message";
        }
    }
}