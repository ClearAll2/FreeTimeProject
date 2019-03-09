using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace AS
{
    public partial class Form2 : Form
    {
        string text = "";
        int count = 0;
        int newx, newy;
        SoundPlayer sound;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            button1.Enabled = false;
            textBox1.Text = "Write here...";
            button3.Hide();
            button4.Hide();
            numericUpDown1.Hide();
            sound = new SoundPlayer(global::AS.Properties.Resources.chat_inbound);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.ReadOnly != true)
            {
                this.Hide();
                if (textBox1.Text != "Write here...")
                    text = textBox1.Text;
                else
                    button1.Enabled = false;
            }
            else
            {
                if (textBox1.Text.Contains("//shutdown"))
                {
                    if (MessageBox.Show("Shut down now?", "//shutdown code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start("ShutDown", "/s /hybrid /t 0");
                }
                if (textBox1.Text.Contains("//hibernate"))
                {
                     if (MessageBox.Show("Hibernate now?", "//hibernate code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                         Application.SetSuspendState(PowerState.Hibernate, true, true);
                }
                if (textBox1.Text.Contains("//sleep"))
                {
                    if (MessageBox.Show("Sleep now?", "//sleep code", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                }
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (text.Length > 0)
                textBox1.Text = text;
            else
            {
                textBox1.Text = "Write here...";
                textBox1.Focus();
                textBox1.SelectAll();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox1.Text != "Write here...")
            {
                if (button4.ImageIndex != 1)
                    button1.Enabled = true;
            }
            else
            {
                if (button4.ImageIndex != 1)
                    button1.Enabled = false;  
            }

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Write here...")
            { 
                textBox1.Text = "";
            }
        }

        public void CloseForm()
        {
            if (text.Length > 0)
            {
                sound.PlayLooping();
                textBox1.ReadOnly = true;
                textBox1.Font = new Font(FontFamily.GenericSansSerif, 14.00F, FontStyle.Bold);
                button2.Hide();
                button3.Show();
                button4.Show();
                button1.Text = "Ok and Exit";
                numericUpDown1.Show();
                this.Show();
                this.Focus();
            }
            else
            {
                textBox1.Text = "!?????????????????????????????????????????????????????????????????????????/???????????????????????????????????????????????????????????????";
                sound.PlayLooping();
                textBox1.ReadOnly = true;
                textBox1.Font = new Font(FontFamily.GenericSansSerif, 14.00F, FontStyle.Bold);
                button2.Hide();
                button3.Show();
                button1.Text = "Ok and Exit";
                numericUpDown1.Show();
                this.Show();
                this.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sound.Stop();
            timer1.Enabled = true;
            timer1.Start();
            count = 0;
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count = count + 1;
            if (count == numericUpDown1.Value * 60)
            {
                sound.PlayLooping();
                timer1.Stop();
                timer1.Enabled = false;
                textBox1.ReadOnly = true;
                textBox1.Font = new Font(FontFamily.GenericSansSerif, 14.00F, FontStyle.Bold);
                button2.Hide();
                button3.Show();
                button4.Show();
                numericUpDown1.Show();
                this.Show();
                this.Focus();
                return;
            }
        }

        public void EndSound()
        {
            sound.Dispose();
            sound.Stop();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 2)
                button3.Text = "More " + numericUpDown1.Value + " minute";
            else
                button3.Text = "More " + numericUpDown1.Value + " minutes";
            if (numericUpDown1.Value > 60)
                numericUpDown1.Value = 60;
            if (numericUpDown1.Value < 1)
                numericUpDown1.Value = 1;
               
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.ImageIndex != 1)
            {
                
                textBox1.ReadOnly = false;
                button4.ImageIndex = 1;
                button3.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                
                textBox1.ReadOnly = true;
                button4.ImageIndex = 0;
                button3.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }
    }
}
