﻿using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace XShort
{
    public partial class Form2 : Form
    {
        int index = 0;
        List<String> dir = new List<String>();
        List<String> sName = new List<String>();
        List<String> sPath = new List<String>();
        List<String> sPara = new List<String>();
        RegistryKey r;
        string dataPath;
        bool ggSearch = true;
        bool csen = false;
        string text = String.Empty;
        int newx, newy;
       

        public Form2(int en)
        {
            InitializeComponent();
            dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "XShort");
            LoadData();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\XShort\\Data");
            if (r.GetValue("Left") != null)
            {
                Top = (int)r.GetValue("Top");
                Left = (int)r.GetValue("Left");
            }
            if (r.GetValue("ggSearch") != null)
            {
                ggSearch = true;
            }
            else
            {
                ggSearch = false;
            }
            if (r.GetValue("Case-sen") != null)
            {
                csen = true;
            }
            else
            {
                csen = false;
            }
            r.Close();
            r.Dispose();

            comboBox1.Focus();
            comboBox1.SelectAll();
            if (en == 0)
            {
                label1.Text = "Mở ứng dụng/đường dẫn/địa chỉ (kể cả không có dữ liệu)";
                label2.Text = "Không cần điền tên đầy đủ, ứng dụng sẽ nhận diện tự động";
                button2.Text = "Hủy";
            }


           
        }


        public void changeGGSeach(bool gg)
        {
            ggSearch = gg;
        }

        public void changeSensitive(bool Csen)
        {
            csen = Csen;
        }

        public void changeLanguages(int en)
        {
            if (en == 0)
            {
                label1.Text = "Mở ứng dụng/đường dẫn/địa chỉ (kể cả không có dữ liệu)";
                label2.Text = "Không cần điền tên đầy đủ, ứng dụng sẽ nhận diện tự động";
                button2.Text = "Hủy";
            }
            else
            {
                label1.Text = "Open app/directory/url (even not in database):";
                label2.Text = "No need to write full call name, app will detect automatically";
                button2.Text = "Cancel";
            }
        }

       
        public int LoadData()
        {
            comboBox1.Items.Clear();
            sName.Clear();
            sPara.Clear(); //fucking forget to clear haha
            sPath.Clear();

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

            
            for (int i = 0; i < sName.Count; i++)
            {
                comboBox1.Items.Add(sName[i]);
            }
            
            return 1;

        }

        private void Run(string tmp)
        {
            if (comboBox1.Text == String.Empty) //do nothing if comboBox is empty
                return;
            if (tmp.Contains("!") != true)
            {
                for (int i = 0; i < sName.Count; i++)
                {
                    if (csen == true) //if case-sensitive is true
                    {
                        if (tmp == sName[i])
                        {
                            try
                            {
                                if (sPara[i] == "None" || sPara[i] == "Not Available")
                                    Process.Start(sPath[i]);
                                else
                                    Process.Start(sPath[i], sPara[i]);
                                this.Hide();
                            }
                            catch
                            {
                                return;
                            }
                            return;
                        }
                    }
                    else
                    {
                        if (tmp.ToLower() == sName[i].ToLower())
                        {
                            try
                            {
                                if (sPara[i] == "None" || sPara[i] == "Not Available")
                                    Process.Start(sPath[i]);
                                else
                                    Process.Start(sPath[i], sPara[i]);
                                this.Hide();
                            }
                            catch
                            {
                                return;
                            }
                            return;
                        }
                    }
                }
                for (int i = 0; i < sName.Count; i++)
                {
                    if (sName[i].Contains(tmp))
                    {
                        try
                        {
                            if (sPara[i] == "None" || sPara[i] == "Not Available")
                                Process.Start(sPath[i]);
                            else
                                Process.Start(sPath[i], sPara[i]);
                            this.Hide();
                        }
                        catch
                        {
                            return;
                        }
                        return;
                    }
                }

                for (int i = 0; i < sPath.Count; i++)
                {
                    if (sPath[i].Contains(tmp))
                    {
                        try
                        {
                            if (sPara[i] == "None" || sPara[i] == "Not Available")
                                Process.Start(sPath[i]);
                            else
                                Process.Start(sPath[i], sPara[i]);
                            this.Hide();
                        }
                        catch
                        {
                            return;
                        }
                        return;
                    }
                }
            }
            else //if contain !
            {
                string tmp2;
                if (tmp.Contains(" !"))
                    tmp2 = tmp.Substring(0, tmp.LastIndexOf("!") - 1);
                else
                    tmp2 = tmp.Substring(0, tmp.LastIndexOf("!"));
                string tmp3 = tmp.Substring(tmp.LastIndexOf("!") + 1);


                for (int i = 0; i < sName.Count; i++)
                {
                    if (csen == true)
                    {
                        if (tmp2 == sName[i])
                        {
                            try
                            {
                                if (sPara[i] != "Not Available")
                                    Process.Start(sPath[i], tmp3);
                                else
                                    Process.Start(sPath[i]);
                                this.Hide();
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (tmp2.ToLower() == sName[i].ToLower())
                        {
                            try
                            {
                                if (sPara[i] != "Not Available")
                                    Process.Start(sPath[i], tmp3);
                                else
                                    Process.Start(sPath[i]);
                                this.Hide();
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }
                }
                for (int i = 0; i < sName.Count; i++)
                {
                    if (sName[i].Contains(tmp2))
                    {
                        try
                        {
                            if (sPara[i] != "Not Available")
                                Process.Start(sPath[i], tmp3);
                            else
                                Process.Start(sPath[i]);
                            this.Hide();
                        }
                        catch
                        {
                            return;
                        }
                        return;
                    }
                }

                for (int i = 0; i < sPath.Count; i++)
                {
                    if (sPath[i].Contains(tmp2))
                    {
                        try
                        {
                            if (sPara[i] != "Not Available")
                                Process.Start(sPath[i], tmp3);
                            else
                                Process.Start(sPath[i]);
                            this.Hide();
                        }
                        catch
                        {
                            return;
                        }
                        return;
                    }
                }
            }
            if (tmp.Contains("\\"))
            {
                try
                {
                    Process.Start(tmp);
                    this.Hide();
                    return;
                }
                catch
                {
                    ///
                }

            }

            if (tmp.Contains("/") || tmp.Contains("."))
            {
                tmp = "http://" + tmp;
                try
                {
                    Process.Start(tmp);
                    this.Hide();
                    return;
                }
                catch
                {
                    ///
                }
            }
            if (ggSearch == true)
            {
                try
                {
                    Process.Start("https://www.google.com/search?q=" + tmp);
                    this.Hide();
                    return;
                }
                catch
                {
                    //
                }
            }
            MessageBox.Show("Unavailable shortcut name!?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Run(comboBox1.Text);
            comboBox1.Text = String.Empty;  
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\XShort\\Data", true);
            if (r == null)
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\XShort\\Data");
            r.SetValue("Left", this.Left);
            r.SetValue("Top", this.Top);
            r.Close();
            r.Dispose();

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            comboBox1.Focus();
            comboBox1.SelectAll();
        }


        private void comboBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (text.Contains("\\") != true)
                {
                    for (int i = index + 1; i < sName.Count; i++)
                    {
                        if (sName[i].Contains(text))
                        {
                            comboBox1.Text = sName[i];
                            index = i;
                            comboBox1.SelectionStart = comboBox1.Text.Length; //set pointer to the end of text
                            comboBox1.SelectionLength = 0;
                            return;
                        }
                    }
                    comboBox1.Text = text;
                    comboBox1.SelectionStart = comboBox1.Text.Length;
                    comboBox1.SelectionLength = 0;
                    index = -1; //-1 for index + 1 = 0 //fucking nice
                }
                else if (text.Contains("\\"))
                {
                    for (int i = index + 1; i < dir.Count; i++)
                    {
                        if (dir[i].Contains(text))
                        {
                            comboBox1.Text = dir[i];
                            index = i;
                            //select text which not belong to "text"
                            comboBox1.SelectionStart = comboBox1.Text.IndexOf(text) + text.Length; //index of "text" + length => position to start selection
                            comboBox1.SelectionLength = comboBox1.Text.Length - text.Length; //length = length of combobox text - length of "text"
                            return;
                        }
                    }
                    comboBox1.Text = text;
                    comboBox1.SelectionStart = comboBox1.Text.Length;
                    comboBox1.SelectionLength = 0;
                    index = -1; //-1 for index + 1 = 0 //fucking nice
                }
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                text = comboBox1.Text;
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
            if (e.KeyCode == Keys.Left) //set pointer to end of text 
            {
                if (comboBox1.SelectionStart == 0)
                    comboBox1.SelectionStart = comboBox1.Text.Length;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {  
            //comboBox1.DroppedDown = true;
            if (e.KeyCode != Keys.Back)
            {
                if (comboBox1.SelectionLength != 0)
                {
                    comboBox1.SelectionStart = comboBox1.Text.Length;
                }
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
