using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public Form1()
        {

            CfgUtil.ReadCfg();

            InitializeComponent();
            InitFoobar();
            InitWS();
        }

        private void InitFoobar()
        {
            try
            {
                String foobar2000exe = Application.StartupPath + "\\foobar2000.exe";
                foobar2000exe = "D:\\Tools\\Player\\foobar2000\\foobar2000.exe";
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = foobar2000exe;
                myProcess.Start();

            }
            catch(Exception e)
            {
                this.foobarLiving = false;
                LogUtil.write(e.Message);
                MessageBox.Show("Failed to run foobar2000.exe, please check if you put foofly-proxy.exe into the folder of foobar2000.");
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
    }
}
