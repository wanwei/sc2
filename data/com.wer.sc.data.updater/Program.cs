﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.updater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //FormChooseUpdater formChoosePlugin = new FormChooseUpdater();
            //formChoosePlugin.ShowDialog();
            Application.Run(new FormChooseUpdater());
        }
    }
}