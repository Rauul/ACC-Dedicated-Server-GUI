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
            this.ballastNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.restrictorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.ballastTrackBar = new System.Windows.Forms.TrackBar();
            this.restrictorTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.ballastNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ballastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackListBox
            // 
            this.trackListBox.FormattingEnabled = true;
            this.trackListBox.Items.AddRange(new object[] {
            "monza",
            "zolder",
            "brands_hatch",
            "silverstone",
            "paul_ricard",
            "misano",
            "spa",
            "nurburgring",
            "barcelona",
            "hungaroring",
            "zandvoort",
            "monza_2019",
            "zolder_2019",
            "brands_hatch_2019",
            "silverstone_2019",
            "paul_ricard_2019",
            "misano_2019",
            "spa_2019",
            "nurburgring_2019",
            "barcelona_2019",
            "hungaroring_2019",
            "zandvoort_2019",
            "kyalami_2019",
            "mount_panorama_2019",
            "suzuka_2019",
            "laguna_seca_2019"});
            this.trackListBox.Location = new System.Drawing.Point(13, 13);
            this.trackListBox.Name = "trackListBox";
            this.trackListBox.Size = new System.Drawing.Size(129, 342);
            this.trackListBox.TabIndex = 0;
            this.trackListBox.SelectedIndexChanged += new System.EventHandler(this.trackListBox_SelectedIndexChanged);
            // 
            // carListBox
            // 
            this.carListBox.FormattingEnabled = true;
            this.carListBox.Items.AddRange(new object[] {
            "Porsche 991 GT3",
            "Mercedes AMG GT3",
            "Ferrari 488 GT3",
            "Audi R8 LMS",
            "Lamborghini Huracan GT3",
            "Mclaren 650s GT3",
            "Nissan GT R Nismo GT3 2018",
            "BMW M6 GT3",
            "Bentley Continental GT3 2018",
            "Porsche 991.2 GT3 Cup",
            "Nissan GT-R Nismo GT3 2017",
            "Bentley Continental GT3 2016",
            "Aston Martin Vantage V12 GT3",
            "Lamborghini Gallardo R-EX",
            "Jaguar G3",
            "Lexus RC F GT3",
            "Lamborghini Huracan Evo (2019)",
            "Honda NSX GT3",
            "Lamborghini Huracan SuperTrofeo",
            "Audi R8 LMS Evo (2019)",
            "AMR V8 Vantage (2019)",
            "Honda NSX Evo (2019)",
            "McLaren 720S GT3 (Special)",
            "Porsche 911 II GT3 R (2019)"});
            this.carListBox.Location = new System.Drawing.Point(149, 13);
            this.carListBox.Name = "carListBox";
            this.carListBox.Size = new System.Drawing.Size(174, 342);
            this.carListBox.TabIndex = 1;
            this.carListBox.SelectedIndexChanged += new System.EventHandler(this.carListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ballast";
            // 
            // ballastNumericUpDown
            // 
            this.ballastNumericUpDown.Enabled = false;
            this.ballastNumericUpDown.Location = new System.Drawing.Point(329, 13);
            this.ballastNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ballastNumericUpDown.Name = "ballastNumericUpDown";
            this.ballastNumericUpDown.Size = new System.Drawing.Size(45, 20);
            this.ballastNumericUpDown.TabIndex = 3;
            this.ballastNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ballastNumericUpDown.ValueChanged += new System.EventHandler(this.ballastNumericUpDown_ValueChanged);
            // 
            // restrictorNumericUpDown
            // 
            this.restrictorNumericUpDown.Enabled = false;
            this.restrictorNumericUpDown.Location = new System.Drawing.Point(392, 13);
            this.restrictorNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.restrictorNumericUpDown.Name = "restrictorNumericUpDown";
            this.restrictorNumericUpDown.Size = new System.Drawing.Size(45, 20);
            this.restrictorNumericUpDown.TabIndex = 5;
            this.restrictorNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.restrictorNumericUpDown.ValueChanged += new System.EventHandler(this.restrictorNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Restrictor";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(329, 322);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(112, 33);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Save&&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ballastTrackBar
            // 
            this.ballastTrackBar.AutoSize = false;
            this.ballastTrackBar.Enabled = false;
            this.ballastTrackBar.Location = new System.Drawing.Point(339, 39);
            this.ballastTrackBar.Maximum = 20;
            this.ballastTrackBar.Name = "ballastTrackBar";
            this.ballastTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ballastTrackBar.Size = new System.Drawing.Size(24, 255);
            this.ballastTrackBar.TabIndex = 7;
            this.ballastTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ballastTrackBar.Scroll += new System.EventHandler(this.ballastTrackBar_Scroll);
            // 
            // restrictorTrackBar
            // 
            this.restrictorTrackBar.AutoSize = false;
            this.restrictorTrackBar.Enabled = false;
            this.restrictorTrackBar.Location = new System.Drawing.Point(403, 39);
            this.restrictorTrackBar.Maximum = 20;
            this.restrictorTrackBar.Name = "restrictorTrackBar";
            this.restrictorTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.restrictorTrackBar.Size = new System.Drawing.Size(24, 255);
            this.restrictorTrackBar.TabIndex = 8;
            this.restrictorTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.restrictorTrackBar.Scroll += new System.EventHandler(this.restrictorTrackBar_Scroll);
            // 
            // BoPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 367);
            this.Controls.Add(this.restrictorTrackBar);
            this.Controls.Add(this.ballastTrackBar);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.restrictorNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ballastNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.carListBox);
            this.Controls.Add(this.trackListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BoPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Additional BoP";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoPForm_FormClosing);
            this.Load += new System.EventHandler(this.BoPForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ballastNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ballastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox trackListBox;
        private System.Windows.Forms.ListBox carListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ballastNumericUpDown;
        private System.Windows.Forms.NumericUpDown restrictorNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TrackBar ballastTrackBar;
        private System.Windows.Forms.TrackBar restrictorTrackBar;
    }
}