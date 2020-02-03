﻿using System;
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
    public partial class Music : Form
    {
        GlobalKeyboardHook gHook;
        int newx, newy;
        RegistryKey r;
        string playlistLoc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Playlists");
        public Music()
        {
            InitializeComponent();        
            StartPosition = FormStartPosition.CenterScreen;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
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
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("DMtmp");
            WMPLib.IWMPMedia media;
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Audio Files (*.mp3, *.m4a, *.aac, *.wma, *.wav)|*.mp3;*.m4a;*.aac;*.wma;*.wav|All files (*.*)|*.*";
            of.CheckFileExists = true;
            of.CheckPathExists = true;
            of.Multiselect = true;
            of.Title = "Select songs";
            of.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            if (of.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in of.FileNames)
                {
                    media = axWindowsMediaPlayer1.newMedia(file);
                    playlist.appendItem(media);
                }
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            of.Dispose();
        }

       
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(playlistLoc + "\\DMtmp.wpl"))
                File.Delete(playlistLoc + "\\DMtmp.wpl");
            gHook.unhook();
        }
        
       

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                if (checkBoxRepeat.Checked)
                {
                   
                }
            }
            
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
            if (e.KeyValue == 0xB3)//pause/play
            {
                if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                else
                    axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if (e.KeyValue == 0xB0)//next
            {
                axWindowsMediaPlayer1.Ctlcontrols.next();
            }
            else if (e.KeyValue == 0xB1)//prev
            {
                axWindowsMediaPlayer1.Ctlcontrols.previous();
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShuffle.Checked)
                checkBoxRepeat.Enabled = false;
            else
                checkBoxRepeat.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked)
                checkBoxShuffle.Enabled = false;
            else
                checkBoxShuffle.Enabled = true;
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
                //button4.Text = "|>";
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                //button4.Text = "||";
            }
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
