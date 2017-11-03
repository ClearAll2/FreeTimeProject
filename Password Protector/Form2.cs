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
using AE.Net.Mail;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.IO;

namespace RamC
{
    public partial class Form2 : Form
    {
        RegistryKey r;
        List<MailMessage> glist = new List<MailMessage>();
        List<string> ams = new List<string>();
        List<string> rams = new List<string>();
        ImapClient mail;
        //Pop3Client mail2;
        WebClient wc = new WebClient();
        MailMessage mesg;
        BackgroundWorker bw;
       // BackgroundWorker bw2;
        //XmlTextReader reader;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            panel1.Hide();

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            if (r.GetValue("Id") != null)
            {
                textBoxUsername.Text = (string)r.GetValue("Id");
                textBoxPass.Text = (string)r.GetValue("Pass");
            }
            r.Close();
            r.Dispose();
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            progressBar1.Hide();
            checkBox2.Checked = true;

        }

      

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text != "" && textBoxPass.Text != "")
            {
                try
                {
                    wc.DownloadString("https://www.google.com");
                }
                catch(Exception)
                {
                    MessageBox.Show("ERROR! Please check your internet connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    mail = new ImapClient("imap.gmail.com", textBoxUsername.Text, textBoxPass.Text, AuthMethods.Login, 993, true);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR! Please check if something isn't right: \n-Your input is correct? \n-You may not turn IMAP on your gmail.\n-You may not allow insecure app.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                button1.Enabled = false;
                mail.SelectMailbox("INBOX");

                progressBar1.Show();
                if (checkBox2.Checked)
                {
                    var mailmess = mail.SearchMessages(SearchCondition.Unseen(), true, true).ToList();
                   
                    Thread.Sleep(2000);
                    progressBar1.Maximum = mailmess.Count;
                    for (int i = mailmess.Count - 1; i >= 0; i--)
                    {
                        Thread.Sleep(1);
                        listBox1.Items.Add(mailmess[i].Value.Subject);
                        glist.Add(mailmess[i].Value);
                        //mailmess[i].Value.Flags = Flags.Seen;
                        progressBar1.PerformStep();
                       // label4.Text = i.ToString() + "/" + (mailmess.Count - 1).ToString();
                    }
                }
                else
                {
                    var mailmess = mail.GetMessages(0, mail.Mailbox.NumMsg, true, true).ToList();
                   
                    Thread.Sleep(2000);
                    progressBar1.Maximum = mailmess.Count;
                    for (int i = mailmess.Count - 1; i >= 0; i--)
                    {
                        Thread.Sleep(1);
                        listBox1.Items.Add(mailmess[i].Subject);
                        glist.Add(mailmess[i]);
                        //mailmess[i].Value.Flags = Flags.Seen;
                        progressBar1.PerformStep();
                        //label4.Text = i.ToString() + "/" + (mailmess.Count - 1).ToString();
                    }
                }
                //
                
                //mail.Disconnect();
                //mail.Dispose();
                //save id and pass after login
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
                r.SetValue("Id", textBoxUsername.Text);
                r.SetValue("Pass", textBoxPass.Text);
                r.Close();
                r.Dispose();
                General.haveId = true;
                panel1.Show();
                button1.Enabled = false;
                wc.Dispose();
            }
            else
            {
                // button1.Enabled = true;
                MessageBox.Show("Missing username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //wc.Dispose();
            //mesg = null;
            if (panel1.Visible)
                mail.Disconnect();
            //mail.Dispose();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\RamC\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\RamC\\Data");
            if (textBoxUsername.Text != "" && textBoxPass.Text != "")
            {
                r.SetValue("Id", textBoxUsername.Text);
                r.SetValue("Pass", textBoxPass.Text);
            }
            r.Close();
        }

        private void listBox1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                mesg = null;
                richTextBox1.ReadOnly = true;
                richTextBox2.ReadOnly = true;
                button2.Text = "Reply";
                mesg = mail.GetMessage(glist[listBox1.SelectedIndex].Uid, false);
                
                richTextBox1.Text = mesg.Body;
                richTextBox2.Text = mesg.Subject;
                richTextBox3.Text = mesg.From.ToString();
                richTextBox4.Text = mesg.Date.ToString();
                //
                button3.Text = "";
                foreach (var att in mesg.Attachments)
                {
                    string fName = att.Filename;
                    button3.Text += fName;
                    button3.Text += "; ";
                }
              
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Enabled = false;
            else
                checkBox1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Enabled = false;
            else
                checkBox2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ReadOnly == true && listBox1.SelectedItem != null)
            {
                richTextBox1.Text = ""; richTextBox1.ReadOnly = false;
                richTextBox2.Text = "RE: " + richTextBox2.Text; richTextBox2.ReadOnly = false;
                richTextBox4.Text = "";
                button2.Text = "Send";
                button3.Text = "Attachment...";
            }
            else
            {
                if (richTextBox3.Text == "")
                {
                    MessageBox.Show("Invalid receive email!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(textBoxUsername.Text, textBoxPass.Text);
                
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(textBoxUsername.Text, glist[listBox1.SelectedIndex].From.ToString(), richTextBox2.Text, richTextBox1.Text);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                for (int i = 0; i < ams.Count;i++ )
                    mm.Attachments.Add(new System.Net.Mail.Attachment(ams[i]));
                mm.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                try
                {
                    client.SendMailAsync(mm);
                    client.SendCompleted += client_SendCompleted;
                    button2.Text = "Sending";
                    button2.Enabled = false;
                }
                catch(Exception)
                {
                    MessageBox.Show("Send failed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button2.Text = "Send";
                    button2.Enabled = true;
                    
                }
                client.Dispose();
                mm.Dispose();
            }

        }

        private void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Send successed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2.Text = "Reply";
            button2.Enabled = true;
          
        }

        private void richTextBox2_Click(object sender, EventArgs e)
        {
            richTextBox2.SelectAll();
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to go?", "Quest?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Process.Start(e.LinkText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button2.Text != "Send")
            {
                if (button3.Text != "")
                {
                    FolderBrowserDialog fd = new FolderBrowserDialog();
                    fd.Description = "Select place to save...";
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var att in mesg.Attachments)
                        {
                            string fName = att.Filename;
                            string fName2 = fd.SelectedPath + "\\" + fName;
                            att.Save(fName2);
                        }
                        MessageBox.Show("Download completed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    fd.Dispose();
                }
                else
                {
                    MessageBox.Show("Found no attachments", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                OpenFileDialog of = new OpenFileDialog();
                of.CheckFileExists = true;
                of.CheckPathExists = true;
                if (of.ShowDialog() == DialogResult.OK)
                {
                    button3.Text = "";
                    
                    ams.Add(of.FileName);
                    rams.Add(of.SafeFileName);
                    for (int i = 0;i<rams.Count;i++)
                    {
                        button3.Text += rams[i];
                        button3.Text += "; ";
                    }
                        
                    
                }
                of.Dispose();
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ams.Clear();
            rams.Clear();
            button3.Text = "";
        }

        
        
    }
}
