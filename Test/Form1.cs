using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using MyNotepad;
using SNote;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management;
using System.Configuration;

namespace Test
{
    public partial class Form1 : Form
    {
        bool saved = true;
        bool opened = false;
        RegistryKey r;
        AboutBox2 ab = new AboutBox2();
        Form2 f;
        
        public Form1()
        {
            InitializeComponent();
            
            f = new Form2(richTextBox1);
            f.Hide();
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
            if (r == null)
            {
                r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\SNote\\Data");
            }

            if (r.GetValue("Wordwrap") != null)
            {
                richTextBox1.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                richTextBox1.WordWrap = true;
            }
            else
            {
                richTextBox1.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                richTextBox1.WordWrap = false;
            }
            if (r.GetValue("Dark") != null)
            {
                darkBackgroundToolStripMenuItem.Checked = true;
                richTextBox1.BackColor = Color.FromArgb(45, 45, 45);
                richTextBox1.ForeColor = Color.White;
                menuStrip1.BackColor = Color.FromArgb(45, 45, 45);
                menuStrip1.ForeColor = Color.White;
                whiteBackgroundToolStripMenuItem.Checked = false;
                saved = true;
            }
           
            r.Close();
            this.Text = "Untitled" + " - SNote"; 
            richTextBox1.DetectUrls = true;
            richTextBox1.AllowDrop = true;   
            richTextBox1.DragEnter += new DragEventHandler(richText1_DragEnter);
            richTextBox1.DragDrop += new DragEventHandler(richText1_DragDrop);
            //this.Size = new Size(global::SNote.Properties.Settings.Default.W, global::SNote.Properties.Settings.Default.H);
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
                i = richTextBox1.SelectionStart;
                s = richTextBox1.Text.Substring(i);
                richTextBox1.Text = richTextBox1.Text.Substring(0, i);

                // Drop the text on to the RichTextBox.
                richTextBox1.Text = richTextBox1.Text +
                   e.Data.GetData(DataFormats.Text).ToString();
                richTextBox1.Text = richTextBox1.Text + s;
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

        string fileName, filePath, trueName;
       
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
            richTextBox1.Text = sr.ReadToEnd();
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
                richTextBox1.ReadOnly = true;
            }
            else
                richTextBox1.ReadOnly = false;

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
                richTextBox1.Text = "";
                
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
                    this.Text = "Untitled - SNote";
            }
                
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opened != true)
            {
                SaveFileDialogWithEncoding sf = new SaveFileDialogWithEncoding();
                sf.DefaultExt = "txt";
                sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
                sf.EncodingType = EncodingType.Unicode;
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    filePath = sf.FileName;
                    fileName = filePath;
                    try
                    {
                        File.WriteAllText(filePath, richTextBox1.Text);
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
                    File.WriteAllText(fileName, richTextBox1.Text);
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
            SaveFileDialogWithEncoding sf = new SaveFileDialogWithEncoding();
            sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
            sf.DefaultExt = "txt";
            sf.EncodingType = EncodingType.Unicode;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                filePath = sf.FileName;

                try
                {
                    File.WriteAllText(filePath, richTextBox1.Text);
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
                    this.Text = "Untitled - SNote";
            }
                
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            
        }

      

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved != true)
            {
                Process.Start(Application.ExecutablePath);
                return;
            }
            richTextBox1.Text = "";
            richTextBox1.ReadOnly = false;
            opened = false;
            saved = false;
            this.Text = "Untitled" + " - SNote";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
            if (wordWrapToolStripMenuItem.Checked != true)
            {
                richTextBox1.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                r.SetValue("Wordwrap", true);
            }
            else
            {
                richTextBox1.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                r.DeleteValue("Wordwrap", false);
            }
            r.Close();

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = richTextBox1.Font;
            if (font.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = font.Font;
                //richTextBox1.ForeColor = font.Color;
              
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
                if (MessageBox.Show("Do you want to save " + this.Text, "SNote", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (opened != true)
                    {
                        SaveFileDialogWithEncoding sf = new SaveFileDialogWithEncoding();
                        sf.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
                        sf.DefaultExt = "txt";
                        sf.EncodingType = EncodingType.Unicode;
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            filePath = sf.FileName;
                            try
                            {
                                File.WriteAllText(filePath, richTextBox1.Text);
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
                            File.WriteAllText(fileName, richTextBox1.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    
                }
                saved = true;
            }
           
            Application.Exit();
        }

      
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }
        
        private void uPPERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = richTextBox1.SelectedText.ToUpper();
          
        }

        private void lowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = richTextBox1.SelectedText.ToLower();
        }



        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
                richTextBox1.SelectedText = "";
            else
            {
                richTextBox1.Select(richTextBox1.SelectionStart, 1);
                richTextBox1.SelectedText = "";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            
        }

        

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
             f.ShowFind(false, richTextBox1.SelectedText);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            f.ShowFind(true, richTextBox1.SelectedText);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int line = 1 + richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            int column = 1 + richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine();
            label1.Text = "Line: " + line.ToString() + ", Column: " + column.ToString();
            if (richTextBox1.CanUndo != true)
            {
                undoToolStripMenuItem.Enabled = false;
                saved = true;
            }
            else
                undoToolStripMenuItem.Enabled = true;
            if (richTextBox1.CanRedo != true)
                redoToolStripMenuItem.Enabled = false;
            else
                redoToolStripMenuItem.Enabled = true;

            if (saved != true)
            {
                label1.Text += " - Not save yet ";
                
            }
            else
            {
                label1.Text += " - Saved ";
                
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
            if (richTextBox1.Focused && keyData == (Keys.Tab | Keys.Shift))
            {
                richTextBox1.Select(richTextBox1.SelectionStart, -1);
                richTextBox1.SelectedText = "";
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void whiteBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (whiteBackgroundToolStripMenuItem.Checked != true)
            {
                whiteBackgroundToolStripMenuItem.Checked = true;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                darkBackgroundToolStripMenuItem.Checked = false;
                saved = true;
                r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\SNote\\Data", true);
                r.DeleteValue("Dark", false);
                r.Close();
            }
            
        }

        private void darkBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (darkBackgroundToolStripMenuItem.Checked != true)
            {
                darkBackgroundToolStripMenuItem.Checked = true;
                richTextBox1.BackColor = Color.FromArgb(45, 45, 45);
                richTextBox1.ForeColor = Color.White;
                menuStrip1.BackColor = Color.FromArgb(45, 45, 45);
                menuStrip1.ForeColor = Color.White;
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

//encode

public enum EncodingType
{
    UTF8 = 0,
    UTF8WithPreamble,
    Unicode,
    Ansi
}

public class SaveFileDialogWithEncoding
{
	private delegate int OFNHookProcDelegate(int hdlg, int msg, int wParam, int lParam);

	private int m_LabelHandle=0;
	private int m_ComboHandle=0;

	private string m_Filter="";
	private string m_DefaultExt="";

	private string m_FileName="";

	private EncodingType m_EncodingType;
	private Screen m_ActiveScreen;

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	private struct OPENFILENAME
	{
		public int lStructSize; 
		public IntPtr hwndOwner; 
		public int hInstance; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrFilter;
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrCustomFilter; 
		public int nMaxCustFilter; 
		public int nFilterIndex; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrFile; 
		public int nMaxFile; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrFileTitle; 
		public int nMaxFileTitle; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrInitialDir; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrTitle; 
		public int Flags; 
		public short nFileOffset; 
		public short nFileExtension; 
		[MarshalAs(UnmanagedType.LPTStr)] public string lpstrDefExt; 
		public int lCustData; 
		public OFNHookProcDelegate lpfnHook;
		[MarshalAs(UnmanagedType.LPTStr)] public string lpTemplateName;
		//only if on nt 5.0 or higher
		public int pvReserved;
		public int dwReserved;
		public int FlagsEx;
	}

	[DllImport("Comdlg32.dll", CharSet=CharSet.Auto, SetLastError=true)]
	private static extern bool GetSaveFileName(ref OPENFILENAME lpofn);

	[DllImport("Comdlg32.dll")]
	private static extern int CommDlgExtendedError();

	[DllImport("user32.dll")]
	private static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	private struct RECT
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	private struct POINT
	{
		public int X;
		public int Y;
	}

	private struct NMHDR 
	{
		public int HwndFrom;
		public int IdFrom;
		public int Code;
	}

	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(int hWnd, ref RECT lpRect);

	[DllImport("user32.dll")]
	private static extern int GetParent(int hWnd);

	[DllImport("user32.dll", CharSet=CharSet.Auto)]
	private static extern bool SetWindowText(int hWnd, string lpString);

	[DllImport("user32.dll")]
	private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

	[DllImport("user32.dll", CharSet=CharSet.Auto)]
	private static extern int SendMessage(int hWnd, int Msg, int wParam, string lParam);

	[DllImport("user32.dll")]
	private static extern bool DestroyWindow(int hwnd);

	private const int OFN_ENABLEHOOK=0x00000020;
	private const int OFN_EXPLORER=0x00080000;
	private const int OFN_FILEMUSTEXIST=0x00001000;
	private const int OFN_HIDEREADONLY=0x00000004;
	private const int OFN_CREATEPROMPT=0x00002000;
	private const int OFN_NOTESTFILECREATE=0x00010000;
	private const int OFN_OVERWRITEPROMPT=0x00000002;
	private const int OFN_PATHMUSTEXIST=0x00000800;

	private const int SWP_NOSIZE=0x0001;
	private const int SWP_NOMOVE=0x0002;
	private const int SWP_NOZORDER=0x0004;

	private const int WM_INITDIALOG=0x110;
	private const int WM_DESTROY=0x2;
	private const int WM_SETFONT=0x0030;
	private const int WM_GETFONT=0x0031;

	private const int CBS_DROPDOWNLIST=0x0003;
	private const int CBS_HASSTRINGS=0x0200;
	private const int CB_ADDSTRING=0x0143;
	private const int CB_SETCURSEL=0x014E;
	private const int CB_GETCURSEL=0x0147;

	private const uint WS_VISIBLE=0x10000000;
	private const uint WS_CHILD=0x40000000;
	private const uint WS_TABSTOP=0x00010000;

	private const int CDN_FILEOK=-606;
	private const int WM_NOTIFY=0x004E;

	[DllImport("user32.dll", CharSet=CharSet.Auto)]
	private static extern int GetDlgItem(int hDlg, int nIDDlgItem);

	[DllImport("user32.dll", CharSet=CharSet.Auto)]
	private static extern int CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int hMenu, int hInstance, int lpParam);

	[DllImport("user32.dll")]
	private static extern bool ScreenToClient(int hWnd, ref POINT lpPoint);

	private int HookProc(int hdlg, int msg, int wParam, int lParam)
	{
		switch (msg)
		{
			case WM_INITDIALOG:

				//we need to centre the dialog
				Rectangle sr=m_ActiveScreen.Bounds;
				RECT cr=new RECT();
				int parent=GetParent(hdlg);
				GetWindowRect(parent, ref cr);

				int x=(sr.Right + sr.Left - (cr.Right-cr.Left))/2;
				int y=(sr.Bottom + sr.Top - (cr.Bottom-cr.Top))/2;

				SetWindowPos(parent, 0, x, y, cr.Right-cr.Left, cr.Bottom - cr.Top + 32, SWP_NOZORDER);
				

				//we need to find the label to position our new label under

				int fileTypeWindow=GetDlgItem(parent, 0x441);

				RECT aboveRect=new RECT();
				GetWindowRect(fileTypeWindow, ref aboveRect);

				//now convert the label's screen co-ordinates to client co-ordinates
				POINT point=new POINT();
				point.X=aboveRect.Left;
				point.Y=aboveRect.Bottom;

				ScreenToClient(parent, ref point);

				//create the label
				int labelHandle=CreateWindowEx(0, "STATIC", "mylabel", WS_VISIBLE | WS_CHILD | WS_TABSTOP, point.X, point.Y + 12, 200, 100, parent, 0, 0, 0);
				SetWindowText(labelHandle, "&Encoding:");

				int fontHandle=SendMessage(fileTypeWindow, WM_GETFONT, 0, 0);
				SendMessage(labelHandle, WM_SETFONT, fontHandle, 0);

				//we now need to find the combo-box to position the new combo-box under

				int fileComboWindow=GetDlgItem(parent, 0x470);
				aboveRect=new RECT();
				GetWindowRect(fileComboWindow, ref aboveRect);

				point=new POINT();
				point.X=aboveRect.Left;
				point.Y=aboveRect.Bottom;
				ScreenToClient(parent, ref point);

				POINT rightPoint=new POINT();
				rightPoint.X=aboveRect.Right;
				rightPoint.Y=aboveRect.Top;

				ScreenToClient(parent, ref rightPoint);

				//we create the new combobox

				int comboHandle=CreateWindowEx(0, "ComboBox", "mycombobox", WS_VISIBLE | WS_CHILD | CBS_HASSTRINGS | CBS_DROPDOWNLIST | WS_TABSTOP, point.X, point.Y + 8, rightPoint.X-point.X, 100, parent, 0, 0, 0);
				SendMessage(comboHandle, WM_SETFONT, fontHandle, 0);

				//and add the encodings we want to offer
				SendMessage(comboHandle, CB_ADDSTRING, 0, "UTF-8");
				SendMessage(comboHandle, CB_ADDSTRING, 0, "UTF-8 with preamble");
				SendMessage(comboHandle, CB_ADDSTRING, 0, "Unicode");
				SendMessage(comboHandle, CB_ADDSTRING, 0, "ANSI");
				
				SendMessage(comboHandle, CB_SETCURSEL, (int)m_EncodingType, 0);

				//remember the handles of the controls we have created so we can destroy them after
				m_LabelHandle=labelHandle;
				m_ComboHandle=comboHandle;

				break;
			case WM_DESTROY:
				//destroy the handles we have created
				if (m_ComboHandle!=0)
				{
					DestroyWindow(m_ComboHandle);
				}

				if (m_LabelHandle!=0)
				{
					DestroyWindow(m_LabelHandle);
				}
				break;
			case WM_NOTIFY:

				//we need to intercept the CDN_FILEOK message
				//which is sent when the user selects a filename

				NMHDR nmhdr=(NMHDR)Marshal.PtrToStructure(new IntPtr(lParam), typeof(NMHDR));

				if (nmhdr.Code==CDN_FILEOK)
				{
					//a file has been selected
					//we need to get the encoding

					m_EncodingType=(EncodingType)SendMessage(m_ComboHandle, CB_GETCURSEL, 0, 0);
				}
				break;

		}
		return 0;
	}

	public string DefaultExt
	{
		get
		{
			return m_DefaultExt;
		}
		set
		{
			m_DefaultExt=value;
		}
	}

	public string Filter
	{
		get
		{
			return m_Filter;
		}
		set
		{
			m_Filter=value;
		}
	}

	public string FileName
	{
		get
		{
			return m_FileName;
		}
		set
		{
			m_FileName=value;
		}
	}

	public EncodingType EncodingType
	{
		get
		{
			return m_EncodingType;
		}
		set
		{
			m_EncodingType=value;
		}
	}

	public DialogResult ShowDialog()
	{

		//set up the struct and populate it

		OPENFILENAME ofn=new OPENFILENAME();

		ofn.lStructSize= Marshal.SizeOf( ofn );
		ofn.lpstrFilter= m_Filter.Replace('|', '\0') + '\0';

		ofn.lpstrFile = m_FileName + new string(' ', 512);
		ofn.nMaxFile= ofn.lpstrFile.Length;
		ofn.lpstrFileTitle= System.IO.Path.GetFileName(m_FileName) + new string(' ', 512);
		ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
		ofn.lpstrTitle= "Save file as";
		ofn.lpstrDefExt=m_DefaultExt;

		//position the dialog above the active window
		ofn.hwndOwner=Form.ActiveForm.Handle;
		
		//we need to find out the active screen so the dialog box is
		//centred on the correct display

		m_ActiveScreen=Screen.FromControl(Form.ActiveForm);

		//set up some sensible flags
		ofn.Flags=OFN_EXPLORER |  OFN_PATHMUSTEXIST | OFN_NOTESTFILECREATE | OFN_ENABLEHOOK | OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT;
		
		//this is where the hook is set. Note that we can use a C# delegate in place of a C function pointer
		ofn.lpfnHook=new OFNHookProcDelegate(HookProc);

		//if we're running on Windows 98/ME then the struct is smaller
		if (System.Environment.OSVersion.Platform!=PlatformID.Win32NT)
		{
			ofn.lStructSize-=12;
		}

		//show the dialog

		if (!GetSaveFileName(ref ofn))
		{
			int ret=CommDlgExtendedError();

			if (ret!=0)
			{
				throw new ApplicationException("Couldn't show file open dialog - " + ret.ToString());
			}

			return DialogResult.Cancel;
		}

		m_FileName=ofn.lpstrFile;

		return DialogResult.OK;
	}
}