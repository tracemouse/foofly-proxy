using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooflyProxy
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogUtil.Open();

            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //LogUtil.write("process name=" + strProcessName);
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                //LogUtil.write("Program is already runing, will exit");
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
