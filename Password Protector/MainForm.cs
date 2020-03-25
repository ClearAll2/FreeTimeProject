using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace RamC
{
    public partial class MainForm : Form
    {
        int newx;
        int newy;
        float fRam;
        float pDisk;
        double pDisk2;
        float pCpu;
        double pCpu2;
        Bitmap Time;
        RegistryKey r;
        PerformanceCounter RamC = new PerformanceCounter("Memory", "Available MBytes");
        PerformanceCounter Disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        PerformanceCounter Cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        Graphics g;
        bool Datelock = true;
        bool Cpulock = true;
        bool Disklock = true;
        bool Ramlock = true;
        bool h = false;
        bool isLong = false;

        string Datefont;
        string Diskfont;
        string Ramfont;
        string Cpufont;
        
        string Clockfont;

        int DateTop;
        int DateLeft;
        int CTop;
        int CLeft;
        int CpuTop;
        int CpuLeft;
        int DiskTop;
        int DiskLeft;
        int RamTop;
        int RamLeft;
        WebClient wc = new WebClient();
        BackgroundWorker bw2;
        
        bool lk = false;
        bool clk = false;
        string id = " ";
        string pass = " ";
        AboutBox1 box = new AboutBox1();
        
        WebClient wb = new WebClient();
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
        int blur = 5;
        Color stroke;
        
       
        public MainForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            //get default  position

            DateTop = labelDate.Top;
            DateLeft = labelDate.Left;
            CTop = labelClock.Top;
            CLeft = labelClock.Left;
            CpuTop = labelCPU.Top;
            CpuLeft = labelCPU.Left;
            DiskTop = labelDisk.Top;
            DiskLeft = labelDisk.Left;
            RamTop = labelRam.Top;
            RamLeft = labelRam.Left;


            //get default font
            Datefont = converter.ConvertToString(labelDate.Font);
            Cpufont = converter.ConvertToString(labelCPU.Font);
            Diskfont = converter.ConvertToString(labelDisk.Font);
            Ramfont = converter.ConvertToString(labelRam.Font);
           
            Clockfont = converter.ConvertToString(labelClock.Font);

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            Top = (int)r.GetValue("Top", Top);
            Left = (int)r.GetValue("Left", Left);

            if (r.GetValue("size1") != null)
            {
                this.Height = (int)r.GetValue("size1");
                this.Width = (int)r.GetValue("size2");
            }

            if (r.GetValue("shortTime") != null)
            {
                shortexcludeSecondToolStripMenuItem_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r.GetValue("longTime") != null)
            {
                longToolStripMenuItem_Click(null, null);
            }
            r.Close();
            r.Dispose();

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r.GetValue("12type") != null)
            {
                hToolStripMenuItem_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);

            if (r.GetValue("24type") != null)
            {
                hToolStripMenuItem1_Click(null, null);
            }
            r.Close();
            r.Dispose();

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r.GetValue("Datefont") != null)
            {
                labelDate.Font = (Font)converter.ConvertFromString((string)r.GetValue("Datefont"));
            }
            if (r.GetValue("Diskfont") != null)
            {
                labelDisk.Font = (Font)converter.ConvertFromString((string)r.GetValue("Diskfont"));
            }
            if (r.GetValue("Cpufont") != null)
            {
                labelCPU.Font = (Font)converter.ConvertFromString((string)r.GetValue("Cpufont"));
            }
            if (r.GetValue("Ramfont") != null)
            {
                labelRam.Font = (Font)converter.ConvertFromString((string)r.GetValue("Ramfont"));
            }
            
            if (r.GetValue("Clockfont") != null)
            {
                labelClock.Font = (Font)converter.ConvertFromString((string)r.GetValue("Clockfont"));
            }


            if (r.GetValue("DateTop") != null)
            {
                labelDate.Top = (int)r.GetValue("DateTop");
                labelDate.Left = (int)r.GetValue("DateLeft");
            }
            if (r.GetValue("CTop") != null)
            {
                labelClock.Top = (int)r.GetValue("CTop");
                labelClock.Left = (int)r.GetValue("CLeft");
            }

            if (r.GetValue("CpuTop") != null)
            {
                labelCPU.Top = (int)r.GetValue("CpuTop");
                labelCPU.Left = (int)r.GetValue("CpuLeft");
            }
            if (r.GetValue("DiskTop") != null)
            {
                labelDisk.Top = (int)r.GetValue("DiskTop");
                labelDisk.Left = (int)r.GetValue("DiskLeft");
            }
            if (r.GetValue("RamTop") != null)
            {
                labelRam.Top = (int)r.GetValue("RamTop");
                labelRam.Left = (int)r.GetValue("RamLeft");
            }
            
            if (r.GetValue("Hide") != null)
            {
                hideTrayIconToolStripMenuItem.Checked = true;
                notifyIcon1.Visible = false;
            }
            if (r.GetValue("Lock") != null)
            {
                lockLocationToolStripMenuItem.Checked = true;
                lk = true;
            }
            if (r.GetValue("CLock") != null)
            {
                lockClockLocationToolStripMenuItem.Checked = true;
                clk = true;
            }
            if (r.GetValue("Id") != null && r.GetValue("Pass") != null)
            {
                id = (string)r.GetValue("Id");
                pass = (string)r.GetValue("Pass");
               
                //saved = true;
            }
            if (r.GetValue("DateColor") != null)
            {
                int tmp = (int)r.GetValue("DateColor");
                labelDate.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("CpuColor") != null)
            {
                int tmp = (int)r.GetValue("CpuColor");
                labelCPU.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("DiskColor") != null)
            {
                int tmp = (int)r.GetValue("DiskColor");
                labelDisk.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("RamColor") != null)
            {
                int tmp = (int)r.GetValue("RamColor");
                labelRam.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("TimeColor") != null)
            {
                int tmp = (int)r.GetValue("TimeColor");
                labelClock.ForeColor = Color.FromArgb(tmp);
            }
            
            if (r.GetValue("Stroke") != null)
            {
                int tmp = (int)r.GetValue("Stroke");
                stroke = Color.FromArgb(tmp);
            }
            else
            {
                stroke = Color.White;
            }


            if (r.GetValue("transparency") != null)
            {
                noneToolStripMenuItem.Checked = true;
               
            }
            else
            {
                noneToolStripMenuItem.Checked = false;
                TransparencyKey = Color.White;
            }

            if (r.GetValue("showdate") != null)
            {
                dATEToolStripMenuItem.Checked = true;
            }
            else
            {
                dATEToolStripMenuItem_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r.GetValue("showcpu") != null)
            {
                cPUToolStripMenuItem.Checked = true;
            }
            else
            {
                closeToolStripMenuItem2_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
           
            if (r.GetValue("showdisk") != null)
            {
                dISKToolStripMenuItem.Checked = true;
            }
            else
            {
                closeToolStripMenuItem_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);


            if (r.GetValue("showram") != null)
            {
                rAMToolStripMenuItem.Checked = true;
            }
            else
            {
                closeToolStripMenuItem1_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);

           
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (r.GetValue("RamC") != null)
            {
                if (Application.ExecutablePath == (string)r.GetValue("RamC"))
                    runAtStartupToolStripMenuItem.Checked = true;
            }
            r.Close();
            r.Dispose();
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;
            labelDate.Text = DateTime.Now.Date.ToLongDateString();
        }

        //resize window

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 1;   // Caption bar height;

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //this if you want to draw   (if)

        //    Color theColor = Color.FromArgb(10, 20, 20, 20);
        //    theColor = SystemColors.Control;
        //    int BORDER_SIZE = 4;
        //    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
        //                                 theColor, BORDER_SIZE, ButtonBorderStyle.Dashed,
        //                                 theColor, BORDER_SIZE, ButtonBorderStyle.Dashed,
        //                                 theColor, BORDER_SIZE, ButtonBorderStyle.Dashed,
        //                                 theColor, BORDER_SIZE, ButtonBorderStyle.Dashed);


        //    Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
        //    //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        //    rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
        //    e.Graphics.FillRectangle(Brushes.Black, rc);



        //    base.OnPaint(e);
       // }


        //set MinimumSize to Form
        //public override Size MinimumSize
        //{
        //    get
        //    {
        //        return base.MinimumSize;
        //    }
        //    set
        //    {
        //        base.MinimumSize = new Size(179, 51);
        //    }
        //}

        //
        //override  WndProc  
        //
        //protected override void WndProc(ref Message m)
        //{
        //    //****************************************************************************

        //    int x = (int)(m.LParam.ToInt64() & 0xFFFF);               //get x mouse position
        //    int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);   //get y mouse position  you can gave (x,y) it from "MouseEventArgs" too
        //    Point pt = PointToClient(new Point(x, y));

        //    if (m.Msg == 0x84)
        //    {
        //        switch (resize.getMosuePosition(pt, this))
        //        {
        //            case "l": m.Result = (IntPtr)10; return;  // the Mouse on Left Form
        //            case "r": m.Result = (IntPtr)11; return;  // the Mouse on Right Form
        //            case "a": m.Result = (IntPtr)12; return;
        //            case "la": m.Result = (IntPtr)13; return;
        //            case "ra": m.Result = (IntPtr)14; return;
        //            case "u": m.Result = (IntPtr)15; return;
        //            case "lu": m.Result = (IntPtr)16; return;
        //            case "ru": m.Result = (IntPtr)17; return; // the Mouse on Right_Under Form
        //            case "": m.Result = pt.Y < 32 /*mouse on title Bar*/ ? (IntPtr)2 : (IntPtr)1; return;

        //        }
        //    }

        //    const int WM_SYSCOMMAND = 0x0112;
        //    const int SC_MOVE = 0xF010;

        //    switch (m.Msg)
        //    {
        //        case WM_SYSCOMMAND:
        //            int command = m.WParam.ToInt32() & 0xfff0;
        //            if (command == SC_MOVE)
        //                return;
        //            break;
        //    }

        //    ////base.WndProc(ref m);

        //    base.WndProc(ref m);

        //}

       
        //update

        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;
            const UInt32 HTBOTTOMRIGHT = 17;
            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);
                Rectangle hitBox = new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE);
                if (hitBox.Contains(clientPoint))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    handled = true;
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        private void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var tmp = wc.DownloadString("https://www.google.com");
            }
            catch (Exception)
            {

                return;
            }
            string version = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdscWlERzNVY0xveHM");
            string changelog = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsWXEyTzdrM1phOWM");
            if (version != Application.ProductVersion)
            {
                if (MessageBox.Show("You are running old version " + Application.ProductVersion + "\nWould you like to download new version " + version + "?\n" + changelog, "RamC Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsdkJ3VEp2R0dlQm8");
                }
            }
        }

        
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {    
            //if (f2 != null && f2.IsDisposed != true)
            //{
            //    f2.Show();
            //    f2.TopMost = true;
            //    f2.WindowState = FormWindowState.Normal;
            //    f2.TopMost = false;
            //}
            //else
            //{
            //    f2.Dispose();
            //    f2 = new Form2();
            //    f2.Show();
            //    f2.TopMost = true;
            //    f2.WindowState = FormWindowState.Normal;
            //    f2.TopMost = false;
            //}
        }

        /*prevent this app from being hidden by "show desktop" button*/
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        const int GWL_HWNDPARENT = -8;


        private void Form1_Load(object sender, EventArgs e)
        {
            
            IntPtr hprog = FindWindowEx(
                FindWindowEx(
                    FindWindow("Progman", "Program Manager"),
                    IntPtr.Zero, "SHELLDLL_DefView", ""
                ),
                IntPtr.Zero, "SysListView32", "FolderView"
            );

            SetWindowLong(this.Handle, GWL_HWNDPARENT, hprog);

          
        }

       
        /*-----------------------------------------------------------------------------------*/

        //prevent from alt-tab
        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }
        //

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            r.SetValue("size1", this.Height);
            r.SetValue("size2", this.Width);

            r.SetValue("Top", Top);
            r.SetValue("Left", Left);
            //
            r.SetValue("DateTop", labelDate.Top);
            r.SetValue("DateLeft", labelDate.Left);
            //
            r.SetValue("CTop", labelClock.Top);
            r.SetValue("CLeft", labelClock.Left);
            //
            r.SetValue("CpuTop", labelCPU.Top);
            r.SetValue("CpuLeft", labelCPU.Left);
            //
            r.SetValue("DiskTop", labelDisk.Top);
            r.SetValue("DiskLeft", labelDisk.Left);
            //
            r.SetValue("RamTop", labelRam.Top);
            r.SetValue("RamLeft", labelRam.Left);
            //
           
            //
            r.Close();

           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string cHour, cMinute, cSecond;
            int getTime = DateTime.Now.Hour;
            int getMinute = DateTime.Now.Minute;
            int getSecond = DateTime.Now.Second;
            if (h == true)
            {
                if (getTime > 12)
                    getTime = getTime - 12;
                if (getTime == 0)
                    getTime = 12;
            }
            cHour = getTime.ToString();
            cMinute = getMinute.ToString();
            cSecond = getSecond.ToString();

            if (getTime < 10)
                cHour = "0" + cHour;
            if (getMinute < 10)
                cMinute = "0" + cMinute;
            if (getSecond < 10)
                cSecond = "0" + cSecond;
            if (!isLong)
                labelClock.Text = cHour + ":" + cMinute;
            else
                labelClock.Text = cHour + ":" + cMinute + ":" + cSecond;
            
            //
            //if (clk == true)
            //{
            //    labelClock.Hide();

            //    g = this.CreateGraphics();
            //    Time = (Bitmap)FancyText.ImageFromText(labelClock.Text, labelClock.Font, labelClock.ForeColor, stroke, blur);

            //    g.FillRectangle(SystemBrushes.Control, this.ClientRectangle);
            //    g.DrawImageUnscaled(Time, labelClock.Location);

            //    g.Dispose();
            //}
            //else
            //{
                labelClock.Show();
            //}
            
        }

        private void runAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (runAtStartupToolStripMenuItem.Checked != true)
            {
                runAtStartupToolStripMenuItem.Checked = true;
                r.SetValue("RamC", Application.ExecutablePath);
            }
            else
            {
                runAtStartupToolStripMenuItem.Checked = false;
                r.DeleteValue("RamC", false);
            }
            r.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
           
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (clk != true && e.Button == MouseButtons.Left && noneToolStripMenuItem.Checked != true)
                {
                    labelClock.Left = labelClock.Left + (e.X - newx);
                    labelClock.Top = labelClock.Top + (e.Y - newy);
                }
            }
        }



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (bw2.IsBusy != true)
            //    bw2.RunWorkerAsync();
            
            if (box.Visible != true)
                box.ShowDialog();
            
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (Cpulock != true && e.Button == MouseButtons.Left)
                {

                    labelCPU.Left = labelCPU.Left + (e.X - newx);
                    labelCPU.Top = labelCPU.Top + (e.Y - newy);
                }
            }
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (Disklock != true && e.Button == MouseButtons.Left)
                {

                    labelDisk.Left = labelDisk.Left + (e.X - newx);
                    labelDisk.Top = labelDisk.Top + (e.Y - newy);
                }
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (Ramlock != true && e.Button == MouseButtons.Left)
                {

                    labelRam.Left = labelRam.Left + (e.X - newx);
                    labelRam.Top = labelRam.Top + (e.Y - newy);
                }
            }
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            if (lockLocationToolStripMenuItem.Checked != true)
            {
                lockLocationToolStripMenuItem.Checked = true;
                r.SetValue("Lock", true);
                lk = true;
            }
            else
            {
                lockLocationToolStripMenuItem.Checked = false;
                r.DeleteValue("Lock", false);
                lk = false;
            }
            r.Close();
            r.Dispose();
        }

        private void lockClockLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lk != true && clk == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
           
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (lockClockLocationToolStripMenuItem.Checked != true)
            {
                lockClockLocationToolStripMenuItem.Checked = true;
                r.SetValue("CLock", true);
                clk = true;
            }
            else
            {
                lockClockLocationToolStripMenuItem.Checked = false;
                r.DeleteValue("CLock", false);
                clk = false;
                
                if (noneToolStripMenuItem.Checked == true)
                {
                    MessageBox.Show("You can not move clock while background transparency is on", "Did you know?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            r.Close();
            r.Dispose();
        }



        private void strokeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = stroke;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                stroke = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Stroke", cd.Color.ToArgb());
                r.Close();
                r.Dispose();

            }
            cd.Dispose();
        }


        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelCPU.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelCPU.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("CpuColor", labelCPU.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelDisk.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelDisk.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("DiskColor", labelDisk.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelRam.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelRam.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("RamColor", labelRam.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelClock.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelClock.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("TimeColor", labelClock.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }


        private void lockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (lk != true && lockToolStripMenuItem1.Checked == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lockToolStripMenuItem1.Checked)
            {
                lockToolStripMenuItem1.Checked = false;
                Cpulock = false;
            }
            else
            {
                lockToolStripMenuItem1.Checked = true;
                Cpulock = true;
            }
        }

        private void lockToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (lk != true && lockToolStripMenuItem2.Checked == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lockToolStripMenuItem2.Checked)
            {
                lockToolStripMenuItem2.Checked = false;
                Disklock = false;
            }
            else
            {
                lockToolStripMenuItem2.Checked = true;
                Disklock = true;
            }
        }

        private void lockToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (lk != true && lockToolStripMenuItem3.Checked == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lockToolStripMenuItem3.Checked)
            {
                lockToolStripMenuItem3.Checked = false;
                Ramlock = false;
            }
            else
            {
                lockToolStripMenuItem3.Checked = true;
                Ramlock = true;
            }
        }


        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelCPU.Top = CpuTop;
            labelCPU.Left = CpuLeft;
        }

        private void defaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            labelDisk.Left = DiskLeft;
            labelDisk.Top = DiskTop;
        }

        private void defaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            labelRam.Left = RamLeft;
            labelRam.Top = RamTop;
        }


        private void defaultToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                labelClock.Top = CTop;
                labelClock.Left = CLeft;
                labelDate.Left = DateLeft;
                labelDate.Top = DateTop;
                labelCPU.Top = CpuTop;
                labelCPU.Left = CpuLeft;
                labelDisk.Left = DiskLeft;
                labelDisk.Top = DiskTop;
                labelRam.Left = RamLeft;
                labelRam.Top = RamTop;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (box.Visible != true)
                box.ShowDialog();
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("taskmgr");
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("taskmgr");
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start("taskmgr");
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void hideTrayIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (hideTrayIconToolStripMenuItem.Checked != true)
            {
                hideTrayIconToolStripMenuItem.Checked = true;
                r.SetValue("Hide", true);
                notifyIcon1.Visible = false;
            }
            else
            {
                hideTrayIconToolStripMenuItem.Checked = false;
                r.DeleteValue("Hide", false);
                notifyIcon1.Visible = true;
            }
            r.Close();
            r.Dispose();
        }

      
        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (noneToolStripMenuItem.Checked != true)
            {
                noneToolStripMenuItem.Checked = true;
                TransparencyKey = SystemColors.Control;
                r.SetValue("transparency", true);
                //lockClockLocationToolStripMenuItem.Enabled = false;
            }
            else
            {
                noneToolStripMenuItem.Checked = false;
                TransparencyKey = Color.White;
                r.DeleteValue("transparency", false);
                //lockClockLocationToolStripMenuItem.Enabled = true;
            }
            r.Close();
            r.Dispose();
        }

        private void closeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            labelCPU.Hide();
            cPUToolStripMenuItem_Click(null, null);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelDisk.Hide();
            dISKToolStripMenuItem_Click(null, null);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            labelRam.Hide();
            rAMToolStripMenuItem_Click(null, null);
        }

        

        private void cPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (cPUToolStripMenuItem.Checked != true)
            {
                cPUToolStripMenuItem.Checked = true;
                labelCPU.Show();
                r.SetValue("showcpu", true);
            }
            else
            {
                cPUToolStripMenuItem.Checked = false;
                labelCPU.Hide();
                r.DeleteValue("showcpu", false);
            }
            r.Close();
            r.Dispose();
        }

        private void rAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (rAMToolStripMenuItem.Checked != true)
            {
                rAMToolStripMenuItem.Checked = true;
                labelRam.Show();
                r.SetValue("showram", true);
            }
            else
            {
                rAMToolStripMenuItem.Checked = false;
                labelRam.Hide();
                r.DeleteValue("showram", false);
            }
            r.Close();
            r.Dispose();
        }

        private void dISKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (dISKToolStripMenuItem.Checked != true)
            {
                dISKToolStripMenuItem.Checked = true;
                labelDisk.Show();
                r.SetValue("showdisk", true);
            }
            else
            {
                dISKToolStripMenuItem.Checked = false;
                labelDisk.Hide();
                r.DeleteValue("showdisk", false);
            }
            r.Close();
            r.Dispose();
        }
        
        //set disk font
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = labelDisk.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelDisk.Font = fd.Font;
                
                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Diskfont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        //set cpu font
        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = labelCPU.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelCPU.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Cpufont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        //set ram font
        private void fontToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = labelRam.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelRam.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Ramfont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        //set mail font
        

        //set clock font
        private void fontToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = labelClock.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelClock.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Clockfont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }
       

        //set disk font to default
        private void setToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelDisk.Font = (Font)converter.ConvertFromString(Diskfont);
            string fontString = converter.ConvertToString(labelDisk.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Diskfont", fontString);
            r.Close();
            r.Dispose();
        }

        //set cpu font to default
        private void setToDefaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            labelCPU.Font = (Font)converter.ConvertFromString(Cpufont);
            string fontString = converter.ConvertToString(labelCPU.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Cpufont", fontString);
            r.Close();
            r.Dispose();
        }

        //set ram font to default
        private void setToDefaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            labelRam.Font = (Font)converter.ConvertFromString(Ramfont);
            string fontString = converter.ConvertToString(labelRam.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Ramfont", fontString);
            r.Close();
            r.Dispose();
        }


        //set clock font to default
        private void setToDefaultToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            labelClock.Font = (Font)converter.ConvertFromString(Clockfont);
            string fontString = converter.ConvertToString(labelClock.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Clockfont", fontString);
            r.Close();
            r.Dispose();
        }

        //set mail font to default
        


        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (hToolStripMenuItem.Checked != true)
            {
                hToolStripMenuItem.Checked = true;
                h = true;
                r.SetValue("12type", true);
                r.DeleteValue("24type", false);
                hToolStripMenuItem1.Checked = false;
            }
            r.Close();
            r.Dispose();
        }

        private void hToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (hToolStripMenuItem1.Checked != true)
            {
                hToolStripMenuItem1.Checked = true;
                h = false;
                r.SetValue("24type", true);
                r.DeleteValue("12type", false);
                hToolStripMenuItem.Checked = false;
            }
            r.Close();
            r.Dispose();
        }

        private void shortexcludeSecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (shortexcludeSecondToolStripMenuItem.Checked != true)
            {
                shortexcludeSecondToolStripMenuItem.Checked = true;
                isLong = false;
                r.SetValue("shortTime", true);
                r.DeleteValue("longTime", false);
                longToolStripMenuItem.Checked = false;
            }
            r.Close();
            r.Dispose();
        }

        private void longToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (longToolStripMenuItem.Checked != true)
            {
                longToolStripMenuItem.Checked = true;
                isLong = true;
                r.SetValue("longTime", true);
                r.DeleteValue("shortTime", false);
                shortexcludeSecondToolStripMenuItem.Checked = false;
            }
            r.Close();
            r.Dispose();
        }

        private void timerSystem_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.Date.ToLongDateString();
            //
            fRam = RamC.NextValue();
            labelRam.Text = "Available memory: " + fRam.ToString() + "MB";
          
            pDisk = Disk.NextValue();
            pDisk2 = Math.Round(pDisk, 1);
            if (pDisk2 > 100)
                pDisk2 = 100;
            labelDisk.Text = "Disk usage: " + pDisk2.ToString() + "%";
            
            pCpu = Cpu.NextValue();
            pCpu2 = Math.Round(pCpu, 1);
            labelCPU.Text = "CPU usage: " + pCpu2.ToString() + "%";
            
        }

        private void defaultToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            labelDate.Top = DateTop;
            labelDate.Left = DateLeft;
        }

        private void customizeFontToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = labelDate.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                labelDate.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Datefont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        private void setToDefaultToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            labelDate.Font = (Font)converter.ConvertFromString(Datefont);
            string fontString = converter.ConvertToString(labelDate.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Datefont", fontString);
            r.Close();
            r.Dispose();
        }

        private void colorToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = labelDate.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                labelDate.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("DateColor", labelDate.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void lockToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (lk != true && lockToolStripMenuItem.Checked == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lockToolStripMenuItem.Checked)
            {
                lockToolStripMenuItem.Checked = false;
                Datelock = false;
            }
            else
            {
                lockToolStripMenuItem.Checked = true;
                Datelock = true;
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelDate.Hide();
            dATEToolStripMenuItem_Click(null, null);
        }

        private void dATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (dATEToolStripMenuItem.Checked != true)
            {
                dATEToolStripMenuItem.Checked = true;
                labelDate.Show();
                r.SetValue("showdate", true);
            }
            else
            {
                dATEToolStripMenuItem.Checked = false;
                labelDate.Hide();
                r.DeleteValue("showdate", false);
            }
            r.Close();
            r.Dispose();
        }

        private void labelDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void labelDate_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (Datelock != true && e.Button == MouseButtons.Left)
                {

                    labelDate.Left = labelDate.Left + (e.X - newx);
                    labelDate.Top = labelDate.Top + (e.Y - newy);
                }
            }
        }

        private void defaultLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelClock.Left = CLeft;
            labelClock.Top = CTop;
        }
    }
}
