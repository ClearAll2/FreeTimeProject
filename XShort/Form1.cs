using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace XShort
{

    public partial class Form1 : Form
    {
        KeyboardHook hook = new KeyboardHook();
        List<String> sName = new List<String>();
        List<String> sPath = new List<String>();
        List<String> sPara = new List<String>();
        List<String> dir = new List<String>();
        Form2 f2;
        RegistryKey r;
        RegistryKey r1;
        int index = 0;
        string text = String.Empty;
        string dataPath;
        bool en = true;
        bool exit = false;
        BackgroundWorker bw;
        public Form1()
        {
            InitializeComponent();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\XShort\\Data");
            if (r.GetValue("EN") != null)
            {
                radioButton1.Checked = true;
            }
            r.Close();
            r.Dispose();
            //
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (r.GetValue("VI") != null)
            {
                radioButton2.Checked = true;
            }
            r.Close();
            r.Dispose();
            dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "XShort");
            System.IO.Directory.CreateDirectory(dataPath);
            int back = LoadData();
            if (back == -1)
            {
                if (en == true)
                    MessageBox.Show("Missing data to complete operation", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Thiếu dữ liệu - không thể hoàn thành", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (r.GetValue("Hide") != null)
            {
                checkBox2.Checked = true;
            }
            if (r.GetValue("ggSearch") != null)
            {
                checkBox3.Checked = true;
            }
            if (r.GetValue("Case-sen") != null)
            {
                checkBox4.Checked = true;
            }
            if (r.GetValue("Tray") != null)
            {
                checkBox5.Checked = true;
            }
            r.Close();
            r.Dispose();

            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (r.GetValue("XShort") != null)
            {
                if (Application.ExecutablePath == (string)r.GetValue("XShort"))
                    checkBox1.Checked = true;
            }
            r.Close();
            r.Dispose();

            if (en == true)
                f2 = new Form2(1);
            else
                f2 = new Form2(0);

            hook.KeyPressed +=
           new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            
            hook.RegisterHotKey(global::ModifierKeys.Alt, Keys.A);
            hook.RegisterHotKey(global::ModifierKeys.Shift, Keys.Z);
            label1.Text = "XShort v" + Application.ProductVersion + "\nCopyright © 2018\nClear All Soft";
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            button1.Enabled = false;
            WebClient wc = new WebClient();
            try
            {
                var tmp = wc.DownloadString("https://www.google.com.vn");
            }
            catch
            {
                if (en)
                    MessageBox.Show("Make sure you have a valid internet connection!", "Can not connect to server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                else
                    MessageBox.Show("Hãy đảm bảo bạn có kết nối internet!", "Không thể kết nối tới máy chủ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
            }
            string ver = "https://drive.google.com/uc?export=download&id=1enOPUAXdYCq6MZNnMJuZ0yqt4eVaJBzr";
            string chlog = "https://drive.google.com/uc?export=download&id=1m8EWUOGwoWY8jTL3BSIYIzLV12A6ruXr";
            string sver = wc.DownloadString(ver);
            string schlog = wc.DownloadString(chlog);
            if (Application.ProductVersion != sver)
            {
                if (en)
                {
                    if (MessageBox.Show("You are running old version!\nWould you like to download new version?\nChangelog:\n" + schlog, "XShort Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start("https://drive.google.com/uc?export=download&id=1Ue1_blwFFAh87JzS6N-5NzcPBiqEPY-5");
                    }
                }
                else
                {
                    if (MessageBox.Show("Bạn đang chạy phiên bản cũ!\nBạn có muốn tải phiên bản mới?\nChangelog:\n" + schlog, "XShort Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Process.Start("https://drive.google.com/uc?export=download&id=1Ue1_blwFFAh87JzS6N-5NzcPBiqEPY-5");
                    }
                }
            }
            else
            {
                if (en)
                {
                    MessageBox.Show("You are running latest version!", "Congratulation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Bạn đang chạy phiên bản mới nhất!", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            wc.Dispose();
            button1.Enabled = true;
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Modifier == global::ModifierKeys.Alt)
            {
                SendKeys.Send(" "); //do not remove this
                runBoxToolStripMenuItem_Click(null, null);
            }
            if (e.Modifier == global::ModifierKeys.Shift)
            {
                mainWindowToolStripMenuItem_Click(null, null);  
            }
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                BeginInvoke(new MethodInvoker(delegate { Hide(); }));
                
            }
        }

        private int LoadData()
        {
            FileStream fs;
            StreamReader sr;
            try
            {
                fs = new FileStream(Path.Combine(dataPath, "data1.data"), FileMode.Open, FileAccess.Read);
            }
            catch
            {
                return 0;
            }
            sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                sName.Add(sr.ReadLine());
            }
            fs.Close();
            sr.Close();

            //
            try
            {
                fs = new FileStream(Path.Combine(dataPath, "data2.data"), FileMode.Open, FileAccess.Read);
            }
            catch
            {
                return -1;
            }
            sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                sPath.Add(sr.ReadLine());
            }
            fs.Close();
            sr.Close();

            //
            try
            {
                fs = new FileStream(Path.Combine(dataPath, "data3.data"), FileMode.Open, FileAccess.Read);
            }
            catch
            {
                return -1;
            }
            sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                sPara.Add(sr.ReadLine());
            }
            fs.Close();
            sr.Close();


            for (int j = 0; j < sName.Count; j++)
            {
                listView1.Items.Add(new ListViewItem(new string[] { sName[j], sPath[j], sPara[j] }));
            }
            return 1;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.CheckFileExists = true;
            of.CheckPathExists = true;
            //of.Filter = "Application (*.exe, *.*)|*.exe;*.*";
            if (en == true)
                of.Title = "Select your file...";
            else
                of.Title = "Chọn fle...";
            of.Multiselect = false;
            if (of.ShowDialog() == DialogResult.OK)
            {
                
                listView1.Items.Add(new ListViewItem(new string[] { of.SafeFileName, of.FileName, "None" }));  
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[0].Text == of.SafeFileName)
                {
                    listView1.EnsureVisible(i);
                    return;
                }
            }
            of.Dispose();
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (en == true)
                fb.Description = "Select folder...";
            else
                fb.Description = "Chọn thư mục...";
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Add(new ListViewItem(new string[] { fb.SelectedPath, fb.SelectedPath, "Not Available" }));
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[0].Text == fb.SelectedPath)
                {
                    listView1.EnsureVisible(i);
                    return;
                }
            }
            fb.Dispose();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (toolStripTextBox1.Text.Contains("http://") || toolStripTextBox1.Text.Contains("https://"))
                    listView1.Items.Add(new ListViewItem(new string[] { toolStripTextBox1.Text, toolStripTextBox1.Text, "Not Available" }));
                else
                    listView1.Items.Add(new ListViewItem(new string[] { toolStripTextBox1.Text, "http://" + toolStripTextBox1.Text, "Not Available" }));
                for (int i=0;i<listView1.Items.Count;i++)
                {
                    if (listView1.Items[i].SubItems[0].Text == toolStripTextBox1.Text)
                    {
                        listView1.EnsureVisible(i);
                        return;
                    }
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
                listView1.Items.Remove(listView1.FocusedItem);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                panel1.Show();
                textBox1.Text = listView1.FocusedItem.SubItems[0].Text;
                textBox1.Focus();
                textBox1.SelectAll();
                label2.Text = "Enter your new call name";

            }

        }

        private void changePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                panel1.Show();
                textBox1.Text = listView1.FocusedItem.SubItems[1].Text;
                textBox1.Focus();
                textBox1.SelectAll();
                label2.Text = "Enter your new path";

            }
        }

        private void changeParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.FocusedItem.SubItems[2].Text != "Not Available")
                {
                    panel1.Show();
                    textBox1.Text = listView1.FocusedItem.SubItems[2].Text;
                    textBox1.Focus();
                    textBox1.SelectAll();
                    label2.Text = "Enter your new parameters";
                }
                else
                {
                    if (en == true)
                        MessageBox.Show("Directories/URLs have no parameters!", "Nothing to change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Thư mục/địa chỉ không có tham số!", "Không có gì để đổi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip2.Show(Cursor.Position);
                    if (listView1.FocusedItem.SubItems[1].Text.Contains("http") || listView1.FocusedItem.SubItems[1].Text.Contains("www"))
                    {
                        checkValidPathToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        checkValidPathToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Hide();
                if (label2.Text == "Enter your new call name")
                {
                    if (textBox1.Text != "")
                    {
                        if (textBox1.Text.Contains("!"))
                        {
                            if (en)
                            {
                                MessageBox.Show("Your call name contains character \"!\"\nPlease rename!", "Special Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Tên bạn đặt có chứa kí tự \"!\"\nHãy đổi lại!", "Kí tự đắc biệt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            if (listView1.Items[i].SubItems[0].Text == textBox1.Text)
                            {
                                if (en == true)
                                {
                                    if (MessageBox.Show("This name is already taken?!\nDo you want to rename? The program will open the first name appear in table", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                    {
                                        listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show("Tên này đã có trong bảng?!\nBạn có muốn đổi tên? Chương trình sẽ mở tên đầu tiên xuất hiện trong bảng", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                    {
                                        listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                        listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                    }
                    else
                    {
                        if (en == true)
                            MessageBox.Show("You new name is empty?!", "New Name?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Tên mới để trống!?", "Tên mới?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (label2.Text == "Enter your new path")
                {
                    if (textBox1.Text != "")
                    {
                        listView1.SelectedItems[0].SubItems[1].Text = textBox1.Text;
                    }
                    else
                    {
                        if (en == true)
                            MessageBox.Show("You new path is empty?!", "New Path?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Đường dẫn mới còn trống?!", "Đường dẫn mới?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (textBox1.Text != "")
                    {
                        listView1.SelectedItems[0].SubItems[2].Text = textBox1.Text;
                    }
                    else
                    {
                        listView1.SelectedItems[0].SubItems[2].Text = "None";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void saveShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sName.Clear();
            sPath.Clear();
            sPara.Clear();
            File.WriteAllText(Path.Combine(dataPath, "data1.data"), string.Empty);
            File.WriteAllText(Path.Combine(dataPath, "data2.data"), string.Empty);
            File.WriteAllText(Path.Combine(dataPath, "data3.data"), string.Empty);

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sName.Add(listView1.Items[i].SubItems[0].Text);
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sPath.Add(listView1.Items[i].SubItems[1].Text);
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[2].Text != "")
                    sPara.Add(listView1.Items[i].SubItems[2].Text);
                else
                    sPara.Add("None");
            }
            FileStream fs = new FileStream(Path.Combine(dataPath, "data1.data"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw.WriteLine(sName[i]);
                //sName.Add(listView1.Items[i].SubItems[0].Text);
            }
            sw.Close();
            fs.Close();

            //

            FileStream fs1 = new FileStream(Path.Combine(dataPath, "data2.data"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw1 = new StreamWriter(fs1);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw1.WriteLine(sPath[i]);
                //sPath.Add(listView1.Items[i].SubItems[1].Text);
            }
            sw1.Close();
            fs1.Close();

            //
            FileStream fs2 = new FileStream(Path.Combine(dataPath, "data3.data"), FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw2 = new StreamWriter(fs2);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw2.WriteLine(sPara[i]);
                //sPath.Add(listView1.Items[i].SubItems[1].Text);
            }
            sw2.Close();
            fs2.Close();

            listView1.Items.Clear();
            for (int j = 0; j < sName.Count; j++)
            {
                listView1.Items.Add(new ListViewItem(new string[] { sName[j], sPath[j], sPara[j] }));
            }


            if (f2 != null )
            {
                f2.LoadData();
                
            }
            
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (en == true)
                notifyIcon1.ShowBalloonTip(1000, "XShort", "XShort is running in background\nPress Alt + A to open run box", ToolTipIcon.Info);
            else
                notifyIcon1.ShowBalloonTip(1000, "XShort", "XShort đang chạy ẩn\nNhấn Alt + A để mở hộp thoại run", ToolTipIcon.Info);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exit = true;
            Application.Exit();
        }

        private void mainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void runBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f2 != null && f2.IsDisposed != true)
            {
                if (f2.Visible != true)
                {
                    f2.Show();
                    f2.WindowState = FormWindowState.Normal;
                    f2.Activate();
                }
                else
                {
                    f2.Hide();
                }
            }
            else
            {
                if (en == true)
                    f2 = new Form2(1);
                else
                    f2 = new Form2(0);
                f2.Show();
                f2.Activate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://mini102.wordpress.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            tabControl1.SelectedIndex = 1;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            tabControl1.SelectedIndex = 2;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                runBoxToolStripMenuItem_Click(null, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (checkBox1.Checked)
            {
                r1.SetValue("XShort", Application.ExecutablePath);
            }
            else
            {
                r1.DeleteValue("XShort", false);

            }
            r1.Close();
            r1.Dispose();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (checkBox2.Checked)
            {
                r1.SetValue("Hide", true);
            }
            else
            {
                r1.DeleteValue("Hide", false);
            }
            r1.Close();
            r1.Dispose();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (checkBox3.Checked)
            {
                r1.SetValue("ggSearch", true);
            }
            else
            {
                r1.DeleteValue("ggSearch", false);
            }
            r1.Close();
            r1.Dispose();
            if (f2 != null && f2.IsDisposed != true)
            {
                if (checkBox3.Checked)
                {
                    f2.changeGGSeach(true);
                }
                else
                {
                    f2.changeGGSeach(false);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            en = true;
            changeLanguages();
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (radioButton1.Checked)
            {
                r1.SetValue("EN", true);
            }
            else
            {
                r1.DeleteValue("EN", false);
            }
            r1.Close();
            r1.Dispose();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            en = false;
            changeLanguages();
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (radioButton2.Checked)
            {
                r1.SetValue("VI", true);
            }
            else
            {
                r1.DeleteValue("VI", false);
            }
            r1.Close();
            r1.Dispose();
        }

        private void changeLanguages()
        {  
            if (en == false)
            {
                if (f2 != null && f2.IsDisposed != true)
                {
                    f2.changeLanguages(0);
                }

                tabPage1.Text = "Dữ liệu";
                tabPage2.Text = "Cài đặt";
                tabPage3.Text = "Giới thiệu";

                applicationToolStripMenuItem.Text = "Chương trình";
                minimizeToolStripMenuItem.Text = "Ẩn chương trình";
                exitToolStripMenuItem.Text = "Thoát";

                itemsToolStripMenuItem.Text = "Lối tắt";
                addToolStripMenuItem.Text = "Thêm ứng dụng/tệp";
                addURLToolStripMenuItem.Text = "Thêm địa chỉ";
                addDirectoryToolStripMenuItem.Text = "Thêm đường dẫn";
                removeToolStripMenuItem.Text = "Xóa";
                renameToolStripMenuItem.Text = "Đổi tên";
                changePathToolStripMenuItem.Text = "Đổi đường dẫn";
                changeParametersToolStripMenuItem.Text = "Đổi tham số";

                saveToolStripMenuItem.Text = "Lưu";
                saveShortcutsToolStripMenuItem.Text = "Lưu và áp dụng lối tắt";

                listView1.Columns[0].Text = "Tên gọi";
                listView1.Columns[1].Text = "Đường dẫn";
                listView1.Columns[2].Text = "Tham số";

                button3.Text = "Đồng ý";
                button4.Text = "Hủy";

                groupBox1.Text = "Cài đặt chung";
                checkBox1.Text = "Khởi động cùng Windows";
                checkBox2.Text = "Ẩn hộp thoại này khi khởi động";
                checkBox3.Text = "Tự động tìm trên mạng nếu không tìm thấy";
                checkBox4.Text = "Phân biệt hoa thường";
                checkBox5.Text = "Ẩn biểu tượng khay";

                groupBox2.Text = "Ngôn ngữ";
                radioButton1.Text = "Tiếng Anh";
                radioButton2.Text = "Tiếng Việt";

                button1.Text = "Kiểm tra cập nhật";
                button2.Text = "Trang chủ";

                addToolStripMenuItem1.Text = "Xoá";
                renameToolStripMenuItem1.Text = "Đổi tên";
                changePathToolStripMenuItem1.Text = "Đổi đường dẫn";
                changeParametersToolStripMenuItem1.Text = "Đổi tham số";
                checkValidPathToolStripMenuItem.Text = "Kiểm tra đường dẫn";

                aboutToolStripMenuItem.Text = "Giới thiệu";
                mainWindowToolStripMenuItem.Text = "Cửa sổ chính";
                runBoxToolStripMenuItem.Text = "Cửa sổ chạy";
                settingsToolStripMenuItem.Text = "Cài Đặt";
                exitToolStripMenuItem1.Text = "Thoát";

                notifyIcon1.Text = "Nhấn Alt + A hoặc bấm vào để mở";
                toolStripTextBox1.ToolTipText = "Điền địa chỉ sau đó bấm enter để thêm";

            }
            else
            {

                if (f2 != null && f2.IsDisposed != true)
                {
                    f2.changeLanguages(1);
                }

                tabPage1.Text = "Main";
                tabPage2.Text = "Settings";
                tabPage3.Text = "About";

                applicationToolStripMenuItem.Text = "Application";
                minimizeToolStripMenuItem.Text = "Minimize to background";
                exitToolStripMenuItem.Text = "Exit";

                itemsToolStripMenuItem.Text = "Shortcuts";
                addDirectoryToolStripMenuItem.Text = "Add Directory";
                addToolStripMenuItem.Text = "Add App/File";
                addURLToolStripMenuItem.Text = "Add URL";
                removeToolStripMenuItem.Text = "Remove";
                renameToolStripMenuItem.Text = "Rename";
                changePathToolStripMenuItem.Text = "Change path";
                changeParametersToolStripMenuItem.Text = "Change parameters";

                saveToolStripMenuItem.Text = "Save";
                saveShortcutsToolStripMenuItem.Text = "Save and Apply Shortcuts";

                listView1.Columns[0].Text = "Call name";
                listView1.Columns[1].Text = "Path";
                listView1.Columns[2].Text = "Parameters";

                button3.Text = "OK";
                button4.Text = "Cancel";

                groupBox1.Text = "General";
                checkBox1.Text = "Run at Windows startup";
                checkBox2.Text = "Hide this dialog box at startup";
                checkBox3.Text = "Automatically search internet if no data";
                checkBox4.Text = "Case-sensitive";
                checkBox5.Text = "Hide tray icon";

                groupBox2.Text = "Languages";
                radioButton1.Text = "English";
                radioButton2.Text = "Vietnamese";

                button1.Text = "Check for Update";
                button2.Text = "Homepage";

                addToolStripMenuItem1.Text = "Remove";
                renameToolStripMenuItem1.Text = "Rename";
                changePathToolStripMenuItem1.Text = "Change path";
                changeParametersToolStripMenuItem1.Text = "Change paremeters";
                checkValidPathToolStripMenuItem.Text = "Check valid path";

                aboutToolStripMenuItem.Text = "About";
                mainWindowToolStripMenuItem.Text = "Main Window";
                runBoxToolStripMenuItem.Text = "Run Box";
                settingsToolStripMenuItem.Text = "Settings";
                exitToolStripMenuItem1.Text = "Exit";

                notifyIcon1.Text = "Press Alt + A or click to run";
                toolStripTextBox1.ToolTipText = "Type your url here then enter to add";
            }
            
            
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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    try
                    {
                        if (listView1.FocusedItem.SubItems[2].Text != "None" && listView1.FocusedItem.SubItems[2].Text != "Not Available")
                            Process.Start(listView1.FocusedItem.SubItems[1].Text, listView1.FocusedItem.SubItems[2].Text);
                        else
                            Process.Start(listView1.FocusedItem.SubItems[1].Text);
                    }
                    catch
                    {
                        MessageBox.Show("Fail to start! Check if it is a valid path", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit != true)
            {
                e.Cancel = true;
                minimizeToolStripMenuItem_Click(null, null);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (checkBox4.Checked)
            {
                r1.SetValue("Case-sen", true);
            }
            else
            {
                r1.DeleteValue("Case-sen", false);
            }
            r1.Close();
            r1.Dispose();
            if (f2 != null && f2.IsDisposed != true)
            {
                if (checkBox4.Checked)
                {
                    f2.changeSensitive(true);
                }
                else
                {
                    f2.changeSensitive(false);
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            r1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (checkBox5.Checked)
            {
                notifyIcon1.Visible = false;
                r1.SetValue("Tray", true);
            }
            else
            {
                notifyIcon1.Visible = true;
                r1.DeleteValue("Tray", false);
            }
            r1.Close();
            r1.Dispose();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (text.Contains("\\"))
                {
                    for (int i = index + 1; i < dir.Count; i++)
                    {
                        if (dir[i].Contains(text))
                        {
                            textBox1.Text = dir[i];
                            index = i;
                            //select text which not belong to "text"
                            textBox1.SelectionStart = textBox1.Text.IndexOf(text) + text.Length; //index of "text" + length => position to start selection
                            textBox1.SelectionLength = textBox1.Text.Length - text.Length; //length = length of combobox text - length of "text"
                            return;
                        }
                    }
                    textBox1.Text = text;
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    index = -1; //-1 for index + 1 = 0 //fucking nice
                }
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (label2.Text.Contains("path"))
            {
                if (e.KeyCode != Keys.Tab)
                {
                    text = textBox1.Text;
                    index = -1;
                    if (text.Contains("\\")) //if input is a directory or a file
                    {
                        try
                        {
                            string[] dirArray = Directory.GetDirectories(text);
                            for (int i = 0; i < dirArray.Length; i++)
                            {
                                if (((File.GetAttributes(dirArray[i]) & FileAttributes.Hidden) != FileAttributes.Hidden))
                                    dir.Add(dirArray[i].ToLower());
                            }
                            string[] fileArray = Directory.GetFiles(text);
                            for (int i = 0; i < fileArray.Length; i++)
                            {
                                if (((File.GetAttributes(fileArray[i]) & FileAttributes.Hidden) != FileAttributes.Hidden))
                                    dir.Add(fileArray[i]);
                            }

                        }
                        catch //in case user input a pre-directory text
                        {
                            dir.Clear();
                            string cut = text.Substring(0, text.LastIndexOf("\\") + 1); //cut from "text" start from 0 to last index of \ => find all directory, then compare
                            try
                            {
                                string[] dirArray = Directory.GetDirectories(cut);
                                for (int i = 0; i < dirArray.Length; i++)
                                {
                                    if (((File.GetAttributes(dirArray[i]) & FileAttributes.Hidden) != FileAttributes.Hidden))
                                        dir.Add(dirArray[i].ToLower());
                                }
                                string[] fileArray = Directory.GetFiles(cut);
                                for (int i = 0; i < fileArray.Length; i++)
                                {
                                    if (((File.GetAttributes(fileArray[i]) & FileAttributes.Hidden) != FileAttributes.Hidden))
                                        dir.Add(fileArray[i]);
                                }

                            }
                            catch
                            {
                                dir.Clear();
                            }
                        }

                    }

                }
            }
            if (e.KeyCode == Keys.Left) //set pointer to end of text 
            {
                if (textBox1.SelectionStart == 0)
                    textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void checkValidPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem.SubItems[1].Text.Contains("\\"))
            {
                if (File.Exists(listView1.FocusedItem.SubItems[1].Text))
                {
                    if (en)
                        MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " is a valid path.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " là một đường dẫn có tồn tại.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (Directory.Exists(listView1.FocusedItem.SubItems[1].Text))
                    {
                        if (en)
                            MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " is a valid path.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " là một đường dẫn có tồn tại.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (en)
                        {
                            if (MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " is an invalid path.\nDo you want to remove it from list now?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                listView1.FocusedItem.Remove();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show(listView1.FocusedItem.SubItems[1].Text + " là một đường dẫn không tồn tại.\nBạn có muốn xóa nó khỏi danh sách ngay bây giờ không?", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                listView1.FocusedItem.Remove();
                            }
                        }
                    }
                }
            }
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
            {
                if (textBox1.SelectionLength != 0)
                {
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
        }
    }

    //this class for sort listview
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

    //this class for 1 instance
    internal class NativeMethods
    {
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
    }
}
