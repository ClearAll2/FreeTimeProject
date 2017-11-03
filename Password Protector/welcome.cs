using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RamC
{
    public partial class welcome : Form
    {
        Form1 fm1;
        public welcome()
        {
            InitializeComponent();
            //fm1 = new Form1();
            
            
        }


        private void label3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void welcome_Load(object sender, EventArgs e)
        {
            //fm1.Show();
            //BeginInvoke(new MethodInvoker(delegate { Hide(); }));
            
        }
    }
}
