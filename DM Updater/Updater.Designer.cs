namespace DM_Updater
{
    partial class Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelFront = new System.Windows.Forms.Panel();
            this.labellatestVer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labellocalVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxAutoOpen = new System.Windows.Forms.CheckBox();
            this.panelFront.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 55);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(394, 1);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "This will take a while. Do not close this";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(134, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Updating Desktop Magic";
            // 
            // panelFront
            // 
            this.panelFront.Controls.Add(this.labellatestVer);
            this.panelFront.Controls.Add(this.label4);
            this.panelFront.Controls.Add(this.label5);
            this.panelFront.Controls.Add(this.labellocalVer);
            this.panelFront.Controls.Add(this.label3);
            this.panelFront.Controls.Add(this.buttonUpdate);
            this.panelFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFront.Location = new System.Drawing.Point(0, 0);
            this.panelFront.Name = "panelFront";
            this.panelFront.Size = new System.Drawing.Size(418, 126);
            this.panelFront.TabIndex = 3;
            // 
            // labellatestVer
            // 
            this.labellatestVer.AutoSize = true;
            this.labellatestVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labellatestVer.Location = new System.Drawing.Point(204, 59);
            this.labellatestVer.Name = "labellatestVer";
            this.labellatestVer.Size = new System.Drawing.Size(65, 15);
            this.labellatestVer.TabIndex = 9;
            this.labellatestVer.Text = "checking...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Latest Desktop Magic version:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(327, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Welcome to DM Updater, you can update DM here";
            // 
            // labellocalVer
            // 
            this.labellocalVer.AutoSize = true;
            this.labellocalVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labellocalVer.Location = new System.Drawing.Point(204, 37);
            this.labellocalVer.Name = "labellocalVer";
            this.labellocalVer.Size = new System.Drawing.Size(44, 15);
            this.labellocalVer.TabIndex = 5;
            this.labellocalVer.Text = "?.?.?.?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Your Desktop Magic version:";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdate.Location = new System.Drawing.Point(262, 85);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(144, 29);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Update DM";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkBoxAutoOpen
            // 
            this.checkBoxAutoOpen.AutoSize = true;
            this.checkBoxAutoOpen.Checked = true;
            this.checkBoxAutoOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoOpen.Location = new System.Drawing.Point(104, 97);
            this.checkBoxAutoOpen.Name = "checkBoxAutoOpen";
            this.checkBoxAutoOpen.Size = new System.Drawing.Size(210, 17);
            this.checkBoxAutoOpen.TabIndex = 4;
            this.checkBoxAutoOpen.Text = "Automatically open DM when complete";
            this.checkBoxAutoOpen.UseVisualStyleBackColor = true;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(418, 126);
            this.Controls.Add(this.panelFront);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBoxAutoOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DM Updater";
            this.Load += new System.EventHandler(this.Updater_Load);
            this.panelFront.ResumeLayout(false);
            this.panelFront.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelFront;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label labellatestVer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labellocalVer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxAutoOpen;
    }
}

