using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace DM
{
    
    public partial class Form1 : Form
    {
        //GlobalKeyboardHook gHook;
        int newx, newy;
        Form2 n;
        public bool started = false;
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
        BackgroundWorker bw2; 
        //BackgroundWorker bw3;
        bool check = false;
        string tmp = "";
        string changelog = "";
        Form3 f;
        int value;
        int totalHits = 0;
        AboutBox1 ab = new AboutBox1();
        string tips1 = "You can pause/resume effect by clicking tray icon";
        string tips2 = "You can check for update by clicking About";
        string tips3 = "You can only open 1 instance of this app";
        string tips4 = "This app checks for update automatically";
        string tips5 = "You can't play music before starting";
        PerformanceCounter ramCo = new PerformanceCounter("Memory", "Available MBytes");
        public Form1()
        {
            InitializeComponent();
            tipsLabel.Text = tips4;
            notifyIcon1.Visible = false;
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;
            //bw3 = new BackgroundWorker();
            //bw3.DoWork += bw3_DoWork;
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data");
            if (r1 == null)
                r1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data");
            Glob g = new Glob();
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Text = "Desktop Magic v" + Application.ProductVersion + "\n" + "Click to pause/resume effect";
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            trackBar4.Value = Glob.Size;
            label13.Text = trackBar4.Value.ToString();
            value = trackBar4.Value;
            if (r.GetValue("Desktop_Magic") == null)
            {
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
               
                //button1_Click(null, null);
            }

            bw.RunWorkerAsync();
            //bw3.RunWorkerAsync();
            button4.Enabled = false;
            button4.Text = "Checking version...";
            r.Close();
            r1.Close();
            f = new Form3();
            //for hook
            f.Show();
            f.Hide();
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
            panel2.DragEnter +=panel2_DragEnter;
            panel2.DragDrop += panel2_DragDrop;

            
        }

        

        //drag-drop pictures
        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] Paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in Paths)
                {
                    if (Path.GetExtension(path) == ".png" || Path.GetExtension(path) == ".jpg" || Path.GetExtension(path) == ".gif")
                    {
                        r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
                        r1.SetValue("CustomFile", path);

                        FileStream fs = new System.IO.FileStream(@path, FileMode.Open, FileAccess.Read);
                        pictureBox2.Image = Image.FromStream(fs);
                        fs.Close();
                        fs.Dispose();

                        //pictureBox2.Image = Image.FromFile(path);
                        pictureBox2.Size = new Size(trackBar4.Value, trackBar4.Value);
                        Glob.Speed = trackBar1.Value;
                        Glob.Number = trackBar2.Value;
                        Glob.Amount = trackBar3.Value;
                        r1.SetValue("DM3", trackBar1.Value);
                        r1.SetValue("DM4", trackBar2.Value);
                        r1.SetValue("DM5", trackBar3.Value);

                        if (rb5 != true)
                        {
                            radioButton3.Checked = true;
                            Glob.Type = 3;
                            r1.SetValue("DMT", 3);
                            //Glob.Custom = true;
                            Glob.Path = (string)r1.GetValue("CustomFile");
                        }
                        else
                        {
                            Glob.Type = 5;
                            r1.SetValue("DMT", 5);
                            //Glob.Custom = false;
                            if (r1.GetValue("CustomFile") != null)
                                Glob.Path = (string)r1.GetValue("CustomFile");
                            else
                                Glob.Path = "";
                        }
                        r1.Close();
                    }
                }
            }
        }

        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] Paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in Paths)
                {
                    if (Path.GetExtension(path) == ".png" || Path.GetExtension(path) == ".jpg" || Path.GetExtension(path) == ".gif")
                    {
                        r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
                        r1.SetValue("CustomFile", path);

                        FileStream fs = new System.IO.FileStream(@path, FileMode.Open, FileAccess.Read);
                        pictureBox2.Image = Image.FromStream(fs);
                        fs.Close();
                        fs.Dispose();

                        //pictureBox2.Image = Image.FromFile(path);
                        pictureBox2.Size = new Size(trackBar4.Value, trackBar4.Value);
                        Glob.Speed = trackBar1.Value;
                        Glob.Number = trackBar2.Value;
                        Glob.Amount = trackBar3.Value;
                        r1.SetValue("DM3", trackBar1.Value);
                        r1.SetValue("DM4", trackBar2.Value);
                        r1.SetValue("DM5", trackBar3.Value);
                        
                        if (rb5 != true)
                        {
                            radioButton3.Checked = true;
                            Glob.Type = 3;
                            r1.SetValue("DMT", 3);
                            //Glob.Custom = true;
                            Glob.Path = (string)r1.GetValue("CustomFile");
                        }
                        else
                        {
                            Glob.Type = 5;
                            r1.SetValue("DMT", 5);
                            //Glob.Custom = false;
                            if (r1.GetValue("CustomFile") != null)
                                Glob.Path = (string)r1.GetValue("CustomFile");
                            else
                                Glob.Path = "";
                        }
                        r1.Close();
                    }
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        

        private void bw2_DoWork(object sender, DoWorkEventArgs e)//open online gallery
        {
            WebClient wc = new WebClient();
            //check if internet is connected
            button9.Text = "Checking internet...";
            try
            {

                var temp = wc.DownloadString("https://www.google.com");

            }
            catch (WebException)
            {
                button9.Text = "Get more arts online";
                MessageBox.Show("Cannot connect to server.\nPlease make sure you have a valid internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button9.Text = "Connecting...";
            Process.Start("https://mini102.wordpress.com/library/");
            button9.Text = "Get more arts online";
            // Process.Start("https://goo.gl/photos/2BZviLR9jR5zMAhG7")
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            WebClient wc = new WebClient();
            string tmp1 = "https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsV3B1OHN5Q0FIa3M"; //check version

            Uri url = new Uri(tmp1.Trim());
            //
            //check if internet is connected
            try
            {
                button4.Text = "Connecting...";
                var temp = wc.DownloadString("https://www.google.com");

            }
            catch (WebException)
            {
                button4.Text = "UpToDate";
                wc.Dispose();
                return;
            }
            button4.Text = "Checking version...";
            var ui = wc.DownloadString(url);
            
            if (ui != Application.ProductVersion)
            {
                changelog = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsbUVpQTd2Sk5SRE0");
                wc.Dispose();
                tmp = ui;
                check = true;
                button4.Text = "Download new...";
            }
            else
            {
                timer1.Stop();
                button4.Text = "UpToDate";
            }
               
            //throw new NotImplementedException();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check == true)
            {
                timer1.Stop();        
                check = false;
                if (MessageBox.Show("A new version is available v" + tmp +"\nWould you like to download now?" + "\n\nChangelog: \n" + changelog, "Desktop Magic Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (n != null)
                    {
                        n.Dispose();
                        n.Close();
                    }
                    label5.Text = "";
                    panel1.Show();
                    this.Show();

                    progressBar1.Hide();
                    label3.Hide();
                    label2.Text = "Download new version\n" + "File: DesktopMagic.rar\n" + "Version: " + tmp;
                    label4.Hide();
                }
                else
                {
                    button4.Text = "Download new...";
                    button4.Enabled = true;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(started != true)
            {
                //button9.Enabled = false;
                started = true;
                button1.Text = "Minimize";
                n = new Form2();
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

        //private void gHook_Keydown(object sender, KeyEventArgs e)
        //{
        //    //if (e.KeyValue == 0xA2 | e.KeyValue == 0xA0)
        //    //{
        //    //    HideForm();
        //    //}
        //    if (e.Modifiers == (Keys.LControlKey | Keys.LShiftKey))
        //    {
        //        HideForm();
        //    }
            
        //}

        

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            panel1.Show();
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
                panel1.Hide();
                panel2.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.Show();
            //WindowState = FormWindowState.Normal;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        public void HideForm()
        {
            if (n != null && n.IsDisposed != true)
            {
                if (n.Visible == true)
                {

                    n.Hide();
                    //notifyIcon1.ShowBalloonTip(1000, "Desktop Magic", "Desktop Magic has been paused", ToolTipIcon.Warning);
                }
                else
                {

                    n.Show();
                    // notifyIcon1.ShowBalloonTip(1000, "Desktop Magic", "Desktop Magic has been resumed", ToolTipIcon.Info);
                }
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
                        //notifyIcon1.ShowBalloonTip(1000, "Desktop Magic", "Desktop Magic has been paused", ToolTipIcon.Warning);
                    }
                    else
                    {

                        n.Show();
                        // notifyIcon1.ShowBalloonTip(1000, "Desktop Magic", "Desktop Magic has been resumed", ToolTipIcon.Info);
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Show();
            panel1.Show();
            panel2.Show();
            WindowState = FormWindowState.Normal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (n != null)
            {
                n.Dispose();
                n.Close();
            }
            label5.Text = "";
            panel1.Show();
            this.Show();

            progressBar1.Hide();
            label3.Hide();
            label2.Text = "Download new version\n" + "File: DesktopMagic.rar\n" + "Version: " + tmp;
            label4.Hide();
        }
       public void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
       {
           progressBar1.Value = e.ProgressPercentage;
           label3.Text = progressBar1.Value.ToString() + "%";
      
       }
       public void wc_DownloadFileCompleted(Object sender, AsyncCompletedEventArgs e)
       {
          if (MessageBox.Show("Download completed!\n" + "Would you like to open downloaded file?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
              Process.Start("explorer.exe", @path);
              
          }
          Application.Exit();
       }

       private void panel1_Paint_1(object sender, PaintEventArgs e)
       {

       }

       private void button6_Click(object sender, EventArgs e)
       {
           if (MessageBox.Show("You really want to quit?", "About", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
           {
               Application.Exit();
           }
          
          
       }

       private void button5_Click(object sender, EventArgs e)
       {
           FolderBrowserDialog fd = new FolderBrowserDialog();
           fd.Description = "Select place to save";
           fd.RootFolder = Environment.SpecialFolder.MyComputer;
           fd.ShowNewFolderButton = true;
           if(fd.ShowDialog() == DialogResult.OK)
           {
               progressBar1.Show();
               label3.Show();
               label4.Show();
               button5.Hide();
               button6.Hide();
               path = fd.SelectedPath + "\\DesktopMagic.rar";
               label5.Text = path;
               fd.Dispose();
               WebClient nwc = new WebClient();
               string tmp2 = "https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsUGxKUWtFTnM4dms";//new version
               Uri url;
               url = new Uri(tmp2.Trim());
               try
               {
                   nwc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                   nwc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                   nwc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                   nwc.DownloadFileAsync(url, @path);
               }
               catch (Exception)
               {
                   MessageBox.Show("Oop! Please try later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   Application.Exit();
               }
               
           }
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
                   if (MessageBox.Show("Warning: Using custom image with high number will cause lag and ram overload!" + "\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                   {
                       panel1.Hide();
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
               op.Title = "Choose your picture";
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
                       op.Dispose();
                       return;
                   }
                   panel1.Show();
                   panel2.Show();
                   panel3.Show();
               }
               else
               {
                   
                    r1.Close();
                    op.Dispose();
                    return;
                   
               }
               r1.Close();
               op.Dispose();
               return;
           }
           if (MessageBox.Show("Do you want to change?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
               }
               else if (rb4 == true)
               {
                   Glob.Type = 4;
                   r1.SetValue("DMT", 4);
                   //Glob.Custom = false;
                   Glob.Path = "";
               }
               else if (rb5 == true)
               {
                   Glob.Type = 5;
                   r1.SetValue("DMT", 5);
                   //Glob.Custom = false;
                   if (r1.GetValue("CustomFile") != null)
                       Glob.Path = (string)r1.GetValue("CustomFile");
                   else
                       Glob.Path = "";
               }
               r1.Close();
               //if (n != null)
               //{
               //    n.Dispose();
               //    n.Close();
               //    n = new Form2();
               //    n.Show();
               //}
           }
           else
               button8_Click(null, null);
           this.Show();
           panel1.Hide();
           panel2.Hide();
          
       }

       private void button8_Click(object sender, EventArgs e)
       {
           notifyIcon1.Visible = true;
           panel1.Hide();
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

       private void panel2_Paint(object sender, PaintEventArgs e)
       {

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
               //pictureBox1.Image = global::Desktop_Magic.Properties.Resources.autumn_leaves_PNG3571;
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
               //pictureBox1.Image = global::Desktop_Magic.Properties.Resources.SnowFlake003;
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
               //
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

       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
       {
           //gHook.unhook();
       }

       private void label1_Click(object sender, EventArgs e)
       {
           timer1.Start();
           if (bw.IsBusy != true)
               bw.RunWorkerAsync();
           if (ab.Visible != true)
               ab.ShowDialog();
       }

       private void button9_Click(object sender, EventArgs e)
       {
           if (bw2.IsBusy != true)
               bw2.RunWorkerAsync();
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
                   f = new Form3();
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
               r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
               Glob.Speed = trackBar1.Value;
               Glob.Number = trackBar2.Value;
               Glob.Amount = trackBar3.Value;
               Glob.Size = trackBar4.Value;
               r1.SetValue("DM3", trackBar1.Value);
               r1.SetValue("DM4", trackBar2.Value);
               r1.SetValue("DM5", trackBar3.Value);
               r1.SetValue("DM6", trackBar4.Value);
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
               }
               else if (rb5 == true)
               {
                   Glob.Type = 5;
                   r1.SetValue("DMT", 5);
                   //Glob.Custom = false;
                   if (r1.GetValue("CustomFile") != null)
                       Glob.Path = (string)r1.GetValue("CustomFile");
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
               r1.Close();
               //if (n != null)
               //{
               //    n.Dispose();
               //    n.Close();
               //    n = new Form2();
               //    n.Show();
               //}
               
           }
           else
               button8_Click(null, null);
           this.Show();
           panel1.Hide();
           panel2.Hide();
           panel3.Hide();
           
       }

       private void pictureBox2_Click(object sender, EventArgs e)
       {

       }

       private void trackBar4_Scroll(object sender, EventArgs e)
       {
           //if (trackBar4.Value > value)
           //{
           //    pictureBox2.Top += (trackBar4.Value - value);
           //    pictureBox2.Left += (trackBar4.Value - value);
           //    value = trackBar4.Value;
           //}
           //else
           //{
           //    pictureBox2.Top += (- trackBar4.Value + value);
           //    pictureBox2.Left += (- trackBar4.Value + value);
           //    value = trackBar4.Value;
           //}
           label13.Text = trackBar4.Value.ToString();
           pictureBox2.Size = new Size(trackBar4.Value + 5, trackBar4.Value + 5);
           //if (pictureBox2.Top > panel4.Top)
           //    pictureBox2.Top = panel4.Top;
           
           
       }

       private void button11_Click(object sender, EventArgs e)
       {
           musicToolStripMenuItem_Click(null, null);
       }

       private void button12_Click(object sender, EventArgs e)
       {
           if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
           {
               panel1.Hide();
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
                }
                else if (rb5 == true)
                {
                    Glob.Type = 5;
                    r1.SetValue("DMT", 5);
                    //Glob.Custom = false;
                    if (r1.GetValue("CustomFile") != null)
                        Glob.Path = (string)r1.GetValue("CustomFile");
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
            }
       }



       private void timer3_Tick(object sender, EventArgs e)
       {
           float ram = ramCo.NextValue();
           if ((int)ram < 900)
           {
               totalHits += 1;
               if (totalHits >= 30)
               {
                   timer3.Enabled = false;
                   if (MessageBox.Show("You are running out of RAM!!!\nDo you want to quit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                   {
                       if (MessageBox.Show("Do not show this again?", "Select", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                           timer3.Enabled = false;
                       else
                       {
                           timer3.Enabled = true;
                           totalHits = 0;
                       }
                           
                   }
                   else
                       Application.Exit();
               }
           }
           else
               totalHits = 0;
           //notifyIcon1.Text = "Time: " + totalHits.ToString() + "\nARam: " + ram.ToString();
       }

       private void configToolStripMenuItem_Click(object sender, EventArgs e)
       {
           Form4 f4 = new Form4();
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

       private void timer4_Tick(object sender, EventArgs e)
       {
           Random r = new Random();
           int o = r.Next(1, 5);
           if (o == 1)
           {
               tipsLabel.Text = tips5;
           }
           else if (o == 2)
           {
               tipsLabel.Text = tips4;
           }
           else if (o == 3)
           {
               tipsLabel.Text = tips3;
           }
           else if (o == 4)
           {
               tipsLabel.Text = tips2;
           }
           else
           {
               tipsLabel.Text = tips1;
           }
       }

       

       
    }
}
