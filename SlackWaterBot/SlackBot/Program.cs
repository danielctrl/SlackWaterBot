using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using SlackBot.Bots;
using SlackBot.Lib;

namespace SlackBot
{
    class Program
    {
        static void Main(string[] args)
        {
            WaterLib.StartWaterBot();
        }       
    }
}
