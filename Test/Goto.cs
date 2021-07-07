using System;
using System.Drawing;
using System.Windows.Forms;

namespace SNote
{
    public partial class Goto : Form
    {
        public int line = -1;
        public Goto()
        {
            InitializeComponent();
            textBoxInput.Select();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            line = -1;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(textBoxInput.Text, out line))
            {
                buttonOK.Enabled = false;
                return;
            }
            line = Int32.Parse(textBoxInput.Text);
            buttonOK.Enabled = true;
        }
    }
}
