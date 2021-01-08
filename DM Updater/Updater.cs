using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace DM_Updater
{
    public partial class Updater : Form
    {
        private BackgroundWorker bw;
        public Updater()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (System.IO.File.Exists(Path.Combine(Application.StartupPath, "DM.exe")))
                labellocalVer.Text = GetProductVersion(Path.Combine(Application.StartupPath, "DM.exe"));
            else
                labellocalVer.Text = "N/A";
            WebClient wc = new WebClient();
            labellatestVer.Text = wc.DownloadString("https://download-cas.000webhostapp.com/download/DM/version");//check version
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
        again:
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadCompleted);
                wc.DownloadFileAsync(new Uri("https://download-cas.000webhostapp.com/download/DM/Release.zip"), Application.StartupPath + "\\Release.zip");
                wc.Dispose();
            }
            catch
            {
                goto again;
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ZipArchive zipArchive = ZipFile.OpenRead(Application.StartupPath + @"\Release.zip");
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                entry.ExtractToFile(System.IO.Path.Combine(Application.StartupPath, entry.Name), true);
            }
            zipArchive.Dispose();
            if (checkBoxAutoOpen.Checked)
                Process.Start(Application.StartupPath + "\\DM.exe");
            System.IO.File.Delete(Application.StartupPath + "\\Release.zip");
            Application.Exit();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            if (Program.FileName != "")
            {
                panelFront.Hide();
                bw.RunWorkerAsync(Program.FileName);
            }
            
        }

        private string GetProductVersion(string s)
        {
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(s);
            return myFileVersionInfo.ProductVersion;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                panelFront.Hide();
                bw.RunWorkerAsync(Program.FileName);
            }
        }
    }
}
