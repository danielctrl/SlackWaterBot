using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using SlackBot.Bots;

namespace SlackBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = ConfigurationManager.AppSettings["SlackAPIToken"];

            #region Exceptions

            if (string.IsNullOrEmpty(token))
                throw new Exception("A chave 'SlackAPIToken' não foi definida no app.config.");

            #endregion

            var bot = new WaterBot(token);

            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Enter)
                key = Console.ReadKey();
        }       
    }
}
