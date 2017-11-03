using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Management;
using System.Diagnostics;
using System.IO;
using PLocker;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace Simple_Media_Player
{
    public partial class Form1 : Form
    {
       
        
        public Form1()
        {
            
            InitializeComponent();
           

            
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Choose folder to lock...";
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Add(new ListViewItem(new String[] { fb.SelectedPath, "Not lock" }));
            }

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    // .Show(Cursor.Position);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public static string GetProcessPath(int processId)
        {
            string MethodResult = "";
            try
            {
                string Query = "SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;

                using (ManagementObjectSearcher mos = new ManagementObjectSearcher(Query))
                {
                    using (ManagementObjectCollection moc = mos.Get())
                    {
                        string ExecutablePath = (from mo in moc.Cast<ManagementObject>() select mo["ExecutablePath"]).First().ToString();

                        MethodResult = ExecutablePath;

                    }

                }

            }
            catch //(Exception ex)
            {
                //ex.HandleException();
            }
            return MethodResult;
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                string folderPath = listView1.SelectedItems[0].Text;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);  
                ds.AddAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);
                MessageBox.Show("Locked");
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = listView1.SelectedItems[0].Text;
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);  
                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(folderPath, ds);
                MessageBox.Show("Unlocked");
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
            listView1.Items.Remove(listView1.FocusedItem);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

       
        
    }
}
