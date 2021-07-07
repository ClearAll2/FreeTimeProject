using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyNotepad
{
    public partial class Search : Form
    {
        private RichTextBox _richTextBox;
        
        public Search( RichTextBox richTextBox)
        {
            InitializeComponent();
            _richTextBox = richTextBox;
            StartPosition = FormStartPosition.CenterScreen;
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Find(_richTextBox, textBoxFind.Text, checkBoxMatchCase.Checked, checkBoxWhole.Checked, radioButtonUp.Checked);
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFind.Text.Length > 0)
            {
                buttonFind.Enabled = true;
                if (textBoxReplace.Text.Length > 0)
                {
                    buttonReplace.Enabled = true;
                    buttonReplaceAll.Enabled = true;
                }
            }
            else
            {
                buttonFind.Enabled = false;
                buttonReplace.Enabled = false;
                buttonReplaceAll.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _richTextBox.SelectedText = textBoxReplace.Text;
               
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _richTextBox.Text = _richTextBox.Text.Replace(textBoxFind.Text, textBoxReplace.Text);
            MessageBox.Show(Application.ProductName + " has finished replacing text in the document.",
                               Application.ProductName, MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
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
            textBoxFind.Text = key;
            textBoxFind.Focus();
            textBoxFind.SelectAll();
            if (textBoxFind.Text.Length <= 0)
            {
                buttonFind.Enabled = false;
            }
            if (textBoxReplace.Text.Length <=0)
            {
                buttonReplace.Enabled = false;
                buttonReplaceAll.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxReplace.Text.Length > 0 && textBoxFind.Text.Length > 0)
            {
                buttonReplace.Enabled = true;
                buttonReplaceAll.Enabled = true;
            }
            else
            {
                buttonReplace.Enabled = false;
                buttonReplaceAll.Enabled = false;
            }
        }
    }
}
