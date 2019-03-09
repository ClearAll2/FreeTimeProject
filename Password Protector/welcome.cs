using System;
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
            label6.Text = Application.ProductVersion;
           

        }

        private void welcome_VisibleChanged_1(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                
                fm1 = new Form1();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit?", "Quit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void welcome_Load(object sender, EventArgs e)
        {
            //
            //BeginInvoke(new MethodInvoker(delegate { Hide(); }));
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fm1 != null)
            {
                Thread.Sleep(1200);
                label5.Text = "Finishing...";
                
                fm1.Show();
                this.Hide();
                
                timer1.Enabled = false;
                timer1.Dispose();
            }
        }

        
    }
}
