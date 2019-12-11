using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooflyProxy
{
    public partial class Form1 : Form
    {
        private WSListen wsListen;
        private int port;
        private bool wsLiving = false;
        private bool foobarLiving = false;
        private bool fooHttpControlLiving = false;
        private string foobar2000Root = Application.StartupPath;
       // private string foobar2000Root = @"D:\Tools\Player\foobar2000";
        //private string foobar2000exe = foobar2000Root + @"\foobar2000.exe";
        private string foobar2000exe;
        private string wwwRoot;

        public Form1()
        {

            this.foobar2000exe = this.foobar2000Root + @"\foobar2000.exe";
            this.wwwRoot = this.foobar2000Root + @"\foo_httpcontrol_data";
            //CfgUtil.ReadCfg();

            this.MaximizeBox = false;

            InitializeComponent();
            InitFormValues();
            //InitFoobar();
            InitWS();
        }

        private void InitFormValues()
        {
            this.Text = this.Text + " - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.inputFoorbar2000.Text = this.foobar2000exe;
            string wwwRoot = Properties.Settings.Default.wwwRoot;
            if (string.IsNullOrEmpty(wwwRoot))
            {
                Properties.Settings.Default.wwwRoot = this.wwwRoot;
                wwwRoot = this.wwwRoot;
            }
            this.inputWWW.Text = wwwRoot;

            this.lblUrl.Text = "";

            Properties.Settings.Default.wwwRoot = wwwRoot;
            this.inputHttpControlPort.Value = Properties.Settings.Default.httpcontrolPort;
            this.port = Properties.Settings.Default.port;
            this.inputPort.Value = this.port;

            //string url = "http://" + NetUtil.GetLocalIP() + ":" + port + "/";
            //this.lblUrl.Text = url;

            List<string> ipList = NetUtil.GetLocalIP();
            foreach (string ip in ipList)
            {
                string url = "http://" + ip + ":" + port + "/";
                this.cbUrl.Items.Add(url);
                this.cbUrl.Text = url;
            }

            this.cbStartupWithWindows.Checked = Properties.Settings.Default.startupWithWindows;

            Properties.Settings.Default.Save();
        }

        private void InitFoobar()
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = foobar2000exe;
                myProcess.Start();

            }
            catch(Exception e)
            {
                this.foobarLiving = false;
                LogUtil.write(e.Message);
                MessageBox.Show("Failed to run foobar2000.exe, please check if you put foofly-proxy.exe into the folder of foobar2000.");
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void InitWS()
        {
            try
            {
                this.port = Properties.Settings.Default.port;
                this.wsListen = new WSListen(this.port);
                this.wsListen.start();
                this.wsLiving = true;
            }catch(Exception e)
            {
                this.wsLiving = false;
                MessageBox.Show("Failed to strat the webservice, please check if the port is used.");
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }

        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void exitxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.wsListen.stop();
            LogUtil.Close();
        }

        private bool GetProcessLivingStatus(String strProcessName)
        {
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = this.inputWWW.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.inputWWW.Text = folderBrowserDialog1.SelectedPath;
            }
        }

 

        private void lblUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NetUtil.GotoUrl(this.lblUrl.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.InitFoobar();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var www = this.inputWWW.Text;
            var httpControlPort = (int)this.inputHttpControlPort.Value;
            var port = (int)this.inputPort.Value;

            if (!Directory.Exists(www))
            {
                MessageBox.Show("The " + www + " folder does not exist!");
                return;
            }

            if(port == httpControlPort)
            {
                MessageBox.Show("The port of Foofly Proxy cannot be same as the port of foobar2000 httpcontrol!");
                return;
            }


            Properties.Settings.Default.httpcontrolPort = httpControlPort;

            Properties.Settings.Default.wwwRoot = www;

            if (port != Properties.Settings.Default.port) {
                Properties.Settings.Default.port = port;
                this.port = port;
                try
                {
                    if (this.wsListen != null)
                    {
                        this.wsListen.stop();
                    }
                    this.InitWS();
                    string url = "http://" + NetUtil.GetLocalIP() + ":" + port + "/";
                    this.lblUrl.Text = url;
                }
                catch(Exception ex)
                {
                }
            }

            //set startup
            bool startup = this.cbStartupWithWindows.Checked;
            Properties.Settings.Default.startupWithWindows = startup;
            WinUtil winUtil = new WinUtil();
            winUtil.SetMeAutoStart(startup);
            winUtil = null;

            Properties.Settings.Default.Save();

            MessageBox.Show("Done");

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string url = this.cbUrl.Text;
            if (url.Length > 0)
                NetUtil.GotoUrl(url);
        }


        private void btnHelp_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/tracemouse/FooFly";
            NetUtil.GotoUrl(url);
        }
    }
}
