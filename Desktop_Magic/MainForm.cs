using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DM
{

    public partial class MainForm : Form
    {
        private int value;
        private int newx, newy;
        private bool started = false;
        private RegistryKey r;
        private RegistryKey r1;
        private int Speed; private int RSpeed = 15;
        private int Number; private int RDirection = 10;
        private int Amount; private int RNumber = 15; private int RType = 1;
        private bool rb1 = false;
        private bool rb2 = false;
        private bool rb3 = false;
        private bool rb4 = false;
        private bool rb5 = false;
        private bool mon, tue, wed, thu, fri, sat, sun = false;
        private bool userHide = false;
        private BackgroundWorker bw;
        private BackgroundWorker bw3;
        private string tmp = "";
        private string changelog = "";
        private EffectForm n;
        private Music f;
        private About ab = new About();
        private KeyboardHook hook = new KeyboardHook();
        private KeyboardHook hook2 = new KeyboardHook();
        private Schedule schedule = new Schedule();
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_DMSHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            this.TopMost = true;
            this.Show();
            WindowState = FormWindowState.Normal;
            this.TopMost = false;
        }

        //minimize form when clicking icon on taskbar
        //for smooth load
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        private const int WS_EX = 0x02000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                //cp.ExStyle |= WS_EX;  // Turn on WS_EX_COMPOSITED //fail
                return cp;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            //notifyIcon1.Visible = false;
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw3 = new BackgroundWorker();
            bw3.DoWork += bw3_DoWork;
            bw3.RunWorkerAsync();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);

            if (r1 == null)
                r1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data");
            CGlob g = new CGlob();
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Text = "Desktop Magic v" + Application.ProductVersion + "\n" + "Click to pause/resume effect";
            panel2.Hide();
            panel3.Hide();
            trackBarSizeCustom.Value = CGlob.Size;
            labelSizeCustom.Text = trackBarSizeCustom.Value.ToString();
            value = trackBarSizeCustom.Value;
            if (r.GetValue("Desktop_Magic") == null)
            {
                if ((string)r.GetValue("Desktop_Magic") == Application.ExecutablePath)
                    checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                notifyIcon1.Visible = true;
                button1_Click(null, null);
                
            }
            //new
            if (r1.GetValue("DM3") == null)
            {
                r1.SetValue("DM3", trackBarDirection.Value);
                Speed = trackBarDirection.Value;
            }
            else
            {
                trackBarDirection.Value = (int)r1.GetValue("DM3");
            }
            if (r1.GetValue("DM4") == null)
            {
                r1.SetValue("DM4", trackBarSpeed.Value);
                Number = trackBarSpeed.Value;
            }
            else
            {
                trackBarSpeed.Value = (int)r1.GetValue("DM4");
            }
            if (r1.GetValue("DM5") == null)
            {
                r1.SetValue("DM5", trackBarNumber.Value);
                Amount = trackBarNumber.Value;
            }
            else
            {
                trackBarNumber.Value = (int)r1.GetValue("DM5");
            }
            labelDirection.Text = trackBarDirection.Value.ToString();
            labelSpeed.Text = trackBarSpeed.Value.ToString();
            labelNumber.Text = trackBarNumber.Value.ToString();
            if (r1.GetValue("DMT") == null)
            {
                if (leafButton.Checked)
                {
                    r1.SetValue("DMT", 1);
                    leafButton.Checked = true;
                }

                else if (customButton.Checked)
                {
                    r1.SetValue("DMT", 3);
                    customButton.Checked = true;
                }
                else if (sakuraButton.Checked)
                {
                    r1.SetValue("DMT", 4);
                    sakuraButton.Checked = true;
                }
                else if (mixButton.Checked)
                {
                    r1.SetValue("DMT", 5);
                    mixButton.Checked = true;
                }
                else
                {
                    r1.SetValue("DMT", 2);
                    snowButton.Checked = true;
                }
            }
            else
            {
                if ((int)r1.GetValue("DMT") == 1)
                    leafButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 2)
                    snowButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 3)
                    customButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 5)
                    mixButton.Checked = true;
                else
                    sakuraButton.Checked = true;
            }

            if (r1.GetValue("Link") != null)
            {
                if (checkBox1.Checked != true)
                {
                    button1_Click(null, null);
                    notifyIcon1.Visible = true;
                }

            }
            if (r1.GetValue("AaR") == null)
                checkBox3.Checked = false;
            else
                timer2.Enabled = true;

            if (r1.GetValue("DM7") == null)
            {
                checkBoxSmoothmotion.Checked = false;
            }
            else
            {
                checkBoxSmoothmotion.Checked = true;
            }

            //never put this to top
            if (r1.GetValue("DM2") == null)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }

            bw.RunWorkerAsync();
            r.Close();
            r.Dispose();
            r1.Close();
            r1.Dispose();



            f = new Music();
            //for hook
            f.Show();
            f.Hide();


            hook.KeyPressed +=
            new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(global::ModifierKeys.Alt, Keys.Left);
            hook.RegisterHotKey(global::ModifierKeys.Control, Keys.Left);
            hook.RegisterHotKey(global::ModifierKeys.Shift, Keys.Left);

            hook2.KeyPressed +=
            new EventHandler<KeyPressedEventArgs>(hook2_KeyPressed);
            hook2.RegisterHotKey(global::ModifierKeys.Alt, Keys.Right);
            hook2.RegisterHotKey(global::ModifierKeys.Control, Keys.Right);
            hook2.RegisterHotKey(global::ModifierKeys.Shift, Keys.Right);

            timer1.Start();
        }

        private void hook2_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == global::ModifierKeys.Alt)
                trackBarDirection.Value += 1;
            else if (e.Modifier == global::ModifierKeys.Control)
                trackBarSpeed.Value += 1;
            else if (e.Modifier == global::ModifierKeys.Shift)
                trackBarNumber.Value += 1;
            saveSettings();
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == global::ModifierKeys.Alt)
                trackBarDirection.Value -= 1;
            else if (e.Modifier == global::ModifierKeys.Control)
                trackBarSpeed.Value -= 1;
            else if (e.Modifier == global::ModifierKeys.Shift)
                trackBarNumber.Value -= 1;
            saveSettings();
        }

        //tips
        private void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadSetting();
            Random r = new Random();
        ret:
            int o = r.Next(1, 10);
            if (o == 1)
            {
                tipsLabel.Text = "You can't play music before starting";
            }
            else if (o == 2)
            {
                tipsLabel.Text = "This app can check and update automatically";
            }
            else if (o == 3)
            {
                tipsLabel.Text = "You can open only one instance of this app";
            }
            else if (o == 4)
            {
                tipsLabel.Text = "Icon made by Freepik. You can know more in About";
            }
            else if (o == 5)
            {
                tipsLabel.Text = "You can pause/resume effect by clicking tray icon";
                tipsLabel.ForeColor = Color.Green;
            }
            else if (o == 6)
            {
                tipsLabel.Text = "Press Alt + Left/Right to change direction";
                tipsLabel.ForeColor = Color.Green;
            }
            else if (o == 7)
            {
                tipsLabel.Text = "Press Ctrl + Left/Right to change speed";
                tipsLabel.ForeColor = Color.Green;
            }
            else if (o == 8)
            {
                tipsLabel.Text = "Press Shift + Left/Right to change number";
            }
            else
            {
                tipsLabel.Text = "Protect yourself and your family from Covid-19!";
                tipsLabel.ForeColor = Color.Red;
            }
            
            Thread.Sleep(10000);
            tipsLabel.ForeColor = Color.Blue;

            goto ret;
        }

        private string GetProductVersion(string s)
        {
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(s);
            return myFileVersionInfo.ProductVersion;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            WebClient wc = new WebClient();
            if (System.IO.File.Exists(Application.StartupPath + "\\temp"))
            {
                if (File.GetLastWriteTime(Application.StartupPath + "\\temp").ToShortDateString() == DateTime.Today.ToShortDateString())
                    return;
            }
            //check if internet is connected
            try
            {
                updateButton.Text = "Connecting...";
                var temp = wc.DownloadString("https://www.google.com.vn");

            }
            catch
            {
                updateButton.Text = "Up to date";
                wc.Dispose();
                return;
            }
            updateButton.Text = "Checking updater...";
            //update new version for the updater
            string ver = "https://download-cas.000webhostapp.com/download/DM/uversion"; //version on the internet
            string curr_ver;
            if (System.IO.File.Exists(Application.StartupPath + "\\DM Updater.exe") && new FileInfo(Application.StartupPath + "\\DM Updater.exe").Length > 1000000)
                curr_ver = GetProductVersion(Application.StartupPath + "\\DM Updater.exe"); //version of updater
            else
                curr_ver = "0.0.0.0";

            string sver = wc.DownloadString(ver);
            if (curr_ver.CompareTo(sver) < 0)
            {
                updateButton.Text = "Updating updater...";
                wc.DownloadFile(new Uri("https://download-cas.000webhostapp.com/download/DM/DM%20Updater.exe"), Application.StartupPath + "\\DM Updater.exe");
            }

            updateButton.Text = "Checking version...";
            var ui = wc.DownloadString("https://download-cas.000webhostapp.com/download/DM/version");//check version

            if (Application.ProductVersion.CompareTo(ui) < 0)
            {
                changelog = wc.DownloadString("https://download-cas.000webhostapp.com/download/DM/changelog");
                wc.Dispose();
                tmp = ui;
                updateButton.Text = "Update available";

                if (MessageBox.Show("New version " + tmp + " is now available\n\n" + changelog + "\n\nWould you like to download now?", "Desktop Magic Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    updateButton.Enabled = true;
                    Process.Start(Application.StartupPath + "\\DM Updater.exe", "update");
                    Application.Exit();
                }
                else
                {
                    updateButton.Text = "Update available";
                    updateButton.Enabled = true;
                }
            }
            else
            {
                updateButton.Text = "Up to date";
            }

            File.WriteAllText(Application.StartupPath + "\\temp", String.Empty);
        }

        private void LoadSetting()
        {
            if (File.Exists(Application.StartupPath + "\\schedule"))
            {
                StreamReader streamReader = new StreamReader(Application.StartupPath + "\\schedule");
                schedule.startTimeH = Int32.Parse(streamReader.ReadLine());
                schedule.startTimeM = Int32.Parse(streamReader.ReadLine());
                schedule.endTimeH = Int32.Parse(streamReader.ReadLine());
                schedule.endTimeM = Int32.Parse(streamReader.ReadLine());
                mon = Boolean.Parse(streamReader.ReadLine());
                tue = Boolean.Parse(streamReader.ReadLine());
                wed = Boolean.Parse(streamReader.ReadLine());
                thu = Boolean.Parse(streamReader.ReadLine());
                fri = Boolean.Parse(streamReader.ReadLine());
                sat = Boolean.Parse(streamReader.ReadLine());
                sun = Boolean.Parse(streamReader.ReadLine());
                schedule.weekDays = new WeekDay(mon, tue, wed, thu, fri, sat, sun);

                streamReader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (started != true)
            {
                started = true;
                startButton.Text = "Minimize to tray";
                n = new EffectForm();
                n.Show();

            }
            else
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000, "Howdy", "Desktop Magic is running in background", ToolTipIcon.None);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Application.Exit();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ab.Visible != true)
                ab.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                BeginInvoke(new MethodInvoker(delegate { Hide(); }));

            }

            //gHook = new GlobalKeyboardHook();
            //gHook.KeyDown += new KeyEventHandler(gHook_Keydown);
            //foreach (Keys key in Enum.GetValues(typeof(Keys)))
            //    gHook.HookedKeys.Add(key);

            //gHook.hook();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (checkBox1.Checked)
            {
                r.SetValue("Desktop_Magic", Application.ExecutablePath);
            }
            else
            {
                r.DeleteValue("Desktop_Magic", false);
            }
            //r.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (checkBox2.Checked)
            {
                r1.SetValue("DM2", true);
            }
            else
            {
                r1.DeleteValue("DM2", false);
            }
            r1.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();

            panel2.Show();
            panel3.Hide();
            WindowState = FormWindowState.Normal;
        }

        private void mainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if (FormWindowState.Minimized == WindowState)
            //{
            //    this.Hide();
            //    panel2.Hide();
            //    notifyIcon1.Visible = true;
            //}
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (n != null && n.IsDisposed != true)
                {
                    if (!userHide)
                        userHide = true;
                    else
                        userHide = false;
                    n.Visible = !userHide;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Show();
            panel2.Show();
            WindowState = FormWindowState.Normal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Speed = trackBarDirection.Value;
            labelDirection.Text = Speed.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Number = trackBarSpeed.Value;
            labelSpeed.Text = Number.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Amount = trackBarNumber.Value;
            labelNumber.Text = Amount.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (customButton.Checked)
            {
                if (trackBarNumber.Value >= 20)
                {
                    if (MessageBox.Show("Warning: Using custom image with high number may cause lag and ram overload!" + "\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        panel2.Hide();
                        panel3.Hide();
                        button8_Click(null, null);
                        this.Show();
                        return;
                    }
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Image (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif";
                op.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                op.CheckFileExists = true;
                op.CheckPathExists = true;
                op.Title = "Choose your image";
                r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
                if (op.ShowDialog() == DialogResult.OK)
                {
                    string path = op.FileName;
                    string extension = Path.GetExtension(path).ToLower();
                    if (extension == ".jpg" || extension == ".png" || extension == ".gif")
                    {
                        r1.SetValue("CustomFile", path);
                        pictureBoxCustom.Image = Image.FromFile(path);
                        pictureBoxCustom.Size = new Size(trackBarSizeCustom.Value, trackBarSizeCustom.Value);
                    }
                    else
                    {
                        MessageBox.Show("Please choose an image (*.jpg, *.png)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        r1.Close();
                        r1.Dispose();
                        op.Dispose();
                        return;
                    }

                    panel2.Show();
                    panel3.Show();
                }
                else
                {

                    r1.Close();
                    r1.Dispose();
                    op.Dispose();
                    return;

                }
                r1.Close();
                r1.Dispose();
                op.Dispose();
                return;

            }
            if (MessageBox.Show("Do you want to change?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveSettings();
            }
            else
                button8_Click(null, null);
            this.Show();

            panel2.Hide();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            panel2.Hide();
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r1.GetValue("DM3") == null)
            {
                r1.SetValue("DM3", trackBarDirection.Value);
                Speed = trackBarDirection.Value;
            }
            else
            {
                trackBarDirection.Value = (int)r1.GetValue("DM3");
            }
            if (r1.GetValue("DM4") == null)
            {
                r1.SetValue("DM4", trackBarSpeed.Value);
                Number = trackBarSpeed.Value;
            }
            else
            {
                trackBarSpeed.Value = (int)r1.GetValue("DM4");
            }
            if (r1.GetValue("DM5") == null)
            {
                r1.SetValue("DM5", trackBarNumber.Value);
                Amount = trackBarNumber.Value;
            }
            else
            {
                trackBarNumber.Value = (int)r1.GetValue("DM5");
            }
            labelDirection.Text = trackBarDirection.Value.ToString();
            labelSpeed.Text = trackBarSpeed.Value.ToString();
            labelNumber.Text = trackBarNumber.Value.ToString();
            if (r1.GetValue("DMT") == null)
            {
                if (leafButton.Checked)
                {
                    r1.SetValue("DMT", 1);
                    leafButton.Checked = true;
                }
                else if (customButton.Checked)
                {
                    r1.SetValue("DMT", 3);
                    customButton.Checked = true;
                }
                else if (sakuraButton.Checked)
                {
                    r1.SetValue("DMT", 4);
                    sakuraButton.Checked = true;
                }
                else if (mixButton.Checked)
                {
                    r.SetValue("DMT", 5);
                    mixButton.Checked = true;
                }
                else
                {
                    r1.SetValue("DMT", 2);
                    customButton.Checked = true;
                }
            }
            else
            {
                if ((int)r1.GetValue("DMT") == 1)
                    leafButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 2)
                    snowButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 3)
                    customButton.Checked = true;
                else if ((int)r1.GetValue("DMT") == 4)
                    sakuraButton.Checked = true;
                else
                    mixButton.Checked = true;
            }
            r1.Close();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (leafButton.Checked)
            {
                rb1 = true;
                rb2 = false;
                rb3 = false;
                rb4 = false;
                rb5 = false;
                applyButton.Text = "Apply";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (snowButton.Checked)
            {
                rb1 = false;
                rb2 = true;
                rb3 = false;
                rb4 = false;
                rb5 = false;
                applyButton.Text = "Apply";
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (customButton.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = true;
                rb4 = false;
                rb5 = false;
                applyButton.Text = "Choose...";

            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (sakuraButton.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = false;
                rb4 = true;
                rb5 = false;
                applyButton.Text = "Apply";
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (mixButton.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = false;
                rb4 = false;
                rb5 = true;
                applyButton.Text = "Apply";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (ab.Visible != true)
                ab.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start("https://clearallsoft.cf/lib");
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f != null && f.IsDisposed != true)
            {
                f.Show();
                f.WindowState = FormWindowState.Normal;
                f.TopMost = true;
                f.TopMost = false;
            }
            else
            {
                f = new Music();
                f.Show();
                f.WindowState = FormWindowState.Normal;
                f.TopMost = true;
                f.TopMost = false;
                // WindowState = FormWindowState.Normal;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to change?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveSettings();
            }
            else
                button8_Click(null, null);
            this.Show();

            panel2.Hide();
            panel3.Hide();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            labelSizeCustom.Text = trackBarSizeCustom.Value.ToString();
            pictureBoxCustom.Size = new Size(trackBarSizeCustom.Value + 5, trackBarSizeCustom.Value + 5);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            musicToolStripMenuItem_Click(null, null);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                panel2.Hide();
                panel3.Hide();
            }
        }

        private void CheckConfig()
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r.GetValue("TTC") != null)
            {

                if ((int)r.GetValue("TTC") > 0)
                    timer2.Interval = (int)r.GetValue("TTC") * 60 * 1000;
                else
                    timer2.Interval = 300000;
                if ((int)r.GetValue("Speed") > 0)
                    RSpeed = (int)r.GetValue("Speed");
                else
                    RSpeed = -9999;
                if ((int)r.GetValue("Direction") > 0)
                    RDirection = (int)r.GetValue("Direction");
                else
                    RDirection = -9999;
                if ((int)r.GetValue("Number") > 0)
                    RNumber = (int)r.GetValue("Number");
                else
                    RNumber = -9999;
                if ((int)r.GetValue("RandomType") == 0)
                {
                    RType = 0;
                }
                else
                {
                    RType = 1;
                }
            }
            r.Close();
            r.Dispose();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (checkBox3.Checked)
            {
                CheckConfig();
                timer2.Enabled = true;
                r1.SetValue("AaR", true);
            }
            else
            {
                timer2.Enabled = false;
                r1.DeleteValue("AaR", false);
                //radioButton5.Enabled = true;
            }
            //if (started == true && checkBox2.Checked)
            // r1.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckConfig();
            if (checkBox3.Checked && started == true)
            {
                if (File.Exists(CGlob.Path))
                {
                    Random rd = new Random();
                    if (RDirection != -9999)
                        trackBarDirection.Value = rd.Next(-10, RDirection);
                    if (RSpeed != -9999)
                        trackBarSpeed.Value = rd.Next(5, RSpeed);
                    if (RNumber != -9999)
                        trackBarNumber.Value = rd.Next(3, RNumber);
                    if (RType == 1)
                    {
                        int ran = rd.Next(1, 5);
                        if (leafButton.Checked)
                        {
                            if (ran == 1)
                                snowButton.Checked = true;
                            else if (ran == 2)
                                sakuraButton.Checked = true;
                            else if (ran == 3)
                                mixButton.Checked = true;
                            else
                                customButton.Checked = true;

                        }
                        else if (snowButton.Checked)
                        {
                            if (ran == 1)
                                sakuraButton.Checked = true;
                            else if (ran == 2)
                                leafButton.Checked = true;
                            else if (ran == 3)
                                customButton.Checked = true;
                            else
                                mixButton.Checked = true;
                        }
                        else if (sakuraButton.Checked)
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                mixButton.Checked = true;
                            else if (ran == 3)
                                customButton.Checked = true;
                            else
                                snowButton.Checked = true;
                        }
                        else if (customButton.Checked)
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                snowButton.Checked = true;
                            else if (ran == 3)
                                mixButton.Checked = true;
                            else
                                sakuraButton.Checked = true;
                        }
                        else
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                snowButton.Checked = true;
                            else if (ran == 3)
                                sakuraButton.Checked = true;
                            else
                                customButton.Checked = true;
                        }
                    }
                }
                else //no custom image
                {
                    Random rd = new Random();
                    if (RDirection != -9999)
                        trackBarDirection.Value = rd.Next(-10, RDirection);
                    if (RSpeed != -9999)
                        trackBarSpeed.Value = rd.Next(5, RSpeed);
                    if (RNumber != -9999)
                        trackBarNumber.Value = rd.Next(3, RNumber);
                    if (RType == 1)
                    {
                        int ran = rd.Next(1, 5);
                        if (leafButton.Checked)
                        {
                            if (ran == 1)
                                snowButton.Checked = true;
                            else if (ran == 2)
                                sakuraButton.Checked = true;
                            else
                                mixButton.Checked = true;


                        }
                        else if (snowButton.Checked)
                        {
                            if (ran == 1)
                                sakuraButton.Checked = true;
                            else if (ran == 2)
                                leafButton.Checked = true;
                            else
                                mixButton.Checked = true;

                        }
                        else if (sakuraButton.Checked)
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                mixButton.Checked = true;
                            else
                                snowButton.Checked = true;
                        }
                        else if (customButton.Checked)
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                snowButton.Checked = true;
                            else if (ran == 3)
                                mixButton.Checked = true;
                            else
                                sakuraButton.Checked = true;
                        }
                        else
                        {
                            if (ran == 1)
                                leafButton.Checked = true;
                            else if (ran == 2)
                                snowButton.Checked = true;
                            else
                                sakuraButton.Checked = true;

                        }
                    }
                }

                //save

                saveSettings();
            }
        }

        private void saveSettings()
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            CGlob.Speed = trackBarDirection.Value;
            CGlob.Number = trackBarSpeed.Value;
            CGlob.Amount = trackBarNumber.Value;
            r1.SetValue("DM3", trackBarDirection.Value);
            r1.SetValue("DM4", trackBarSpeed.Value);
            r1.SetValue("DM5", trackBarNumber.Value);
            if (rb1 == true)
            {
                CGlob.Type = 1;
                r1.SetValue("DMT", 1);
                //Glob.Custom = false;
                CGlob.Path = "";
            }
            else if (rb2 == true)
            {
                CGlob.Type = 2;
                r1.SetValue("DMT", 2);
                //Glob.Custom = false;
                CGlob.Path = "";
            }
            else if (rb3 == true)
            {
                CGlob.Type = 3;
                r1.SetValue("DMT", 3);
                //Glob.Custom = true;
                CGlob.Path = (string)r1.GetValue("CustomFile");
                CGlob.Size = trackBarSizeCustom.Value;
                r1.SetValue("DM6", CGlob.Size);
            }
            else if (rb5 == true)
            {
                CGlob.Type = 5;
                r1.SetValue("DMT", 5);
                //Glob.Custom = false;
                if (r1.GetValue("CustomFile") != null)
                {
                    CGlob.Path = (string)r1.GetValue("CustomFile");
                    CGlob.Size = trackBarSizeCustom.Value;
                    r1.SetValue("DM6", CGlob.Size);
                }
                else
                    CGlob.Path = "";

            }
            else
            {
                CGlob.Type = 4;
                r1.SetValue("DMT", 4);
                //Glob.Custom = false;
                CGlob.Path = "";
            }
            labelDirection.Text = trackBarDirection.Value.ToString();
            labelSpeed.Text = trackBarSpeed.Value.ToString();
            labelNumber.Text = trackBarNumber.Value.ToString();

            r1.Close();
            r1.Dispose();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config f4 = new Config();
            f4.ShowDialog();
        }

        //new control
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\schedule") && checkBox1.Checked)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && mon || DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && tue || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && wed || DateTime.Now.DayOfWeek == DayOfWeek.Thursday && thu || DateTime.Now.DayOfWeek == DayOfWeek.Friday && fri || DateTime.Now.DayOfWeek == DayOfWeek.Saturday && sat || DateTime.Now.DayOfWeek == DayOfWeek.Sunday && sun)
                {
                    if (schedule.startTimeH < schedule.endTimeH)//if start time < end time
                    {
                        if (DateTime.Now.Hour == schedule.startTimeH)//check time = start time
                        {
                            if (DateTime.Now.Minute >= schedule.startTimeM) //check minute >= start minute
                                CheckForStart();
                            else
                                CheckForStop();
                        }
                        else if (DateTime.Now.Hour > schedule.startTimeH) //check time > start time
                        {
                            if (DateTime.Now.Hour < schedule.endTimeH)//check time < end time
                            {
                                CheckForStart();
                            }
                            else if (DateTime.Now.Hour == schedule.endTimeH)//check time == end time
                            {
                                if (DateTime.Now.Minute <= schedule.endTimeM) //check time <= end minute
                                    CheckForStart();
                                else
                                    CheckForStop();
                            }
                            else//else time > end time
                                CheckForStop();
                        }
                        else //else time < start time
                        {
                            CheckForStop();
                        }
                    }
                    else if (schedule.startTimeH > schedule.endTimeH) //if start time > end time
                    {
                        if (DateTime.Now.Hour == schedule.startTimeH) //check time = start time
                        {
                            if (DateTime.Now.Minute >= schedule.startTimeM) //check minute >= start minute
                                CheckForStart();
                            else
                                CheckForStop();
                        }
                        else if (DateTime.Now.Hour > schedule.startTimeH)// if time > start time
                        {
                            CheckForStart();
                        }
                        else
                        {
                            if (DateTime.Now.Hour == schedule.endTimeH) //if time <= end time
                            {
                                if (DateTime.Now.Minute <= schedule.endTimeM) //if (minute <= end minute
                                    CheckForStart();
                                else
                                    CheckForStop();
                            }
                            else if (DateTime.Now.Hour < schedule.endTimeH)
                            {
                                CheckForStart();
                            }
                            else
                                CheckForStop();
                        }
                    }
                    else //else they're equal
                    {
                        if (DateTime.Now.Hour == schedule.startTimeH)//if time = start time
                        {
                            if (DateTime.Now.Minute >= schedule.startTimeM)//if minute >= start minute
                            {
                                CheckForStart();
                            }
                            else
                            {
                                CheckForStop();
                            }
                        }
                        else
                        {
                            CheckForStop();
                        }
                    }
                }
                else
                {
                    CheckForStop();
                }
                
            }
        }

        private void CheckForStop()
        {
            if (n!= null && !n.IsDisposed)
                if (n.Visible)
                    n.Visible = false;
        }

        private void CheckForStart()
        {
            if (checkBox1.Checked)
            {
                if (n != null && !n.IsDisposed)
                {
                    n.Visible = !userHide;
                }
                else
                {
                    button1_Click(null, null);
                }
            }
        }

        private void checkBoxSmoothmotion_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSmoothmotion.Checked)
            {
                CGlob.Smooth = true;  
            }
            else
            {
                CGlob.Smooth = false;
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (checkBoxSmoothmotion.Checked)
            {
                r1.SetValue("DM7", true);
            }
            else
            {
                r1.DeleteValue("DM7", false);
            }
            r1.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Form frm = (Form)sender;
            ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid);
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid);
        }

        private void buttonSchedule_Click(object sender, EventArgs e)
        {
            ScheduleSetting scheduleSetting = new ScheduleSetting();
            scheduleSetting.FormClosed += ScheduleSetting_FormClosed;
            scheduleSetting.ShowDialog();
        }

        private void ScheduleSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadSetting();
        }
    }

    internal class NativeMethods
    {
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_DMSHOWME = RegisterWindowMessage("WM_DMSHOWME");
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}
