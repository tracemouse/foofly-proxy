namespace FooflyProxy
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblListenOn = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inputFoorbar2000 = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblWWWFolder = new System.Windows.Forms.Label();
            this.inputWWW = new System.Windows.Forms.TextBox();
            this.inputHttpControlPort = new System.Windows.Forms.NumericUpDown();
            this.lblHttpcontrolPort = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.cbUrl = new System.Windows.Forms.ComboBox();
            this.inputPort = new System.Windows.Forms.NumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.cbStartupWithWindows = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputHttpControlPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPort)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainWindowToolStripMenuItem,
            this.exitxToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mainWindowToolStripMenuItem
            // 
            resources.ApplyResources(this.mainWindowToolStripMenuItem, "mainWindowToolStripMenuItem");
            this.mainWindowToolStripMenuItem.Name = "mainWindowToolStripMenuItem";
            this.mainWindowToolStripMenuItem.Click += new System.EventHandler(this.mainWindowToolStripMenuItem_Click);
            // 
            // exitxToolStripMenuItem
            // 
            resources.ApplyResources(this.exitxToolStripMenuItem, "exitxToolStripMenuItem");
            this.exitxToolStripMenuItem.Name = "exitxToolStripMenuItem";
            this.exitxToolStripMenuItem.Click += new System.EventHandler(this.exitxToolStripMenuItem_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Name = "label4";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblListenOn
            // 
            resources.ApplyResources(this.lblListenOn, "lblListenOn");
            this.lblListenOn.Name = "lblListenOn";
            // 
            // lblUrl
            // 
            resources.ApplyResources(this.lblUrl, "lblUrl");
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.TabStop = true;
            this.lblUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrl_LinkClicked);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.inputFoorbar2000);
            this.groupBox1.Controls.Add(this.btnRun);
            this.groupBox1.Controls.Add(this.btnBrowser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblWWWFolder);
            this.groupBox1.Controls.Add(this.inputWWW);
            this.groupBox1.Controls.Add(this.inputHttpControlPort);
            this.groupBox1.Controls.Add(this.lblHttpcontrolPort);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // inputFoorbar2000
            // 
            resources.ApplyResources(this.inputFoorbar2000, "inputFoorbar2000");
            this.inputFoorbar2000.Name = "inputFoorbar2000";
            this.inputFoorbar2000.ReadOnly = true;
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRun.Name = "btnRun";
            this.btnRun.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBrowser
            // 
            resources.ApplyResources(this.btnBrowser, "btnBrowser");
            this.btnBrowser.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblWWWFolder
            // 
            resources.ApplyResources(this.lblWWWFolder, "lblWWWFolder");
            this.lblWWWFolder.Name = "lblWWWFolder";
            // 
            // inputWWW
            // 
            resources.ApplyResources(this.inputWWW, "inputWWW");
            this.inputWWW.Name = "inputWWW";
            // 
            // inputHttpControlPort
            // 
            resources.ApplyResources(this.inputHttpControlPort, "inputHttpControlPort");
            this.inputHttpControlPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.inputHttpControlPort.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.inputHttpControlPort.Name = "inputHttpControlPort";
            this.inputHttpControlPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // lblHttpcontrolPort
            // 
            resources.ApplyResources(this.lblHttpcontrolPort, "lblHttpcontrolPort");
            this.lblHttpcontrolPort.Name = "lblHttpcontrolPort";
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnTest);
            this.groupBox2.Controls.Add(this.cbUrl);
            this.groupBox2.Controls.Add(this.inputPort);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Controls.Add(this.lblListenOn);
            this.groupBox2.Controls.Add(this.lblUrl);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTest.Name = "btnTest";
            this.btnTest.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbUrl
            // 
            resources.ApplyResources(this.cbUrl, "cbUrl");
            this.cbUrl.FormattingEnabled = true;
            this.cbUrl.Name = "cbUrl";
            // 
            // inputPort
            // 
            resources.ApplyResources(this.inputPort, "inputPort");
            this.inputPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.inputPort.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.inputPort.Name = "inputPort";
            this.inputPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // lblPort
            // 
            resources.ApplyResources(this.lblPort, "lblPort");
            this.lblPort.Name = "lblPort";
            // 
            // cbStartupWithWindows
            // 
            resources.ApplyResources(this.cbStartupWithWindows, "cbStartupWithWindows");
            this.cbStartupWithWindows.Name = "cbStartupWithWindows";
            this.cbStartupWithWindows.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.cbStartupWithWindows);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputHttpControlPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainWindowToolStripMenuItem;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblListenOn;
        private System.Windows.Forms.LinkLabel lblUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox inputWWW;
        private System.Windows.Forms.NumericUpDown inputHttpControlPort;
        private System.Windows.Forms.Label lblHttpcontrolPort;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblWWWFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown inputPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox inputFoorbar2000;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox cbUrl;
        private System.Windows.Forms.CheckBox cbStartupWithWindows;
        private System.Windows.Forms.Button btnHelp;
    }
}

