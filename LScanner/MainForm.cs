using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;


namespace LScanner
{
    public partial class MainForm : Form
    {
        List<bool> done = new List<bool>();
        List<string> list = new List<string>();
        string host;
        bool stop = false;
        BackgroundWorker bw;
        BackgroundWorker bw2;
        BackgroundWorker bw3;
        BackgroundWorker bw4;
        BackgroundWorker bw5;
        WebClient wc;
       
        NetworkInterface[] nics;

        public MainForm()
        {
            
            InitializeComponent();
            InitializeNetworkInterface();
            GetLocalIPAddress();
            GetLocalIPAddress2();
            labelNetInterfaceStatus.ForeColor = Color.Red;

            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;

            bw3 = new BackgroundWorker();
            bw3.DoWork += bw3_DoWork;
            labelAbout.Text = "Simple Network Scanner v" + Application.ProductVersion + "\nCopyright © 2017 - 2022 \nBuilt by Duc Nguyen";
            wc = new WebClient();
           

            bw4 = new BackgroundWorker();
            bw4.DoWork +=bw4_DoWork;

            bw5 = new BackgroundWorker();
            bw5.DoWork += bw5_DoWork;

            
        }


        //normal ping
        private void bw5_DoWork(object sender, DoWorkEventArgs e)
        {
            string subnet;
            try
            {
                subnet = comboBoxPrivateIP.Text.Substring(0, comboBoxPrivateIP.Text.LastIndexOf("."));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid IP address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonScan.Enabled = true;
                buttonStop.Enabled = false;
                return;
            }
            PingReply reply;
            Ping myPing;
            IPAddress addr;
            IPHostEntry host;
            string mac = "?-?-?-?-?-?";
            listViewMain.Items.Clear();

            for (int i = 1; i < 255; i++)
            {
                if (stop == true)
                    break;
                string subnetn = "." + i.ToString();
                //MessageBox.Show(subnet + subnetn, "info");
                myPing = new Ping();
                try
                {
                    reply = myPing.Send(subnet + subnetn, 900);
                    labelRealStatus.ForeColor = System.Drawing.Color.Green;
                    labelRealStatus.Text = "Scanning " + subnetn +"/.254";
                    if (reply.Status == IPStatus.Success)
                    {
                        try
                        {
                            addr = IPAddress.Parse(subnet + subnetn);
                            host = Dns.GetHostEntry(addr);
                            mac = GetMacAddress(subnet + subnetn);
                            listViewMain.Items.Add(new ListViewItem(new String[] { subnet + subnetn, host.HostName, mac }));
                           
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                catch(Exception)
                {
                    continue;
                }
                myPing.Dispose();
            }
            buttonScan.Enabled = true;
            buttonStop.Enabled = false;
            labelRealStatus.Text = "Done";

            int count = listViewMain.Items.Count;
            MessageBox.Show("Scanning done!\nFound " + count.ToString() + " hosts.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void InitializeNetworkInterface()
        {
            // Grab all local interfaces to this computer
            nics = NetworkInterface.GetAllNetworkInterfaces();

            // Add each interface name to the combo box
            for (int i = 0; i < nics.Length; i++)
                cmbInterface.Items.Add(nics[i].Name);

            // Change the initial selection to the first interface
            cmbInterface.SelectedIndex = 0;
        }

        private void UpdateNetworkInterface()
        {
            // Grab NetworkInterface object that describes the current interface
            NetworkInterface nic = nics[cmbInterface.SelectedIndex];

            // Grab the stats for that interface
            IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();

            // Calculate the speed of bytes going in and out
            // NOTE: we could use something faster and more reliable than Windows Forms Tiemr
            //       such as HighPerformanceTimer http://www.m0interactive.com/archives/2006/12/21/high_resolution_timer_in_net_2_0.html
            int bytesSentSpeed = (int)(interfaceStats.BytesSent - double.Parse(lblBytesSent.Text)) / 1024;
            int bytesReceivedSpeed = (int)(interfaceStats.BytesReceived - double.Parse(lblBytesReceived.Text)) / 1024;

            // Update the labels
            
            lblSpeed.Text = nic.Speed.ToString();
            lblInterfaceType.Text = nic.NetworkInterfaceType.ToString();
            lblSpeed.Text = nic.Speed.ToString();
            lblBytesReceived.Text = interfaceStats.BytesReceived.ToString();
            lblBytesSent.Text = interfaceStats.BytesSent.ToString();
            lblUpload.Text = bytesSentSpeed.ToString() + " KB/s";
            lblDownload.Text = bytesReceivedSpeed.ToString() + " KB/s";
            labelNetInterfaceStatus.Text = nic.OperationalStatus.ToString();
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateNetworkInterface();
        }


        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return GetMACAddress1(ipAddress);
            }
        }

        public static string GetMACAddress1(string ipAddress)
        {

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                var ipProps = adapter.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if ((adapter.OperationalStatus == OperationalStatus.Up)
                        && (ip.Address.AddressFamily == AddressFamily.InterNetwork))
                    {
                        if (ip.Address.ToString() == ipAddress)  
                        {
                            IPInterfaceProperties properties = adapter.GetIPProperties();
                            sMacAddress = adapter.GetPhysicalAddress().ToString();
                            sMacAddress = sMacAddress.Insert(2, "-");
                            sMacAddress = sMacAddress.Insert(5, "-");
                            sMacAddress = sMacAddress.Insert(8, "-");
                            sMacAddress = sMacAddress.Insert(11, "-");
                            sMacAddress = sMacAddress.Insert(14, "-");
                            return sMacAddress;
                        }
                    }
                }   
                
            } 
            return sMacAddress;
        }

        //check for update
        private void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            buttonCheckUpdate.Enabled = false;
            buttonCheckUpdate.Text = "Checking Internet...";
            try
            {
                var tmp = wc.DownloadString("https://www.google.com.vn");
            }
            catch (Exception)
            {
                MessageBox.Show("Make sure you have a valid internet connection!", "Fail to connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                buttonCheckUpdate.Enabled = true;
                buttonCheckUpdate.Text = "Check for Update";
                return;
            }
            buttonCheckUpdate.Text = "Checking version...";
            string version = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsZDdmbHcwQWVHY00");
            string changelog = wc.DownloadString("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsci1IRldyUkx4YXc");
            if (version != Application.ProductVersion)
            {
                if (MessageBox.Show("You are running old version " + Application.ProductVersion + "\nWould you like to download new version " + version + "?\n" + "Changelog: \n" + changelog, "LScanner Version Checker", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("https://drive.google.com/uc?export=download&id=0B-QP4eT8oLdsbEZGaVlRMzRyUkE");
                }
            }
            else
            {
                MessageBox.Show("You are running latest version!", "Congratulation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            buttonCheckUpdate.Text = "Check for Update";
            buttonCheckUpdate.Enabled = true;
        }

        //query info
        private void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            //string acc;
            //string os;
            //string board;
            //string biosVersion;
            string temp = null;
            string[] _searchClass = { "Win32_ComputerSystem", "Win32_OperatingSystem", "Win32_BaseBoard", "Win32_BIOS" };
            string[] param = { "UserName", "Caption", "Product", "Description" };

            labelRealStatus.ForeColor = System.Drawing.Color.Green;

            for (int i = 0; i <= _searchClass.Length - 1; i++)
            {
                labelRealStatus.Text = "Getting information.";
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT *FROM " + _searchClass[i]);
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        labelRealStatus.Text = "Getting information. .";

                        temp += obj.GetPropertyValue(param[i]).ToString() + "\n";
                        if (i == _searchClass.Length - 1)
                        {
                            labelRealStatus.Text = "Done";
                            MessageBox.Show(temp, "Hostinfo: " + host, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        labelRealStatus.Text = "Getting information. . .";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not get information!?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    labelRealStatus.Text = "Ready";
                    break;
                }
            }
        }



        //scan
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //localIP = "192.168.182.1";
            string subnet;
            try
            {
                subnet = comboBoxPrivateIP.Text.Substring(0, comboBoxPrivateIP.Text.LastIndexOf("."));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid IP address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonScan.Enabled = true;
                buttonStop.Enabled = false;
                return;
            }
            
            
            listViewMain.Items.Clear();
           
            for (int i = 1; i < 255; i++)
            {
                if (stop == true)
                    break;
                string subnetn = "." + i.ToString();
                //MessageBox.Show(subnet + subnetn, "info");
                Ping myPing = new Ping();
                myPing.PingCompleted += myPing_PingCompleted;
                try
                {
                    myPing.SendAsync(subnet + subnetn, 10000, subnet+subnetn);
                    labelRealStatus.ForeColor = System.Drawing.Color.Green;
                    labelRealStatus.Text = "Scanning " + subnetn +"/.254";
                }
                catch (Exception)
                {
                    continue;
                }
                //myPing.Dispose();
            }
            while (bw4.IsBusy)
            { }
            bw4.RunWorkerAsync();
            
            buttonScan.Enabled = true;
            buttonStop.Enabled = false;
            labelRealStatus.Text = "Done";

            MessageBox.Show("Scanning done!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            string ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            { 
                for (int i=0;i<list.Count;i++)
                {
                    if(list[i] == ip)
                    {
                        return;
                    }
                }
                listViewMain.Items.Add(new ListViewItem(new String[] { ip, "Getting hostname", "Getting MAC" }));
                list.Add(ip);
                done.Add(false);
            }
            
            
        }

        private void bw4_DoWork(object sender, DoWorkEventArgs e)
        {
            buttonScan.Enabled = false;
            buttonStop.Enabled = false;
            
                for (int j = 0; j < list.Count; j++)
                {
                    if (stop == true)
                        return;
                    if (done[j] != true)
                    {
                        IPAddress addr = null;
                        IPHostEntry host = null;
                        string mac = "?-?-?-?-?-?";
                        try
                        {
                            addr = IPAddress.Parse(list[j]);
                            host = Dns.GetHostEntry(addr);
                            mac = GetMacAddress(list[j]);
                            
                        }
                        catch (Exception)
                        {
                            for (int i = 0; i < listViewMain.Items.Count; i++)
                            {
                                if (listViewMain.Items[i].SubItems[0].Text == list[j])
                                {
                                    listViewMain.Items[i].SubItems[1].ForeColor = Color.Red;
                                    listViewMain.Items[i].SubItems[1].Text = "Unknown";
                                    listViewMain.Items[i].SubItems[2].ForeColor = Color.Red;
                                    listViewMain.Items[i].SubItems[2].Text = "Unknown";
                                    done[j] = false;
                                    break;
                                }
                            }
                            continue;
                        }
                        for (int i = 0; i < listViewMain.Items.Count; i++)
                        {
                            if (listViewMain.Items[i].SubItems[0].Text == list[j])
                            {
                                listViewMain.Items[i].SubItems[1].ForeColor = Color.White;
                                listViewMain.Items[i].SubItems[2].ForeColor = Color.White;
                                listViewMain.Items[i].SubItems[1].Text = host.HostName;
                                listViewMain.Items[i].SubItems[2].Text = mac;
                                done[j] = true;
                                break;
                            }
                        }
                    }
                }
            
            buttonScan.Enabled = true;

        }

        public void controlSys(string host, int flags)
        {
            #region
            /*
             *Flags:
             *  0 (0x0)Log Off
             *  4 (0x4)Forced Log Off (0 + 4)
             *  1 (0x1)Shutdown
             *  5 (0x5)Forced Shutdown (1 + 4)
             *  2 (0x2)Reboot
             *  6 (0x6)Forced Reboot (2 + 4)
             *  8 (0x8)Power Off
             *  12 (0xC)Forced Power Off (8 + 4)
             */
            #endregion

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT *FROM Win32_OperatingSystem");

                foreach (ManagementObject obj in searcher.Get())
                {
                    ManagementBaseObject inParams = obj.GetMethodParameters("Win32Shutdown");

                    inParams["Flags"] = flags;

                    ManagementBaseObject outParams = obj.InvokeMethod("Win32Shutdown", inParams, null);
                }
            }
            catch (ManagementException manex) { MessageBox.Show("Error:\n\n" + manex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (UnauthorizedAccessException unaccex) { MessageBox.Show("Authorized?\n\n" + unaccex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void GetLocalIPAddress()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                comboBoxPrivateIP.Items.Add(ip.ToString());
            }
            comboBoxPrivateIP.SelectedIndex = 0;
        }

        public void GetLocalIPAddress2()
        {
            try
            {
                comboBoxPublicIP.Items.Add(new WebClient().DownloadString("http://icanhazip.com"));
                comboBoxPublicIP.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            list.Clear();
            done.Clear();
            if (radioButtonFastScan.Checked)
            {
                if (!bw.IsBusy)
                {
                    stop = true;
                    stop = false;
                    buttonScan.Enabled = false;
                    buttonStop.Enabled = true;
                    bw.RunWorkerAsync();
                }
            }
            else
            {
                if (!bw5.IsBusy)
                {
                    stop = true;
                    stop = false;
                    buttonScan.Enabled = false;
                    buttonStop.Enabled = true;
                    bw5.RunWorkerAsync();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            stop = true;
            buttonScan.Enabled = true;
            buttonStop.Text = "&Stop";
            //txtIP.Enabled = true;
            labelRealStatus.ForeColor = System.Drawing.Color.Red;
            labelRealStatus.Text = "&Ready";
            

        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            host = listViewMain.SelectedItems[0].Text.ToString();
            if (!bw2.IsBusy)
            {
                bw2.RunWorkerAsync();
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewMain.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    if (buttonScan.Enabled)
                    { 
                        resolveToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        resolveToolStripMenuItem.Enabled = false;
                    }
                    
                }
            }
        }

        private void buttonCheckForUpdate_Click(object sender, EventArgs e)
        {
            if (!bw3.IsBusy)
            {
                bw3.RunWorkerAsync();
            }
        }


        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ip = listViewMain.SelectedItems[0].Text.ToString();
            string cmd = "/c " +  "ping "  + ip + " -t";
            Process.Start("cmd.exe", cmd);
        }

        private void buttonHomePage_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.pling.com/u/clearall2");
        }


        private int sortColumn = -1;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listViewMain.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewMain.Sorting == SortOrder.Ascending)
                    listViewMain.Sorting = SortOrder.Descending;
                else
                    listViewMain.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewMain.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            listViewMain.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listViewMain.Sorting);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw.IsBusy == true || bw3.IsBusy == true || bw4.IsBusy == true)
            {
                if (MessageBox.Show("Some processes are still running\nDo you really want to quit?", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    
                    e.Cancel = true;
                }
                
            }
        }

        private void resolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bw4.IsBusy != true)
            {
                if (list.Count > 0)
                {
                    for (int i = 0; i < listViewMain.Items.Count; i++)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (listViewMain.Items[i].SubItems[0].Text == list[j])
                            {
                                if (done[j] != true)
                                {
                                    stop = false;
                                    bw4.RunWorkerAsync();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("This host has been resolved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This host has beed resolved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("This process is running, try again later!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    class ListViewItemComparer : System.Collections.IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                    ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }
}
