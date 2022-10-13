namespace LScanner
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
            this.listViewMain = new System.Windows.Forms.ListView();
            this.columnHeaderIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelPublicIP = new System.Windows.Forms.Label();
            this.labelRealStatus = new System.Windows.Forms.Label();
            this.labelLocalIP = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonScan = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScan = new System.Windows.Forms.TabPage();
            this.comboBoxPublicIP = new System.Windows.Forms.ComboBox();
            this.comboBoxPrivateIP = new System.Windows.Forms.ComboBox();
            this.tabPageNetInterfaces = new System.Windows.Forms.TabPage();
            this.labelNetInterfaceStatus = new System.Windows.Forms.Label();
            this.labelInterfaceStatus = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.lblDownload = new System.Windows.Forms.Label();
            this.labelUpload = new System.Windows.Forms.Label();
            this.labelDownload = new System.Windows.Forms.Label();
            this.lblInterfaceType = new System.Windows.Forms.Label();
            this.labelInterfaceType = new System.Windows.Forms.Label();
            this.lblBytesReceived = new System.Windows.Forms.Label();
            this.lblBytesSent = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.labelByteReceived = new System.Windows.Forms.Label();
            this.labelByteSent = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.lblInterface = new System.Windows.Forms.Label();
            this.cmbInterface = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBoxSetting = new System.Windows.Forms.GroupBox();
            this.groupBoxNote = new System.Windows.Forms.GroupBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.radioButtonFastScan = new System.Windows.Forms.RadioButton();
            this.radioButtonNormalScan = new System.Windows.Forms.RadioButton();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.labelAbout = new System.Windows.Forms.Label();
            this.buttonHomePage = new System.Windows.Forms.Button();
            this.buttonCheckUpdate = new System.Windows.Forms.Button();
            this.labelAboutInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageScan.SuspendLayout();
            this.tabPageNetInterfaces.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBoxSetting.SuspendLayout();
            this.groupBoxNote.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMain
            // 
            this.listViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIP,
            this.columnHeaderHostName,
            this.columnHeaderMAC});
            this.listViewMain.FullRowSelect = true;
            this.listViewMain.HideSelection = false;
            this.listViewMain.Location = new System.Drawing.Point(9, 4);
            this.listViewMain.Margin = new System.Windows.Forms.Padding(4);
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(517, 335);
            this.listViewMain.TabIndex = 0;
            this.listViewMain.UseCompatibleStateImageBehavior = false;
            this.listViewMain.View = System.Windows.Forms.View.Details;
            this.listViewMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listViewMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeaderIP
            // 
            this.columnHeaderIP.Text = "IP";
            this.columnHeaderIP.Width = 81;
            // 
            // columnHeaderHostName
            // 
            this.columnHeaderHostName.Text = "Hostname";
            this.columnHeaderHostName.Width = 180;
            // 
            // columnHeaderMAC
            // 
            this.columnHeaderMAC.Text = "MAC";
            this.columnHeaderMAC.Width = 109;
            // 
            // labelPublicIP
            // 
            this.labelPublicIP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPublicIP.AutoSize = true;
            this.labelPublicIP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPublicIP.Location = new System.Drawing.Point(25, 425);
            this.labelPublicIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPublicIP.Name = "labelPublicIP";
            this.labelPublicIP.Size = new System.Drawing.Size(156, 16);
            this.labelPublicIP.TabIndex = 9;
            this.labelPublicIP.Text = "This computer internet IP:";
            this.toolTip1.SetToolTip(this.labelPublicIP, "Click to see details");
            // 
            // labelRealStatus
            // 
            this.labelRealStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelRealStatus.AutoSize = true;
            this.labelRealStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRealStatus.ForeColor = System.Drawing.Color.Red;
            this.labelRealStatus.Location = new System.Drawing.Point(186, 475);
            this.labelRealStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRealStatus.Name = "labelRealStatus";
            this.labelRealStatus.Size = new System.Drawing.Size(43, 13);
            this.labelRealStatus.TabIndex = 7;
            this.labelRealStatus.Text = "Ready";
            // 
            // labelLocalIP
            // 
            this.labelLocalIP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelLocalIP.AutoSize = true;
            this.labelLocalIP.Location = new System.Drawing.Point(39, 367);
            this.labelLocalIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLocalIP.Name = "labelLocalIP";
            this.labelLocalIP.Size = new System.Drawing.Size(142, 16);
            this.labelLocalIP.TabIndex = 0;
            this.labelLocalIP.Text = "This computer local IP:";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(131, 475);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(47, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status:";
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(324, 508);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(108, 39);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "&Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonScan
            // 
            this.buttonScan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonScan.Location = new System.Drawing.Point(189, 508);
            this.buttonScan.Margin = new System.Windows.Forms.Padding(4);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(108, 39);
            this.buttonScan.TabIndex = 1;
            this.buttonScan.Text = "&Scan";
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem,
            this.resolveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 48);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.pingToolStripMenuItem.Text = "Ping";
            this.pingToolStripMenuItem.Click += new System.EventHandler(this.pingToolStripMenuItem_Click);
            // 
            // resolveToolStripMenuItem
            // 
            this.resolveToolStripMenuItem.Name = "resolveToolStripMenuItem";
            this.resolveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.resolveToolStripMenuItem.Text = "Resolve";
            this.resolveToolStripMenuItem.Click += new System.EventHandler(this.resolveToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageScan);
            this.tabControl1.Controls.Add(this.tabPageNetInterfaces);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(543, 592);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageScan
            // 
            this.tabPageScan.Controls.Add(this.comboBoxPublicIP);
            this.tabPageScan.Controls.Add(this.comboBoxPrivateIP);
            this.tabPageScan.Controls.Add(this.labelRealStatus);
            this.tabPageScan.Controls.Add(this.buttonScan);
            this.tabPageScan.Controls.Add(this.buttonStop);
            this.tabPageScan.Controls.Add(this.labelPublicIP);
            this.tabPageScan.Controls.Add(this.labelStatus);
            this.tabPageScan.Controls.Add(this.labelLocalIP);
            this.tabPageScan.Controls.Add(this.listViewMain);
            this.tabPageScan.Location = new System.Drawing.Point(4, 25);
            this.tabPageScan.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageScan.Name = "tabPageScan";
            this.tabPageScan.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageScan.Size = new System.Drawing.Size(535, 563);
            this.tabPageScan.TabIndex = 0;
            this.tabPageScan.Text = "Scan Network";
            this.tabPageScan.UseVisualStyleBackColor = true;
            // 
            // comboBoxPublicIP
            // 
            this.comboBoxPublicIP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxPublicIP.BackColor = System.Drawing.Color.White;
            this.comboBoxPublicIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPublicIP.FormattingEnabled = true;
            this.comboBoxPublicIP.Location = new System.Drawing.Point(189, 422);
            this.comboBoxPublicIP.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPublicIP.Name = "comboBoxPublicIP";
            this.comboBoxPublicIP.Size = new System.Drawing.Size(240, 24);
            this.comboBoxPublicIP.TabIndex = 12;
            // 
            // comboBoxPrivateIP
            // 
            this.comboBoxPrivateIP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxPrivateIP.BackColor = System.Drawing.Color.White;
            this.comboBoxPrivateIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrivateIP.FormattingEnabled = true;
            this.comboBoxPrivateIP.Location = new System.Drawing.Point(189, 364);
            this.comboBoxPrivateIP.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPrivateIP.Name = "comboBoxPrivateIP";
            this.comboBoxPrivateIP.Size = new System.Drawing.Size(240, 24);
            this.comboBoxPrivateIP.TabIndex = 11;
            // 
            // tabPageNetInterfaces
            // 
            this.tabPageNetInterfaces.Controls.Add(this.labelNetInterfaceStatus);
            this.tabPageNetInterfaces.Controls.Add(this.labelInterfaceStatus);
            this.tabPageNetInterfaces.Controls.Add(this.lblUpload);
            this.tabPageNetInterfaces.Controls.Add(this.lblDownload);
            this.tabPageNetInterfaces.Controls.Add(this.labelUpload);
            this.tabPageNetInterfaces.Controls.Add(this.labelDownload);
            this.tabPageNetInterfaces.Controls.Add(this.lblInterfaceType);
            this.tabPageNetInterfaces.Controls.Add(this.labelInterfaceType);
            this.tabPageNetInterfaces.Controls.Add(this.lblBytesReceived);
            this.tabPageNetInterfaces.Controls.Add(this.lblBytesSent);
            this.tabPageNetInterfaces.Controls.Add(this.lblSpeed);
            this.tabPageNetInterfaces.Controls.Add(this.labelByteReceived);
            this.tabPageNetInterfaces.Controls.Add(this.labelByteSent);
            this.tabPageNetInterfaces.Controls.Add(this.labelSpeed);
            this.tabPageNetInterfaces.Controls.Add(this.lblInterface);
            this.tabPageNetInterfaces.Controls.Add(this.cmbInterface);
            this.tabPageNetInterfaces.Location = new System.Drawing.Point(4, 25);
            this.tabPageNetInterfaces.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageNetInterfaces.Name = "tabPageNetInterfaces";
            this.tabPageNetInterfaces.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageNetInterfaces.Size = new System.Drawing.Size(535, 563);
            this.tabPageNetInterfaces.TabIndex = 1;
            this.tabPageNetInterfaces.Text = "Network Interfaces";
            this.tabPageNetInterfaces.UseVisualStyleBackColor = true;
            // 
            // labelNetInterfaceStatus
            // 
            this.labelNetInterfaceStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelNetInterfaceStatus.AutoSize = true;
            this.labelNetInterfaceStatus.Location = new System.Drawing.Point(209, 383);
            this.labelNetInterfaceStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNetInterfaceStatus.Name = "labelNetInterfaceStatus";
            this.labelNetInterfaceStatus.Size = new System.Drawing.Size(11, 16);
            this.labelNetInterfaceStatus.TabIndex = 31;
            this.labelNetInterfaceStatus.Text = "-";
            // 
            // labelInterfaceStatus
            // 
            this.labelInterfaceStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelInterfaceStatus.AutoSize = true;
            this.labelInterfaceStatus.Location = new System.Drawing.Point(132, 383);
            this.labelInterfaceStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterfaceStatus.Name = "labelInterfaceStatus";
            this.labelInterfaceStatus.Size = new System.Drawing.Size(47, 16);
            this.labelInterfaceStatus.TabIndex = 30;
            this.labelInterfaceStatus.Text = "Status:";
            // 
            // lblUpload
            // 
            this.lblUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(209, 351);
            this.lblUpload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(14, 16);
            this.lblUpload.TabIndex = 29;
            this.lblUpload.Text = "0";
            // 
            // lblDownload
            // 
            this.lblDownload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDownload.AutoSize = true;
            this.lblDownload.Location = new System.Drawing.Point(209, 319);
            this.lblDownload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(14, 16);
            this.lblDownload.TabIndex = 28;
            this.lblDownload.Text = "0";
            // 
            // labelUpload
            // 
            this.labelUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelUpload.AutoSize = true;
            this.labelUpload.Location = new System.Drawing.Point(124, 351);
            this.labelUpload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUpload.Name = "labelUpload";
            this.labelUpload.Size = new System.Drawing.Size(55, 16);
            this.labelUpload.TabIndex = 27;
            this.labelUpload.Text = "Upload:";
            // 
            // labelDownload
            // 
            this.labelDownload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDownload.AutoSize = true;
            this.labelDownload.Location = new System.Drawing.Point(108, 319);
            this.labelDownload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDownload.Name = "labelDownload";
            this.labelDownload.Size = new System.Drawing.Size(71, 16);
            this.labelDownload.TabIndex = 26;
            this.labelDownload.Text = "Download:";
            // 
            // lblInterfaceType
            // 
            this.lblInterfaceType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInterfaceType.AutoSize = true;
            this.lblInterfaceType.Location = new System.Drawing.Point(209, 191);
            this.lblInterfaceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInterfaceType.Name = "lblInterfaceType";
            this.lblInterfaceType.Size = new System.Drawing.Size(14, 16);
            this.lblInterfaceType.TabIndex = 25;
            this.lblInterfaceType.Text = "0";
            // 
            // labelInterfaceType
            // 
            this.labelInterfaceType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelInterfaceType.AutoSize = true;
            this.labelInterfaceType.Location = new System.Drawing.Point(83, 191);
            this.labelInterfaceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterfaceType.Name = "labelInterfaceType";
            this.labelInterfaceType.Size = new System.Drawing.Size(96, 16);
            this.labelInterfaceType.TabIndex = 24;
            this.labelInterfaceType.Text = "Interface Type:";
            // 
            // lblBytesReceived
            // 
            this.lblBytesReceived.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBytesReceived.AutoSize = true;
            this.lblBytesReceived.Location = new System.Drawing.Point(209, 287);
            this.lblBytesReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBytesReceived.Name = "lblBytesReceived";
            this.lblBytesReceived.Size = new System.Drawing.Size(14, 16);
            this.lblBytesReceived.TabIndex = 23;
            this.lblBytesReceived.Text = "0";
            // 
            // lblBytesSent
            // 
            this.lblBytesSent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBytesSent.AutoSize = true;
            this.lblBytesSent.Location = new System.Drawing.Point(209, 255);
            this.lblBytesSent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBytesSent.Name = "lblBytesSent";
            this.lblBytesSent.Size = new System.Drawing.Size(14, 16);
            this.lblBytesSent.TabIndex = 22;
            this.lblBytesSent.Text = "0";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(209, 223);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(14, 16);
            this.lblSpeed.TabIndex = 21;
            this.lblSpeed.Text = "0";
            // 
            // labelByteReceived
            // 
            this.labelByteReceived.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelByteReceived.AutoSize = true;
            this.labelByteReceived.Location = new System.Drawing.Point(73, 287);
            this.labelByteReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelByteReceived.Name = "labelByteReceived";
            this.labelByteReceived.Size = new System.Drawing.Size(106, 16);
            this.labelByteReceived.TabIndex = 20;
            this.labelByteReceived.Text = "Bytes Received:";
            // 
            // labelByteSent
            // 
            this.labelByteSent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelByteSent.AutoSize = true;
            this.labelByteSent.Location = new System.Drawing.Point(105, 255);
            this.labelByteSent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelByteSent.Name = "labelByteSent";
            this.labelByteSent.Size = new System.Drawing.Size(74, 16);
            this.labelByteSent.TabIndex = 19;
            this.labelByteSent.Text = "Bytes Sent:";
            // 
            // labelSpeed
            // 
            this.labelSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(128, 223);
            this.labelSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(51, 16);
            this.labelSpeed.TabIndex = 18;
            this.labelSpeed.Text = "Speed:";
            // 
            // lblInterface
            // 
            this.lblInterface.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInterface.AutoSize = true;
            this.lblInterface.Location = new System.Drawing.Point(121, 156);
            this.lblInterface.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(58, 16);
            this.lblInterface.TabIndex = 17;
            this.lblInterface.Text = "Interface";
            // 
            // cmbInterface
            // 
            this.cmbInterface.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterface.FormattingEnabled = true;
            this.cmbInterface.Location = new System.Drawing.Point(212, 153);
            this.cmbInterface.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInterface.Name = "cmbInterface";
            this.cmbInterface.Size = new System.Drawing.Size(272, 24);
            this.cmbInterface.TabIndex = 16;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBoxSetting);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(535, 563);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Settings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBoxSetting
            // 
            this.groupBoxSetting.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxSetting.Controls.Add(this.groupBoxNote);
            this.groupBoxSetting.Controls.Add(this.radioButtonFastScan);
            this.groupBoxSetting.Controls.Add(this.radioButtonNormalScan);
            this.groupBoxSetting.Location = new System.Drawing.Point(9, 79);
            this.groupBoxSetting.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSetting.Name = "groupBoxSetting";
            this.groupBoxSetting.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSetting.Size = new System.Drawing.Size(517, 361);
            this.groupBoxSetting.TabIndex = 15;
            this.groupBoxSetting.TabStop = false;
            this.groupBoxSetting.Text = "Scan Speed";
            // 
            // groupBoxNote
            // 
            this.groupBoxNote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxNote.Controls.Add(this.labelNote);
            this.groupBoxNote.Location = new System.Drawing.Point(71, 223);
            this.groupBoxNote.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxNote.Name = "groupBoxNote";
            this.groupBoxNote.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxNote.Size = new System.Drawing.Size(368, 107);
            this.groupBoxNote.TabIndex = 16;
            this.groupBoxNote.TabStop = false;
            this.groupBoxNote.Text = "Note";
            // 
            // labelNote
            // 
            this.labelNote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelNote.AutoSize = true;
            this.labelNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNote.ForeColor = System.Drawing.Color.Blue;
            this.labelNote.Location = new System.Drawing.Point(22, 26);
            this.labelNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(308, 48);
            this.labelNote.TabIndex = 15;
            this.labelNote.Text = "This option will reset every time this app is opened.\r\nWe recommend Normal mode f" +
    "or you because of\r\nFast mode may cause lag after scanning.";
            // 
            // radioButtonFastScan
            // 
            this.radioButtonFastScan.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonFastScan.AutoSize = true;
            this.radioButtonFastScan.Location = new System.Drawing.Point(343, 104);
            this.radioButtonFastScan.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFastScan.Name = "radioButtonFastScan";
            this.radioButtonFastScan.Size = new System.Drawing.Size(51, 20);
            this.radioButtonFastScan.TabIndex = 14;
            this.radioButtonFastScan.Text = "Fast";
            this.toolTip1.SetToolTip(this.radioButtonFastScan, "Find devives faster but can cause lag after using.");
            this.radioButtonFastScan.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormalScan
            // 
            this.radioButtonNormalScan.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonNormalScan.AutoSize = true;
            this.radioButtonNormalScan.Checked = true;
            this.radioButtonNormalScan.Location = new System.Drawing.Point(71, 104);
            this.radioButtonNormalScan.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonNormalScan.Name = "radioButtonNormalScan";
            this.radioButtonNormalScan.Size = new System.Drawing.Size(174, 20);
            this.radioButtonNormalScan.TabIndex = 13;
            this.radioButtonNormalScan.TabStop = true;
            this.radioButtonNormalScan.Text = "Normal (Recommended)";
            this.radioButtonNormalScan.UseVisualStyleBackColor = true;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.labelAbout);
            this.tabPageAbout.Controls.Add(this.buttonHomePage);
            this.tabPageAbout.Controls.Add(this.buttonCheckUpdate);
            this.tabPageAbout.Controls.Add(this.labelAboutInfo);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 25);
            this.tabPageAbout.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageAbout.Size = new System.Drawing.Size(535, 563);
            this.tabPageAbout.TabIndex = 2;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbout.ForeColor = System.Drawing.Color.Blue;
            this.labelAbout.Location = new System.Drawing.Point(134, 171);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(251, 70);
            this.labelAbout.TabIndex = 14;
            this.labelAbout.Text = "LScanner v\r\nCopyright ©  2017\r\nClear All Soft";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHomePage
            // 
            this.buttonHomePage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonHomePage.Location = new System.Drawing.Point(134, 342);
            this.buttonHomePage.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHomePage.Name = "buttonHomePage";
            this.buttonHomePage.Size = new System.Drawing.Size(251, 32);
            this.buttonHomePage.TabIndex = 13;
            this.buttonHomePage.Text = "Home Page";
            this.toolTip1.SetToolTip(this.buttonHomePage, "https://mini102.wordpress.com");
            this.buttonHomePage.UseVisualStyleBackColor = true;
            this.buttonHomePage.Click += new System.EventHandler(this.buttonHomePage_Click);
            // 
            // buttonCheckUpdate
            // 
            this.buttonCheckUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCheckUpdate.Location = new System.Drawing.Point(134, 302);
            this.buttonCheckUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCheckUpdate.Name = "buttonCheckUpdate";
            this.buttonCheckUpdate.Size = new System.Drawing.Size(251, 32);
            this.buttonCheckUpdate.TabIndex = 12;
            this.buttonCheckUpdate.Text = "Check for Update";
            this.buttonCheckUpdate.UseVisualStyleBackColor = true;
            this.buttonCheckUpdate.Click += new System.EventHandler(this.buttonCheckForUpdate_Click);
            // 
            // labelAboutInfo
            // 
            this.labelAboutInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelAboutInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAboutInfo.Location = new System.Drawing.Point(134, 257);
            this.labelAboutInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAboutInfo.Name = "labelAboutInfo";
            this.labelAboutInfo.Size = new System.Drawing.Size(251, 16);
            this.labelAboutInfo.TabIndex = 10;
            this.labelAboutInfo.Text = "Scan all devices in your network";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonScan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(543, 592);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Network Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageScan.ResumeLayout(false);
            this.tabPageScan.PerformLayout();
            this.tabPageNetInterfaces.ResumeLayout(false);
            this.tabPageNetInterfaces.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBoxSetting.ResumeLayout(false);
            this.groupBoxSetting.PerformLayout();
            this.groupBoxNote.ResumeLayout(false);
            this.groupBoxNote.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ColumnHeader columnHeaderIP;
        private System.Windows.Forms.ColumnHeader columnHeaderHostName;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelLocalIP;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.ColumnHeader columnHeaderMAC;
        private System.Windows.Forms.Label labelRealStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScan;
        private System.Windows.Forms.TabPage tabPageNetInterfaces;
        private System.Windows.Forms.Label labelPublicIP;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.Label labelUpload;
        private System.Windows.Forms.Label labelDownload;
        private System.Windows.Forms.Label lblInterfaceType;
        private System.Windows.Forms.Label labelInterfaceType;
        private System.Windows.Forms.Label lblBytesReceived;
        private System.Windows.Forms.Label lblBytesSent;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label labelByteReceived;
        private System.Windows.Forms.Label labelByteSent;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.ComboBox cmbInterface;
        private System.Windows.Forms.Label labelNetInterfaceStatus;
        private System.Windows.Forms.Label labelInterfaceStatus;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton radioButtonFastScan;
        private System.Windows.Forms.RadioButton radioButtonNormalScan;
        private System.Windows.Forms.GroupBox groupBoxSetting;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.GroupBox groupBoxNote;
        private System.Windows.Forms.ToolStripMenuItem resolveToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxPrivateIP;
        private System.Windows.Forms.ComboBox comboBoxPublicIP;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Button buttonHomePage;
        private System.Windows.Forms.Button buttonCheckUpdate;
        private System.Windows.Forms.Label labelAboutInfo;
    }
}

