namespace EjectHdd
{
    partial class EjectHddForm
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
            this.Label_DiskNumber = new System.Windows.Forms.Label();
            this.ComboBox_DiskNumber = new System.Windows.Forms.ComboBox();
            this.ButtonOffline = new System.Windows.Forms.Button();
            this.ButtonOnline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_DiskNumber
            // 
            this.Label_DiskNumber.AutoSize = true;
            this.Label_DiskNumber.Location = new System.Drawing.Point(2, 8);
            this.Label_DiskNumber.Name = "Label_DiskNumber";
            this.Label_DiskNumber.Size = new System.Drawing.Size(71, 13);
            this.Label_DiskNumber.TabIndex = 1;
            this.Label_DiskNumber.Text = "Disk Number:";
            // 
            // ComboBox_DiskNumber
            // 
            this.ComboBox_DiskNumber.FormattingEnabled = true;
            this.ComboBox_DiskNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.ComboBox_DiskNumber.Location = new System.Drawing.Point(76, 5);
            this.ComboBox_DiskNumber.Name = "ComboBox_DiskNumber";
            this.ComboBox_DiskNumber.Size = new System.Drawing.Size(147, 21);
            this.ComboBox_DiskNumber.TabIndex = 3;
            // 
            // ButtonOffline
            // 
            this.ButtonOffline.Location = new System.Drawing.Point(148, 45);
            this.ButtonOffline.Name = "ButtonOffline";
            this.ButtonOffline.Size = new System.Drawing.Size(75, 23);
            this.ButtonOffline.TabIndex = 4;
            this.ButtonOffline.Text = "Offline";
            this.ButtonOffline.UseVisualStyleBackColor = true;
            this.ButtonOffline.Click += new System.EventHandler(this.ButtonOffline_Click);
            // 
            // ButtonOnline
            // 
            this.ButtonOnline.Location = new System.Drawing.Point(12, 45);
            this.ButtonOnline.Name = "ButtonOnline";
            this.ButtonOnline.Size = new System.Drawing.Size(75, 23);
            this.ButtonOnline.TabIndex = 5;
            this.ButtonOnline.Text = "Online";
            this.ButtonOnline.UseVisualStyleBackColor = true;
            this.ButtonOnline.Click += new System.EventHandler(this.ButtonOnline_Click);
            // 
            // EjectHddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 89);
            this.Controls.Add(this.ButtonOnline);
            this.Controls.Add(this.ButtonOffline);
            this.Controls.Add(this.ComboBox_DiskNumber);
            this.Controls.Add(this.Label_DiskNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EjectHddForm";
            this.Load += new System.EventHandler(this.EjectHdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_DiskNumber;
        private System.Windows.Forms.ComboBox ComboBox_DiskNumber;
        private System.Windows.Forms.Button ButtonOnline;
        private System.Windows.Forms.Button ButtonOffline;
    }
}

