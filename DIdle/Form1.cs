using System;
using System.IO;
using System.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using CSCore.CoreAudioAPI;

namespace DIdle
{
    public partial class Form1 : Form
    {
        RegistryKey r;
        SoundPlayer sp;
        int time = 30;
        //int count = 0;
        BackgroundWorker bw;
        //Color prevColor;
        
        public Form1()
        {
            InitializeComponent();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (r.GetValue("jafba") != null)
            {
                if (Application.ExecutablePath == (string)r.GetValue("jafba"))
                {
                    khởiĐộngCùngWindowsToolStripMenuItem.Checked = true;
                }
            }
            r.Close();
            r.Dispose();
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
            loadConfig();
            //notifyIcon1.ShowBalloonTip(1000, "Jafba", "Jafba đang chạy ngầm nha hí hí. Cảm ơn đã thử cái chương trình nhảm nhí này!", ToolTipIcon.Info);
            
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            here:
            if (GetIdleTime() >= time * 1000)
            {
                
                if (!this.Visible)
                {
                    if (!IsAudioPlaying(GetDefaultRenderDevice()))
                    {
                        // if (count >= time)
                            this.Show();
                    }
                }

                
                
            }
            else
            {
                if (this.Visible)
                    this.Hide();
            }
            Thread.Sleep(500);
            goto here;
        }

        void loadConfig()
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\config.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                Int32.TryParse(sr.ReadLine(), out time);
                sr.Close();
                fs.Close();
                fs.Dispose();
                sr.Dispose();  
            }
            catch
            {
                time = 30;
            }
            notifyIcon1.ShowBalloonTip(1000, "Thời gian được cấu hình: " + time + " giây", "Jafba", ToolTipIcon.Info);
            if (pictureBox5.Image == null)
            {
                here:
                try
                {
                    pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\whattoshow.gif");
                }
                catch
                {
                    if (MessageBox.Show("Làm ơn làm phước cho 1 cái hình tên \"whattoshow.gif\" vào thư mục của chương trình này rồi bấm Ok.", "Không có hình thì nhìn cái gì? Vcl", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        goto here;
                    }

                }
            }
            if (!File.Exists(Application.StartupPath + "\\whattolisten.wav"))
            {
                MessageBox.Show("Không có file âm thanh, nêu muốn nghe hãy thêm file \"whattolisten.wav\" vào thư mục của chương trình này. Ok?.", "Không có gì để nghe nhá!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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

        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate { Hide(); }));
            //prevColor = GetColorAt(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void reloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ghi thời gian mong muốn vào (giây), sau đó lưu lại file. Chọn Đọc lại cấu hình để hoàn tất", "Cách làm", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            try
            {
                Process.Start(Application.StartupPath + "\\config.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                try
                {
                    sp = new SoundPlayer(Application.StartupPath + "\\whattolisten.wav");
                    sp.PlayLooping();
                }
                catch
                {
                    return;
                }
            }
            else
            {
                sp.Stop();
                sp.Dispose();
            }
        }

        private void khởiĐộngCùngWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (khởiĐộngCùngWindowsToolStripMenuItem.Checked != true)
            {
                khởiĐộngCùngWindowsToolStripMenuItem.Checked = true;
                r.SetValue("jafba", Application.ExecutablePath);
            }
            else
            {
                khởiĐộngCùngWindowsToolStripMenuItem.Checked = false;
                r.DeleteValue("jafba", false);
            }
            r.Close();
            r.Dispose();
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        //Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        //public Color GetColorAt(Point location)
        //{
        //    using (Graphics gdest = Graphics.FromImage(screenPixel))
        //    {
        //        using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
        //        {
        //            IntPtr hSrcDC = gsrc.GetHdc();
        //            IntPtr hDC = gdest.GetHdc();
        //            int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
        //            gdest.ReleaseHdc();
        //            gsrc.ReleaseHdc();
        //        }
        //    }

        //    return screenPixel.GetPixel(0, 0);
        //}

        //public void checkForColorDifference()
        //{
        //    Color currtColor = GetColorAt(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));
        //    if (prevColor != currtColor)
        //    {
        //        count = 0;
        //    }
        //    else
        //    {
        //        count += 1;
        //        if (count > Int32.MaxValue)
        //            count = time + 1;
        //    }
        //    prevColor = currtColor;

        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    checkForColorDifference();
        //    screenPixel.Dispose();
        //    screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        //}

        // Gets the default device for the system
        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        // Checks if audio is playing on a certain device
        public static bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jafba v" + Application.ProductVersion + "\nClear All Soft (CAS)\nCopyright © 2018" + "\n\nThanks for all the support given!", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void đọcLạiCấuHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadConfig();
        }
    }

}
