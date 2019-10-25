using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FooflyProxy
{
    public class CfgUtil
    {
        private static string cfgFilename = @"D:\Tools\Player\foobar2000\configuration\foo_httpcontrol.dll.cfg";
        private static Dictionary<string, string> httpControl { get; set; }

        public static void ReadCfg()
        {
            httpControl = new Dictionary<string, string>();
            try
            {
                LogUtil.writeLog("cfg filename=" + cfgFilename);

                FileStream readStream = new FileStream(cfgFilename, FileMode.Open);
                LogUtil.writeLog("cfg length=" + readStream.Length);
                byte[] bytes = new byte[readStream.Length];
                readStream.Read(bytes, 0, bytes.Length);
                readStream.Seek(0, SeekOrigin.Begin);

                String hex = FileUtil.ByteToHex(bytes);
                ParseCfg(hex);
                readStream.Close();
            }catch(Exception ex)
            {
                LogUtil.writeLog(ex.Message);
            }
        }

        private static void ParseCfg(string hex)
        {
            LogUtil.writeLog("all=" + hex);
            string[] arr1 = Regex.Split(hex, "07000000", RegexOptions.IgnoreCase);

            string line2 = arr1[2];
            LogUtil.writeLog("line2=" + line2);

            string[] arr2 = Regex.Split(line2, "302E302E302E3000", RegexOptions.IgnoreCase);

            foreach (string i in arr2)
                LogUtil.writeLog("line =" + i.ToString());

            string portHex = arr2[1];
            //LogUtil.writeLog("port hex=" + portHex);

        }
 
    }
}

   