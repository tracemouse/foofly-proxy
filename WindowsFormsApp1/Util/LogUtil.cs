using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FooflyProxy
{
    public class LogUtil
    {
        private static string filename = @"R:\foofly-log.txt";

        public LogUtil()
        {
        }

        public static void write(String message)
        {
            if (!Properties.Settings.Default.log)
            {
                return;
            }
            

            Encoding encoding = Encoding.UTF8;
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true);
            file.WriteLine(DateTime.Now.ToLocalTime().ToString() + " " + message);
            file.Close();

        }
    }
}