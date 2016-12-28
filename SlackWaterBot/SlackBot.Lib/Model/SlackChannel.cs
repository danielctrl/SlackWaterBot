using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackBot.Lib.Model
{
    public class SlackChannelProps
    {
        //IMPORTANT! If adding here, remember to add the same on Resources.SlackChannelJSONProps
        public const string Id = "id";
        public const string Name = "name";
        public const string IsChannel = "is_channel";
        public const string Creator = "creator";
        public const string IsArchived = "is_archived";
        public const string IsGeneral = "is_general";
        public const string IsMember = "is_member";
        public const string Members = "members";
        public const string Topic = "topic";
        public const string Purpose = "purpose";        
    }

    public class SlackChannelTopicPurposeProps
    {
        //IMPORTANT! If adding here, remember to add the same on Resources.SlackChannelTopicPurposeJSONProps
        public const string Value = "value";
        public const string Creator = "Creator";
    }

    public class SlackChannel
    {
        [JsonProperty(SlackChannelProps.Id)]
        public string Id { get; set; }

        [JsonProperty(SlackChannelProps.Name)]
        public string Name { get; set; }

        [JsonProperty(SlackChannelProps.IsChannel)]
        public bool IsChannel { get; set; }

        [JsonProperty(SlackChannelProps.Creator)]
        public string Creator { get; set; }

        [JsonProperty(SlackChannelProps.IsArchived)]
        public bool IsArchived { get; set; }

        [JsonProperty(SlackChannelProps.IsGeneral)]
        public bool IsGeneral { get; set; }

        [JsonProperty(SlackChannelProps.IsMember)]
        public bool IsMember { get; set; }

        [JsonProperty(SlackChannelProps.Members)]
        public List<string> Members { get; set; }

        [JsonProperty(SlackChannelProps.Topic)]
        public SlackTopic Topic { get; set; }

        public class SlackTopic
        {
            [JsonProperty(SlackChannelTopicPurposeProps.Value)]
            public string Value { get; set; }

            [JsonProperty(SlackChannelTopicPurposeProps.Creator)]
            public string Creator { get; set; }
        }

        [JsonProperty(SlackChannelProps.Purpose)]
        public SlackPurpose Purpose { get; set; }

        public class SlackPurpose
        {
            [JsonProperty(SlackChannelTopicPurposeProps.Value)]
            public string Value { get; set; }

            [JsonProperty(SlackChannelTopicPurposeProps.Creator)]
            public string Creator { get; set; }
        }
    }
}