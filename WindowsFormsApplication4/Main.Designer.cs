namespace WindowsFormsApplication4
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hibernateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sleepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noticeSomethingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButtonHibernate = new System.Windows.Forms.RadioButton();
            this.radioButtonSleep = new System.Windows.Forms.RadioButton();
            this.labelAbout = new System.Windows.Forms.Label();
            this.checkBoxSafeMode = new System.Windows.Forms.CheckBox();
            this.radioButtonRemind = new System.Windows.Forms.RadioButton();
            this.progressBarTime = new System.Windows.Forms.ProgressBar();
            this.checkBoxAutoRun = new System.Windows.Forms.CheckBox();
            this.checkBoxNoti = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.radioButtonShutdown = new System.Windows.Forms.RadioButton();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelRealTime = new System.Windows.Forms.Panel();
            this.numericUpDownMinute = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownHour = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.labelCurrentTimeInfo = new System.Windows.Forms.Label();
            this.buttonTime = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxModes = new System.Windows.Forms.GroupBox();
            this.groupBoxOption = new System.Windows.Forms.GroupBox();
            this.panelShowNoti = new System.Windows.Forms.Panel();
            this.checkBox30sec = new System.Windows.Forms.CheckBox();
            this.checkBox1min = new System.Windows.Forms.CheckBox();
            this.checkBox5min = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panelRealTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).BeginInit();
            this.groupBoxModes.SuspendLayout();
            this.groupBoxOption.SuspendLayout();
            this.panelShowNoti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 104);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.aboutToolStripMenuItem.Image = global::AS.Properties.Resources.off_button;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem1.Text = "Main Window";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shutdownToolStripMenuItem,
            this.hibernateToolStripMenuItem,
            this.sleepToolStripMenuItem,
            this.noticeSomethingToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.settingsToolStripMenuItem.Text = "Modes";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // shutdownToolStripMenuItem
            // 
            this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            this.shutdownToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.shutdownToolStripMenuItem.Text = "Shutdown";
            this.shutdownToolStripMenuItem.Click += new System.EventHandler(this.shutdownToolStripMenuItem_Click);
            // 
            // hibernateToolStripMenuItem
            // 
            this.hibernateToolStripMenuItem.Name = "hibernateToolStripMenuItem";
            this.hibernateToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.hibernateToolStripMenuItem.Text = "Hibernate";
            this.hibernateToolStripMenuItem.Click += new System.EventHandler(this.hibernateToolStripMenuItem_Click);
            // 
            // sleepToolStripMenuItem
            // 
            this.sleepToolStripMenuItem.Name = "sleepToolStripMenuItem";
            this.sleepToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sleepToolStripMenuItem.Text = "Sleep";
            this.sleepToolStripMenuItem.Click += new System.EventHandler(this.sleepToolStripMenuItem_Click);
            // 
            // noticeSomethingToolStripMenuItem
            // 
            this.noticeSomethingToolStripMenuItem.Name = "noticeSomethingToolStripMenuItem";
            this.noticeSomethingToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.noticeSomethingToolStripMenuItem.Text = "Remind Something";
            this.noticeSomethingToolStripMenuItem.Click += new System.EventHandler(this.noticeSomethingToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "AS";
            this.notifyIcon1.BalloonTipTitle = "AS";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "AS";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // radioButtonHibernate
            // 
            this.radioButtonHibernate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonHibernate.AutoSize = true;
            this.radioButtonHibernate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonHibernate.Location = new System.Drawing.Point(13, 49);
            this.radioButtonHibernate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonHibernate.Name = "radioButtonHibernate";
            this.radioButtonHibernate.Size = new System.Drawing.Size(88, 19);
            this.radioButtonHibernate.TabIndex = 3;
            this.radioButtonHibernate.TabStop = true;
            this.radioButtonHibernate.Text = "Hibernate";
            this.toolTip1.SetToolTip(this.radioButtonHibernate, "Turn off the PC but apps stay open.\r\nWhen the PC is turned on, you\'re back to whe" +
        "re you left off.");
            this.radioButtonHibernate.UseVisualStyleBackColor = true;
            this.radioButtonHibernate.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButtonSleep
            // 
            this.radioButtonSleep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSleep.AutoSize = true;
            this.radioButtonSleep.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonSleep.Location = new System.Drawing.Point(13, 74);
            this.radioButtonSleep.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonSleep.Name = "radioButtonSleep";
            this.radioButtonSleep.Size = new System.Drawing.Size(62, 19);
            this.radioButtonSleep.TabIndex = 5;
            this.radioButtonSleep.TabStop = true;
            this.radioButtonSleep.Text = "Sleep";
            this.toolTip1.SetToolTip(this.radioButtonSleep, "The PC stays on but uses low power. Apps stay open.\r\nWhen the PC wakes up, you\'re" +
        " instantly back to where you left off.");
            this.radioButtonSleep.UseVisualStyleBackColor = true;
            this.radioButtonSleep.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelAbout.AutoSize = true;
            this.labelAbout.BackColor = System.Drawing.Color.Transparent;
            this.labelAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAbout.ForeColor = System.Drawing.Color.White;
            this.labelAbout.Location = new System.Drawing.Point(143, 7);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(124, 45);
            this.labelAbout.TabIndex = 15;
            this.labelAbout.Text = "AS v1.3\r\nCopyright © 2021\r\nFreedom Software";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelAbout, "About");
            this.labelAbout.Click += new System.EventHandler(this.label4_Click);
            // 
            // checkBoxSafeMode
            // 
            this.checkBoxSafeMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSafeMode.AutoSize = true;
            this.checkBoxSafeMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSafeMode.Location = new System.Drawing.Point(26, 25);
            this.checkBoxSafeMode.Name = "checkBoxSafeMode";
            this.checkBoxSafeMode.Size = new System.Drawing.Size(86, 19);
            this.checkBoxSafeMode.TabIndex = 6;
            this.checkBoxSafeMode.Text = "Safe Mode";
            this.toolTip1.SetToolTip(this.checkBoxSafeMode, "Enabled to force the suspended mode immediately, disabled to cause Windows to sen" +
        "d a suspend request to every application.");
            this.checkBoxSafeMode.UseVisualStyleBackColor = true;
            this.checkBoxSafeMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButtonRemind
            // 
            this.radioButtonRemind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRemind.AutoSize = true;
            this.radioButtonRemind.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonRemind.Location = new System.Drawing.Point(13, 99);
            this.radioButtonRemind.Name = "radioButtonRemind";
            this.radioButtonRemind.Size = new System.Drawing.Size(146, 19);
            this.radioButtonRemind.TabIndex = 4;
            this.radioButtonRemind.TabStop = true;
            this.radioButtonRemind.Text = "Remind something";
            this.toolTip1.SetToolTip(this.radioButtonRemind, "Just remind something");
            this.radioButtonRemind.UseVisualStyleBackColor = true;
            this.radioButtonRemind.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // progressBarTime
            // 
            this.progressBarTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTime.Enabled = false;
            this.progressBarTime.Location = new System.Drawing.Point(12, 282);
            this.progressBarTime.Maximum = 1000000000;
            this.progressBarTime.Name = "progressBarTime";
            this.progressBarTime.Size = new System.Drawing.Size(347, 10);
            this.progressBarTime.Step = 1000;
            this.progressBarTime.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTime.TabIndex = 22;
            this.toolTip1.SetToolTip(this.progressBarTime, "Elapsed Time");
            // 
            // checkBoxAutoRun
            // 
            this.checkBoxAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoRun.AutoSize = true;
            this.checkBoxAutoRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAutoRun.ForeColor = System.Drawing.SystemColors.Desktop;
            this.checkBoxAutoRun.Location = new System.Drawing.Point(26, 99);
            this.checkBoxAutoRun.Name = "checkBoxAutoRun";
            this.checkBoxAutoRun.Size = new System.Drawing.Size(73, 19);
            this.checkBoxAutoRun.TabIndex = 23;
            this.checkBoxAutoRun.Text = "AutoRun";
            this.toolTip1.SetToolTip(this.checkBoxAutoRun, "Load at Windows startup and run with last settings.");
            this.checkBoxAutoRun.UseVisualStyleBackColor = true;
            this.checkBoxAutoRun.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBoxNoti
            // 
            this.checkBoxNoti.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNoti.AutoSize = true;
            this.checkBoxNoti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoti.ForeColor = System.Drawing.SystemColors.Desktop;
            this.checkBoxNoti.Location = new System.Drawing.Point(26, 63);
            this.checkBoxNoti.Name = "checkBoxNoti";
            this.checkBoxNoti.Size = new System.Drawing.Size(119, 19);
            this.checkBoxNoti.TabIndex = 7;
            this.checkBoxNoti.Text = "Show notification";
            this.toolTip1.SetToolTip(this.checkBoxNoti, "Show notification");
            this.checkBoxNoti.UseVisualStyleBackColor = true;
            this.checkBoxNoti.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.checkBox4.Location = new System.Drawing.Point(366, 273);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(104, 19);
            this.checkBox4.TabIndex = 25;
            this.checkBox4.Text = "Use password";
            this.toolTip1.SetToolTip(this.checkBox4, "Require password to exit. If you close this app unexpectedly\r\nthis PC will shutdo" +
        "wn instantly.");
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // buttonBack
            // 
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.Color.Black;
            this.buttonBack.Location = new System.Drawing.Point(49, 82);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(72, 29);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "&Back";
            this.toolTip1.SetToolTip(this.buttonBack, "Back to options");
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.button6_Click);
            // 
            // radioButtonShutdown
            // 
            this.radioButtonShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonShutdown.AutoSize = true;
            this.radioButtonShutdown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonShutdown.Location = new System.Drawing.Point(13, 24);
            this.radioButtonShutdown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonShutdown.Name = "radioButtonShutdown";
            this.radioButtonShutdown.Size = new System.Drawing.Size(92, 19);
            this.radioButtonShutdown.TabIndex = 2;
            this.radioButtonShutdown.TabStop = true;
            this.radioButtonShutdown.Text = "Shut down";
            this.toolTip1.SetToolTip(this.radioButtonShutdown, "Close all apps and turn off the PC");
            this.radioButtonShutdown.UseVisualStyleBackColor = true;
            this.radioButtonShutdown.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.ForeColor = System.Drawing.Color.White;
            this.buttonMinimize.Location = new System.Drawing.Point(318, 0);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(25, 24);
            this.buttonMinimize.TabIndex = 32;
            this.buttonMinimize.Text = "-";
            this.toolTip1.SetToolTip(this.buttonMinimize, "Minimize");
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.label12_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(346, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(25, 24);
            this.buttonExit.TabIndex = 31;
            this.buttonExit.Text = "X";
            this.toolTip1.SetToolTip(this.buttonExit, "Close");
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.label11_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(60, 86);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar1.Maximum = 2880;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(208, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(276, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "1 min";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonClose.Location = new System.Drawing.Point(247, 305);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(95, 38);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Exit";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelRealTime
            // 
            this.panelRealTime.BackColor = System.Drawing.Color.White;
            this.panelRealTime.Controls.Add(this.numericUpDownMinute);
            this.panelRealTime.Controls.Add(this.label10);
            this.panelRealTime.Controls.Add(this.numericUpDownHour);
            this.panelRealTime.Controls.Add(this.label9);
            this.panelRealTime.Controls.Add(this.labelCurrentTime);
            this.panelRealTime.Controls.Add(this.labelCurrentTimeInfo);
            this.panelRealTime.Location = new System.Drawing.Point(0, 58);
            this.panelRealTime.Name = "panelRealTime";
            this.panelRealTime.Size = new System.Drawing.Size(372, 66);
            this.panelRealTime.TabIndex = 26;
            // 
            // numericUpDownMinute
            // 
            this.numericUpDownMinute.BackColor = System.Drawing.Color.White;
            this.numericUpDownMinute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownMinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMinute.Location = new System.Drawing.Point(218, 39);
            this.numericUpDownMinute.Name = "numericUpDownMinute";
            this.numericUpDownMinute.Size = new System.Drawing.Size(57, 21);
            this.numericUpDownMinute.TabIndex = 5;
            this.numericUpDownMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMinute.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(206, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = ":";
            // 
            // numericUpDownHour
            // 
            this.numericUpDownHour.BackColor = System.Drawing.Color.White;
            this.numericUpDownHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHour.Location = new System.Drawing.Point(146, 39);
            this.numericUpDownHour.Name = "numericUpDownHour";
            this.numericUpDownHour.Size = new System.Drawing.Size(57, 21);
            this.numericUpDownHour.TabIndex = 3;
            this.numericUpDownHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownHour.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(92, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Setting:";
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.AutoSize = true;
            this.labelCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentTime.ForeColor = System.Drawing.Color.Black;
            this.labelCurrentTime.Location = new System.Drawing.Point(200, 11);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(61, 15);
            this.labelCurrentTime.TabIndex = 1;
            this.labelCurrentTime.Text = "hh:mm:ss";
            // 
            // labelCurrentTimeInfo
            // 
            this.labelCurrentTimeInfo.AutoSize = true;
            this.labelCurrentTimeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentTimeInfo.ForeColor = System.Drawing.Color.Black;
            this.labelCurrentTimeInfo.Location = new System.Drawing.Point(113, 11);
            this.labelCurrentTimeInfo.Name = "labelCurrentTimeInfo";
            this.labelCurrentTimeInfo.Size = new System.Drawing.Size(81, 15);
            this.labelCurrentTimeInfo.TabIndex = 0;
            this.labelCurrentTimeInfo.Text = "Current Time:";
            // 
            // buttonTime
            // 
            this.buttonTime.AutoSize = true;
            this.buttonTime.FlatAppearance.BorderSize = 0;
            this.buttonTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonTime.Location = new System.Drawing.Point(29, 305);
            this.buttonTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonTime.Name = "buttonTime";
            this.buttonTime.Size = new System.Drawing.Size(95, 38);
            this.buttonTime.TabIndex = 27;
            this.buttonTime.Text = "Real Time";
            this.buttonTime.UseVisualStyleBackColor = false;
            this.buttonTime.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // groupBoxModes
            // 
            this.groupBoxModes.Controls.Add(this.radioButtonSleep);
            this.groupBoxModes.Controls.Add(this.radioButtonHibernate);
            this.groupBoxModes.Controls.Add(this.radioButtonShutdown);
            this.groupBoxModes.Controls.Add(this.radioButtonRemind);
            this.groupBoxModes.Location = new System.Drawing.Point(12, 130);
            this.groupBoxModes.Name = "groupBoxModes";
            this.groupBoxModes.Size = new System.Drawing.Size(174, 142);
            this.groupBoxModes.TabIndex = 28;
            this.groupBoxModes.TabStop = false;
            this.groupBoxModes.Text = "Main Modes";
            // 
            // groupBoxOption
            // 
            this.groupBoxOption.Controls.Add(this.panelShowNoti);
            this.groupBoxOption.Controls.Add(this.checkBoxNoti);
            this.groupBoxOption.Controls.Add(this.checkBoxAutoRun);
            this.groupBoxOption.Controls.Add(this.checkBoxSafeMode);
            this.groupBoxOption.Location = new System.Drawing.Point(192, 130);
            this.groupBoxOption.Name = "groupBoxOption";
            this.groupBoxOption.Size = new System.Drawing.Size(168, 143);
            this.groupBoxOption.TabIndex = 29;
            this.groupBoxOption.TabStop = false;
            this.groupBoxOption.Text = "Options";
            // 
            // panelShowNoti
            // 
            this.panelShowNoti.Controls.Add(this.buttonBack);
            this.panelShowNoti.Controls.Add(this.checkBox30sec);
            this.panelShowNoti.Controls.Add(this.checkBox1min);
            this.panelShowNoti.Controls.Add(this.checkBox5min);
            this.panelShowNoti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShowNoti.Location = new System.Drawing.Point(3, 17);
            this.panelShowNoti.Name = "panelShowNoti";
            this.panelShowNoti.Size = new System.Drawing.Size(162, 123);
            this.panelShowNoti.TabIndex = 24;
            // 
            // checkBox30sec
            // 
            this.checkBox30sec.AutoSize = true;
            this.checkBox30sec.Checked = true;
            this.checkBox30sec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox30sec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox30sec.ForeColor = System.Drawing.Color.Black;
            this.checkBox30sec.Location = new System.Drawing.Point(9, 57);
            this.checkBox30sec.Name = "checkBox30sec";
            this.checkBox30sec.Size = new System.Drawing.Size(151, 19);
            this.checkBox30sec.TabIndex = 2;
            this.checkBox30sec.Text = "30 seconds notification";
            this.checkBox30sec.UseVisualStyleBackColor = true;
            this.checkBox30sec.CheckedChanged += new System.EventHandler(this.checkBox30sec_CheckedChanged);
            // 
            // checkBox1min
            // 
            this.checkBox1min.AutoSize = true;
            this.checkBox1min.Checked = true;
            this.checkBox1min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1min.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1min.ForeColor = System.Drawing.Color.Black;
            this.checkBox1min.Location = new System.Drawing.Point(9, 32);
            this.checkBox1min.Name = "checkBox1min";
            this.checkBox1min.Size = new System.Drawing.Size(136, 19);
            this.checkBox1min.TabIndex = 1;
            this.checkBox1min.Text = "1 minute notification";
            this.checkBox1min.UseVisualStyleBackColor = true;
            this.checkBox1min.CheckedChanged += new System.EventHandler(this.checkBox1min_CheckedChanged);
            // 
            // checkBox5min
            // 
            this.checkBox5min.AutoSize = true;
            this.checkBox5min.Checked = true;
            this.checkBox5min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5min.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox5min.ForeColor = System.Drawing.Color.Black;
            this.checkBox5min.Location = new System.Drawing.Point(9, 7);
            this.checkBox5min.Name = "checkBox5min";
            this.checkBox5min.Size = new System.Drawing.Size(142, 19);
            this.checkBox5min.TabIndex = 0;
            this.checkBox5min.Text = "5 minutes notification";
            this.checkBox5min.UseVisualStyleBackColor = true;
            this.checkBox5min.CheckedChanged += new System.EventHandler(this.checkBox5min_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AS.Properties.Resources.off_button;
            this.pictureBox1.Location = new System.Drawing.Point(74, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.buttonMinimize);
            this.panel3.Controls.Add(this.buttonExit);
            this.panel3.Controls.Add(this.labelAbout);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 58);
            this.panel3.TabIndex = 31;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            // 
            // buttonStart
            // 
            this.buttonStart.AutoSize = true;
            this.buttonStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStart.FlatAppearance.BorderSize = 0;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonStart.Location = new System.Drawing.Point(132, 305);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(107, 38);
            this.buttonStart.TabIndex = 32;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(371, 355);
            this.ControlBox = false;
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.groupBoxModes);
            this.Controls.Add(this.panelRealTime);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.buttonTime);
            this.Controls.Add(this.progressBarTime);
            this.Controls.Add(this.groupBoxOption);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Main_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panelRealTime.ResumeLayout(false);
            this.panelRealTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).EndInit();
            this.groupBoxModes.ResumeLayout(false);
            this.groupBoxModes.PerformLayout();
            this.groupBoxOption.ResumeLayout(false);
            this.groupBoxOption.PerformLayout();
            this.panelShowNoti.ResumeLayout(false);
            this.panelShowNoti.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem shutdownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hibernateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sleepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noticeSomethingToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonShutdown;
        private System.Windows.Forms.RadioButton radioButtonHibernate;
        private System.Windows.Forms.RadioButton radioButtonSleep;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxSafeMode;
        private System.Windows.Forms.RadioButton radioButtonRemind;
        private System.Windows.Forms.CheckBox checkBoxNoti;
        private System.Windows.Forms.ProgressBar progressBarTime;
        private System.Windows.Forms.CheckBox checkBoxAutoRun;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Panel panelRealTime;
        private System.Windows.Forms.NumericUpDown numericUpDownMinute;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownHour;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelCurrentTimeInfo;
        private System.Windows.Forms.Button buttonTime;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.GroupBox groupBoxModes;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelShowNoti;
        private System.Windows.Forms.CheckBox checkBox30sec;
        private System.Windows.Forms.CheckBox checkBox1min;
        private System.Windows.Forms.CheckBox checkBox5min;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonStart;
    }
}

