using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hammergo.MainApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            
            DevExpress.UserSkins.BonusSkins.Register();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 1 && args[0] == "zfking")
            {
                MainForm.Instance.setAdminToolBarVisible(true);
            }
            Application.Run(MainForm.Instance);
           



        }
    }
}