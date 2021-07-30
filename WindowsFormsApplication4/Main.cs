using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using AS;
using Microsoft.Win32;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace WindowsFormsApplication4
{
    public partial class Main : Form
    {
        int setting = 1 * 60 * 1000;
        int count = 0;
        int remain = 0;
        bool started = false;
        bool sd = false;
        bool hn = false;
        bool sl = false;
        bool ns = false;
        bool safe = false;
        BackgroundWorker bw;
        bool check = false;
        string ver = "";
        string changelog = "";
        Remind f;
        RegistryKey r;
        //string password = "";
        bool showed = false;
        bool tick = false;
        bool wait = false;
        bool real = false;
        bool click = false;
        int newx, newy;
        int tfa;
        About ab = new About();
        //SpeechRecognitionEngine speech = new SpeechRecognitionEngine();
        SpeechSynthesizer jarvis = new SpeechSynthesizer();
        //string[] lines = { "Set Mode", "Shutdown", "Hibernate", "Sleep", "Remind", "Safe Mode", "Show Notification", "AutoRun", "Start", "Stop", "1", "2", "3", "4", "5", "6", "7", "8", "9", "minute", "hour", "exit" };
        public Main(int type)
        {
            InitializeComponent();
            //StartPosition = FormStartPosition.CenterScreen;
            //
            //tb.Description = "AS";
            //panel1.Hide();
            panel2.Hide();
            panelShowNoti.Hide();
            checkBox4.Hide();
            //
            //speech.RequestRecognizerUpdate();
            //speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speech_SpeechRecognize);
            //speech.SetInputToDefaultAudioDevice();
            jarvis.SelectVoiceByHints(VoiceGender.Female);
            //LoadGrammar(); 
            //
            //bw = new BackgroundWorker();
            //bw.DoWork += bw_DoWork;
            //bw.RunWorkerAsync();
            progressBar1.Enabled = false;
            progressBar1.Hide();
            f = new Remind();
            if (type == 1)
            {
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                if (r == null)
                    r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\AS\\Data");
                if (r.GetValue("Kid") != null)
                {
                    if ((int)r.GetValue("Kid") == 1)
                    {
                        tick = true;//check = code
                        checkBox3.Checked = true;
                        tick = false;
                        if (r.GetValue("Time") != null)
                        {
                            trackBar1.Value = (int)r.GetValue("Time");
                            if (trackBar1.Value <= 60)
                                label3.Text = trackBar1.Value.ToString() + " min";
                            else
                                label3.Text = (trackBar1.Value / 60).ToString() + "h " + (trackBar1.Value - (60 * (trackBar1.Value / 60))).ToString() + " min";
                            setting = trackBar1.Value * 60 * 1000;
                            progressBar1.Maximum = setting;
                            if (r.GetValue("Rm") != null)
                            {
                                remain = (int)r.GetValue("Rm");
                                if (remain > 0)
                                {
                                    count = setting - remain;
                                    progressBar1.Value = count;
                                }
                            }
                        }
                        else
                        {
                            real = true;
                            panel2.Show();
                            numericUpDown1.Value = (int)r.GetValue("SH");
                            numericUpDown2.Value = (int)r.GetValue("SM");
                        }
                        //
                        if ((int)r.GetValue("Mode") == 1)
                            radioButton1.Checked = true;
                        else if ((int)r.GetValue("Mode") == 2)
                            radioButton2.Checked = true;
                        else
                            radioButton3.Checked = true;

                        if ((int)r.GetValue("Safe") == 1)
                            checkBox1.Checked = true;

                        if ((int)r.GetValue("Sn") == 1)
                            checkBox2.Checked = true;

                       
                        button1_Click(null, null);
                    }
                    else
                    {
                        //SoundPlayer sound = new SoundPlayer(global::AS.Properties.Resources.welcome);
                        //sound.Play();
                    }
                }
                else
                {
                   // SoundPlayer sound = new SoundPlayer(global::AS.Properties.Resources.welcome);
                    //sound.Play();
                }
                r.Close();
                r.Dispose();
                tfa = 1;
            }
            else
            {
                checkBox3.Enabled = false;
                //SoundPlayer sound = new SoundPlayer(global::AS.Properties.Resources.welcome);
                //sound.Play();
                tfa = 2;
            }
            
           
        }
        


        //private void LoadGrammar()
        //{
        //    Choices text = new Choices();
        //    text.Add(lines);
        //    Grammar words = new Grammar(new GrammarBuilder(text));
        //    speech.LoadGrammar(words);
        //}

        /*private void speech_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {  
            string s = e.Result.Text;
            if (s == "Shutdown")
            {
                jarvis.SpeakAsync("Shutdown Mode has been selected!");
                radioButton1.Checked = true;
            }
            if (s == "Hibernate")
            {
                jarvis.SpeakAsync("Hibernate Mode has been selected!");
                radioButton2.Checked = true;
            }
            if (s == "Sleep")
            {
                jarvis.SpeakAsync("Sleep Mode has been selected!");
                radioButton3.Checked = true;
            }
            if (s == "Remind")
            {
                
                radioButton4.Checked = true;
            }
            if (s == "Safe Mode")
            {
                if (checkBox1.Checked != true)
                {
                    jarvis.SpeakAsync("Safe Mode has been selected!");
                    checkBox1.Checked = true;
                }
                else
                {
                    jarvis.SpeakAsync("Safe Mode has been deselected!");
                    checkBox1.Checked = false;
                }
            }
            if (s == "Show Notification")
            {
                if (checkBox2.Checked != true)
                {
                    jarvis.SpeakAsync("Show Notification option has been selected!");
                    checkBox2.Checked = true;
                }
                else
                {
                    jarvis.SpeakAsync("Show Notification option has been deselected!");
                    checkBox2.Checked = false;
                }
            }
            if (s == "AutoRun")
            {
                if (checkBox3.Checked != true)
                {
                    jarvis.SpeakAsync("AutoRun option has been selected!");
                    checkBox3.Checked = true;
                }
                else
                {
                    jarvis.SpeakAsync("AutoRun option has been deselected!");
                    checkBox3.Checked = false;
                }
            }
            if (s == "Start")
            {
                button1_Click(null, null);
            }
            if (s == "Stop")
            {
                button2_Click(null, null);
            }
            if (s == "minute" )
            {
                //MessageBox.Show(s.Substring(0, s.LastIndexOf(" ") - 0), "Info");
            }
            if (s == "exit")
            {
                Application.Exit();
            }
        }*/
        private void Form1_Load(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                BeginInvoke(new MethodInvoker(delegate { Hide(); }));

            }
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\AS\\Data");
            if (r.GetValue("Sn5") != null)
                checkBox5min.Checked = true;
            else
                checkBox5min.Checked = false;
            if (r.GetValue("Sn1") != null)
                checkBox1min.Checked = true;
            else
                checkBox1min.Checked = false;
            if (r.GetValue("Sn30") != null)
                checkBox30sec.Checked = true;
            else
                checkBox30sec.Checked = false;
            r.Close();
            r.Dispose();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
            WebClient wc = new WebClient();
            string tmp1 = "https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsOUd4VVdCUzlva00"; //check version
            string tmp2 = "https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsbC03X0tmU2tneHM";
            Uri url = new Uri(tmp1.Trim());
            //
            //check if internet is connected
            try
            {
                var temp = wc.DownloadString("https://www.google.com");

            }
            catch (WebException)
            {
                return;
            }
            var ui = wc.DownloadString(url);
            //if (ui == "0.0.0.0")
            //{
            //    MessageBox.Show("Sorry! Server has been closed." + "\nTry later!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (ui != Application.ProductVersion)
            {
                changelog = wc.DownloadString(tmp2);
                ver = ui;
                check = true;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (started != true)
            {
                if (sd == true || hn == true || sl == true || ns == true)
                {
                    //this.Hide();
                    timer1.Stop();
                    timer1.Start();
                    if (real != true)
                    {
                        progressBar1.Maximum = setting;
                        progressBar1.Enabled = true;
                        progressBar1.Show();  
                        
                    }
                    else
                    {
                        //numericUpDown1.Enabled = false;
                        //numericUpDown2.Enabled = false;
                        //int h = (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) - DateTime.Now.Hour) * 60 * 60;
                        //int m = (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute) * 60;
                        //int s = (60 - DateTime.Now.Second);
                        //progressBar1.Maximum = (h + m + s) * 1000;
                        ////progressBar1.Maximum = 1000 * ((Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) - DateTime.Now.Hour) * 60 * 60 + (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute) * 60 - DateTime.Now.Second);
                        //progressBar1.Enabled = true;
                        //progressBar1.Show();
                        //MessageBox.Show(progressBar1.Maximum.ToString(), "Info");
                        
                    }
                    started = true;
                    //notifyIcon1.ShowBalloonTip(2000, "Auto Shutdown", "Auto Shutdown is running in background", ToolTipIcon.Info);
                    buttonStart.Text = "Minimize";
                    buttonClose.Text = "Stop";
                    buttonTime.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You must choose mode!?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Hide();
                //jarvis.SpeakAsync("Auto Shutdown is running in background");
                notifyIcon1.ShowBalloonTip(2000, "AS", "AS is running in background", ToolTipIcon.Info);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (started != true)
            {
                if (trackBar1.Value <= 60)
                    label3.Text = trackBar1.Value.ToString() + " min";
                else
                    label3.Text = (trackBar1.Value / 60).ToString() + "h " + (trackBar1.Value - (60 * (trackBar1.Value / 60))).ToString() + " min";
                setting = trackBar1.Value * 60 * 1000;
                progressBar1.Maximum = setting;
            }
            else
            {
                if (trackBar1.Value * 60 * 1000 > count)
                {
                    if (trackBar1.Value <= 60)
                        label3.Text = trackBar1.Value.ToString() + " min";
                    else
                        label3.Text = (trackBar1.Value / 60).ToString() + "h " + (trackBar1.Value - (60 * (trackBar1.Value / 60))).ToString() + " min";
                    setting = trackBar1.Value * 60 * 1000;
                    progressBar1.Maximum = setting;
                }
                else
                {
                    //fix here
                    //button2_Click(null, null);
                    
                    if (trackBar1.Value <= 60)
                        label3.Text = trackBar1.Value.ToString() + " min";
                    else
                        label3.Text = (trackBar1.Value / 60).ToString() + "h " + (trackBar1.Value - (60 * (trackBar1.Value / 60))).ToString() + " min";
                    setting = trackBar1.Value * 60 * 1000;
                    progressBar1.Maximum = setting;
                    
                    MessageBox.Show("Your new time is lower than counted time!" + "\nTimer has been restarted!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    count = 0;
                    progressBar1.Value = 0;
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Show();
                WindowState = FormWindowState.Normal;
            }
            
        }
        //Stop timer
        private void Stop()
        {
            buttonTime.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            if (started == false)
            {
                if (checkBox4.Checked != true)
                    Application.Exit();
                else
                {
                    //panel1.Show();
                    //textBox1.Text = "";
                    //button3.Text = "Ok";
                    //label6.Hide();
                    //textBox2.Hide();
                }
            }
            else
            {
                //
                remain = 0;
                Save();//Save settings to registry
                //
                if (f != null)
                {
                    f.EndSound();
                    f.Dispose();
                    f.Close();
                    f = new Remind();
                }
                timer1.Stop();
                setting = trackBar1.Value * 60 * 1000;
                started = false;
                sd = false;
                hn = false;
                sl = false;
                ns = false;
                //label5.Text = (trackBar1.Value * 60).ToString();
                //label6.Text = "----";
                progressBar1.Hide();
                progressBar1.Value = 0;
                progressBar1.Enabled = false;
                buttonStart.Text = "Start";
                buttonClose.Text = "Exit";
                count = 0;
                notifyIcon1.Text = "Auto Shutdown";

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;

                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                trackBar1.Enabled = true;
                //
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                //

                //
                shutdownToolStripMenuItem.Checked = false;
                hibernateToolStripMenuItem.Checked = false;
                sleepToolStripMenuItem.Checked = false;
                noticeSomethingToolStripMenuItem.Checked = false;
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                click = true;
                //panel1.Show();
                //textBox1.Text = "";
                //button3.Text = "Ok";
                //label6.Hide();
                //textBox2.Hide();
            }
            else
            {
                Stop();
            }
        }
        private void fiveNoti()
        {
            if (sd == true)
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Shutdown)", "5 minutes remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Shut down Mode, 5 minutes remaining");
            }
            else if (hn == true)
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Hibernate)", "5 minutes remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Hibernate Mode, 5 minutes remaining");
            }
            else
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Sleep)", "5 minutes remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Sleep Mode, 5 minutes remaining");
            }
        }
        private void oneNoti()
        {
            if (sd == true)
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Shutdown)", "1 minute remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Shut down Mode, 1 minute remaining");
            }
            else if (hn == true)
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Hibernate)", "1 minute remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Hibernate Mode, 1 minute remaining");
            }
            else
            {
                notifyIcon1.ShowBalloonTip(2000, "AS (Sleep)", "1 minute remaining...", ToolTipIcon.Info);
                jarvis.SpeakAsync("Sleep Mode, 1 minute remaining");
            }
        }
        private void Noti()
        {
            if ((setting - count) / 1000 == 30)
            {
                if (sd == true)
                {
                    notifyIcon1.ShowBalloonTip(2000, "AS (Shutdown)", "30 seconds remaining...", ToolTipIcon.Info);
                    jarvis.SpeakAsync("Shut down Mode, 30 seconds remaining");
                }
                else if (hn == true)
                {
                    notifyIcon1.ShowBalloonTip(2000, "AS (Hibernate)", "30 seconds remaining...", ToolTipIcon.Info);
                    jarvis.SpeakAsync("Hibernate Mode, 30 seconds remaining");
                }
                else
                {
                    notifyIcon1.ShowBalloonTip(2000, "AS (Sleep)", "30 seconds remaining...", ToolTipIcon.Info);
                    jarvis.SpeakAsync("Sleep Mode, 30 seconds remaining");
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (started == true)
            {
                if (real != true)
                {
                    count = count + 1000;
                    remain = setting - count;
                    progressBar1.Maximum = setting;
                    progressBar1.PerformStep();
                   
                    if (safe != true)
                    {
                        notifyIcon1.Text = "AS\n" + "Eslaped Time: " + (count / 1000).ToString() + "/" + (setting / 1000).ToString() + "s\n" + "Safe Mode is Off";
                    }
                    else
                    {
                        notifyIcon1.Text = "AS\n" + "Eslaped Time: " + (count / 1000).ToString() + "/" + (setting / 1000).ToString() + "s\n" + "Safe Mode is On";
                    }
                    if (ns != true && checkBox2.Checked)
                    {
                        if ((setting - count) / 1000 == 300)
                        {
                            if (checkBox5min.Checked)
                                fiveNoti();
                        }
                        if ((setting - count) / 1000 == 60)
                        {
                            if (checkBox1min.Checked)
                                oneNoti();
                        }
                        if ((setting - count) / 1000 == 30)
                        {
                            if (checkBox30sec.Checked)
                                Noti();
                        }
                    }
                    if (count == setting)
                    {
                        if (sd == true)
                        {
                            System.Diagnostics.Process.Start("ShutDown", "/s /hybrid /t 0");
                            Application.Exit();
                        }
                        else if (hn == true)
                        {
                            if (safe == true)
                            {
                                //
                                Application.SetSuspendState(PowerState.Hibernate, false, true);
                            }
                            else
                            {
                                //
                                Application.SetSuspendState(PowerState.Hibernate, true, true);
                            }
                            Application.Exit();
                        }
                        else if (sl == true)
                        {
                            if (safe == true)
                                Application.SetSuspendState(PowerState.Suspend, false, true);
                            else
                                Application.SetSuspendState(PowerState.Suspend, true, true);
                            Application.Exit();
                        }
                        else if (ns == true)
                        {
                            timer1.Stop();
                            f.CloseForm();
                            radioButton1.Enabled = false;
                            radioButton2.Enabled = false;
                            radioButton3.Enabled = false;
                            trackBar1.Enabled = false;
                        }
                    }
                }
                else//real time
                {
                    //progressBar1.Maximum = 1000 * ((Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) - DateTime.Now.Hour) * 60 * 60 + (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute) * 60);
                    
                    //progressBar1.PerformStep();
                    if (safe != true)
                    {
                        notifyIcon1.Text = "AS\n" + "Setting Time: " + Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) + ":" + Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) + "\n" + "Safe Mode is Off";
                    }
                    else
                    {
                        notifyIcon1.Text = "AS\n" + "Setting Time: " + Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) + ":" + Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) + "\n" + "Safe Mode is On";
                    }
                    if (ns != true && checkBox2.Checked)//show noti
                    {
                        //cung h
                        if ((Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == DateTime.Now.Hour) && (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute == 5) && DateTime.Now.Second == 0)
                        {
                            if (checkBox5min.Checked)
                                fiveNoti();
                        }

                        if ((Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == DateTime.Now.Hour) && (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute == 1) && DateTime.Now.Second == 0)
                        {
                            if (checkBox1min.Checked)
                                oneNoti();
                        }

                        if ((Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == DateTime.Now.Hour) && (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) - DateTime.Now.Minute == 1) && DateTime.Now.Second == 30)
                        {
                            if (checkBox30sec.Checked)
                                Noti();
                        }
                        //khac h
                        if (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) != 0 && Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) - DateTime.Now.Hour == 1)//h cai dat khong phai 0
                        {
                            if ((Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) == 0))//8:55 -> 9:00
                            {
                                if ((60 - DateTime.Now.Minute == 5) && DateTime.Now.Second == 0)
                                {
                                    if (checkBox5min.Checked)
                                        fiveNoti();
                                }

                                if ((60 - DateTime.Now.Minute == 1) && DateTime.Now.Second == 0)
                                {
                                    if (checkBox1min.Checked)
                                        oneNoti();
                                }
                            }
                        }
                        else if (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == 0)//khi cai dat thoi gian la 0, truowng hop dac cmn biet
                        {
                            if (24 - DateTime.Now.Hour == 1)
                            {
                                if ((Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) == 0))//8:55 -> 9:00
                                {
                                    if ((60 - DateTime.Now.Minute == 5) && DateTime.Now.Second == 0)
                                    {
                                        if (checkBox5min.Checked)
                                            fiveNoti();
                                    }

                                    if ((60 - DateTime.Now.Minute == 1) && DateTime.Now.Second == 0)
                                    {
                                        if (checkBox1min.Checked)
                                            oneNoti();
                                    }
                                }
                            }
                        }
                    }
                    if (DateTime.Now.Hour == Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) && DateTime.Now.Minute == Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)))
                    {
                        if (sd == true)
                        {
                            System.Diagnostics.Process.Start("ShutDown", "/s /hybrid /t 0");
                            Application.Exit();
                        }
                        else if (hn == true)
                        {
                            if (safe == true)
                            {
                                //
                                Application.SetSuspendState(PowerState.Hibernate, false, true);
                            }
                            else
                            {
                                //
                                Application.SetSuspendState(PowerState.Hibernate, true, true);
                            }
                            Application.Exit();
                        }
                        else if (sl == true)
                        {
                            if (safe == true)
                                Application.SetSuspendState(PowerState.Suspend, false, true);
                            else
                                Application.SetSuspendState(PowerState.Suspend, true, true);
                            Application.Exit();
                        }
                        else if (ns == true)
                        {
                            timer1.Stop();
                            f.CloseForm();
                            radioButton1.Enabled = false;
                            radioButton2.Enabled = false;
                            radioButton3.Enabled = false;
                            trackBar1.Enabled = false;
                        }
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                sd = true;
                sl = false;
                hn = false;
                ns = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = true;
                shutdownToolStripMenuItem.Checked = true;
                hibernateToolStripMenuItem.Checked = false;
                sleepToolStripMenuItem.Checked = false;
                noticeSomethingToolStripMenuItem.Checked = false;
                checkBox3.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                hn = true;
                sd = false;
                sl = false;
                ns = false;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                shutdownToolStripMenuItem.Checked = false;
                hibernateToolStripMenuItem.Checked = true;
                sleepToolStripMenuItem.Checked = false;
                noticeSomethingToolStripMenuItem.Checked = false;
                checkBox3.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                sl = true;
                sd = false;
                hn = false;
                ns = false;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                shutdownToolStripMenuItem.Checked = false;
                hibernateToolStripMenuItem.Checked = false;
                sleepToolStripMenuItem.Checked = true;
                noticeSomethingToolStripMenuItem.Checked = false;
                checkBox3.Enabled = true;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
            //
            if (radioButton4.Checked == true)
            {
                f.Show();
                sl = false;
                sd = false;
                hn = false;
                ns = true;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                shutdownToolStripMenuItem.Checked = false;
                hibernateToolStripMenuItem.Checked = false;
                sleepToolStripMenuItem.Checked = false;
                noticeSomethingToolStripMenuItem.Checked = true;
                checkBox3.Enabled = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ab.Visible != true)
                ab.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
           // button1.Text = "Minimize";
            //button2.Text = "Stop";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //if (bw.IsBusy != true)
            //    bw.RunWorkerAsync();
            if (ab.Visible != true)
                ab.ShowDialog();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                safe = true;
            }
            else
            {
                safe = false;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shutdownToolStripMenuItem.Checked = true;
            hibernateToolStripMenuItem.Checked = false;
            sleepToolStripMenuItem.Checked = false;
            noticeSomethingToolStripMenuItem.Checked = false;
            sd = true;
            sl = false;
            hn = false;
            ns = false;
            radioButton1.Select();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shutdownToolStripMenuItem.Checked = false;
            hibernateToolStripMenuItem.Checked = true;
            sleepToolStripMenuItem.Checked = false;
            noticeSomethingToolStripMenuItem.Checked = false;
            hn = true;
            sl = false;
            sd = false;
            ns = false;
            radioButton2.Select();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shutdownToolStripMenuItem.Checked = false;
            hibernateToolStripMenuItem.Checked = false;
            sleepToolStripMenuItem.Checked = true;
            noticeSomethingToolStripMenuItem.Checked = false;
            sl = true;
            sd = false;
            hn = false;
            ns = false;
            radioButton3.Select();
        }

        private void noticeSomethingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shutdownToolStripMenuItem.Checked = false;
            hibernateToolStripMenuItem.Checked = false;
            sleepToolStripMenuItem.Checked = false;
            noticeSomethingToolStripMenuItem.Checked = true;
            sl = false;
            sd = false;
            hn = false;
            ns = true;
            radioButton4.Select();
            if (f != null)
            {
                f.Show();
            }
            else
            {
                f = new Remind();
                f.Show();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (check == true)
            {
                check = false;
                if (MessageBox.Show("You are running old version! (" +Application.ProductVersion + ")" + "\nWould you like to download new version? (" + ver + ")" + "\n\nChangelog: \n" + changelog, "Auto Shutdown Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsNk9kM0hkS25QR0k");
                    //FolderBrowserDialog fd = new FolderBrowserDialog();
                    //fd.Description = "Select place to save";
                    //fd.RootFolder = Environment.SpecialFolder.MyComputer;
                    //fd.ShowNewFolderButton = true;
                    //if (fd.ShowDialog() == DialogResult.OK)
                    //{
                    //    string path = fd.SelectedPath + "\\AS.rar";
                    //    WebClient nwc = new WebClient();
                    //    string tmp2 = "https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsNk9kM0hkS25QR0k";//new version
                    //    Uri url;
                    //    url = new Uri(tmp2.Trim());
                    //    try
                    //    {
                    //        jarvis.SpeakAsync("Starting download, please do not exit this app");
                    //        nwc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                    //        nwc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    //        nwc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                    //        nwc.DownloadFileAsync(url, @path);
                    //        //this.ControlBox = false;
                    //        this.button2.Enabled = false;
                    //    }
                    //    catch (Exception)
                    //    {
                    //        MessageBox.Show("Oop! Please try later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        //this.ControlBox = true;
                    //        this.button2.Enabled = true;
                    //        return;
                    //    }
                    //}
                   
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (tfa == 1)
                {
                    r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                    r.SetValue("Sn", 1);
                }
                panelShowNoti.Show();
            }
            else
            {
                if (tfa == 1)
                {
                    r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                    r.SetValue("Sn", 0);
                }
                
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                if (tick != true)//kiem tra neu check = code
                {
                    r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                    if (r.GetValue("Pass") != null)
                    {
                        r.Close();
                        r.Dispose();
                        if (MessageBox.Show("Do you want to change your password?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //panel1.Show();
                            //label6.Show();
                            //textBox2.Show();
                            //button3.Text = "Save";
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            showed = true;
                        }
                    }
                    else
                    {
                        //panel1.Show();
                        showed = true;
                    }
                }
            }
            else
            {
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                if (r.GetValue("Pass") != null)
                {
                    //panel1.Show();
                    //textBox1.Text = "";
                    //button3.Text = "Ok";
                    //label6.Hide();
                    //textBox2.Hide();
                }
                r.Close();
                r.Dispose();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
            if (checkBox5min.Checked)
            {
                r.SetValue("Sn5", 1);
            }
            else
            {
                r.DeleteValue("Sn5", false);
            }
            if (checkBox1min.Checked)
            {
                r.SetValue("Sn1", 1);
            }
            else
            {
                r.DeleteValue("Sn1", false);
            }
            if (checkBox30sec.Checked)
            {
                r.SetValue("Sn30", 1);
            }
            else
            {
                r.DeleteValue("Sn30", false);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (checkBox3.Enabled)
            {
                if (checkBox3.Checked)
                {
                    if (remain > 0 || real == true)//real time also works
                    {
                        Process.Start(Application.ExecutablePath);//open new instance if form is closed unexpectedly
                    }
                }
            }
        }
        private void Save()
        {
            if (checkBox3.Enabled)
            {
                if (checkBox3.Checked)
                {
                    if (hn == true || sl == true || sd == true)
                    {
                        r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                        r.SetValue("Kid", 1);
                        if (real != true)
                        {
                            r.SetValue("Time", trackBar1.Value);
                            r.SetValue("Rm", remain);
                            r.DeleteValue("SH", false);
                            r.DeleteValue("SM", false);
                        }
                        else
                        {
                            r.DeleteValue("Time", false);
                            r.DeleteValue("Rm", false);
                            r.SetValue("SH", Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)));
                            r.SetValue("SM", Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)));
                        }
                        if (sd == true)
                            r.SetValue("Mode", 1);
                        else if (sl == true)
                            r.SetValue("Mode", 3);
                        else if (hn == true)
                            r.SetValue("Mode", 2);
                        if (checkBox1.Checked)
                            r.SetValue("Safe", 1);
                        else
                            r.SetValue("Safe", 0);
                        r.Close();
                        r.Dispose();
                        RegistryKey r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        r1.SetValue("AS", Application.ExecutablePath);
                        r1.Close();
                        r.Dispose();
                    }
                }
                else
                {
                    if (hn == true || sl == true || sd == true)
                    {
                        r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                        r.SetValue("Kid", 0);
                        if (real != true)
                        {
                            r.SetValue("Time", trackBar1.Value);
                            r.SetValue("Rm", remain);
                            r.DeleteValue("SH", false);
                            r.DeleteValue("SM", false);
                        }
                        else
                        {
                            r.DeleteValue("Time", false);
                            r.DeleteValue("Rm", false);
                            r.SetValue("SH", Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)));
                            r.SetValue("SM", Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)));
                        }
                        if (sd == true)
                            r.SetValue("Mode", 1);
                        else if (sl == true)
                            r.SetValue("Mode", 3);
                        else if (hn == true)
                            r.SetValue("Mode", 2);
                        if (checkBox1.Checked)
                            r.SetValue("Safe", 1);
                        else
                            r.SetValue("Safe", 0);
                        r.Close();
                        r.Dispose();
                        RegistryKey r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        r1.DeleteValue("AS", false);
                        r1.Close();
                        r.Dispose();
                    }
                }
            }
        } 

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                wait = false;
                if (tick != true)//kiem tra neu check = code
                {
                    r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                    if (r.GetValue("Pass") != null)
                    {
                        r.Close();
                        r.Dispose();
                        if (MessageBox.Show("Do you want to change your password?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //panel1.Show();
                            //label6.Show();
                            //textBox2.Show();
                            //button3.Text = "Save";
                            //textBox1.Text = "";
                            //textBox2.Text = "";
                            showed = true;
                        }
                    }
                    else
                    {
                        //panel1.Show();
                        showed = true;
                    }
                }
            }
            else
            {
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                if (r.GetValue("Pass") != null)
                {
                    //panel1.Show();
                    //textBox1.Text = "";
                    //button3.Text = "Ok";
                    //label6.Hide();
                    //textBox2.Hide();
                }
                r.Close();
                r.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //speech.RecognizeAsync(RecognizeMode.Single);
            if(real == true)
            {
                real = false;
                panel2.Hide();
                buttonTime.Text = "Real Time";
            }
            else
            {
                real = true;
                numericUpDown1.Value = DateTime.Now.Hour;
                if (DateTime.Now.Minute == 59)
                {
                    numericUpDown1.Value = DateTime.Now.Hour + 1;
                    numericUpDown2.Value = 0;
                }
                else
                    numericUpDown2.Value = DateTime.Now.Minute + 1;
                panel2.Show();
                buttonTime.Text = "Raw Time";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 23)
                numericUpDown1.Value = 0;
            if (numericUpDown1.Value < 0)
                numericUpDown1.Value = 23;
            //if (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) < DateTime.Now.Hour)
            //{
            //    numericUpDown1.Value = DateTime.Now.Hour;
            //    //MessageBox.Show("Please choose a valid time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > 59)
                numericUpDown2.Value = 0;
            if (numericUpDown2.Value < 0)
                numericUpDown2.Value = 59;
            //if (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) <= DateTime.Now.Minute && Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == DateTime.Now.Hour)
            //{
            //    if (DateTime.Now.Minute != 59)
            //        numericUpDown2.Value = DateTime.Now.Minute + 1;
            //    else
            //        numericUpDown2.Value = 0;
            //    //MessageBox.Show("Please choose a valid time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            //}
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            //if (Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)) < DateTime.Now.Minute && Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) == DateTime.Now.Hour)
            //    numericUpDown2.Value = DateTime.Now.Minute;
            //if (Convert.ToInt32(Math.Round(numericUpDown1.Value, 0)) < DateTime.Now.Hour)
            //    numericUpDown1.Value = DateTime.Now.Hour;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1min.Checked != true && checkBox5min.Checked != true && checkBox30sec.Checked != true)
                checkBox2.Checked = false;
            panelShowNoti.Hide();
        }

        private void checkBox5min_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5min.Checked != true)
            {
                if (checkBox1min.Checked != true)
                {
                    if (checkBox30sec.Checked != true)
                    {
                        checkBox2.Checked = false;
                        //panelShowNoti.Hide();
                        //MessageBox.Show("Show Notification is now off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
        }

        private void checkBox1min_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1min.Checked != true)
            {
                if (checkBox5min.Checked != true)
                {
                    if (checkBox30sec.Checked != true)
                    {
                        checkBox2.Checked = false;
                        //panelShowNoti.Hide();
                        //MessageBox.Show("Show Notification is now off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void checkBox30sec_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30sec.Checked != true)
            {
                if (checkBox5min.Checked != true)
                {
                    if (checkBox1min.Checked != true)
                    {
                        checkBox2.Checked = false;
                        //panelShowNoti.Hide();
                        //MessageBox.Show("Show Notification is now off!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
   
    }
}
