using Newtonsoft.Json.Linq;
using SlackBot.Lib.Model;
using SlackBot.Lib.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Lib.API
{
    public class SlackBotAPI
    {
        [Obsolete("Only SlackToken without underscore( _ ) can access this property")]
        private static string _slackToken { get; set; }

        public static string SlackToken
        {
            get
            {
                if (_slackToken == null)
                    throw new ArgumentNullException("The slack token must be set up. Use SlackBot.Lib.API.SlackBotAPI.SetupToken(string slackToken).");

                return _slackToken;
            }

            private set
            {
                _slackToken = value;
            }
        }

        [Obsolete("Only client without underscore( _ ) can access this property")]
        private static WebClient _client { get; set; }
        private static WebClient client
        {
            get
            {
                //Guarantee that there's only one WebClient
                if (_client == null)
                    _client = new WebClient();

                return _client;
            }
        }

        /// <summary>
        /// Set the token needed to call slack's api commands
        /// </summary>
        /// <returns>Authorized</returns>
        public static bool SetupToken(string slackToken)
        {
            if (slackToken == null)
                throw new Exception("Token cannot be null");

            if (!IsValidToken(slackToken))
                throw new Exception("Slack didn't authorized this token");

            SlackToken = slackToken;
            return true;
        }

        private static bool IsValidToken(string slackToken)
        {
            //Setup parameters
            JObject parameters = new JObject
            {
                {"token", slackToken}
            };

            //Create an url to send the request
            var urlParams = string.Format("?token={0}", slackToken);

            //Get the response
            string response = client.DownloadString($"https://slack.com/api/" + SlackAPICalls.AuthTest + urlParams);

            //Parse its json
            var jsonResponse = JObject.Parse(response);

            //Return if it's valid
            return (bool)jsonResponse["ok"];
        }

        /// <summary>
        /// Call slack's api using the given apiCommand
        /// </summary>
        /// <param name="apiCommand"></param>
        /// <param name="parameters"></param>
        /// <returns>Response Json</returns>
        public static JObject Call(string apiCommand, JObject parameters)
        {
            //Tests if the given string is a valid api command
            if (!IsValidAPI(apiCommand))
                throw new Exception("Invalid api call to " + apiCommand + ". The given string is not a valid string for SlackBot.Lib.Resources.SlackAPICalls");

            //Setup the token
            var urlParams = string.Format("?token={0}", SlackToken);

            //If there's parameters
            if (parameters != null)
            {
                //Add each parameter in the url
                foreach (KeyValuePair<string, JToken> parameter in parameters)
                {
                    var value = parameter.Value.ToString();
                    if(!string.IsNullOrEmpty(value))
                        urlParams += $"&{parameter.Key}={WebUtility.UrlEncode(value)}";
                }
            }
            
            //Pretty print the call in console
            Console.WriteLine("\r\n Api Call: " + apiCommand);
            Console.WriteLine(" Params: " + urlParams.Replace(SlackToken, "[CURRENTTOKEN]") + "\r\n");

            //Get the response
            string response = client.DownloadString($"https://slack.com/api/" + apiCommand + urlParams);

            var jResponse = JObject.Parse(response);

            //If threw an error
            if (!(bool)jResponse["ok"])
                throw new Exception(jResponse["error"].ToString());

            //Converts it to json
            return jResponse;
        }

        /// <summary>
        /// Call slack's api using the given apiCommand
        /// </summary>
        /// <param name="apiCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="filePath"></param>
        /// <returns>Response Json</returns>
        public static JObject Call(string apiCommand, JObject parameters, string filePath)
        {
            //If the file is null or not valid
            if (filePath == null || !File.Exists(filePath))
                throw new Exception("Invalid file path. It cannot be null or it must exist");

            parameters.Add("content", GenerateSlackFileContent(client, filePath));

            return Call(apiCommand, parameters);
        }

        /// <summary>
        /// Returns every channel in the current token's connection
        /// </summary>
        public static IEnumerable<SlackChannel> GetChannels()
        {
            //Tries to get all channels calling the api
            JObject response = Call(SlackAPICalls.ChannelsList, new JObject());

            //Gets every channel
            JArray channelArray = (JArray)response["channels"];

            //Converts it to the ienumerable
            var channels = channelArray.ToObject<IEnumerable<SlackChannel>>();

            return channels;
        }

        /// <summary>
        /// Returns every private channel/group in the current token's connection
        /// </summary>
        public static IEnumerable<SlackChannel> GetGroups()
        {
            //Tries to get all channels calling the api
            JObject response = Call(SlackAPICalls.GroupsList, new JObject());

            //Gets every channel
            JArray channelArray = (JArray)response["groups"];

            //Converts it to the ienumerable
            var channels = channelArray.ToObject<IEnumerable<SlackChannel>>();

            return channels;
        }

        /// <summary>
        /// Returns every direct message's channel in the current token's connection
        /// </summary>
        public static IEnumerable<SlackChannel> GetIms()
        {
            //Tries to get all channels calling the api
            JObject response = Call(SlackAPICalls.IMList, new JObject());

            //Gets every channel
            JArray channelArray = (JArray)response["ims"];

            //Converts it to the ienumerable
            var channels = channelArray.ToObject<IEnumerable<SlackChannel>>();

            return channels;
        }

        /// <summary>
        /// Returns every user in the current token's connection
        /// </summary>
        public static IEnumerable<SlackUser> GetUsers()
        {
            //Tries to get all users calling the api
            JObject response = Call(SlackAPICalls.UsersList, new JObject());

            //Gets every user
            JArray userArray = (JArray)response["members"];

            //Converts it to the ienumerable
            var users = userArray.ToObject<IEnumerable<SlackUser>>();

            return users;
        }

        /// <summary>
        /// Returns a list of users inside an specific channel
        /// </summary>
        public IEnumerable<SlackUser> GetUsersFromChannel(SlackChannel channel)
        {
            return GetChannelFromId(channel.Id).Members.Select(GetUserFromId);
        }

        /// <summary>
        /// Get a channel by giving its id
        /// </summary>
        /// <param name="id">A valid channel id</param>
        public static SlackChannel GetChannelFromId(string id)
        {
            var channelType = id[0].ToString().ToLower();

            SlackChannel channel = null;

            if(channelType == "c")
            {
                channel = GetChannels().FirstOrDefault(c => c.Id == id);
            }
            else if (channelType == "g")
            {
                channel = GetGroups().FirstOrDefault(c => c.Id == id);
            }
            else if (channelType == "d")
            {
                channel = GetIms().FirstOrDefault(c => c.Id == id);
            }

            return channel;
        }

        /// <summary>
        /// Get a user by giving its id
        /// </summary>
        /// <param name="id">A valid user id</param>
        public static SlackUser GetUserFromId(string id)
        {
            return GetUsers().FirstOrDefault(user => user.Id == id);
        }

        private static bool IsValidAPI(string api)
        {
            //We create a new Slack API calls to know what calls are inside of it
            var exitingCalls = new SlackAPICalls();

            //Get its fields
            var fields = exitingCalls.GetType().GetFields();

            //Takes its *values*
            var values = fields.Select(f => f.GetValue(exitingCalls));

            //If the current api being called exists
            return values.Any(v => v.ToString() == api);
        }

        private static byte[] GenerateSlackFileContent(WebClient wc, string filePath)
        {
            FileStream str = File.OpenRead(filePath);
            byte[] fBytes = new byte[str.Length];
            str.Read(fBytes, 0, fBytes.Length);
            str.Close();

            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
            wc.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            var fileData = wc.Encoding.GetString(fBytes);
            var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filePath, "multipart/form-data", fileData);

            return wc.Encoding.GetBytes(package);
        }
    }
}
