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
using SharpPcap;
using SharpPcap.WinPcap;
using SharpPcap.LibPcap;
using PacketDotNet;

namespace LScanner
{
    public partial class Form1 : Form
    {
        List<bool> done = new List<bool>();
        List<string> list = new List<string>();

        string localIP;
        string host;
        bool stop = false;
        bool stop2 = false;
        BackgroundWorker bw;
        BackgroundWorker bw2;
        BackgroundWorker bw3;
        BackgroundWorker bw4;
        BackgroundWorker bw5;
        WebClient wc;
       
        NetworkInterface[] nics;

        int newx, newy;
        public Form1()
        {
            
            InitializeComponent();
            InitializeNetworkInterface();
            localIP = GetLocalIPAddress();
            textBox1.Text = localIP;
            label14.ForeColor = Color.Red;
            //label16.Text = "LAN Scanner - v" + Application.ProductVersion;

            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw2 = new BackgroundWorker();
            bw2.DoWork += bw2_DoWork;

            bw3 = new BackgroundWorker();
            bw3.DoWork += bw3_DoWork;
            label4.Text = "LScanner v" + Application.ProductVersion + "\nCopyright ©  2017 \nClear All Soft";
            wc = new WebClient();
            label6.Text = "This computer internet IP: " + GetLocalIPAddress2();

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
                subnet = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf("."));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid IP address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                button2.Enabled = false;
                return;
            }
            PingReply reply;
            Ping myPing;
            IPAddress addr;
            IPHostEntry host;
            string mac = "?-?-?-?-?-?";
            listView1.Items.Clear();

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
                    label3.ForeColor = System.Drawing.Color.Green;
                    label3.Text = "Scanning " + subnetn +"/.254";
                    if (reply.Status == IPStatus.Success)
                    {
                        try
                        {
                            addr = IPAddress.Parse(subnet + subnetn);
                            host = Dns.GetHostEntry(addr);
                            mac = GetMacAddress(subnet + subnetn);
                            listView1.Items.Add(new ListViewItem(new String[] { subnet + subnetn, host.HostName, mac }));
                           
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
            button1.Enabled = true;
            button2.Enabled = false;
            label3.Text = "Done";

            int count = listView1.Items.Count;
            MessageBox.Show("Scanning done!\nFound " + count.ToString() + " hosts.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void spoof(string dstIp, string dstMac)
        {
           // LibPcapLiveDevice tmp = null;
           // var devices = LibPcapLiveDeviceList.Instance;
           // tmp = devices[3];
           //// LibPcapLiveDevice device = getDevice();
           // IPAddress ip = IPAddress.Parse(host);
            
            
           // // Create a new ARP resolver
           // ARP arp = new ARP(tmp);
           // arp.Timeout = new System.TimeSpan(1200000 * 2); // 100ms

            
           // // Preparar ip y mac fake, solo para spoofing
           // IPAddress local_ip;
           // IPAddress.TryParse("192.168.1.1", out local_ip);

           // PhysicalAddress mac;
           // mac = PhysicalAddress.Parse("11-22-33-44-55-66");

           // // Enviar ARP

           // for (int i = 0; i < 10000; i++)
           // {
                
           //     try
           //     {
           //         arp.Resolve(ip, local_ip, mac);
           //         //arp.Resolve(ip, ip, mac);
           //     }
           //     catch (Exception e)
           //     {
           //         MessageBox.Show(ip + " stopped responding: " + e.Message);
           //         return;
           //     }
                
           //     System.Threading.Thread.Sleep(3000); // 5 sec
           //     if (stop2 == true)
           //         break;
           // }

           // return;
            
            //for (int i = 0; i < 10000; i++)
            //{
            dstMac = dstMac.ToUpper();
            //MessageBox.Show(dstIp + " " + dstMac, "info");
            try
            {
                ARPPacket arp = new ARPPacket(ARPOperation.Request, PhysicalAddress.Parse(dstMac), IPAddress.Parse(dstIp), PhysicalAddress.Parse("11-22-33-44-55-66"), IPAddress.Parse("192.168.1.1"));
                LibPcapLiveDevice tmp = null;
                var devices = LibPcapLiveDeviceList.Instance;
                foreach(var div in devices)
                {
                    for (int i=0;i<div.Addresses.Count - 1;i++)
                    {
                        var ip = div.Addresses[i].Addr.ipAddress;
                        if (ip != null)
                        {
                            if (ip.ToString() == textBox1.Text)
                            {
                                tmp = div;
                                break;
                            }
                        }
                        
                    }
                }
                tmp.Open();
                for (int i = 0; i < 10000; i++)
                {
                    tmp.SendPacket(arp);
                    Thread.Sleep(3000);
                    if (stop2 == true)
                        break;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "info");
            }
            //    Thread.Sleep(3000);
            //    if (stop2 == true)
            //        break;
            //}

            
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
            label14.Text = nic.OperationalStatus.ToString();
            

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
            button4.Enabled = false;
            button4.Text = "Checking Internet...";
            try
            {
                var tmp = wc.DownloadString("https://www.google.com.vn");
            }
            catch (Exception)
            {
                MessageBox.Show("Make sure you have a valid internet connection!", "Fail to connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button4.Enabled = true;
                button4.Text = "Check for Update";
                return;
            }
            button4.Text = "Checking version...";
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
            button4.Text = "Check for Update";
            button4.Enabled = true;
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

            label3.ForeColor = System.Drawing.Color.Green;

            for (int i = 0; i <= _searchClass.Length - 1; i++)
            {
                label3.Text = "Getting information.";
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT *FROM " + _searchClass[i]);
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        label3.Text = "Getting information. .";

                        temp += obj.GetPropertyValue(param[i]).ToString() + "\n";
                        if (i == _searchClass.Length - 1)
                        {
                            label3.Text = "Done";
                            MessageBox.Show(temp, "Hostinfo: " + host, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        label3.Text = "Getting information. . .";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not get information!?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    label3.Text = "Idle";
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
                subnet = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf("."));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid IP address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                button2.Enabled = false;
                return;
            }
            
            
            listView1.Items.Clear();
           
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
                    label3.ForeColor = System.Drawing.Color.Green;
                    label3.Text = "Scanning " + subnetn +"/.254";
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
            
            button1.Enabled = true;
            button2.Enabled = false;
            label3.Text = "Done";

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
                listView1.Items.Add(new ListViewItem(new String[] { ip, "Getting hostname", "Getting MAC" }));
                list.Add(ip);
                done.Add(false);
            }
            
            
        }

        private void bw4_DoWork(object sender, DoWorkEventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            
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
                            for (int i = 0; i < listView1.Items.Count; i++)
                            {
                                if (listView1.Items[i].SubItems[0].Text == list[j])
                                {
                                    listView1.Items[i].SubItems[1].ForeColor = Color.Red;
                                    listView1.Items[i].SubItems[1].Text = "Unknown";
                                    listView1.Items[i].SubItems[2].ForeColor = Color.Red;
                                    listView1.Items[i].SubItems[2].Text = "Unknown";
                                    done[j] = false;
                                    break;
                                }
                            }
                            continue;
                        }
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            if (listView1.Items[i].SubItems[0].Text == list[j])
                            {
                                listView1.Items[i].SubItems[1].ForeColor = Color.White;
                                listView1.Items[i].SubItems[2].ForeColor = Color.White;
                                listView1.Items[i].SubItems[1].Text = host.HostName;
                                listView1.Items[i].SubItems[2].Text = mac;
                                done[j] = true;
                                break;
                            }
                        }
                    }
                }
            
            button1.Enabled = true;
            button3.Enabled = true;

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

        public static string GetLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public static string GetLocalIPAddress2()
        {
            string externalip = "?.?.?.?";
            try
            {
                externalip = new WebClient().DownloadString("http://icanhazip.com");
            }
            catch (Exception)
            {
                externalip = "?.?.?.?";
            }
            return externalip;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            localIP = GetLocalIPAddress();
            list.Clear();
            done.Clear();
            if (radioButton2.Checked)
            {
                if (!bw.IsBusy)
                {
                    stop = true;
                    stop = false;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    bw.RunWorkerAsync();
                }
            }
            else
            {
                if (!bw5.IsBusy)
                {
                    stop = true;
                    stop = false;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    bw5.RunWorkerAsync();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop = true;
            button1.Enabled = true;
            button2.Text = "&Stop";
            //txtIP.Enabled = true;
            label3.ForeColor = System.Drawing.Color.Red;
            label3.Text = "&Idle";
            

        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            host = listView1.SelectedItems[0].Text.ToString();
            if (!bw2.IsBusy)
            {
                bw2.RunWorkerAsync();
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    if (button1.Enabled)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (!bw3.IsBusy)
            {
                bw3.RunWorkerAsync();
            }
        }


        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.whatismyip.com/");
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ip = listView1.SelectedItems[0].Text.ToString();
            string cmd = "/c " +  "ping "  + ip + " -t";
            Process.Start("cmd.exe", cmd);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://mini102.wordpress.com");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            localIP = GetLocalIPAddress();
            textBox1.Text = localIP;
            //label6.Text = "This computer internet IP: " + GetLocalIPAddress2();
        }

        private int sortColumn = -1;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView1.Sorting);
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
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (listView1.Items[i].SubItems[0].Text == list[j])
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

        private void label17_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

        private void label16_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newx = e.X;
                newy = e.Y;
            }
        }

       
        

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left = Left + (e.X - newx);
                Top = Top + (e.Y - newy);
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
