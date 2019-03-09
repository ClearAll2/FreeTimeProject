using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;
using WMPLib;
using System.IO;
using System.Diagnostics;

namespace DM
{
    public partial class Form3 : Form
    {
        GlobalKeyboardHook gHook;
        List<String> fileNames = new List<String>();
        List<String> filePaths = new List<String>();
        List<bool> played = new List<bool>(); 
        String[] tempFileNames;
        String[] tempFilePaths;
        Random rd;
        bool prev = false;
        int last = 0;
        int newx, newy;
        RegistryKey r;
        public Form3()
        {
            InitializeComponent();        
            StartPosition = FormStartPosition.CenterScreen;
            rd = new Random();
            listBox1.AllowDrop = true;
            listBox1.DragEnter += new DragEventHandler(listBox1_DragEnter);
            listBox1.DragDrop += new DragEventHandler(listBox1_DragDrop);
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data");
            if (r.GetValue("Link") != null)
            {
                checkBox3.Checked = true;
                string path = (string)r.GetValue("Link");
                string name = path.Substring(path.LastIndexOf("\\") + 1);
                fileNames.Add(name);
                filePaths.Add(path);
                listBox1.Items.Add(name);
                listBox1.SelectedIndex = 0;
                if (r.GetValue("Repeat") != null)
                {
                    checkBox1.Checked = true;
                    checkBox1.Enabled = true;
                    checkBox2.Checked = false;
                    checkBox2.Enabled = false;
                }
            }
            r.Close();
            r.Dispose();
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] Paths = (string[])e.Data.GetData(DataFormats.FileDrop);
               foreach(string path in Paths)
               {
                   if (Path.GetExtension(path) == ".mp3")
                   {
                       string name = path.Substring(path.LastIndexOf("\\") + 1);
                       if (!listBox1.Items.Contains(name))
                       {
                           fileNames.Add(name);
                           filePaths.Add(path);
                           played.Add(false);
                           listBox1.Items.Add(name);
                           if (listBox1.Items.Count < 2)
                               listBox1.SelectedIndex = 0;
                       }
                   }
               }
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Audio Files (*.mp3, *.m4a, *.aac, *.wma, *.wav)|*.mp3;*.m4a;*.aac;*.wma;*.wav|All files (*.*)|*.*";
            of.CheckFileExists = true;
            of.CheckPathExists = true;
            of.Multiselect = true;
            of.Title = "Select songs";
            of.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            if (of.ShowDialog() == DialogResult.OK)
            {
                if (listBox1.Items.Count <= 0)
                {
                    tempFileNames = of.SafeFileNames;
                    tempFilePaths = of.FileNames;
                    for (int i = 0; i < tempFileNames.Length; i++)
                    {
                        fileNames.Add(tempFileNames[i]);
                        filePaths.Add(tempFilePaths[i]);
                        played.Add(false);
                        listBox1.Items.Add(tempFileNames[i]);                        
                    }
                    if (checkBox2.Checked)
                        listBox1.SelectedIndex = rd.Next(0, listBox1.Items.Count - 1);
                    else
                        listBox1.SelectedIndex = 0;
                }
                else
                {
                    tempFileNames = of.SafeFileNames;
                    tempFilePaths = of.FileNames;
                    for (int i = 0; i < tempFileNames.Length;i++)
                    {
                        if (!listBox1.Items.Contains(tempFileNames[i]))
                        {
                            fileNames.Add(tempFileNames[i]);
                            filePaths.Add(tempFilePaths[i]);
                            played.Add(false);
                            listBox1.Items.Add(tempFileNames[i]);
                        }
                    }
                }
            }
            of.Dispose();
        }

       
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (checkBox3.Checked)
            {
                r.SetValue("Link", filePaths[listBox1.SelectedIndex]);
                if (checkBox1.Checked)
                    r.SetValue("Repeat", true);
                else
                    r.DeleteValue("Repeat", false);
            }
            else
            {
                r.DeleteValue("Link", false);
                r.DeleteValue("Repeat", false);
            }
            r.Close();
            r.Dispose();
            gHook.unhook();
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null )
                axWindowsMediaPlayer1.URL = filePaths[listBox1.SelectedIndex];    
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                timer1.Interval = 100;
                timer1.Enabled = true;            
                played[listBox1.SelectedIndex] = true;
                last = listBox1.SelectedIndex; 
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {  
            if (listBox1.Items.Count > 1)
            {
                if (prev == true && checkBox1.Checked != true)
                {
                    listBox1.SelectedIndex = last;
                    timer1.Enabled = false;
                    prev = false;
                    return;
                }
                if (!checkBox2.Checked)//neu khong check shuffle
                {
                    if ((listBox1.SelectedIndex + 1) <= (fileNames.Count - 1))
                    {
                        if (!checkBox1.Checked)
                        {
                            listBox1.SelectedIndex++;
                        }
                        else
                        {
                            axWindowsMediaPlayer1.URL = filePaths[listBox1.SelectedIndex];
                        }
                        timer1.Enabled = false;
                    }
                    else
                    {
                        //Kiem tra bai cuoi
                        if (!checkBox1.Checked)//neu khong lap, quay lai 0
                        {
                            listBox1.SelectedIndex = 0;
                        }
                        else//neu lap
                        {
                            axWindowsMediaPlayer1.URL = filePaths[listBox1.SelectedIndex];
                        }
                        timer1.Enabled = false;

                    }
                }
                else //neu check shuffle
                {   
                    int random = rd.Next(1, listBox1.Items.Count - 1);
                    if (Count() > 1)
                    {
                        while (played[random] == true)
                            random = rd.Next(0, listBox1.Items.Count - 1);
                    }
                    else
                    {
                        random = GetLast();
                        for (int i = 0; i < played.Count; i++)
                            played[i] = false;
                    }
                    listBox1.SelectedIndex = random;
                    timer1.Enabled = false;
                }
            }
            else
            {
                if (listBox1.Items.Count == 1)
                    if (checkBox1.Checked)
                        axWindowsMediaPlayer1.URL = filePaths[listBox1.SelectedIndex];
                timer1.Enabled = false;
            }
            
        }

        private int Count()
        {
            int count = 0;
            for (int i = 0; i < played.Count; i++)
                if (played[i] != true)
                    count++;
            return count;
        } 

        private int GetLast()
        {
            for (int i = 0; i < played.Count; i++)
                if (played[i] != true)
                    return i;
            return -1;
        }

        
        private void Form3_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_Keydown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();
        }

        private void gHook_Keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0xB3)
            {
                button4_Click(null, null);
            }
            else if (e.KeyValue == 0xB0)
            {
                button1_Click(null, null);
            }
            else if (e.KeyValue == 0xB1)
            {
                button2_Click(null, null);
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
            prev = true;
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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                button4.Text = "|>";
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                button4.Text = "||";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;

        }

        

        //protected override void WndProc(ref Message msg)
        //{
        //    if (msg.Msg == 0x319)   // WM_APPCOMMAND message
        //    {
        //        // extract cmd from LPARAM (as GET_APPCOMMAND_LPARAM macro does)
        //        int cmd = (int)((uint)msg.LParam >> 16 & ~0xf000);
        //        switch (cmd)
        //        {
                    
        //            case 14:  // APPCOMMAND_MEDIA_PLAY_PAUSE
        //                button4_Click(null, null);
        //                break;
        //            case 11:  // APPCOMMAND_MEDIA_NEXTTRACK
        //                button1_Click(null, null);
        //                break;
        //            case 12:  // APPCOMMAND_MEDIA_PREVIOUSTRACK
        //                button2_Click(null, null);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    base.WndProc(ref msg);
        //}


        

        //private void Form3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Message m = new Message();
        //    m.Msg = e.KeyChar;

        //    WndProc(ref m);
        //}
        

       

       
    }
}
