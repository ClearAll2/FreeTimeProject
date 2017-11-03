using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form2 : Form
    {
        int a = 0;
        int b = 0;
        int result = 0;
        int c = 0;
        int d = 0;
        int t = 2000;
        int score = 0;
        string s = "";
        string sa = "";
        string sb = "";
        string sc = "";
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            //
            progressBar1.PerformStep();
            Random rd = new Random();
            a = rd.Next(0, 9);
            b = rd.Next(1, 9);
            c = rd.Next(0, 4);
            if (c == 0)
            {
                s = " + ";
                result = a + b;
                d = result - rd.Next(0, 3);
            }
            else if (c == 1)
            {
                s = " - ";
                result = a - b;
                d = result + rd.Next(0, 3);
            }
            else if (c == 2)
            {
                s = " x ";
                result = a * b;
                d = result - rd.Next(0, 3);
            }
            else
            {
                s = " / ";
                result = a / b;
                d = result + rd.Next(0, 3);
            }
            sa = a.ToString();
            sb = b.ToString();
            c = rd.Next(0, 3);
            if (c == 0)
            {
                sc = result.ToString();
               
            }
            else if (c == 2)
            {
                sc = d.ToString();
                
            }
            else
            {
                sc = d.ToString();
               
            }
            label1.Text = sa + s + sb + " = " + sc;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t = t - 100;
            label2.Text = (t/100).ToString();
            label3.Text = score.ToString();
            progressBar1.PerformStep();
            if (t == 0)
            {
                timer1.Stop();
                MessageBox.Show("Your score: " + score, "Info");
                this.Hide();
                Form1 f = new Form1();
                this.Close();
                f.ShowDialog();
                score = 0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (sc == result.ToString())
            {
                score += 1;
                timer1.Stop();
                t = 2000;
                timer1.Start();
                progressBar1.Value = 0;
                Random rd = new Random();
                a = rd.Next(0, 9);
                b = rd.Next(1, 9);
                c = rd.Next(0, 4);
                if (c == 0)
                {
                    s = " + ";
                    result = a + b;
                    d = result - rd.Next(0, 3);
                }
                else if (c == 1)
                {
                    s = " - ";
                    result = a - b;
                    d = result + rd.Next(0, 3);
                }
                else if (c == 2)
                {
                    s = " x ";
                    result = a * b;
                    d = result - rd.Next(0, 3);
                }
                else
                {
                    s = " / ";
                    result = a / b;
                    d = result + rd.Next(0, 3);
                }
                sa = a.ToString();
                sb = b.ToString();
                c = rd.Next(0, 3);
                if (c == 0)
                {
                    sc = result.ToString();
                    
                }
                else if (c == 2)
                {
                    sc = d.ToString();
                   
                }
                else
                {
                    sc = d.ToString();
                   
                }
                label1.Text = sa + s + sb + " = " + sc;
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Your score: " + score, "Info");
                this.Hide();
                Form1 f = new Form1();
                this.Close();
                f.ShowDialog();
                score = 0;
                progressBar1.Value = 0;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (sc == d.ToString())
            {
                score += 1;
                timer1.Stop();
                t = 2000;
                timer1.Start();
                progressBar1.Value = 0;
                Random rd = new Random();
                a = rd.Next(0, 9);
                b = rd.Next(1, 9);
                c = rd.Next(0, 4);
                if (c == 0)
                {
                    s = " + ";
                    result = a + b;
                    d = result - rd.Next(0, 3);
                }
                else if (c == 1)
                {
                    s = " - ";
                    result = a - b;
                    d = result + rd.Next(0, 3);
                }
                else if (c == 2)
                {
                    s = " x ";
                    result = a * b;
                    d = result - rd.Next(0, 3);
                }
                else
                {
                    s = " / ";
                    result = a / b;
                    d = result + rd.Next(0, 3);
                }
                sa = a.ToString();
                sb = b.ToString();
                c = rd.Next(0, 3);
                if (c == 0)
                {
                    sc = result.ToString();
                   
                }
                else if (c == 2)
                {
                    sc = d.ToString();
                  
                }
                else
                {
                    sc = d.ToString();
                   
                }
                label1.Text = sa + s + sb + " = " + sc;
            }
            else
            {
                
                timer1.Stop();
                MessageBox.Show("Your score: " + score, "Info");
                this.Hide();
                Form1 f = new Form1();
                this.Close();
                f.ShowDialog();
                score = 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
