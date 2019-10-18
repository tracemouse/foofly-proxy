using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AimpFlyPlugin
{
    partial class Setting : Form
    {

        Thread httpThread;

        public Setting()
        {

            InitializeComponent();
            this.Text = String.Format("{0} Settings", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct + " - " + AssemblyDescription;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;

            int port = FooflyProxy.Properties.Settings.Default.port;
            string password = FooflyProxy.Properties.Settings.Default.password;

            String url = "http://" + this.GetLocalIP() + ":" + port + "/";
            this.lblUrl.Text = url;

            String tips = "The UAC dialog may be prompted for enable the http port in the firewall, please click 'YES' otherwise the http service cannot be opened";
            //this.tips.Text = tips;

            this.inputPort.Value = port;
            this.inputPassword.Text = password;

            //debug
            //String uploadJsonString = "{\"action\":\"getNowPlayingList\"}";
            //MBService mbService = new MBService(uploadJsonString, ref aimpInterface);
            //String returnJsonString = mbService.invokeMB();

            //TestTask testTask = new TestTask(ref aimpInterface);
            //UIntPtr taskThread;
            //this.aimpInterface.getPlayer().ServiceThreadPool.Execute(testTask, out taskThread);

 
        }



        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {

            int port = (int)this.inputPort.Value;
            string password = this.inputPassword.Text;
            int oldport = FooflyProxy.Properties.Settings.Default.port;
            string oldpassword = FooflyProxy.Properties.Settings.Default.password;
            if (port != oldport || password != oldpassword)
            {
                //this.RegisterHttp(port);
                FooflyProxy.Properties.Settings.Default.port = port;
                FooflyProxy.Properties.Settings.Default.password = password;
                FooflyProxy.Properties.Settings.Default.Save();
            }
            
            Close();
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private string GetLocalIP()
        {
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
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get ip address:" + ex.Message);
                return "";
            }
        }

        private void RegisterHttp(int port)
        {

            //获取当前登录的Windows用户的标识 
            System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(wid);

            // 判断当前用户是否是管理员 
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                return;
            }
            else // 用管理员用户运行 
            {

                System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo("cmd", " /c \"netsh http add urlacl url=http://*:" + port + "/ user=Everyone\"");
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                Process p = System.Diagnostics.Process.Start(startInfo);
                //让 Process 组件等候相关的进程进入闲置状态。 
                //p.WaitForInputIdle();
                //设定要等待相关的进程结束的时间，并且阻止目前的线程执行，直到等候时间耗尽或者进程已经结束为止。 
                p.WaitForExit();

                if (p != null)
                {
                    p.Close();
                    p.Dispose();
                    p = null;
                }


                return;
            }
        }
    }
}
