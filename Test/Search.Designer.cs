namespace MyNotepad
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.labelReplace = new System.Windows.Forms.Label();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxWhole = new System.Windows.Forms.CheckBox();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.labelFind = new System.Windows.Forms.Label();
            this.buttonFind = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.panelReplace = new System.Windows.Forms.Panel();
            this.groupBoxDirection = new System.Windows.Forms.GroupBox();
            this.radioButtonDown = new System.Windows.Forms.RadioButton();
            this.radioButtonUp = new System.Windows.Forms.RadioButton();
            this.panelOption = new System.Windows.Forms.Panel();
            this.panelSearch.SuspendLayout();
            this.panelReplace.SuspendLayout();
            this.groupBoxDirection.SuspendLayout();
            this.panelOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(333, 34);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(82, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Location = new System.Drawing.Point(88, 23);
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.Size = new System.Drawing.Size(225, 20);
            this.textBoxReplace.TabIndex = 4;
            this.textBoxReplace.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // labelReplace
            // 
            this.labelReplace.AutoSize = true;
            this.labelReplace.Location = new System.Drawing.Point(11, 26);
            this.labelReplace.Name = "labelReplace";
            this.labelReplace.Size = new System.Drawing.Size(72, 13);
            this.labelReplace.TabIndex = 5;
            this.labelReplace.Text = "Replace with:";
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(333, 2);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(82, 25);
            this.buttonReplace.TabIndex = 6;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(334, 37);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(82, 25);
            this.buttonReplaceAll.TabIndex = 7;
            this.buttonReplaceAll.Text = "Replace All";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Checked = true;
            this.checkBoxMatchCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(14, 16);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMatchCase.TabIndex = 8;
            this.checkBoxMatchCase.Text = "Match case";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxWhole
            // 
            this.checkBoxWhole.AutoSize = true;
            this.checkBoxWhole.Location = new System.Drawing.Point(14, 39);
            this.checkBoxWhole.Name = "checkBoxWhole";
            this.checkBoxWhole.Size = new System.Drawing.Size(113, 17);
            this.checkBoxWhole.TabIndex = 9;
            this.checkBoxWhole.Text = "Match whole word";
            this.checkBoxWhole.UseVisualStyleBackColor = true;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(88, 15);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(224, 20);
            this.textBoxFind.TabIndex = 0;
            this.textBoxFind.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelFind
            // 
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(11, 18);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(30, 13);
            this.labelFind.TabIndex = 1;
            this.labelFind.Text = "Find:";
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(333, 12);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(82, 25);
            this.buttonFind.TabIndex = 2;
            this.buttonFind.Text = "Find Next";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.buttonFind);
            this.panelSearch.Controls.Add(this.labelFind);
            this.panelSearch.Controls.Add(this.textBoxFind);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(427, 48);
            this.panelSearch.TabIndex = 10;
            // 
            // panelReplace
            // 
            this.panelReplace.Controls.Add(this.buttonReplace);
            this.panelReplace.Controls.Add(this.labelReplace);
            this.panelReplace.Controls.Add(this.textBoxReplace);
            this.panelReplace.Controls.Add(this.buttonReplaceAll);
            this.panelReplace.Location = new System.Drawing.Point(0, 54);
            this.panelReplace.Name = "panelReplace";
            this.panelReplace.Size = new System.Drawing.Size(427, 65);
            this.panelReplace.TabIndex = 11;
            // 
            // groupBoxDirection
            // 
            this.groupBoxDirection.Controls.Add(this.radioButtonDown);
            this.groupBoxDirection.Controls.Add(this.radioButtonUp);
            this.groupBoxDirection.Location = new System.Drawing.Point(167, 3);
            this.groupBoxDirection.Name = "groupBoxDirection";
            this.groupBoxDirection.Size = new System.Drawing.Size(146, 56);
            this.groupBoxDirection.TabIndex = 12;
            this.groupBoxDirection.TabStop = false;
            this.groupBoxDirection.Text = "Direction";
            // 
            // radioButtonDown
            // 
            this.radioButtonDown.AutoSize = true;
            this.radioButtonDown.Checked = true;
            this.radioButtonDown.Location = new System.Drawing.Point(78, 25);
            this.radioButtonDown.Name = "radioButtonDown";
            this.radioButtonDown.Size = new System.Drawing.Size(53, 17);
            this.radioButtonDown.TabIndex = 1;
            this.radioButtonDown.TabStop = true;
            this.radioButtonDown.Text = "Down";
            this.radioButtonDown.UseVisualStyleBackColor = true;
            // 
            // radioButtonUp
            // 
            this.radioButtonUp.AutoSize = true;
            this.radioButtonUp.Location = new System.Drawing.Point(16, 24);
            this.radioButtonUp.Name = "radioButtonUp";
            this.radioButtonUp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonUp.TabIndex = 0;
            this.radioButtonUp.TabStop = true;
            this.radioButtonUp.Text = "Up";
            this.radioButtonUp.UseVisualStyleBackColor = true;
            // 
            // panelOption
            // 
            this.panelOption.Controls.Add(this.groupBoxDirection);
            this.panelOption.Controls.Add(this.checkBoxWhole);
            this.panelOption.Controls.Add(this.checkBoxMatchCase);
            this.panelOption.Controls.Add(this.buttonCancel);
            this.panelOption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOption.Location = new System.Drawing.Point(0, 125);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(427, 71);
            this.panelOption.TabIndex = 13;
            // 
            // Search
            // 
            this.AcceptButton = this.buttonFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(427, 196);
            this.ControlBox = false;
            this.Controls.Add(this.panelOption);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelReplace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and Replace";
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelReplace.ResumeLayout(false);
            this.panelReplace.PerformLayout();
            this.groupBoxDirection.ResumeLayout(false);
            this.groupBoxDirection.PerformLayout();
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.Label labelReplace;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxWhole;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panelReplace;
        private System.Windows.Forms.GroupBox groupBoxDirection;
        private System.Windows.Forms.RadioButton radioButtonDown;
        private System.Windows.Forms.RadioButton radioButtonUp;
        private System.Windows.Forms.Panel panelOption;
    }
}