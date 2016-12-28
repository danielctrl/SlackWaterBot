using Newtonsoft.Json;

namespace SlackBot.Lib.Model
{
    public class SlackUserProps
    {
        public const string Id = "id";
        public const string Name = "name";
        public const string Deleted = "deleted";
        public const string Profile = "profile";
        public const string IsAdmin = "is_admin";
        public const string IsOwner = "is_owner";
    }

    public class SlackUserProfileProps
    {
        public const string FirstName = "first_name";
        public const string LastName = "last_name";
        public const string RealName = "real_name";
        public const string Email = "email";
        public const string Skype = "skype";
        public const string Phone = "phone";
    }

    public class SlackUser
    {
        [JsonProperty(SlackUserProps.Id)]
        public string Id { get; set; }

        [JsonProperty(SlackUserProps.Name)]
        public string Name { get; set; }

        [JsonProperty(SlackUserProps.Deleted)]
        public bool Deleted { get; set; }

        [JsonProperty(SlackUserProps.Profile)]
        public SlackProfile Profile { get; set; }

        [JsonProperty(SlackUserProps.IsAdmin)]
        public bool IsAdmin { get; set; }

        [JsonProperty(SlackUserProps.IsOwner)]
        public bool IsOwner { get; set; }

        public class SlackProfile
        {
            [JsonProperty(SlackUserProfileProps.FirstName)]
            public string FirstName { get; set; }

            [JsonProperty(SlackUserProfileProps.LastName)]
            public string LastName { get; set; }

            [JsonProperty(SlackUserProfileProps.RealName)]
            public string RealName { get; set; }

            [JsonProperty(SlackUserProfileProps.Email)]
            public string Email { get; set; }

            [JsonProperty(SlackUserProfileProps.Skype)]
            public string Skype { get; set; }

            [JsonProperty(SlackUserProfileProps.Phone)]
            public string Phone { get; set; }
        }
    }
}