using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace FooflyProxy
{
    public class NetUtil
    {
        public static List<string> GetLocalIP()
        {
            List<string> ipList = new List<string>();
            ipList.Add("127.0.0.1");
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        string ip = IpEntry.AddressList[i].ToString();
                        ipList.Add(ip);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get ip address:" + ex.Message);
            }

            return ipList;
        }

        /*　　
　　　　使用注册表监视工具发现 \SOFTWARE\Clients\StartMenuInternet\ 记录了默认浏览器名字
     
      [HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\默认浏览器名字\shell\open\command] 记录了物理路径
*/
        /// <summary>
        /// 获取默认浏览器的路径
        /// </summary>
        /// <returns></returns>
        public static String GetDefaultWebBrowserFilePath()
        {
            string _BrowserKey1 = @"Software\Clients\StartmenuInternet\";
            string _BrowserKey2 = @"\shell\open\command";

            RegistryKey _RegistryKey = Registry.CurrentUser.OpenSubKey(_BrowserKey1, false);
            if (_RegistryKey == null)
                _RegistryKey = Registry.LocalMachine.OpenSubKey(_BrowserKey1, false);
            String _Result = _RegistryKey.GetValue("").ToString();
            _RegistryKey.Close();

            _RegistryKey = Registry.LocalMachine.OpenSubKey(_BrowserKey1 + _Result + _BrowserKey2);
            _Result = _RegistryKey.GetValue("").ToString();
            _RegistryKey.Close();

            if (_Result.Contains("\""))
            {
                _Result = _Result.TrimStart('"');
                _Result = _Result.Substring(0, _Result.IndexOf('"'));
            }
            return _Result;
        }

        /// <summary>
        /// 使用默认的浏览器打开指定的url地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void GotoUrl(string url)
        {
            //string BrowserPath = GetDefaultWebBrowserFilePath();
            string gotoUrl = url;
            if (!gotoUrl.StartsWith("http://"))
            {
                gotoUrl = "http://" + gotoUrl;
            }
            //如果输入的url地址为空，则清空url地址，浏览器默认跳转到默认页面
            if (gotoUrl == "http://")
            {
                gotoUrl = "";
            }
            //System.Diagnostics.Process.Start(BrowserPath, gotoUrl);
            System.Diagnostics.Process.Start(gotoUrl);
        }

    }
}

   