using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using MyNotepad;
using SNote;
using System.Diagnostics;
using System.ComponentModel;

namespace Test
{
    public partial class Main : Form
    {
        private bool saved = true;
        private bool opened = false;
        private RegistryKey r;
        private About ab = new About();
        private Search f;
        private TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
        private string fileName, filePath, trueName;
        public Main()
        {
            InitializeComponent();
            f = new Search(richTextBoxMain);
            f.Hide();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
            if (r == null)
            {
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\SNote\\Data");
            }
            var font = r.GetValue("Font");
            if (font != null)
            {
                richTextBoxMain.Font = (Font)converter.ConvertFromString(font.ToString());
            }
            if (r.GetValue("Wordwrap") != null)
            {
                richTextBoxMain.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                richTextBoxMain.WordWrap = true;
            }
            else
            {
                richTextBoxMain.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                richTextBoxMain.WordWrap = false;
            }
            if (r.GetValue("Dark") != null)
            {
                darkBackgroundToolStripMenuItem.Checked = true;
                richTextBoxMain.BackColor = Color.FromArgb(30, 30, 30);
                richTextBoxMain.ForeColor = SystemColors.ControlDark;
                menuStrip1.BackColor = richTextBoxMain.BackColor;
                menuStrip1.ForeColor = richTextBoxMain.ForeColor;
                labelStatus.BackColor = menuStrip1.BackColor;
                labelStatus.ForeColor = menuStrip1.ForeColor;
                whiteBackgroundToolStripMenuItem.Checked = false;
                saved = true;
            }
            r.Close();
            richTextBoxMain.DetectUrls = true;
            richTextBoxMain.AllowDrop = true;   
            richTextBoxMain.DragEnter += new DragEventHandler(richText1_DragEnter);
            richTextBoxMain.DragDrop += new DragEventHandler(richText1_DragDrop);
        }

        void GotoLine(int wantedLine_zero_based) // int wantedLine_zero_based = wanted line number; 1st line = 0
        {
            try
            {
                int index = this.richTextBoxMain.GetFirstCharIndexFromLine(wantedLine_zero_based);
                this.richTextBoxMain.Select(index, 0);
                this.richTextBoxMain.ScrollToCaret();
            }
            catch { }
        }

        private void richText1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else 
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
        }
        
        private void richText1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                int i;
                String s;
                
                // Get start position to drop the text.
                i = richTextBoxMain.SelectionStart;
                s = richTextBoxMain.Text.Substring(i);
                richTextBoxMain.Text = richTextBoxMain.Text.Substring(0, i);

                // Drop the text on to the RichTextBox.
                richTextBoxMain.Text = richTextBoxMain.Text +
                   e.Data.GetData(DataFormats.Text).ToString();
                richTextBoxMain.Text = richTextBoxMain.Text + s;
                saved = false;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(docPath[0]))
                {
                    
                    fileName = docPath[0];          //do not delete this
                    if (OpenFile(docPath[0]) == true)
                    {
                        saved = true;
                    }
                      
                }
            }
           
        }

        
       
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Program.FileName != "")
            {
                if (OpenFile(Program.FileName) == true)
                {
                    opened = true;
                    filePath = Program.FileName;
                    fileName = filePath;
                    trueName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    this.Text = trueName + " - SNote";
                }
            }
            else
            {
                filePath = String.Empty;
                fileName = "Untittled";
            }
            
        }

       

        private bool OpenFile(string fileName2)
        {
            FileStream fs;
            StreamReader sr;
            try
            {
                fs = new FileStream(fileName2, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            richTextBoxMain.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            opened = true;
            saved = true;
            fileName = fileName2;
            trueName = fileName2.Substring(fileName2.LastIndexOf("\\")+1);
            this.Text = trueName + " - SNote";
            FileInfo fi = new FileInfo(fileName2);
            if (fi.IsReadOnly == true)
            {
                MessageBox.Show(fileName2 + " is read only!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxMain.ReadOnly = true;
            }
            else
                richTextBoxMain.ReadOnly = false;

            return true;
            
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saved != true)
            {
                if (opened == true)
                    saveToolStripMenuItem_Click(null, null);
                else
                {
                    if (MessageBox.Show("Do you want to save the change you made in " + fileName, "Save your file first", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
                    {
                        saveToolStripMenuItem_Click(null, null);
                    }
                }
            }
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
            op.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            op.Title = "Select file to open";
            if (op.ShowDialog() == DialogResult.OK)
            {
                richTextBoxMain.Text = "";
                
                filePath = op.FileName;
                fileName = filePath;

                if (OpenFile(filePath) == true)
                {
                    trueName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    this.Text = trueName + " - SNote";
                }

            }
            else
            {
                if (opened != true)
                    this.Text = "Untitled";
            }
                
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opened != true)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
                sf.DefaultExt = "txt";
                sf.OverwritePrompt = true;
                sf.RestoreDirectory = true;
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    filePath = sf.FileName;
                    fileName = filePath;
                    try
                    {
                        File.WriteAllText(filePath, richTextBoxMain.Text);
                        saved = true;
                        trueName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                        this.Text = trueName + " - SNote";
                        opened = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.IsReadOnly == true)
                {
                    saved = true;
                    MessageBox.Show(fileName + " is read only!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    File.WriteAllText(fileName, richTextBoxMain.Text);
                    saved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
            sf.DefaultExt = "txt";
            sf.OverwritePrompt = true;
            sf.RestoreDirectory = true;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                filePath = sf.FileName;

                try
                {
                    File.WriteAllText(filePath, richTextBoxMain.Text);
                    saved = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (opened != true)
                    this.Text = "Untitled";
            }
                
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Undo();
            
        }

      

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved != true)
            {
                Process.Start(Application.ExecutablePath);
                return;
            }
            richTextBoxMain.Text = "";
            richTextBoxMain.ReadOnly = false;
            opened = false;
            saved = false;
            this.Text = "Untitled";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
            if (wordWrapToolStripMenuItem.Checked != true)
            {
                richTextBoxMain.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                r.SetValue("Wordwrap", true);
            }
            else
            {
                richTextBoxMain.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                r.DeleteValue("Wordwrap", false);
            }
            r.Close();

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = richTextBoxMain.Font;
            if (font.ShowDialog() == DialogResult.OK)
            {
                richTextBoxMain.Font = font.Font;
                using (var r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true))
                {
                   r.SetValue("Font", converter.ConvertToString(richTextBoxMain.Font));
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ab.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text.Contains("Untitled") != true)
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.IsReadOnly == true)
                {
                    saved = true;
                    Application.Exit();
                }
            }
            
            if (saved != true)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save " + this.Text, "SNote", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (opened != true)
                    {
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
                        sf.DefaultExt = "txt";
                        sf.OverwritePrompt = true;
                        sf.RestoreDirectory = true;
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            filePath = sf.FileName;
                            try
                            {
                                File.WriteAllText(filePath, richTextBoxMain.Text);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                    else
                    {

                        try
                        {
                            File.WriteAllText(fileName, richTextBoxMain.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    saved = true;
                }
                else if  (dialogResult == DialogResult.No)
                {
                    saved = true;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

      
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Paste();
            
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Redo();
        }
        
        private void uPPERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectedText = richTextBoxMain.SelectedText.ToUpper();
          
        }

        private void lowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxMain.SelectedText = richTextBoxMain.SelectedText.ToLower();
        }



        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxMain.SelectedText != "")
                richTextBoxMain.SelectedText = "";
            else
            {
                richTextBoxMain.Select(richTextBoxMain.SelectionStart, 1);
                richTextBoxMain.SelectedText = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            
        }

        

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
             f.ShowFind(false, richTextBoxMain.SelectedText);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            f.ShowFind(true, richTextBoxMain.SelectedText);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Copy();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Cut();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxMain.Paste();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int line = 1 + richTextBoxMain.GetLineFromCharIndex(richTextBoxMain.GetFirstCharIndexOfCurrentLine());
            int column = 1 + richTextBoxMain.SelectionStart - richTextBoxMain.GetFirstCharIndexOfCurrentLine();
            labelStatus.Text = "Line: " + line.ToString() + ", Column: " + column.ToString();
            if (richTextBoxMain.CanUndo != true)
            {
                undoToolStripMenuItem.Enabled = false;
                saved = true;
            }
            else
                undoToolStripMenuItem.Enabled = true;
            if (richTextBoxMain.CanRedo != true)
                redoToolStripMenuItem.Enabled = false;
            else
                redoToolStripMenuItem.Enabled = true;

            if (saved != true)
            {
                labelStatus.Text += " - Not save yet ";
                
            }
            else
            {
                labelStatus.Text += " - Saved ";
                
            }
            
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (MessageBox.Show("Do you want to go to this link: " + e.LinkText, "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.Start(e.LinkText);
            }
        }

        private void uPPERCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uPPERToolStripMenuItem_Click(null, null);
        }

        private void lowerCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowerToolStripMenuItem_Click(null, null);
        }

        //shift+tab
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (richTextBoxMain.Focused && keyData == (Keys.Tab | Keys.Shift))
            {
                richTextBoxMain.Select(richTextBoxMain.SelectionStart, -1);
                richTextBoxMain.SelectedText = "";
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void whiteBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (whiteBackgroundToolStripMenuItem.Checked != true)
            {
                whiteBackgroundToolStripMenuItem.Checked = true;
                richTextBoxMain.BackColor = Color.White;
                richTextBoxMain.ForeColor = Color.Black;
                menuStrip1.BackColor = richTextBoxMain.BackColor;
                menuStrip1.ForeColor = richTextBoxMain.ForeColor;
                labelStatus.BackColor = menuStrip1.BackColor;
                labelStatus.ForeColor = menuStrip1.ForeColor;
                darkBackgroundToolStripMenuItem.Checked = false;
                saved = true;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
                r.DeleteValue("Dark", false);
                r.Close();
            }
            
        }

        private void goToLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Goto go = new Goto(richTextBoxMain.Lines.Length);
            go.FormClosed += Go_FormClosed;
            go.ShowDialog();
        }

        private void Go_FormClosed(object sender, FormClosedEventArgs e)
        {
            Goto go = (Goto)sender;
            if (go.line > 1)
                GotoLine(go.line - 1);
        }

        private void darkBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (darkBackgroundToolStripMenuItem.Checked != true)
            {
                darkBackgroundToolStripMenuItem.Checked = true;
                richTextBoxMain.BackColor = Color.FromArgb(30, 30, 30);
                richTextBoxMain.ForeColor = SystemColors.ControlDark;
                menuStrip1.BackColor = richTextBoxMain.BackColor;
                menuStrip1.ForeColor = richTextBoxMain.ForeColor;
                labelStatus.BackColor = menuStrip1.BackColor;
                labelStatus.ForeColor = menuStrip1.ForeColor;
                whiteBackgroundToolStripMenuItem.Checked = false;
                saved = true;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
                r.SetValue("Dark", true);
                r.Close();
            }
        }

        void FindKeyWords(RichTextBox richText, string text, bool matchCase, bool matchWholeWord, bool upDirection)
        {
            richText.SelectionStart = 0;
            int index;
            RichTextBoxFinds options = RichTextBoxFinds.None;
            if (matchCase)
                options |= RichTextBoxFinds.MatchCase;
            if (matchWholeWord)
                options |= RichTextBoxFinds.WholeWord;
            if (upDirection)
                options |= RichTextBoxFinds.Reverse;


            do
            {
                if (upDirection)
                    index = richText.Find(text, 0, richText.SelectionStart, options);
                else
                    index = richText.Find(text, richText.SelectionStart + richText.SelectionLength, options);

                if (index >= 0)
                {
                    richText.SelectionStart = index;
                    richText.SelectionLength = text.Length;
                    //if (richText.SelectionColor != Color.Blue)
                    richText.SelectionColor = Color.Blue;
                    //saved = true;
                }
            }
            while (index >= 0);
            
        }   
    }
}