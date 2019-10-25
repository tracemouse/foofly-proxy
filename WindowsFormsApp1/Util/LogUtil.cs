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
        private static string filename1 = @"R:\foofly-log1.txt";
        private static StreamWriter file;

        public LogUtil()
        {

        }

        public static void Open()
        {
            if (!Properties.Settings.Default.log)
            {
                return;
            }
            try { 
                Encoding encoding = Encoding.UTF8;
                file = new System.IO.StreamWriter(filename, true);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Close()
        {
            if (!Properties.Settings.Default.log)
            {
                return;
            }
            try
            {
                file.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public static void write(String message)
        {
            if (!Properties.Settings.Default.log)
            {
                return;
            }

            try { 
                file.WriteLine(DateTime.Now.ToLocalTime().ToString() + " " + message);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        public static void writeLog(string message)
        {
            Encoding encoding = Encoding.UTF8;
            StreamWriter file1 = new System.IO.StreamWriter(filename1, true);
            file1.WriteLine(message);
            file1.Close();
        }

    
    }
}