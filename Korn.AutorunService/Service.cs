﻿using Korn.Shared;
using Korn.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace Korn.AutorunService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        const string KORN_DISABLE_AUTORUN_SERVICE_VAR = "KORN_DISABLE_AUTORUN_SERVICE";       
        static string ServicePath = Path.Combine(KornShared.RootDirectory, "Service", "bin", "Korn.Service.exe");
        static TimeSpan Delay = TimeSpan.FromSeconds(5);

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                if (Process.GetProcessesByName("Korn.Service").Length == 0)
                {
                    var variable = SystemVariablesUtils.GetVariable(KORN_DISABLE_AUTORUN_SERVICE_VAR);
                    if (variable == null || !(variable == "1" || variable.Equals("true", StringComparison.OrdinalIgnoreCase)))
                        Process.Start(ServicePath);
                }

                Thread.Sleep(Delay);
            }
        }

        protected override void OnStop() { }
    }
}
