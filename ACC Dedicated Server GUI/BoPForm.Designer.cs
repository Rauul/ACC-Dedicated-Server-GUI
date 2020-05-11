namespace ACC_Dedicated_Server_GUI
{
    partial class BoPForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoPForm));
            this.trackListBox = new System.Windows.Forms.ListBox();
            this.carListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.ballastTrackBar = new System.Windows.Forms.TrackBar();
            this.restrictorTrackBar = new System.Windows.Forms.TrackBar();
            this.ballastLabel = new System.Windows.Forms.Label();
            this.restrictorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ballastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackListBox
            // 
            this.trackListBox.DisplayMember = "name";
            this.trackListBox.FormattingEnabled = true;
            this.trackListBox.Location = new System.Drawing.Point(12, 12);
            this.trackListBox.Name = "trackListBox";
            this.trackListBox.Size = new System.Drawing.Size(142, 342);
            this.trackListBox.TabIndex = 0;
            this.trackListBox.TabStop = false;
            this.trackListBox.SelectedIndexChanged += new System.EventHandler(this.trackListBox_SelectedIndexChanged);
            // 
            // carListBox
            // 
            this.carListBox.DisplayMember = "model";
            this.carListBox.FormattingEnabled = true;
            this.carListBox.Location = new System.Drawing.Point(160, 12);
            this.carListBox.Name = "carListBox";
            this.carListBox.Size = new System.Drawing.Size(196, 342);
            this.carListBox.TabIndex = 1;
            this.carListBox.TabStop = false;
            this.carListBox.SelectedIndexChanged += new System.EventHandler(this.carListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ballast";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(422, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Restrictor";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(362, 321);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(112, 33);
            this.closeButton.TabIndex = 6;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "Save&&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ballastTrackBar
            // 
            this.ballastTrackBar.AutoSize = false;
            this.ballastTrackBar.Enabled = false;
            this.ballastTrackBar.Location = new System.Drawing.Point(372, 38);
            this.ballastTrackBar.Maximum = 50;
            this.ballastTrackBar.Name = "ballastTrackBar";
            this.ballastTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ballastTrackBar.Size = new System.Drawing.Size(24, 255);
            this.ballastTrackBar.TabIndex = 7;
            this.ballastTrackBar.TabStop = false;
            this.ballastTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ballastTrackBar.ValueChanged += new System.EventHandler(this.ballastTrackBar_ValueChanged);
            this.ballastTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ballastTrackBar_MouseUp);
            // 
            // restrictorTrackBar
            // 
            this.restrictorTrackBar.AutoSize = false;
            this.restrictorTrackBar.Enabled = false;
            this.restrictorTrackBar.Location = new System.Drawing.Point(436, 38);
            this.restrictorTrackBar.Maximum = 50;
            this.restrictorTrackBar.Name = "restrictorTrackBar";
            this.restrictorTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.restrictorTrackBar.Size = new System.Drawing.Size(24, 255);
            this.restrictorTrackBar.TabIndex = 8;
            this.restrictorTrackBar.TabStop = false;
            this.restrictorTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.restrictorTrackBar.ValueChanged += new System.EventHandler(this.restrictorTrackBar_ValueChanged);
            this.restrictorTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.restrictorTrackBar_MouseUp);
            // 
            // ballastLabel
            // 
            this.ballastLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ballastLabel.Location = new System.Drawing.Point(362, 12);
            this.ballastLabel.Name = "ballastLabel";
            this.ballastLabel.Size = new System.Drawing.Size(45, 20);
            this.ballastLabel.TabIndex = 9;
            this.ballastLabel.Text = "0";
            this.ballastLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // restrictorLabel
            // 
            this.restrictorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.restrictorLabel.Location = new System.Drawing.Point(425, 12);
            this.restrictorLabel.Name = "restrictorLabel";
            this.restrictorLabel.Size = new System.Drawing.Size(45, 20);
            this.restrictorLabel.TabIndex = 10;
            this.restrictorLabel.Text = "0";
            this.restrictorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BoPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 366);
            this.Controls.Add(this.restrictorLabel);
            this.Controls.Add(this.ballastLabel);
            this.Controls.Add(this.restrictorTrackBar);
            this.Controls.Add(this.ballastTrackBar);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.carListBox);
            this.Controls.Add(this.trackListBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BoPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Additional BoP";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoPForm_FormClosing);
            this.Load += new System.EventHandler(this.BoPForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ballastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox trackListBox;
        private System.Windows.Forms.ListBox carListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TrackBar ballastTrackBar;
        private System.Windows.Forms.TrackBar restrictorTrackBar;
        private System.Windows.Forms.Label ballastLabel;
        private System.Windows.Forms.Label restrictorLabel;
    }
}