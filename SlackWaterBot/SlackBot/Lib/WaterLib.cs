using SlackBot.Bots;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Lib
{
    public class WaterLib
    {
        public static void StartWaterBot()
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
