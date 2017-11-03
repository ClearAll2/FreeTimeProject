using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Management;
using System.Security.Permissions;
using AE.Net.Mail;

namespace RamC
{
    public partial class Form1 : Form
    {
        int newx;
        int newy;
        int getTime;
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
        bool Cpulock = true;
        bool Disklock = true;
        bool Ramlock = true;
        bool Maillock = true;
        bool Mailstop = false;
        bool h = false;
        bool isLong = false;

        string Diskfont;
        string Ramfont;
        string Cpufont;
        string Mailfont;
        string Clockfont;

        int CTop;
        int CLeft;
        int CpuTop;
        int CpuLeft;
        int DiskTop;
        int DiskLeft;
        int RamTop;
        int RamLeft;
        int MailTop;
        int MailLeft;
        int urno = 0;
        WebClient wc = new WebClient();
        BackgroundWorker bw;
        BackgroundWorker bw2;
        
        bool lk = false;
        bool clk = false;
        //bool saved = false;
        string id = " ";
        string pass = " ";
        ImapClient mail;
        AboutBox1 box = new AboutBox1();
        General gn = new General();
        WebClient wb = new WebClient();
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
       // ReSize resize = new ReSize();

        int blur = 5;
        Color stroke;
        
        Form2 f2;
        Form3 f3;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            
            //get default  position
            

            CTop = label2.Top;
            CLeft = label2.Left;
            CpuTop = label4.Top;
            CpuLeft = label4.Left;
            DiskTop = label3.Top;
            DiskLeft = label3.Left;
            RamTop = label1.Top;
            RamLeft = label1.Left;
            MailTop = label5.Top;
            MailLeft = label5.Left;

            //get default font
            Cpufont = converter.ConvertToString(label4.Font);
            Diskfont = converter.ConvertToString(label3.Font);
            Ramfont = converter.ConvertToString(label1.Font);
            Mailfont = converter.ConvertToString(label5.Font);
            Clockfont = converter.ConvertToString(label2.Font);
            


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

            if (r.GetValue("Diskfont") != null)
            {
                label3.Font = (Font)converter.ConvertFromString((string)r.GetValue("Diskfont"));
            }
            if (r.GetValue("Cpufont") != null)
            {
                label4.Font = (Font)converter.ConvertFromString((string)r.GetValue("Cpufont"));
            }
            if (r.GetValue("Ramfont") != null)
            {
                label1.Font = (Font)converter.ConvertFromString((string)r.GetValue("Ramfont"));
            }
            if (r.GetValue("Mailfont") != null)
            {
                label5.Font = (Font)converter.ConvertFromString((string)r.GetValue("Mailfont"));
            }
            if (r.GetValue("Clockfont") != null)
            {
                label2.Font = (Font)converter.ConvertFromString((string)r.GetValue("Clockfont"));
            }


            if (r.GetValue("CTop") != null)
            {
                label2.Top = (int)r.GetValue("CTop");
                label2.Left = (int)r.GetValue("CLeft");
            }

            if (r.GetValue("CpuTop") != null)
            {
                label4.Top = (int)r.GetValue("CpuTop");
                label4.Left = (int)r.GetValue("CpuLeft");
            }
            if (r.GetValue("DiskTop") != null)
            {
                label3.Top = (int)r.GetValue("DiskTop");
                label3.Left = (int)r.GetValue("DiskLeft");
            }
            if (r.GetValue("RamTop") != null)
            {
                label1.Top = (int)r.GetValue("RamTop");
                label1.Left = (int)r.GetValue("RamLeft");
            }
            if (r.GetValue("MailTop") != null)
            {
                label5.Top = (int)r.GetValue("MailTop");
                label5.Left = (int)r.GetValue("MailLeft");
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
            else
            {
                blurAmountToolStripMenuItem.Enabled = false;
                strokeColorToolStripMenuItem.Enabled = false;
            }
            if (r.GetValue("Id") != null && r.GetValue("Pass") != null)
            {
                id = (string)r.GetValue("Id");
                pass = (string)r.GetValue("Pass");
                General.haveId = true;
                //saved = true;
            }
            if (r.GetValue("CpuColor") != null)
            {
                int tmp = (int)r.GetValue("CpuColor");
                label4.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("DiskColor") != null)
            {
                int tmp = (int)r.GetValue("DiskColor");
                label3.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("RamColor") != null)
            {
                int tmp = (int)r.GetValue("RamColor");
                label1.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("TimeColor") != null)
            {
                int tmp = (int)r.GetValue("TimeColor");
                label2.ForeColor = Color.FromArgb(tmp);
            }
            if (r.GetValue("MailColor") != null)
            {
                int tmp = (int)r.GetValue("MailColor");
                label5.ForeColor = Color.FromArgb(tmp);
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
            
            if (r.GetValue("Blur") != null)
            {
                blur = (int)r.GetValue("Blur");
                if (blur == 3)
                {
                    blur3tools.Checked = true;
                }
                else if (blur == 5)
                {
                    blur5tools.Checked = true;
                }
                else if (blur == 7)
                {
                    blur7tools.Checked = true;
                }
                else if (blur == 9)
                {
                    blur9tools.Checked = true;
                }
            }
            else
            {
                blur5tools.Checked = true;
            }

           
            if (r.GetValue("showWeather") != null)
            {
                showAtStartupToolStripMenuItem.Checked = true;
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

            if (r.GetValue("showmail") != null)
            {
                showToolStripMenuItem.Checked = true;
            }
            else
            {
                closeToolStripMenuItem3_Click(null, null);
            }
            r.Close();
            r.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (r.GetValue("RamC") != null)
            {
                runAtStartupToolStripMenuItem.Checked = true;
            }
            r.Close();
            r.Dispose();
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;
            
            //Time = (Bitmap)FancyText.ImageFromText("Time", label2.Font, Color.Black, Color.White);
            label5.Text = "Mail:";
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
            f2 = new Form2();
            f2.Hide();

            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;
            bw2.RunWorkerAsync();

            

            f3 = new Form3();
            if (showAtStartupToolStripMenuItem.Checked)
            {
                
                f3.Show();
            }

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
                if (MessageBox.Show("You are running old version " + Application.ProductVersion + "\nWould you like to download new version " + version + "?\n" + "Changelog: \n" + changelog, "RamC Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsdkJ3VEp2R0dlQm8");
                }
            }
        }

        //mail
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Start:
            int unread = 0;
            while (true)
            {
                if (General.haveId == true)
                {
                    label5.Text = "Mail: load";
                    r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                    id = (string)r.GetValue("Id");
                    pass = (string)r.GetValue("Pass");
                    r.Close();
                    break;
                }
                Thread.Sleep(2000);
            }
            exitToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem1.Enabled = false;
            while (true)
            {
                try
                {
                    label5.Text = "Mail: check";
                    var temp = wb.DownloadString("https://www.google.com");
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {
                    label5.Text = "Mail: dis";
                    exitToolStripMenuItem.Enabled = true;
                    exitToolStripMenuItem1.Enabled = true;
                    Thread.Sleep(2000);
                    continue;
                }
                break;
            }
            label5.Text = "Mail: init";
            try
            {
                mail = new ImapClient("imap.gmail.com", id, pass, AuthMethods.Login, 993, true);
            }
            catch
            {
                label5.Text = "Mail: error";
                exitToolStripMenuItem.Enabled = true;
                exitToolStripMenuItem1.Enabled = true;
                Thread.Sleep(10000);
                label5.Text = "Mail: reload";
                Thread.Sleep(2000);
                goto Start;
            }
            label5.Text = "Mail: ok";
            while (true)
            {
                exitToolStripMenuItem.Enabled = false;
                exitToolStripMenuItem1.Enabled = false;
                unread = 0;
                label5.Text = "Mail: init";
                try
                {
                    var temp = wb.DownloadString("https://www.google.com");

                }
                catch (Exception)
                {
                    label5.Text = "Mail: dis";
                    exitToolStripMenuItem.Enabled = true;
                    exitToolStripMenuItem1.Enabled = true;
                    Thread.Sleep(2000);
                    continue;
                }
                label5.Text = "Mail: load";
                try
                {
                    mail.SelectMailbox("INBOX");
                    var mailmess = mail.SearchMessages(SearchCondition.Unseen(), true, false).ToList();
                    for (int i = 0; i < mailmess.Count; i++)
                        unread++;
                    label5.Text = "Mail: " + unread.ToString();
                    if (unread > 0)
                    {
                        if (urno != unread)
                        {
                            if (notifyIcon1.Visible)
                                notifyIcon1.ShowBalloonTip(1000, "New Emails", "You have " + unread.ToString() + " new emails", ToolTipIcon.Info);
                            else
                            {
                                MessageBox.Show("You have " + unread.ToString() + " new emails", "New Emails", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                    urno = unread;
                }
                catch (Exception)
                {
                    label5.Text = "Mail: new";
                    //mail.Disconnect();
                    try
                    {
                        mail = new ImapClient("imap.gmail.com", id, pass, AuthMethods.Login, 993, true);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Thread.Sleep(2000);
                    continue;
                }
                exitToolStripMenuItem.Enabled = true;
                exitToolStripMenuItem1.Enabled = true;
                //Thread.Sleep(1800000);
                //mail.Disconnect();
                //mail.Dispose();

                

                Thread.Sleep(75000);

                
                while (Mailstop == true)
                {
                    label5.Text = "Mail: stop";
                    
                }
                
           
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {    
            if (f2 != null && f2.IsDisposed != true)
            {
                f2.Show();
                f2.TopMost = true;
                f2.WindowState = FormWindowState.Normal;
                f2.TopMost = false;
            }
            else
            {
                f2.Dispose();
                f2 = new Form2();
                f2.Show();
                f2.TopMost = true;
                f2.WindowState = FormWindowState.Normal;
                f2.TopMost = false;
            }
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
            if (General.haveId == true && label5.Text != "Mail: error")
                mail.Disconnect();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            r.SetValue("size1", this.Height);
            r.SetValue("size2", this.Width);

            r.SetValue("Top", Top);
            r.SetValue("Left", Left);
            //
            r.SetValue("CTop", label2.Top);
            r.SetValue("CLeft", label2.Left);
            //
            r.SetValue("CpuTop", label4.Top);
            r.SetValue("CpuLeft", label4.Left);
            //
            r.SetValue("DiskTop", label3.Top);
            r.SetValue("DiskLeft", label3.Left);
            //
            r.SetValue("RamTop", label1.Top);
            r.SetValue("RamLeft", label1.Left);
            //
            r.SetValue("MailTop", label5.Top);
            r.SetValue("MailLeft", label5.Left);
            //
            r.Close();

           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getTime = DateTime.Now.Hour;
            if (h == true)
            {
                if (getTime > 12)
                    getTime = getTime - 12;
                if (getTime == 0)
                    getTime = 12;
            }
            if (getTime > 9)
            {
                if (isLong != true)
                {
                    if (DateTime.Now.Minute > 9)
                        label2.Text = getTime + ":" + DateTime.Now.Minute;
                    else
                        label2.Text = getTime + ":" + "0" + DateTime.Now.Minute;
                }
                else
                {
                    if (DateTime.Now.Minute > 9)
                        label2.Text = getTime + ":" + DateTime.Now.Minute  + ":" + DateTime.Now.Second;
                    else
                        label2.Text = getTime + ":" + "0" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                }
            }
            else
            {
                if (isLong != true)
                {
                    if (DateTime.Now.Minute > 9)
                        label2.Text = "0" + getTime + ":" + DateTime.Now.Minute;
                    else
                        label2.Text = "0" + getTime + ":" + "0" + DateTime.Now.Minute;
                }
                else
                {
                    if (DateTime.Now.Minute > 9)
                        label2.Text = "0" + getTime + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    else
                        label2.Text = "0" + getTime + ":" + "0" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                }
            }
            
            
            //
            if (clk == true)
            {
                label2.Hide();

                g = this.CreateGraphics();
                Time = (Bitmap)FancyText.ImageFromText(label2.Text, label2.Font, label2.ForeColor, stroke, blur);

                g.FillRectangle(SystemBrushes.Control, this.ClientRectangle);
                g.DrawImageUnscaled(Time, label2.Location);

                g.Dispose();
            }
            else
            {
                label2.Show();
            }
            //
            fRam = RamC.NextValue();
            label1.Text = "Available memory: " + fRam.ToString() + "MB";
            //label1.Hide();
            // RAM = (Bitmap)FancyText.ImageFromText(label1.Text, label1.Font, Color.Black, Color.White);
            // g.FillRectangle(SystemBrushes.Control, label1.Bounds);
            // g.DrawImageUnscaled(RAM, label1.Location);
            //
            pDisk = Disk.NextValue();
            pDisk2 = Math.Round(pDisk, 1);
            if (pDisk2 > 100)
                pDisk2 = 100;
            label3.Text = "Disk usage: " + pDisk2.ToString() + "%";
            // label3.Hide();
            //DISK = (Bitmap)FancyText.ImageFromText(label3.Text, label3.Font, Color.Black, Color.White);
            // g.FillRectangle(SystemBrushes.Control, label3.Bounds);
            // g.DrawImageUnscaled(DISK, label3.Location);
            //
            pCpu = Cpu.NextValue();
            pCpu2 = Math.Round(pCpu, 1);
            label4.Text = "CPU usage: " + pCpu2.ToString() + "%";
            //label4.Text = "CPU speed: " + (CPUSpeed()/1000).ToString() + "GHz";
            // label4.Hide();
            // CPU = (Bitmap)FancyText.ImageFromText(label4.Text, label4.Font, Color.Black, Color.White);
            // g.FillRectangle(SystemBrushes.Control, label4.Bounds);
            // g.DrawImageUnscaled(CPU, label4.Location);
            

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
                    label2.Left = label2.Left + (e.X - newx);
                    label2.Top = label2.Top + (e.Y - newy);
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

                    label4.Left = label4.Left + (e.X - newx);
                    label4.Top = label4.Top + (e.Y - newy);
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

                    label3.Left = label3.Left + (e.X - newx);
                    label3.Top = label3.Top + (e.Y - newy);
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

                    label1.Left = label1.Left + (e.X - newx);
                    label1.Top = label1.Top + (e.Y - newy);
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
                blurAmountToolStripMenuItem.Enabled = true;
                strokeColorToolStripMenuItem.Enabled = true;
                
            }
            else
            {
                lockClockLocationToolStripMenuItem.Checked = false;
                r.DeleteValue("CLock", false);
                clk = false;
                blurAmountToolStripMenuItem.Enabled = false;
                strokeColorToolStripMenuItem.Enabled = false;
               
                if (noneToolStripMenuItem.Checked == true)
                {
                    MessageBox.Show("You can not move clock while background transparency is on", "Did you know?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            r.Close();
            r.Dispose();
        }

        private void gmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f2 != null && f2.IsDisposed != true)
            {
                f2.Show();
                f2.WindowState = FormWindowState.Normal;
            }
            else
            {
                f2.Dispose();
                f2 = new Form2();
                f2.Show();
            }
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void label5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lk != true)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
            else
            {
                if (Maillock != true && e.Button == MouseButtons.Left)
                {

                    label5.Left = label5.Left + (e.X - newx);
                    label5.Top = label5.Top + (e.Y - newy);
                }
            }
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
            cd.Color = label4.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                label4.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("CpuColor", label4.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = label3.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                label3.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("DiskColor", label3.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = label1.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("RamColor", label1.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void colorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = label2.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                label2.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("TimeColor", label2.ForeColor.ToArgb());
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

        private void colorToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = label5.ForeColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                label5.ForeColor = cd.Color;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("MailColor", label5.ForeColor.ToArgb());
                r.Close();
                r.Dispose();
            }
            cd.Dispose();
        }

        private void lockToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (lk != true && lockToolStripMenuItem4.Checked == true)
            {
                MessageBox.Show("You must lock location of window before unlocking this", "Please!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (lockToolStripMenuItem4.Checked)
            {
                lockToolStripMenuItem4.Checked = false;
                Maillock = false;
            }
            else
            {
                lockToolStripMenuItem4.Checked = true;
                Maillock = true;
            }
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {

            if (f2 != null && f2.IsDisposed != true)
            {
                f2.Show();
                f2.WindowState = FormWindowState.Normal;
            }
            else
            {
                f2.Dispose();
                f2 = new Form2();
                f2.Show();
            }

        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Top = CpuTop;
            label4.Left = CpuLeft;
        }

        private void defaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label3.Left = DiskLeft;
            label3.Top = DiskTop;
        }

        private void defaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            label1.Left = RamLeft;
            label1.Top = RamTop;
        }

        private void defaultToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            label5.Left = MailLeft;
            label5.Top = MailTop;
        }

        private void defaultToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                label2.Top = CTop;
                label2.Left = CLeft;
                label4.Top = CpuTop;
                label4.Left = CpuLeft;
                label3.Left = DiskLeft;
                label3.Top = DiskTop;
                label1.Left = RamLeft;
                label1.Top = RamTop;
                label5.Left = MailLeft;
                label5.Top = MailTop;
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

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

        private void weatherBetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f3.Visible)
            {
                f3.Hide();    
            }
            else
            {
                f3.Show();
            }
        }

        private void showAtStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (showAtStartupToolStripMenuItem.Checked != true)
            {
                showAtStartupToolStripMenuItem.Checked = true;
                r.SetValue("showWeather", true);
            }
            else
            {
                showAtStartupToolStripMenuItem.Checked = false;
                r.DeleteValue("showWeather", false);
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
            label4.Hide();
            cPUToolStripMenuItem_Click(null, null);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Hide();
            dISKToolStripMenuItem_Click(null, null);
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label1.Hide();
            rAMToolStripMenuItem_Click(null, null);
        }

        private void closeToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            label5.Hide();
            showToolStripMenuItem_Click(null, null);
        }

        private void cPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (cPUToolStripMenuItem.Checked != true)
            {
                cPUToolStripMenuItem.Checked = true;
                label4.Show();
                r.SetValue("showcpu", true);
            }
            else
            {
                cPUToolStripMenuItem.Checked = false;
                label4.Hide();
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
                label1.Show();
                r.SetValue("showram", true);
            }
            else
            {
                rAMToolStripMenuItem.Checked = false;
                label1.Hide();
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
                label3.Show();
                r.SetValue("showdisk", true);
            }
            else
            {
                dISKToolStripMenuItem.Checked = false;
                label3.Hide();
                r.DeleteValue("showdisk", false);
            }
            r.Close();
            r.Dispose();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (showToolStripMenuItem.Checked != true)
            {
                showToolStripMenuItem.Checked = true;
                label5.Show();
                r.SetValue("showmail", true);
            }
            else
            {
                showToolStripMenuItem.Checked = false;
                label5.Hide();
                r.DeleteValue("showmail", false);
            }
            r.Close();
            r.Dispose();
        }

        private void blur4tools_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (blur3tools.Checked != true)
            {
                blur = 3;
                blur3tools.Checked = true;
                blur5tools.Checked = false;
                blur7tools.Checked = false;
                blur9tools.Checked = false;
                r.SetValue("Blur", 3);
            }
            r.Close();
            r.Dispose();
        }

        private void blur5tools_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (blur5tools.Checked != true)
            {
                blur = 5;
                blur5tools.Checked = true;
                blur3tools.Checked = false;
                blur7tools.Checked = false;
                blur9tools.Checked = false;
                r.SetValue("Blur", 5);
            }
            r.Close();
            r.Dispose();
        }

        private void blur6tools_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (blur7tools.Checked != true)
            {
                blur = 7;
                blur7tools.Checked = true;
                blur5tools.Checked = false;
                blur3tools.Checked = false;
                blur9tools.Checked = false;
                r.SetValue("Blur", 7);
            }
            r.Close();
            r.Dispose();
        }

        private void blur7tools_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (blur9tools.Checked != true)
            {
                blur = 9;
                blur9tools.Checked = true;
                blur5tools.Checked = false;
                blur7tools.Checked = false;
                blur3tools.Checked = false;
                r.SetValue("Blur", 9);
            }
            r.Close();
            r.Dispose();
        }
        //disconnect mail
        private void stopConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Mailstop != true)
            {
                Mailstop = true;
                stopConnectionToolStripMenuItem.Text = "Resume Connection";
                label5.Text = "Mail:";
            }
            else
            {
                Mailstop = false;
                stopConnectionToolStripMenuItem.Text = "Stop Connection";
            }
        }
        
        //set disk font
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = label3.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label3.Font = fd.Font;
                
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
            fd.Font = label4.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label4.Font = fd.Font;

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
            fd.Font = label1.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Ramfont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        //set mail font
        private void fontToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = label5.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label5.Font = fd.Font;

                string fontString = converter.ConvertToString(fd.Font);
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Mailfont", fontString);
                r.Close();
                r.Dispose();
            }
            fd.Dispose();
        }

        //set clock font
        private void fontToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = label2.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label2.Font = fd.Font;

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
            label3.Font = (Font)converter.ConvertFromString(Diskfont);
            string fontString = converter.ConvertToString(label3.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Diskfont", fontString);
            r.Close();
            r.Dispose();
        }

        //set cpu font to default
        private void setToDefaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label4.Font = (Font)converter.ConvertFromString(Cpufont);
            string fontString = converter.ConvertToString(label4.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Cpufont", fontString);
            r.Close();
            r.Dispose();
        }

        //set ram font to default
        private void setToDefaultToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            label1.Font = (Font)converter.ConvertFromString(Ramfont);
            string fontString = converter.ConvertToString(label1.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Ramfont", fontString);
            r.Close();
            r.Dispose();
        }


        //set clock font to default
        private void setToDefaultToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            label2.Font = (Font)converter.ConvertFromString(Clockfont);
            string fontString = converter.ConvertToString(label2.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Clockfont", fontString);
            r.Close();
            r.Dispose();
        }

        //set mail font to default
        private void setToDefaultToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            label5.Font = (Font)converter.ConvertFromString(Mailfont);
            string fontString = converter.ConvertToString(label5.Font);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Mailfont", fontString);
            r.Close();
            r.Dispose();
        }

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

        

        

        

        

       











    }
}
