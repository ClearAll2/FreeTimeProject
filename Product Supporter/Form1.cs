using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;

namespace Product_Supporter
{
    public partial class Form1 : Form
    {
        RegistryKey r1;
        BackgroundWorker bw;
        BackgroundWorker bw2;
        string tmp;
        string changelog;
        bool check = false;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;
            panel1.Hide();
            panel2.Hide();
        }
        //Auto Shutdown updater
        private void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
        //Destop Magic updater
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            button3.Enabled = false;
            button3.Text = "Connecting...";
            WebClient wc = new WebClient();
            string tmp1 = "https://googledrive.com/host/0B-QP4eT8oLdsYnZpV1NLcFFQcGs/version.txt"; //check version

            Uri url = new Uri(tmp1.Trim());
            //
            //check if internet is connected
            try
            {
                var temp = wc.DownloadString("https://www.google.com");

            }
            catch (WebException)
            {
                wc.Dispose();
                button3.Text = "Update Desktop Magic";
                MessageBox.Show("Cannot connect to server.\nPlease make sure you have a valid internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ui = wc.DownloadString(url);
            button3.Text = "Checking version...";
            if (ui != Application.ProductVersion)
            {
                changelog = wc.DownloadString("https://googledrive.com/host/0B-QP4eT8oLdsYnZpV1NLcFFQcGs/changelog.txt");
                wc.Dispose();
                tmp = ui;
                check = true;
                button3.Enabled = true;
                button3.Text = "Update Desktop Magic";
            }
            else
            {
                timer1.Stop();
                button3.Text = "Update Desktop Magic";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "Please Wait...";
            button2.Enabled = false;
            //Desktop Magic
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r1 == null)
                return;
            //delete value key
            r1.DeleteValue("DM2", false);
            r1.DeleteValue("DM3", false);
            r1.DeleteValue("DM4", false);
            r1.DeleteValue("DM5", false);
            r1.DeleteValue("DMT", false);
            r1.DeleteValue("Link", false);
            r1.DeleteValue("CustomFile", false);
            r1.Close();
            //delete run at startup
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            r1.DeleteValue("Desktop_Magic", false);
            r1.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                timer1.Start();
                bw.RunWorkerAsync();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check == true)
            {
                timer1.Stop();        
                check = false;
                if (MessageBox.Show("A new version is available v" + tmp + "\nWould you like to download now?" + "\n\nChangelog: \n" + changelog, "New Version " + tmp, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FolderBrowserDialog fd = new FolderBrowserDialog();
                    fd.Description = "Select place to save";
                    fd.RootFolder = Environment.SpecialFolder.MyComputer;
                    fd.ShowNewFolderButton = true;
                   if (fd.ShowDialog() == DialogResult.OK)
                   {
                       string path = fd.SelectedPath + "\\DesktopMagic.rar";
                       WebClient nwc = new WebClient();
                       string tmp2 = "https://googledrive.com/host/0B-QP4eT8oLdsYnZpV1NLcFFQcGs/DesktopMagic.rar";//new version
                       Uri url;
                       url = new Uri(tmp2.Trim());
                       try
                       {
                           nwc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
                           nwc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                           nwc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                           nwc.DownloadFileAsync(url, @path);
                           panel1.Show();
                           panel2.Show();
                       }
                       catch (Exception)
                       {
                           MessageBox.Show("Oop! Please try later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           Application.Exit();
                       }
                   }
                }
            }
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel1.Hide();
            panel2.Hide();
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

       
    }
}
