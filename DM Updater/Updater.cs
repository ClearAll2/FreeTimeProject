using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace DM_Updater
{
    public partial class Updater : Form
    {
        BackgroundWorker bw;
        public Updater()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
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
            Process.Start(Application.StartupPath + "\\DM.exe");
            System.IO.File.Delete(Application.StartupPath + "\\Release.zip");
            Application.Exit();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            if (Program.FileName != "")
            {
                bw.RunWorkerAsync(Program.FileName);
            }
            else
            {
                MessageBox.Show("This app can't be opened manually!", "Manual? No no", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }
    }
}
