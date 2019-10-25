using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FooflyProxy
{
    public class PCControl
    {

        private int sizeofInt32 = Marshal.SizeOf(typeof(Int32));
        private int sizeofDouble = Marshal.SizeOf(typeof(double));

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);

        private const uint WM_SYSCOMMAND = 0x112;                    //系统消息
        private const int SC_MONITORPOWER = 0xF170;                  //关闭显示器的系统命令
        private const int MonitorPowerOff = 2;                       //2为PowerOff, 1为省电状态，-1为开机
        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);//广播消息，所有顶级窗体都会接收

        public PCControl()
        {

            try
            {
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public string closeScreen()
        {
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);

            return "{\"isSucc\":true}";
        }

        public string shutdown(int minutes)
        {
            if(minutes > 999)
                Process.Start("c:/windows/system32/shutdown.exe", "-a");
            else
                Process.Start("c:/windows/system32/shutdown.exe", "-s -t " + (minutes * 60));

            return "{\"isSucc\":true}";
        }

       
    }
}