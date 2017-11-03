using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace RamC
{
    public partial class Form3 : Form
    {
        SpeechSynthesizer voice = new SpeechSynthesizer();
        string query1 = "https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='";  
        string query2 = "') and u='c'&format=xml";
        string query3 = "') and u='f'&format=xml";
        string fullquery = String.Empty;
        string location = String.Empty;
        int newx;
        int newy;
        RegistryKey r;
        BackgroundWorker bw;
        BackgroundWorker bw2;
        BackgroundWorker bw3;
        bool useVoice = false;
        public Form3()
        {
            InitializeComponent();
            voice.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            
            //voice.SelectVoice("Microsoft Server Speech Text to Speech Voice (en-US, Helen)");
            panel1.Hide();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r.GetValue("Top3") != null)
            {
                this.Top = (int)r.GetValue("Top3");
                this.Left = (int)r.GetValue("Left3");
               
            }
            if (r.GetValue("Loc") != null)
            {
                location = (string)r.GetValue("Loc");
                textBox1.Text = location;
            }
            else
            {
                location = textBox1.Text;
            }
            if (r.GetValue("Rt") != null)
            {
                int tmp = (int)r.GetValue("Rt");
                numericUpDown1.Value = tmp;
            }
            if (r.GetValue("useVoice") != null)
            {
                useVoice = true;
                label18.Text = "ON";
                label18.ForeColor = Color.Red;
                r.SetValue("useVoice", true);
            }
            else
            {
                useVoice = false;
                label18.Text = "OFF";
                label18.ForeColor = Color.Black;
                r.DeleteValue("useVoice", false);
            }
           
            
            r.Close();
            r.Dispose();


            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;
            //bw2.RunWorkerAsync();


            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();

            bw3 = new BackgroundWorker();
            bw3.DoWork += bw3_DoWork;
            

        }

        private void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            voice.Speak("Conditions for " + location);
            Thread.Sleep(200);
            voice.Speak("right now it's "+ label5.Text + " and " + label1.Text);
            Thread.Sleep(200);
            voice.Speak("the higher is" + label3.Text + " and the lower is " + label2.Text);
            voice.Speak("the humidity is " + humidLabel.Text);
        }

        

        //locate
        void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            
            WebClient wc = new WebClient();
            string loc = String.Empty;
            try
            {
                sttLabel.Text = "Checking Internet";
                wc.DownloadString("https://www.google.com");
               
            }
            catch
            {
                sttLabel.Text = "No Internet";
                MessageBox.Show("Make sure you have a valid internet connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    var data = wc.DownloadString("http://www.freegeoip.net/xml/");
            //    for (int i = data.IndexOf("<City>") + 6; i < data.Length; i++)
            //    {
            //        loc += data[i];
            //        if (data[i + 1] == '<')
            //            break;
            //    }
            //    textBox1.Text = loc;
            //    textBox1.Text = loc;
            //    MessageBox.Show("You are in \"" + loc + "\"!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch
            //{
            //    MessageBox.Show("Error!? Please try again!", "Fail to detect location", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            string externalip = String.Empty;
            try
            {
                sttLabel.Text = "Checking IP";
                externalip = new WebClient().DownloadString("http://icanhazip.com");
            }
            catch
            {
                sttLabel.Text = "Error in getting ip";
                MessageBox.Show("Error!? Please try again!", "Fail to detect location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wc.Dispose();
                return;
            }
            try
            {
                sttLabel.Text = "Checking location";
                string api = "http://api.db-ip.com/v2/9e8119470065bc5ebc1cbe158336560e8cb71b35/";
                api += externalip;
                var data = wc.DownloadString(api);
                for (int i = data.IndexOf("\"city\": \"") + 9; i < data.Length; i++)
                {
                    loc += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
            }
            catch
            {
                sttLabel.Text = "Fail to locate your location";
                MessageBox.Show("Error!? Please try again!", "Fail to detect location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                wc.Dispose();
                return;
            }
            sttLabel.Text = "Done";
            textBox1.Text = loc;
            MessageBox.Show("You are in \"" + loc + "\"!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            wc.Dispose();
            
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                WebClient wc = new WebClient();
                while (true)
                {
                    try
                    {
                        wc.DownloadString("https://www.google.com");
                    }
                    catch
                    {
                        label3.Text = "0";
                        label2.Text = "0";
                        label5.Text = "Internet unavailabe";
                        Thread.Sleep(5000);
                    }
                    break;
                }
                GetWeatherData2();
                if (useVoice == true && this.Visible == true)
                {
                    if (!bw3.IsBusy)
                    {
                        bw3.RunWorkerAsync();
                    }
                }
                wc.Dispose();
                Thread.Sleep((int)numericUpDown1.Value*60*1000);
            }
        }

        public void GetWeatherData()
        {
            try
            { 
                WebClient wc = new WebClient();
                string data = String.Empty;
                if (location != String.Empty)
                {
                    if (radioButton1.Checked)
                        fullquery = query1 + location + query2;
                    else
                        fullquery = query1 + location + query3;
                    data = wc.DownloadString(fullquery);
                }
                else
                {
                    data = wc.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text=%27ho%20chi%20minh%27)%20and%20u=%27c%27&format=xml");
                }


                string hr = String.Empty;
                for (int i=data.IndexOf("high=\"")+6;i<data.Length;i++)
                {
                    hr += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }

                string lwr = String.Empty;// = data.Substring(data.IndexOf("low=\"") + 5, 2);
                for (int i = data.IndexOf("low=\"") + 5; i < data.Length; i++)
                {
                    lwr += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }

                
                label3.Text = hr + "\xB0";          //+for superscript
                label2.Text = lwr + "\xB0";
                

                string cdt = String.Empty;
                string time = String.Empty;

                for (int i=data.IndexOf("text=\"")+6;i<data.Length;i++)
                {
                    cdt += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                string crrt = data.Substring(data.IndexOf("temp=\"") + 6, 2);

                for (int i=data.IndexOf("Conditions");i<data.Length;i++)
                {
                    time += data[i];
                    if (data[i + 1] == '<')
                        break;
                }

                string humidity = String.Empty;
                for (int i = data.IndexOf("humidity=\"") + 10; i < data.Length; i++)
                {
                    humidity += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                humidLabel.Text = humidity + "%";

                string sunrise = String.Empty;
                for (int i = data.IndexOf("sunrise=\"") + 9; i < data.Length; i++)
                {
                    sunrise += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                sriseLabel.Text = sunrise;

                string sunset = String.Empty;
                for (int i = data.IndexOf("sunset=\"") + 8; i < data.Length; i++)
                {
                    sunset += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                ssetLabel.Text = sunset;


                if (cdt.Contains("Partly Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.partly_cloudy;
                else if (cdt.Contains("Partly Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.Partly_Sunny;
                else if (cdt.Contains("Rainy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.rainy;
                else if (cdt.Contains("Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.cloudy;
                else if (cdt.Contains("Thunderstorms"))
                    this.BackgroundImage = global::RamC.Properties.Resources.ThunderStorm;
                else if (cdt.Contains("Mostly Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.mostly_cloudy;
                else if (cdt.Contains("Mostly Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.mostly_sunny;
                else if (cdt.Contains("Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.sunny;
                else if (cdt.Contains("Rain"))
                    this.BackgroundImage = global::RamC.Properties.Resources.rain;
                else if (cdt.Contains("Shower"))
                    this.BackgroundImage = global::RamC.Properties.Resources.shower;
                else if (cdt.Contains("Clear"))
                    this.BackgroundImage = global::RamC.Properties.Resources.clear;
                else if (cdt.Contains("Breezy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.breezy;
                else
                    this.BackgroundImage = global::RamC.Properties.Resources.main;

                label4.Text = time;
               
                label1.Text = crrt + "\xB0";
                
                label5.Text = cdt;


                wc.Dispose();
            }
            catch(Exception)
            {
                MessageBox.Show("Something is not right!? Please try later!", "Fail to refresh data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //not show error
        public void GetWeatherData2()
        {
            try
            {
                WebClient wc = new WebClient();
                string data = String.Empty;
                if (location != String.Empty)
                {
                    if (radioButton1.Checked)
                        fullquery = query1 + location + query2;
                    else
                        fullquery = query1 + location + query3;
                    data = wc.DownloadString(fullquery);
                }
                else
                {
                    data = wc.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text=%27ho%20chi%20minh%27)%20and%20u=%27c%27&format=xml");
                }


                string hr = data.Substring(data.IndexOf("high=\"") + 6, 2);
                string lwr = data.Substring(data.IndexOf("low=\"") + 5, 2);

                label3.Text = hr + "\xB0";          //+for superscript
                label2.Text = lwr + "\xB0";

                string cdt = String.Empty;
                string time = String.Empty;

                for (int i = data.IndexOf("text=\"") + 6; i < data.Length; i++)
                {
                    cdt += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                string crrt = data.Substring(data.IndexOf("temp=\"") + 6, 2);

                for (int i = data.IndexOf("Conditions"); i < data.Length; i++)
                {
                    time += data[i];
                    if (data[i + 1] == '<')
                        break;
                }

                string humidity = String.Empty;
                for (int i = data.IndexOf("humidity=\"") + 10; i < data.Length; i++)
                {
                    humidity += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                humidLabel.Text = humidity + "%";

                string sunrise = String.Empty;
                for (int i = data.IndexOf("sunrise=\"") + 9; i < data.Length; i++)
                {
                    sunrise += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                sriseLabel.Text = sunrise;

                string sunset = String.Empty;
                for (int i = data.IndexOf("sunset=\"") + 8; i < data.Length; i++)
                {
                    sunset += data[i];
                    if (data[i + 1] == '\"')
                        break;
                }
                ssetLabel.Text = sunset;

                if (cdt.Contains("Partly Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.partly_cloudy;
                else if (cdt.Contains("Partly Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.Partly_Sunny;
                else if (cdt.Contains("Rainy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.rainy;
                else if (cdt.Contains("Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.cloudy;
                else if (cdt.Contains("Thunderstorms"))
                    this.BackgroundImage = global::RamC.Properties.Resources.ThunderStorm;
                else if (cdt.Contains("Mostly Cloudy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.mostly_cloudy;
                else if (cdt.Contains("Mostly Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.mostly_sunny;
                else if (cdt.Contains("Sunny"))
                    this.BackgroundImage = global::RamC.Properties.Resources.sunny;
                else if (cdt.Contains("Rain"))
                    this.BackgroundImage = global::RamC.Properties.Resources.rain;
                else if (cdt.Contains("Shower"))
                    this.BackgroundImage = global::RamC.Properties.Resources.shower;
                else if (cdt.Contains("Clear"))
                    this.BackgroundImage = global::RamC.Properties.Resources.clear;
                else if (cdt.Contains("Breezy"))
                    this.BackgroundImage = global::RamC.Properties.Resources.breezy;
                else
                    this.BackgroundImage = global::RamC.Properties.Resources.main;

                label4.Text = time;
                label1.Text = crrt + "\xB0";
                label5.Text = cdt;

                
                wc.Dispose();
            }
            catch (Exception)
            {
                label3.Text = "N/A";         
                label2.Text = "N/A";
                label4.Text = "No internet connection";
                label1.Text = "N/A";
                label5.Text = "N/A";
                return;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        const int GWL_HWNDPARENT = -8;

        private void Form3_Load(object sender, EventArgs e)
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

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }

        

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
           
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            GetWeatherData();
            Cursor.Current = System.Windows.Forms.Cursors.Default;
            if (useVoice == true)
            {
                if (!bw3.IsBusy)
                {
                    bw3.RunWorkerAsync();
                }
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            r.SetValue("Top3", this.Top);
            r.SetValue("Left3", this.Left);
            r.SetValue("Loc", textBox1.Text);
            r.SetValue("Rt", (int)numericUpDown1.Value);
            r.Close();
            voice.Dispose();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            location = textBox1.Text;
            GetWeatherData();
            
            panel1.Hide();
            if (useVoice == true)
            {
                if (!bw3.IsBusy)
                {
                    bw3.RunWorkerAsync();
                }
            }
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (bw2.IsBusy != true)
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                bw2.RunWorkerAsync();
                Cursor.Current = System.Windows.Forms.Cursors.Default;

            }
            else
            {
                MessageBox.Show("It's detecting your location, please wait!", "Doing work...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        

        private void Form3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                Process.Start("https://www.yahoo.com/news/weather/");
        }

        private void label18_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (useVoice != true)
            {
                useVoice = true;
                label18.Text = "ON";
                label18.ForeColor = Color.Red;
                r.SetValue("useVoice", true);
                
            }
            else
            {
                useVoice = false;
                label18.Text = "OFF";
                label18.ForeColor = Color.Black;
                r.DeleteValue("useVoice", false);
            }
            r.Close();
            r.Dispose();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Hide();

        }

    }
}
