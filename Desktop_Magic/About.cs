using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DM
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", "Desktop Magic");
            labelProductName.Text = Application.ProductName;
            labelVersion.Text = String.Format("Version {0}", Application.ProductVersion);
            labelCopyright.Text = "Copyright © 2016 - 2022";
            labelCompanyName.Text = Application.CompanyName;
            buttonOK.Select();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
