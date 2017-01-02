using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using SlackBot;
using SlackBot.Lib;
using System.Configuration;

namespace SlackBot.WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WaterLib.StartWaterBot();
        }

        protected override void OnStop()
        {
        }
    }
}
