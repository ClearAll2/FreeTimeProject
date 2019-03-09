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
using System.Net.Http;
using System.Net.Sockets;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Donwloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            textBox1.Select();
            textBox1.Focus();
            label2.Text = "GLink" + Application.ProductVersion + "\nCopyright ©  2017\n Clear All Soft";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://drive.google.com/uc?export=download&id=";
            string url2 = "https://dl.dropboxusercontent.com/";
            string url3 = "";
            string url4 = "https://www.fshare.vn/download/getfs_csrf=";
            string cut;
            if (textBox1.Text.Contains("drive.google.com/") )
            {
               
                try
                {
                        cut = textBox1.Text.Substring(textBox1.Text.LastIndexOf("d/")+2, textBox1.Text.LastIndexOf("/") - textBox1.Text.LastIndexOf("d/")-2);
                }
                catch(Exception)
                {
                    textBox2.Text = "";
                    return;  
                }
                url = url + cut;
                textBox2.Text = url;
               
            }
            else if (textBox1.Text.Contains("www.dropbox.com/"))
            {
                if (textBox1.Text.IndexOf("s/") != -1)
                {
                    try
                    {
                        cut = textBox1.Text.Substring(textBox1.Text.LastIndexOf("s/"));
                        url2 = url2 + cut;
                        textBox2.Text = url2;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
            else if (textBox1.Text.Contains("www.mediafire.com/file/"))
            {
                string source;
                WebClient wc = new WebClient();
                try
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    source = wc.DownloadString(textBox1.Text);
                    int pos = source.IndexOf("href='http://download");
                    for (int i = pos+6; i < source.Length; i++)
                    {
                        url3 += source[i];
                        if (source[i + 1] == '\'')
                        {
                            break;
                        }
                    }
                    textBox2.Text = url3;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error! Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon
                        .Error);
                }
                button1.Enabled = true;
                button2.Enabled = true;
                wc.Dispose();
            }
            else if (textBox1.Text.Contains("www.fshare.vn/file"))
            {
              
                string id = textBox1.Text.Substring(textBox1.Text.LastIndexOf("/")+1);
                string source;
                WebClient wc = new WebClient();
                //try
                //{
                    button1.Enabled = false;
                    button2.Enabled = false;
                    source = wc.DownloadString(textBox1.Text);
                    int pos = source.IndexOf("<input type=\"hidden\" value=\"");
                    for (int i = pos + 28; i < source.Length; i++)
                    {
                        url3 += source[i];
                        if (source[i + 1] == '\"')
                        {
                            break;
                        }
                    }
                    url4 += url3;
                    url4 += "&DownloadForm%5Bpwd%5D=&DownloadForm%5Blinkcode%5D=";
                    url4 += id;
                    url4 += "&ajax=download-form&undefined=undefined";
                    byte[] data = Encoding.ASCII.GetBytes("fs_csrf="+ url3 + "&DownloadForm%5Bpwd%5D=&DownloadForm%5Blinkcode%5D=" + id + "&ajax=download-form&undefined=undefined");
                    WebRequest wr = WebRequest.Create("https://www.fshare.vn/download/get");
                    wr.Method = "POST";
                    wr.ContentType = "application/x-www-form-urlencoded";
                    wr.ContentLength = data.Length;
                    Stream stream = wr.GetRequestStream();
                    
                    stream.Write(data, 0, data.Length);
                    
                    string responseContent = null;
                    try
                    {
                        WebResponse response = wr.GetResponse();

                        Stream str = response.GetResponseStream();

                        StreamReader sr99 = new StreamReader(str);

                        responseContent = sr99.ReadToEnd();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Info");
                    }
                            
                        
                    

                    MessageBox.Show(responseContent);
                    //textBox2.Text = url4;
                //}
               // catch (Exception)
                //{
                //    MessageBox.Show("Error! Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon
                //        .Error);
                //}
                button1.Enabled = true;
                button2.Enabled = true;
                wc.Dispose();
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
                Process.Start(textBox2.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copy link vào ô thứ nhất, bấm Get Link\nĐợi link hiện ở ô thứ hai, bấm Download hoặc Copy\n", "Hướng Dẫn Sử Dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
            textBox2.Copy();
           
        }

        

        
        
    }
}
