using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Media;
using System.Diagnostics;

namespace RamC
{
    partial class AboutBox1 : Form
    {
        BackgroundWorker bw;
        SoundPlayer sp;
        public AboutBox1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;

            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;

            this.VisibleChanged += AboutBox1_VisibleChanged;
            
        }

        private void AboutBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                try
                {
                    sp = new SoundPlayer(Application.StartupPath + "\\about.wav");
                    sp.Play();
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

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            okButton.Enabled = false;
            WebClient wc = new WebClient();
            try
            {
                okButton.Text = "Cheking internet...";
                var tmp = wc.DownloadString("https://www.google.com");
            }
            catch (Exception)
            {
                okButton.Text = "Check for update";
                okButton.Enabled = true;
                return;
            }
            okButton.Text = "Checking version...";
            string version = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdscWlERzNVY0xveHM");
            string changelog = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsWXEyTzdrM1phOWM");
            
            if (version != Application.ProductVersion)
            {
                if (MessageBox.Show("You are running old version " + Application.ProductVersion + "\nWould you like to download new version " + version + "?\n" + "Changelog: \n" + changelog, "RamC Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsdkJ3VEp2R0dlQm8");
                }
            }
            else
            {
                okButton.Text = "Check for update";
                MessageBox.Show("You are running latest version", "Congratulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            wc.Dispose();
            okButton.Text = "Check for update";
            okButton.Enabled = true;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

       

        private void okButton_Click_1(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
                bw.RunWorkerAsync();
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            Process.Start("https://mini102.wordpress.com/about/");
        }

       

    }
}
