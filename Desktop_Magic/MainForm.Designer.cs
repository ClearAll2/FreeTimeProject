namespace DM
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.musicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.trackBarDirection = new System.Windows.Forms.TrackBar();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.trackBarNumber = new System.Windows.Forms.TrackBar();
            this.libraryButton = new System.Windows.Forms.Button();
            this.mixButton = new System.Windows.Forms.RadioButton();
            this.customButton = new System.Windows.Forms.RadioButton();
            this.sakuraButton = new System.Windows.Forms.RadioButton();
            this.buttonShortcut = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelAbout = new System.Windows.Forms.Label();
            this.buttonRandomConfig = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.customizeButton = new System.Windows.Forms.Button();
            this.musicButton = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.tipsLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonCancelCustom = new System.Windows.Forms.Button();
            this.labelSizeCustom = new System.Windows.Forms.Label();
            this.trackBarSizeCustom = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonOKCustom = new System.Windows.Forms.Button();
            this.pictureBoxCustom = new System.Windows.Forms.PictureBox();
            this.snowButton = new System.Windows.Forms.RadioButton();
            this.leafButton = new System.Windows.Forms.RadioButton();
            this.backButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.labelNumber = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumber)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSizeCustom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCustom)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.mainWindowToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripSeparator3,
            this.musicToolStripMenuItem,
            this.toolStripSeparator2,
            this.configToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 160);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // mainWindowToolStripMenuItem
            // 
            this.mainWindowToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainWindowToolStripMenuItem.Name = "mainWindowToolStripMenuItem";
            this.mainWindowToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.mainWindowToolStripMenuItem.Text = "Main Window";
            this.mainWindowToolStripMenuItem.Click += new System.EventHandler(this.mainWindowToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.optionsToolStripMenuItem.Text = "Customize";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // musicToolStripMenuItem
            // 
            this.musicToolStripMenuItem.Name = "musicToolStripMenuItem";
            this.musicToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.musicToolStripMenuItem.Text = "Music Player";
            this.musicToolStripMenuItem.Click += new System.EventHandler(this.musicToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.configToolStripMenuItem.Text = "Random Configuration";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(193, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(6, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(162, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Run at Windows startup";
            this.toolTip1.SetToolTip(this.checkBox1, "Start this application when Windows startup.");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // updateButton
            // 
            this.updateButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.updateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.updateButton.Location = new System.Drawing.Point(228, 157);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(142, 26);
            this.updateButton.TabIndex = 10;
            this.updateButton.Text = "Up to date";
            this.toolTip1.SetToolTip(this.updateButton, "Automatically check for update");
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(6, 97);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(169, 17);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "All random and automatic";
            this.toolTip1.SetToolTip(this.checkBox3, "Automatically select direction, speed, number and type.\r\nChange every thing every" +
        " 5 minutes (default). Read readme\r\nfor more information.");
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // trackBarDirection
            // 
            this.trackBarDirection.LargeChange = 2;
            this.trackBarDirection.Location = new System.Drawing.Point(48, 16);
            this.trackBarDirection.Minimum = -10;
            this.trackBarDirection.Name = "trackBarDirection";
            this.trackBarDirection.Size = new System.Drawing.Size(297, 45);
            this.trackBarDirection.TabIndex = 7;
            this.trackBarDirection.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trackBarDirection, "Alt + Left/Right");
            this.trackBarDirection.Value = 10;
            this.trackBarDirection.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.LargeChange = 2;
            this.trackBarSpeed.Location = new System.Drawing.Point(48, 56);
            this.trackBarSpeed.Maximum = 15;
            this.trackBarSpeed.Minimum = 1;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(297, 45);
            this.trackBarSpeed.TabIndex = 8;
            this.trackBarSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trackBarSpeed, "Ctrl + Left/Right");
            this.trackBarSpeed.Value = 5;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBarNumber
            // 
            this.trackBarNumber.Location = new System.Drawing.Point(48, 93);
            this.trackBarNumber.Maximum = 40;
            this.trackBarNumber.Minimum = 1;
            this.trackBarNumber.Name = "trackBarNumber";
            this.trackBarNumber.Size = new System.Drawing.Size(297, 45);
            this.trackBarNumber.TabIndex = 9;
            this.trackBarNumber.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trackBarNumber, "Shift + Left/Right");
            this.trackBarNumber.Value = 20;
            this.trackBarNumber.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // libraryButton
            // 
            this.libraryButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.libraryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.libraryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.libraryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.libraryButton.Location = new System.Drawing.Point(208, 126);
            this.libraryButton.Name = "libraryButton";
            this.libraryButton.Size = new System.Drawing.Size(156, 27);
            this.libraryButton.TabIndex = 16;
            this.libraryButton.Text = "Online Library";
            this.toolTip1.SetToolTip(this.libraryButton, "Get more custom images");
            this.libraryButton.UseVisualStyleBackColor = true;
            this.libraryButton.Click += new System.EventHandler(this.button9_Click);
            // 
            // mixButton
            // 
            this.mixButton.AutoSize = true;
            this.mixButton.Location = new System.Drawing.Point(122, 139);
            this.mixButton.Name = "mixButton";
            this.mixButton.Size = new System.Drawing.Size(41, 17);
            this.mixButton.TabIndex = 18;
            this.mixButton.Text = "Mix";
            this.toolTip1.SetToolTip(this.mixButton, "Combine Leaf, Snow, Sakura and custom image");
            this.mixButton.UseVisualStyleBackColor = true;
            this.mixButton.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // customButton
            // 
            this.customButton.AutoSize = true;
            this.customButton.Location = new System.Drawing.Point(124, 161);
            this.customButton.Name = "customButton";
            this.customButton.Size = new System.Drawing.Size(60, 17);
            this.customButton.TabIndex = 13;
            this.customButton.Text = "Custom";
            this.toolTip1.SetToolTip(this.customButton, "You should choose images which have no background and small size");
            this.customButton.UseVisualStyleBackColor = true;
            this.customButton.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // sakuraButton
            // 
            this.sakuraButton.AutoSize = true;
            this.sakuraButton.Location = new System.Drawing.Point(12, 161);
            this.sakuraButton.Name = "sakuraButton";
            this.sakuraButton.Size = new System.Drawing.Size(59, 17);
            this.sakuraButton.TabIndex = 12;
            this.sakuraButton.Text = "Sakura";
            this.toolTip1.SetToolTip(this.sakuraButton, "You should choose images which have no background and small size");
            this.sakuraButton.UseVisualStyleBackColor = true;
            this.sakuraButton.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // buttonShortcut
            // 
            this.buttonShortcut.BackColor = System.Drawing.Color.Transparent;
            this.buttonShortcut.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonShortcut.FlatAppearance.BorderSize = 0;
            this.buttonShortcut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonShortcut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShortcut.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonShortcut.Location = new System.Drawing.Point(306, 5);
            this.buttonShortcut.Name = "buttonShortcut";
            this.buttonShortcut.Size = new System.Drawing.Size(20, 20);
            this.buttonShortcut.TabIndex = 12;
            this.buttonShortcut.Text = "?";
            this.toolTip1.SetToolTip(this.buttonShortcut, "Hotkeys Info");
            this.buttonShortcut.UseVisualStyleBackColor = false;
            this.buttonShortcut.Click += new System.EventHandler(this.label18_Click);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMinimize.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonMinimize.Location = new System.Drawing.Point(332, 5);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(20, 20);
            this.buttonMinimize.TabIndex = 11;
            this.buttonMinimize.Text = "-";
            this.toolTip1.SetToolTip(this.buttonMinimize, "Minimize");
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.label15_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonClose.Location = new System.Drawing.Point(358, 5);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(20, 20);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "X";
            this.toolTip1.SetToolTip(this.buttonClose, "Close");
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.label14_Click);
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.BackColor = System.Drawing.Color.Transparent;
            this.labelAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbout.ForeColor = System.Drawing.SystemColors.Control;
            this.labelAbout.Location = new System.Drawing.Point(151, 9);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(116, 39);
            this.labelAbout.TabIndex = 0;
            this.labelAbout.Text = "Desktop Magic v2x\r\nCopyright ©  2020\r\nFreedom Software";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.labelAbout, "About");
            this.labelAbout.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonRandomConfig
            // 
            this.buttonRandomConfig.BackColor = System.Drawing.Color.Transparent;
            this.buttonRandomConfig.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonRandomConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonRandomConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRandomConfig.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonRandomConfig.ImageIndex = 0;
            this.buttonRandomConfig.ImageList = this.imageList1;
            this.buttonRandomConfig.Location = new System.Drawing.Point(176, 88);
            this.buttonRandomConfig.Name = "buttonRandomConfig";
            this.buttonRandomConfig.Size = new System.Drawing.Size(30, 30);
            this.buttonRandomConfig.TabIndex = 14;
            this.toolTip1.SetToolTip(this.buttonRandomConfig, "Random Configuration");
            this.buttonRandomConfig.UseVisualStyleBackColor = false;
            this.buttonRandomConfig.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "settings.png");
            // 
            // startButton
            // 
            this.startButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.startButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.startButton.Location = new System.Drawing.Point(228, 92);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(142, 26);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(6, 65);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(196, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Hide this dialog box at startup";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // customizeButton
            // 
            this.customizeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.customizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.customizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customizeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customizeButton.Location = new System.Drawing.Point(228, 124);
            this.customizeButton.Name = "customizeButton";
            this.customizeButton.Size = new System.Drawing.Size(142, 26);
            this.customizeButton.TabIndex = 2;
            this.customizeButton.Text = "Customize";
            this.customizeButton.UseVisualStyleBackColor = false;
            this.customizeButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // musicButton
            // 
            this.musicButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.musicButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.musicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.musicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.musicButton.Location = new System.Drawing.Point(228, 189);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(142, 26);
            this.musicButton.TabIndex = 12;
            this.musicButton.Text = "Music Player";
            this.musicButton.UseVisualStyleBackColor = false;
            this.musicButton.Click += new System.EventHandler(this.button11_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRandomConfig);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 123);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(96, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "Homepage:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(164, 229);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(105, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://clearallsoft.cf";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(33, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 15);
            this.label17.TabIndex = 18;
            this.label17.Text = "Tips:";
            // 
            // tipsLabel
            // 
            this.tipsLabel.AutoSize = true;
            this.tipsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipsLabel.ForeColor = System.Drawing.Color.Blue;
            this.tipsLabel.Location = new System.Drawing.Point(77, 66);
            this.tipsLabel.Name = "tipsLabel";
            this.tipsLabel.Size = new System.Drawing.Size(61, 15);
            this.tipsLabel.TabIndex = 19;
            this.tipsLabel.Text = "Loading...";
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.sakuraButton);
            this.panel2.Controls.Add(this.customButton);
            this.panel2.Controls.Add(this.snowButton);
            this.panel2.Controls.Add(this.leafButton);
            this.panel2.Controls.Add(this.backButton);
            this.panel2.Controls.Add(this.applyButton);
            this.panel2.Controls.Add(this.labelNumber);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.labelSpeed);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.labelDirection);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.mixButton);
            this.panel2.Controls.Add(this.libraryButton);
            this.panel2.Controls.Add(this.trackBarNumber);
            this.panel2.Controls.Add(this.trackBarSpeed);
            this.panel2.Controls.Add(this.trackBarDirection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 199);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonCancelCustom);
            this.panel3.Controls.Add(this.labelSizeCustom);
            this.panel3.Controls.Add(this.trackBarSizeCustom);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.buttonOKCustom);
            this.panel3.Controls.Add(this.pictureBoxCustom);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 199);
            this.panel3.TabIndex = 17;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // buttonCancelCustom
            // 
            this.buttonCancelCustom.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonCancelCustom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonCancelCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelCustom.Location = new System.Drawing.Point(99, 153);
            this.buttonCancelCustom.Name = "buttonCancelCustom";
            this.buttonCancelCustom.Size = new System.Drawing.Size(81, 29);
            this.buttonCancelCustom.TabIndex = 6;
            this.buttonCancelCustom.Text = "Cancel";
            this.buttonCancelCustom.UseVisualStyleBackColor = true;
            this.buttonCancelCustom.Click += new System.EventHandler(this.button12_Click);
            // 
            // labelSizeCustom
            // 
            this.labelSizeCustom.AutoSize = true;
            this.labelSizeCustom.Location = new System.Drawing.Point(133, 31);
            this.labelSizeCustom.Name = "labelSizeCustom";
            this.labelSizeCustom.Size = new System.Drawing.Size(19, 13);
            this.labelSizeCustom.TabIndex = 5;
            this.labelSizeCustom.Text = "50";
            // 
            // trackBarSizeCustom
            // 
            this.trackBarSizeCustom.Location = new System.Drawing.Point(15, 61);
            this.trackBarSizeCustom.Maximum = 200;
            this.trackBarSizeCustom.Minimum = 1;
            this.trackBarSizeCustom.Name = "trackBarSizeCustom";
            this.trackBarSizeCustom.Size = new System.Drawing.Size(174, 45);
            this.trackBarSizeCustom.TabIndex = 4;
            this.trackBarSizeCustom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarSizeCustom.Value = 50;
            this.trackBarSizeCustom.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "Maximum Size:";
            // 
            // buttonOKCustom
            // 
            this.buttonOKCustom.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonOKCustom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonOKCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOKCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOKCustom.Location = new System.Drawing.Point(12, 153);
            this.buttonOKCustom.Name = "buttonOKCustom";
            this.buttonOKCustom.Size = new System.Drawing.Size(81, 29);
            this.buttonOKCustom.TabIndex = 2;
            this.buttonOKCustom.Text = "Ok";
            this.buttonOKCustom.UseVisualStyleBackColor = true;
            this.buttonOKCustom.Click += new System.EventHandler(this.button10_Click);
            // 
            // pictureBoxCustom
            // 
            this.pictureBoxCustom.Location = new System.Drawing.Point(197, 32);
            this.pictureBoxCustom.Name = "pictureBoxCustom";
            this.pictureBoxCustom.Size = new System.Drawing.Size(132, 119);
            this.pictureBoxCustom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCustom.TabIndex = 0;
            this.pictureBoxCustom.TabStop = false;
            // 
            // snowButton
            // 
            this.snowButton.AutoSize = true;
            this.snowButton.Location = new System.Drawing.Point(64, 138);
            this.snowButton.Name = "snowButton";
            this.snowButton.Size = new System.Drawing.Size(52, 17);
            this.snowButton.TabIndex = 11;
            this.snowButton.Text = "Snow";
            this.snowButton.UseVisualStyleBackColor = true;
            this.snowButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // leafButton
            // 
            this.leafButton.AutoSize = true;
            this.leafButton.Location = new System.Drawing.Point(12, 138);
            this.leafButton.Name = "leafButton";
            this.leafButton.Size = new System.Drawing.Size(46, 17);
            this.leafButton.TabIndex = 10;
            this.leafButton.Text = "Leaf";
            this.leafButton.UseVisualStyleBackColor = true;
            this.leafButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // backButton
            // 
            this.backButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Location = new System.Drawing.Point(289, 159);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 27);
            this.backButton.TabIndex = 15;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.button8_Click);
            // 
            // applyButton
            // 
            this.applyButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.applyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.Location = new System.Drawing.Point(208, 159);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 27);
            this.applyButton.TabIndex = 14;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.button7_Click);
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(351, 93);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(13, 13);
            this.labelNumber.TabIndex = 8;
            this.labelNumber.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Number";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(351, 56);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(13, 13);
            this.labelSpeed.TabIndex = 6;
            this.labelSpeed.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Speed";
            // 
            // labelDirection
            // 
            this.labelDirection.AutoSize = true;
            this.labelDirection.Location = new System.Drawing.Point(351, 16);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(13, 13);
            this.labelDirection.TabIndex = 4;
            this.labelDirection.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Direction";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.buttonShortcut);
            this.panel4.Controls.Add(this.buttonMinimize);
            this.panel4.Controls.Add(this.buttonClose);
            this.panel4.Controls.Add(this.labelAbout);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.SystemColors.Control;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(382, 58);
            this.panel4.TabIndex = 14;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(99, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(382, 257);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.musicButton);
            this.Controls.Add(this.customizeButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.tipsLabel);
            this.Controls.Add(this.label17);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DM";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumber)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSizeCustom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCustom)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button customizeButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.ToolStripMenuItem musicToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button musicButton;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label tipsLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonCancelCustom;
        private System.Windows.Forms.Label labelSizeCustom;
        private System.Windows.Forms.TrackBar trackBarSizeCustom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonOKCustom;
        private System.Windows.Forms.PictureBox pictureBoxCustom;
        private System.Windows.Forms.RadioButton sakuraButton;
        private System.Windows.Forms.RadioButton customButton;
        private System.Windows.Forms.RadioButton snowButton;
        private System.Windows.Forms.RadioButton leafButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton mixButton;
        private System.Windows.Forms.Button libraryButton;
        private System.Windows.Forms.TrackBar trackBarNumber;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.TrackBar trackBarDirection;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonShortcut;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Button buttonRandomConfig;
        private System.Windows.Forms.ImageList imageList1;
    }
}