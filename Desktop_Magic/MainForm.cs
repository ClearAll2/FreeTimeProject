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
        int newx, newy;
        bool started = false;
        string path = "";
        RegistryKey r;
        RegistryKey r1;
        int Speed; int RSpeed = 15;
        int Number; int RDirection = 10;
        int Amount; int RNumber = 15; int RType = 1;
        bool rb1 = false;
        bool rb2 = false;
        bool rb3 = false;
        bool rb4 = false;
        bool rb5 = false;
        BackgroundWorker bw;
        BackgroundWorker bw3;
        string tmp = "";
        string changelog = "";
        EffectForm n;
        Music f;
        //Form5 f5;
        int value;
        About ab = new About();
        KeyboardHook hook = new KeyboardHook();
        KeyboardHook hook2 = new KeyboardHook();

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
            Glob g = new Glob();
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Text = "Desktop Magic v" + Application.ProductVersion + "\n" + "Click to pause/resume effect";
            panel2.Hide();
            panel3.Hide();
            trackBar4.Value = Glob.Size;
            label13.Text = trackBar4.Value.ToString();
            value = trackBar4.Value;
            if (r.GetValue("Desktop_Magic") == null)
            {
                if ((string)r.GetValue("Desktop_Magic") == Application.ExecutablePath)
                    checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                button1_Click(null, null);
                notifyIcon1.Visible = true;
            }
            //new
            if (r1.GetValue("DM3") == null)
            {
                r1.SetValue("DM3", trackBar1.Value);
                Speed = trackBar1.Value;
            }
            else
            {
                trackBar1.Value = (int)r1.GetValue("DM3");
            }
            if (r1.GetValue("DM4") == null)
            {
                r1.SetValue("DM4", trackBar2.Value);
                Number = trackBar2.Value;
            }
            else
            {
                trackBar2.Value = (int)r1.GetValue("DM4");
            }
            if (r1.GetValue("DM5") == null)
            {
                r1.SetValue("DM5", trackBar3.Value);
                Amount = trackBar3.Value;
            }
            else
            {
                trackBar3.Value = (int)r1.GetValue("DM5");
            }
            label7.Text = trackBar1.Value.ToString();
            label9.Text = trackBar2.Value.ToString();
            label11.Text = trackBar3.Value.ToString();
            if (r1.GetValue("DMT") == null)
            {
                if (radioButton1.Checked)
                {
                    r1.SetValue("DMT", 1);
                    radioButton1.Checked = true;
                }

                else if (radioButton3.Checked)
                {
                    r1.SetValue("DMT", 3);
                    radioButton3.Checked = true;
                }
                else if (radioButton4.Checked)
                {
                    r1.SetValue("DMT", 4);
                    radioButton4.Checked = true;
                }
                else if (radioButton5.Checked)
                {
                    r1.SetValue("DMT", 5);
                    radioButton5.Checked = true;
                }
                else
                {
                    r1.SetValue("DMT", 2);
                    radioButton2.Checked = true;
                }
            }
            else
            {
                if ((int)r1.GetValue("DMT") == 1)
                    radioButton1.Checked = true;
                else if ((int)r1.GetValue("DMT") == 2)
                    radioButton2.Checked = true;
                else if ((int)r1.GetValue("DMT") == 3)
                    radioButton3.Checked = true;
                else if ((int)r1.GetValue("DMT") == 5)
                    radioButton5.Checked = true;
                else
                    radioButton4.Checked = true;
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
            button4.Enabled = false;
            button4.Text = "Checking version...";
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

        }

        private void hook2_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == global::ModifierKeys.Alt)
                trackBar1.Value += 1;
            else if (e.Modifier == global::ModifierKeys.Control)
                trackBar2.Value += 1;
            else if (e.Modifier == global::ModifierKeys.Shift)
                trackBar3.Value += 1;
            saveSettings();
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == global::ModifierKeys.Alt)
                trackBar1.Value -= 1;
            else if (e.Modifier == global::ModifierKeys.Control)
                trackBar2.Value -= 1;
            else if (e.Modifier == global::ModifierKeys.Shift)
                trackBar3.Value -= 1;
            saveSettings();
        }

        //tips
        private void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            Random r = new Random();
        ret:
            int o = r.Next(1, 9);
            if (o == 1)
            {
                tipsLabel.Text = "You can't play music before starting";
            }
            else if (o == 2)
            {
                tipsLabel.Text = "This app checks for update automatically";
            }
            else if (o == 3)
            {
                tipsLabel.Text = "You can only open 1 instance of this app";
            }
            else if (o == 4)
            {
                tipsLabel.Text = "You can check for update by clicking About";
            }
            else if (o == 5)
            {
                tipsLabel.Text = "You can pause/resume effect by clicking tray icon";
            }
            else if (o == 6)
            {
                tipsLabel.Text = "Alt + Left/Right to change driection";
            }
            else if (o == 7)
            {
                tipsLabel.Text = "Ctrl + Left/Right to change speed";
            }
            else if (o == 8)
            {
                tipsLabel.Text = "Shift + Left/Right to change number";
            }
            else
            {
                tipsLabel.Text = "Have a nice day!";
            }

            Thread.Sleep(10000);

            goto ret;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            WebClient wc = new WebClient();
            //check if internet is connected
            try
            {
                button4.Text = "Connecting...";
                var temp = wc.DownloadString("https://www.google.com.vn");

            }
            catch
            {
                button4.Text = "UpToDate";
                wc.Dispose();
                return;
            }
            button4.Text = "Checking version...";
            var ui = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsV3B1OHN5Q0FIa3M");//check version

            if (Application.ProductVersion.CompareTo(ui) < 0)
            {
                changelog = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsbUVpQTd2Sk5SRE0");
                wc.Dispose();
                tmp = ui;
                button4.Text = "Download new...";

                if (MessageBox.Show("New version " + tmp + " is now available\n\n" + changelog + "\n\nWould you like to download now?", "Desktop Magic Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    button4.Enabled = true;
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsUGxKUWtFTnM4dms");
                }
                else
                {
                    button4.Text = "Download new...";
                    button4.Enabled = true;
                }
            }
            else
            {
                button4.Text = "UpToDate";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (started != true)
            {
                //button9.Enabled = false;
                started = true;
                button1.Text = "Minimize";
                n = new EffectForm();
                n.Show();

            }
            else
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000, "Desktop Magic", "Desktop Magic is running in background", ToolTipIcon.Info);
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
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
                panel2.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (n != null && n.IsDisposed != true)
                {
                    if (n.Visible == true)
                    {
                        n.Hide();
                    }
                    else
                    {
                        n.Show();
                    }
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
            Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsUGxKUWtFTnM4dms");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Speed = trackBar1.Value;
            label7.Text = Speed.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Number = trackBar2.Value;
            label9.Text = Number.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Amount = trackBar3.Value;
            label11.Text = Amount.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                if (trackBar3.Value >= 20)
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
                        pictureBox2.Image = Image.FromFile(path);
                        pictureBox2.Size = new Size(trackBar4.Value, trackBar4.Value);
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
            this.Hide();
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r1.GetValue("DM3") == null)
            {
                r1.SetValue("DM3", trackBar1.Value);
                Speed = trackBar1.Value;
            }
            else
            {
                trackBar1.Value = (int)r1.GetValue("DM3");
            }
            if (r1.GetValue("DM4") == null)
            {
                r1.SetValue("DM4", trackBar2.Value);
                Number = trackBar2.Value;
            }
            else
            {
                trackBar2.Value = (int)r1.GetValue("DM4");
            }
            if (r1.GetValue("DM5") == null)
            {
                r1.SetValue("DM5", trackBar3.Value);
                Amount = trackBar3.Value;
            }
            else
            {
                trackBar3.Value = (int)r1.GetValue("DM5");
            }
            label7.Text = trackBar1.Value.ToString();
            label9.Text = trackBar2.Value.ToString();
            label11.Text = trackBar3.Value.ToString();
            if (r1.GetValue("DMT") == null)
            {
                if (radioButton1.Checked)
                {
                    r1.SetValue("DMT", 1);
                    radioButton1.Checked = true;
                }
                else if (radioButton3.Checked)
                {
                    r1.SetValue("DMT", 3);
                    radioButton3.Checked = true;
                }
                else if (radioButton4.Checked)
                {
                    r1.SetValue("DMT", 4);
                    radioButton4.Checked = true;
                }
                else if (radioButton5.Checked)
                {
                    r.SetValue("DMT", 5);
                    radioButton5.Checked = true;
                }
                else
                {
                    r1.SetValue("DMT", 2);
                    radioButton3.Checked = true;
                }
            }
            else
            {
                if ((int)r1.GetValue("DMT") == 1)
                    radioButton1.Checked = true;
                else if ((int)r1.GetValue("DMT") == 2)
                    radioButton2.Checked = true;
                else if ((int)r1.GetValue("DMT") == 3)
                    radioButton3.Checked = true;
                else if ((int)r1.GetValue("DMT") == 4)
                    radioButton4.Checked = true;
                else
                    radioButton5.Checked = true;
            }
            r1.Close();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                rb1 = true;
                rb2 = false;
                rb3 = false;
                rb4 = false;
                rb5 = false;
                button7.Text = "Apply";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                rb1 = false;
                rb2 = true;
                rb3 = false;
                rb4 = false;
                rb5 = false;
                button7.Text = "Apply";
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = true;
                rb4 = false;
                rb5 = false;
                button7.Text = "Choose...";
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = false;
                rb4 = true;
                rb5 = false;
                button7.Text = "Apply";
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                rb1 = false;
                rb2 = false;
                rb3 = false;
                rb4 = false;
                rb5 = true;
                button7.Text = "Apply";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
                bw.RunWorkerAsync();
            if (ab.Visible != true)
                ab.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start("https://clearallsoft.cf/lib");
        }

        private void musicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (started == true)
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
            else
                MessageBox.Show("Play effect before playing music!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            label13.Text = trackBar4.Value.ToString();
            pictureBox2.Size = new Size(trackBar4.Value + 5, trackBar4.Value + 5);
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
                if (File.Exists(Glob.Path))
                {
                    Random rd = new Random();
                    if (RDirection != -9999)
                        trackBar1.Value = rd.Next(-10, RDirection);
                    if (RSpeed != -9999)
                        trackBar2.Value = rd.Next(5, RSpeed);
                    if (RNumber != -9999)
                        trackBar3.Value = rd.Next(3, RNumber);
                    if (RType == 1)
                    {
                        int ran = rd.Next(1, 5);
                        if (radioButton1.Checked)
                        {
                            if (ran == 1)
                                radioButton2.Checked = true;
                            else if (ran == 2)
                                radioButton4.Checked = true;
                            else if (ran == 3)
                                radioButton5.Checked = true;
                            else
                                radioButton3.Checked = true;

                        }
                        else if (radioButton2.Checked)
                        {
                            if (ran == 1)
                                radioButton4.Checked = true;
                            else if (ran == 2)
                                radioButton1.Checked = true;
                            else if (ran == 3)
                                radioButton3.Checked = true;
                            else
                                radioButton5.Checked = true;
                        }
                        else if (radioButton4.Checked)
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton5.Checked = true;
                            else if (ran == 3)
                                radioButton3.Checked = true;
                            else
                                radioButton2.Checked = true;
                        }
                        else if (radioButton3.Checked)
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton2.Checked = true;
                            else if (ran == 3)
                                radioButton5.Checked = true;
                            else
                                radioButton4.Checked = true;
                        }
                        else
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton2.Checked = true;
                            else if (ran == 3)
                                radioButton4.Checked = true;
                            else
                                radioButton3.Checked = true;
                        }
                    }
                }
                else //no custom image
                {
                    Random rd = new Random();
                    if (RDirection != -9999)
                        trackBar1.Value = rd.Next(-10, RDirection);
                    if (RSpeed != -9999)
                        trackBar2.Value = rd.Next(5, RSpeed);
                    if (RNumber != -9999)
                        trackBar3.Value = rd.Next(3, RNumber);
                    if (RType == 1)
                    {
                        int ran = rd.Next(1, 5);
                        if (radioButton1.Checked)
                        {
                            if (ran == 1)
                                radioButton2.Checked = true;
                            else if (ran == 2)
                                radioButton4.Checked = true;
                            else
                                radioButton5.Checked = true;


                        }
                        else if (radioButton2.Checked)
                        {
                            if (ran == 1)
                                radioButton4.Checked = true;
                            else if (ran == 2)
                                radioButton1.Checked = true;
                            else
                                radioButton5.Checked = true;

                        }
                        else if (radioButton4.Checked)
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton5.Checked = true;
                            else
                                radioButton2.Checked = true;
                        }
                        else if (radioButton3.Checked)
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton2.Checked = true;
                            else if (ran == 3)
                                radioButton5.Checked = true;
                            else
                                radioButton4.Checked = true;
                        }
                        else
                        {
                            if (ran == 1)
                                radioButton1.Checked = true;
                            else if (ran == 2)
                                radioButton2.Checked = true;
                            else
                                radioButton4.Checked = true;

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
            Glob.Speed = trackBar1.Value;
            Glob.Number = trackBar2.Value;
            Glob.Amount = trackBar3.Value;
            r1.SetValue("DM3", trackBar1.Value);
            r1.SetValue("DM4", trackBar2.Value);
            r1.SetValue("DM5", trackBar3.Value);
            if (rb1 == true)
            {
                Glob.Type = 1;
                r1.SetValue("DMT", 1);
                //Glob.Custom = false;
                Glob.Path = "";
            }
            else if (rb2 == true)
            {
                Glob.Type = 2;
                r1.SetValue("DMT", 2);
                //Glob.Custom = false;
                Glob.Path = "";
            }
            else if (rb3 == true)
            {
                Glob.Type = 3;
                r1.SetValue("DMT", 3);
                //Glob.Custom = true;
                Glob.Path = (string)r1.GetValue("CustomFile");
                Glob.Size = trackBar4.Value;
                r1.SetValue("DM6", Glob.Size);
            }
            else if (rb5 == true)
            {
                Glob.Type = 5;
                r1.SetValue("DMT", 5);
                //Glob.Custom = false;
                if (r1.GetValue("CustomFile") != null)
                {
                    Glob.Path = (string)r1.GetValue("CustomFile");
                    Glob.Size = trackBar4.Value;
                    r1.SetValue("DM6", Glob.Size);
                }
                else
                    Glob.Path = "";

            }
            else
            {
                Glob.Type = 4;
                r1.SetValue("DMT", 4);
                //Glob.Custom = false;
                Glob.Path = "";
            }
            label7.Text = trackBar1.Value.ToString();
            label9.Text = trackBar2.Value.ToString();
            label11.Text = trackBar3.Value.ToString();

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



        private void label18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Shortcut keys:\n\nAlt + Left/Right to change direction\nCtrl + Left/Right to change speed\nShift + Left/Right to change number", "About shortcut keys", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
