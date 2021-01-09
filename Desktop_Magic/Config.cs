using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace DM
{
    public partial class Config : Form
    {
        private RegistryKey r;
        public Config(Color color)
        {
            InitializeComponent();
            timecheckBox.Hide();
            CheckConfig();
            button1.ForeColor = color;
            button2.ForeColor = color;
        }

        private void CheckConfig()
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r.GetValue("TTC") != null)
            {
                int tmp;
                tmp = (int)r.GetValue("TTC");
                if (tmp > 0)
                {
                    numericUpDown4.Value = tmp;
                }
                else
                {
                    numericUpDown4.Enabled = false;
                    timecheckBox.Checked = false;
                }

                tmp = (int)r.GetValue("Speed");
                if (tmp > 0)
                {
                    numericUpDown3.Value = tmp;
                }
                else
                {
                    numericUpDown3.Enabled = false;
                    speedcheckBox.Checked = false;
                }

                tmp = (int)r.GetValue("Direction");
                if (tmp != -9999)
                {
                    numericUpDown2.Value = tmp;
                }
                else
                {
                    numericUpDown2.Enabled = false;
                    directioncheckBox.Checked = false;
                }

                tmp = (int)r.GetValue("Number");
                if (tmp > 0)
                {
                    numericUpDown1.Value = tmp;
                }
                else
                {
                    numericUpDown1.Enabled = false;
                    numbercheckBox.Checked = false;
                }

                tmp = (int)r.GetValue("RandomType");
                if (tmp == 0)
                {
                    TypecheckBox.Checked = true;
                }
                else
                {
                    TypecheckBox.Checked = false;
                }
            }
            r.Close();
            r.Dispose();
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 1)
                numericUpDown1.Value = 1;
            if (numericUpDown1.Value > 40)
                numericUpDown1.Value = 40;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < -10)
                numericUpDown2.Value = -10;
            if (numericUpDown2.Value > 10)
                numericUpDown2.Value = 10;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value < 1)
                numericUpDown3.Value = 1;
            if (numericUpDown3.Value > 15)
                numericUpDown3.Value = 15;
        
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value < 1)
                numericUpDown4.Value = 1;
            if (numericUpDown4.Value > 1440)
                numericUpDown4.Value = 1440;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            
            if (timecheckBox.Checked)
                r.SetValue("TTC", (int)numericUpDown4.Value);
            else
            {
                r.SetValue("TTC", -9999);
            }
            if (speedcheckBox.Checked)
                r.SetValue("Speed", (int)numericUpDown3.Value);
            else
            {
                r.SetValue("Speed", -9999);
            }
            if (directioncheckBox.Checked)
                r.SetValue("Direction", (int)numericUpDown2.Value);
            else
            {
                r.SetValue("Direction", -9999);
            }
            if (numbercheckBox.Checked)
                r.SetValue("Number", (int)numericUpDown1.Value);
            else
            {
                r.SetValue("Number", -9999);
            }
            if (TypecheckBox.Checked)
            {
                r.SetValue("RandomType", 0);
            }
            else
            {
                r.SetValue("RandomType", 1);
            }
            r.Close();
            r.Dispose();
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void numbercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (numbercheckBox.Checked)
                numericUpDown1.Enabled = true;
            else
                numericUpDown1.Enabled = false;
        }

        private void directioncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (directioncheckBox.Checked)
                numericUpDown2.Enabled = true;
            else
                numericUpDown2.Enabled = false;
        }

        private void speedcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (speedcheckBox.Checked)
                numericUpDown3.Enabled = true;
            else
                numericUpDown3.Enabled = false;
        }

        private void timecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (timecheckBox.Checked)
                numericUpDown4.Enabled = true;
            else
                numericUpDown4.Enabled = false;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

      

    }
}
