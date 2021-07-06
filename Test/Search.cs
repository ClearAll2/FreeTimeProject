using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyNotepad
{
    public partial class Form2 : Form
    {
        private RichTextBox _richTextBox;
        
        public Form2( RichTextBox richTextBox)
        {
            InitializeComponent();
            _richTextBox = richTextBox;
            StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Find(_richTextBox, textBox1.Text, checkBox1.Checked, checkBox2.Checked, radioButton1.Checked);
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
                if (textBox2.Text.Length > 0)
                {
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }
            else
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _richTextBox.SelectedText = textBox2.Text;
               
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _richTextBox.Text = _richTextBox.Text.Replace(textBox1.Text, textBox2.Text);
        }

        void Find(RichTextBox richText, string text, bool matchCase, bool matchWholeWord, bool upDirection)
        {
            RichTextBoxFinds options = RichTextBoxFinds.None;
            if (matchCase)
                options |= RichTextBoxFinds.MatchCase;
            if (matchWholeWord)
                options |= RichTextBoxFinds.WholeWord;
            if (upDirection)
                options |= RichTextBoxFinds.Reverse;

            int index;
            if (upDirection)
                index = richText.Find(text, 0, richText.SelectionStart, options);
            else
                index = richText.Find(text, richText.SelectionStart + richText.SelectionLength, options);

            if (index >= 0)
            {
                richText.SelectionStart = index;
                richText.SelectionLength = text.Length;
                //richText.SelectionColor = Color.CadetBlue;
                
            }
            else // text not found
            {
                MessageBox.Show(Application.ProductName + " has finished searching the document.",
                                Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        public void ShowFind(bool replaceMode, string key)
        {
            this.Text = replaceMode ? "Replace" : "Find";
            panelReplace.Visible = replaceMode;

            if (!this.Visible)
                this.Show(_richTextBox);
            // resize form
            this.ClientSize = new Size(this.ClientSize.Width, panelOption.Bottom);
            textBox1.Text = key;
            textBox1.Focus();
            textBox1.SelectAll();
            if (textBox1.Text.Length <= 0)
            {
                button1.Enabled = false;
            }
            if (textBox2.Text.Length <=0)
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }
    }
}
