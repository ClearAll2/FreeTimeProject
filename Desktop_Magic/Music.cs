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
    public partial class Music : Form
    {
        GlobalKeyboardHook gHook;
        int newx, newy;
        RegistryKey r;
        string playlistLoc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Playlists");

        //minimize form when clicking icon on taskbar
        //for smooth load
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        private const int WS_EX = 0x02000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                //cp.ExStyle |= WS_EX;  // Turn on WS_EX_COMPOSITED //fail
                return cp;
            }
        }

        public Music()
        {
            InitializeComponent();        
            StartPosition = FormStartPosition.CenterScreen;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 30, (workingArea.Bottom - Size.Height) / 2);
            RegistryKey r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (r.GetValue("Link") != null)
            {
                checkBoxRunatStart.Checked = true;
                axWindowsMediaPlayer1.URL = playlistLoc + "\\DMtmp.wpl";
                axWindowsMediaPlayer1.settings.setMode("shuffle", true);
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            r.Close();
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
                if (File.Exists(playlistLoc + "\\DMtmp.wpl"))
                    File.Delete(playlistLoc + "\\DMtmp.wpl");
                WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("DMtmp");
                WMPLib.IWMPMedia media;
                foreach (string file in of.FileNames)
                {
                    media = axWindowsMediaPlayer1.newMedia(file);
                    playlist.appendItem(media);
                }
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.settings.setMode("shuffle", true);
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            of.Dispose();
        }

       
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        { 
            if (File.Exists(playlistLoc + "\\DMtmp.wpl") && !checkBoxRunatStart.Checked)
                File.Delete(playlistLoc + "\\DMtmp.wpl");
            gHook.unhook();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked && axWindowsMediaPlayer1.currentMedia != null)
            {
                int length = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                if (length - (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition == 1)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
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

    

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            if (checkBoxRunatStart.Checked)
            {
                r.SetValue("Link", true);
            }
            else
            {
                r.DeleteValue("Link", false);
            }
            r.Close();
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

        private void Music_Paint(object sender, PaintEventArgs e)
        {
            Form frm = (Form)sender;
            ControlPaint.DrawBorder(e.Graphics, frm.ClientRectangle,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 1, ButtonBorderStyle.Solid,
            Color.LightBlue, 0, ButtonBorderStyle.Solid);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }
      
    }
}
