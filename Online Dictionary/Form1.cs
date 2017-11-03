using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using SpeechLib;

namespace Online_Dictionary
{
    public partial class Form1 : Form
    {
        List<string> data = new List<string>();
        BackgroundWorker bw;
        bool ok = false;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            btnSearch.Enabled = false;
            btnSpeak.Enabled = false;
            //textBox1.Focus();
            textBox1.Select();
            textBox2.ScrollBars = ScrollBars.Both;
            bw = new BackgroundWorker();
            bw.DoWork +=bw_DoWork;
            bw.RunWorkerAsync();
            
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            FileStream fs = File.Open("Database.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while(sr.EndOfStream!= true)
                data.Add(sr.ReadLine());
            sr.Close();
            fs.Close();
            fs.Dispose();
            sr.Dispose();
            //MessageBox.Show("Every thing is now ok!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            for (int i=0;i<data.Count;i++)
            {
                if (data[i].Contains(textBox1.Text))
                {
                    textBox2.Text = data[i];
                    ok = true;
                    return;
                }
            }
            ok = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                btnSearch.Enabled = true;
                btnSpeak.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = false;
                btnSpeak.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
            if (ok == true)
            {
                SpVoice voice = new SpVoice();
                voice.Speak(textBox1.Text, SpeechVoiceSpeakFlags.SVSFDefault);
                
            }
            
        }

       
    }
}
