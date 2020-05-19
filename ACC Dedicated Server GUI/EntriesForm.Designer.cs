namespace ACC_Dedicated_Server_GUI
{
    partial class EntriesForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntriesForm));
            this.entriesTreeView = new System.Windows.Forms.TreeView();
            this.carSettingsPanel = new System.Windows.Forms.Panel();
            this.overrideCustomCarModelCheckBox = new System.Windows.Forms.CheckBox();
            this.restrictorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.ballastNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.gridPositionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.adminCheckBox = new System.Windows.Forms.CheckBox();
            this.customCarComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.forcedCarModelComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.overrideDriverInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.carNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.forceEntryListCheckBox = new System.Windows.Forms.CheckBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.driverSettingsPanel = new System.Windows.Forms.Panel();
            this.playerIDTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.shortNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.entryListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapsAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entryContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapsAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.driverContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeDriverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ballastNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPositionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carNumberNumericUpDown)).BeginInit();
            this.driverSettingsPanel.SuspendLayout();
            this.entryListContextMenuStrip.SuspendLayout();
            this.entryContextMenuStrip.SuspendLayout();
            this.driverContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // entriesTreeView
            // 
            this.entriesTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.entriesTreeView.HideSelection = false;
            this.entriesTreeView.Indent = 14;
            this.entriesTreeView.Location = new System.Drawing.Point(13, 13);
            this.entriesTreeView.Name = "entriesTreeView";
            this.entriesTreeView.Size = new System.Drawing.Size(231, 402);
            this.entriesTreeView.TabIndex = 0;
            this.entriesTreeView.TabStop = false;
            this.entriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.entriesTreeView_AfterSelect);
            this.entriesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.entriesTreeView_MouseDown);
            // 
            // carSettingsPanel
            // 
            this.carSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carSettingsPanel.Controls.Add(this.overrideCustomCarModelCheckBox);
            this.carSettingsPanel.Controls.Add(this.restrictorNumericUpDown);
            this.carSettingsPanel.Controls.Add(this.label7);
            this.carSettingsPanel.Controls.Add(this.ballastNumericUpDown);
            this.carSettingsPanel.Controls.Add(this.label6);
            this.carSettingsPanel.Controls.Add(this.gridPositionNumericUpDown);
            this.carSettingsPanel.Controls.Add(this.label5);
            this.carSettingsPanel.Controls.Add(this.adminCheckBox);
            this.carSettingsPanel.Controls.Add(this.customCarComboBox);
            this.carSettingsPanel.Controls.Add(this.label3);
            this.carSettingsPanel.Controls.Add(this.forcedCarModelComboBox);
            this.carSettingsPanel.Controls.Add(this.label2);
            this.carSettingsPanel.Controls.Add(this.overrideDriverInfoCheckBox);
            this.carSettingsPanel.Controls.Add(this.label1);
            this.carSettingsPanel.Controls.Add(this.carNumberNumericUpDown);
            this.carSettingsPanel.Location = new System.Drawing.Point(251, 13);
            this.carSettingsPanel.Name = "carSettingsPanel";
            this.carSettingsPanel.Size = new System.Drawing.Size(377, 402);
            this.carSettingsPanel.TabIndex = 1;
            this.carSettingsPanel.Visible = false;
            // 
            // overrideCustomCarModelCheckBox
            // 
            this.overrideCustomCarModelCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.overrideCustomCarModelCheckBox.Location = new System.Drawing.Point(193, 87);
            this.overrideCustomCarModelCheckBox.Name = "overrideCustomCarModelCheckBox";
            this.overrideCustomCarModelCheckBox.Size = new System.Drawing.Size(174, 20);
            this.overrideCustomCarModelCheckBox.TabIndex = 6;
            this.overrideCustomCarModelCheckBox.Text = "Override Custom Car Model";
            this.overrideCustomCarModelCheckBox.UseVisualStyleBackColor = true;
            this.overrideCustomCarModelCheckBox.CheckedChanged += new System.EventHandler(this.overrideCustomCarModelCheckBox_CheckedChanged);
            // 
            // restrictorNumericUpDown
            // 
            this.restrictorNumericUpDown.Location = new System.Drawing.Point(123, 166);
            this.restrictorNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.restrictorNumericUpDown.Name = "restrictorNumericUpDown";
            this.restrictorNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.restrictorNumericUpDown.TabIndex = 5;
            this.restrictorNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.restrictorNumericUpDown.ValueChanged += new System.EventHandler(this.restrictorNumericUpDown_ValueChanged);
            this.restrictorNumericUpDown.Enter += new System.EventHandler(this.quickBoxs_Enter);
            this.restrictorNumericUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.quickBoxs_MouseDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Restrictor";
            // 
            // ballastNumericUpDown
            // 
            this.ballastNumericUpDown.Location = new System.Drawing.Point(123, 140);
            this.ballastNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ballastNumericUpDown.Name = "ballastNumericUpDown";
            this.ballastNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.ballastNumericUpDown.TabIndex = 4;
            this.ballastNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ballastNumericUpDown.ValueChanged += new System.EventHandler(this.ballastNumericUpDown_ValueChanged);
            this.ballastNumericUpDown.Enter += new System.EventHandler(this.quickBoxs_Enter);
            this.ballastNumericUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.quickBoxs_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ballast (Kg)";
            // 
            // gridPositionNumericUpDown
            // 
            this.gridPositionNumericUpDown.Location = new System.Drawing.Point(123, 114);
            this.gridPositionNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.gridPositionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.gridPositionNumericUpDown.Name = "gridPositionNumericUpDown";
            this.gridPositionNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.gridPositionNumericUpDown.TabIndex = 3;
            this.gridPositionNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gridPositionNumericUpDown.ValueChanged += new System.EventHandler(this.gridPositionNumericUpDown_ValueChanged);
            this.gridPositionNumericUpDown.Enter += new System.EventHandler(this.quickBoxs_Enter);
            this.gridPositionNumericUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.quickBoxs_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Default Grid Position";
            // 
            // adminCheckBox
            // 
            this.adminCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.adminCheckBox.Location = new System.Drawing.Point(193, 113);
            this.adminCheckBox.Name = "adminCheckBox";
            this.adminCheckBox.Size = new System.Drawing.Size(174, 20);
            this.adminCheckBox.TabIndex = 7;
            this.adminCheckBox.Text = "Admin";
            this.adminCheckBox.UseVisualStyleBackColor = true;
            this.adminCheckBox.CheckedChanged += new System.EventHandler(this.adminCheckBox_CheckedChanged);
            // 
            // customCarComboBox
            // 
            this.customCarComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customCarComboBox.FormattingEnabled = true;
            this.customCarComboBox.Items.AddRange(new object[] {
            ""});
            this.customCarComboBox.Location = new System.Drawing.Point(183, 34);
            this.customCarComboBox.Name = "customCarComboBox";
            this.customCarComboBox.Size = new System.Drawing.Size(184, 21);
            this.customCarComboBox.TabIndex = 1;
            this.customCarComboBox.SelectedIndexChanged += new System.EventHandler(this.customCarComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Custom Car";
            // 
            // forcedCarModelComboBox
            // 
            this.forcedCarModelComboBox.DisplayMember = "model";
            this.forcedCarModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forcedCarModelComboBox.FormattingEnabled = true;
            this.forcedCarModelComboBox.Location = new System.Drawing.Point(183, 7);
            this.forcedCarModelComboBox.Name = "forcedCarModelComboBox";
            this.forcedCarModelComboBox.Size = new System.Drawing.Size(184, 21);
            this.forcedCarModelComboBox.TabIndex = 0;
            this.forcedCarModelComboBox.SelectedIndexChanged += new System.EventHandler(this.forcedCarModelComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Forced Car Model";
            // 
            // overrideDriverInfoCheckBox
            // 
            this.overrideDriverInfoCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.overrideDriverInfoCheckBox.Location = new System.Drawing.Point(193, 139);
            this.overrideDriverInfoCheckBox.Name = "overrideDriverInfoCheckBox";
            this.overrideDriverInfoCheckBox.Size = new System.Drawing.Size(174, 20);
            this.overrideDriverInfoCheckBox.TabIndex = 8;
            this.overrideDriverInfoCheckBox.Text = "Override Driver Info";
            this.overrideDriverInfoCheckBox.UseVisualStyleBackColor = true;
            this.overrideDriverInfoCheckBox.CheckedChanged += new System.EventHandler(this.overrideDriverInfoCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Car Number";
            // 
            // carNumberNumericUpDown
            // 
            this.carNumberNumericUpDown.Location = new System.Drawing.Point(123, 88);
            this.carNumberNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.carNumberNumericUpDown.Name = "carNumberNumericUpDown";
            this.carNumberNumericUpDown.Size = new System.Drawing.Size(54, 20);
            this.carNumberNumericUpDown.TabIndex = 2;
            this.carNumberNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.carNumberNumericUpDown.ValueChanged += new System.EventHandler(this.carNumberNumericUpDown_ValueChanged);
            this.carNumberNumericUpDown.Enter += new System.EventHandler(this.quickBoxs_Enter);
            this.carNumberNumericUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.quickBoxs_MouseDown);
            // 
            // forceEntryListCheckBox
            // 
            this.forceEntryListCheckBox.AutoSize = true;
            this.forceEntryListCheckBox.Location = new System.Drawing.Point(13, 421);
            this.forceEntryListCheckBox.Name = "forceEntryListCheckBox";
            this.forceEntryListCheckBox.Size = new System.Drawing.Size(99, 17);
            this.forceEntryListCheckBox.TabIndex = 2;
            this.forceEntryListCheckBox.TabStop = false;
            this.forceEntryListCheckBox.Text = "Force Entry List";
            this.forceEntryListCheckBox.UseVisualStyleBackColor = true;
            this.forceEntryListCheckBox.CheckedChanged += new System.EventHandler(this.forceEntryListCheckBox_CheckedChanged);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(541, 421);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(87, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "Save&&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // driverSettingsPanel
            // 
            this.driverSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.driverSettingsPanel.Controls.Add(this.playerIDTextBox);
            this.driverSettingsPanel.Controls.Add(this.label14);
            this.driverSettingsPanel.Controls.Add(this.categoryComboBox);
            this.driverSettingsPanel.Controls.Add(this.shortNameTextBox);
            this.driverSettingsPanel.Controls.Add(this.lastNameTextBox);
            this.driverSettingsPanel.Controls.Add(this.firstNameTextBox);
            this.driverSettingsPanel.Controls.Add(this.label10);
            this.driverSettingsPanel.Controls.Add(this.label11);
            this.driverSettingsPanel.Controls.Add(this.label12);
            this.driverSettingsPanel.Controls.Add(this.label13);
            this.driverSettingsPanel.Location = new System.Drawing.Point(251, 13);
            this.driverSettingsPanel.Name = "driverSettingsPanel";
            this.driverSettingsPanel.Size = new System.Drawing.Size(377, 402);
            this.driverSettingsPanel.TabIndex = 16;
            this.driverSettingsPanel.Visible = false;
            // 
            // playerIDTextBox
            // 
            this.playerIDTextBox.Location = new System.Drawing.Point(183, 87);
            this.playerIDTextBox.Name = "playerIDTextBox";
            this.playerIDTextBox.Size = new System.Drawing.Size(184, 20);
            this.playerIDTextBox.TabIndex = 3;
            this.playerIDTextBox.TextChanged += new System.EventHandler(this.playerIDTextBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Player ID";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Items.AddRange(new object[] {
            "Bronze",
            "Silver",
            "Gold",
            "Platinum"});
            this.categoryComboBox.Location = new System.Drawing.Point(183, 113);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(184, 21);
            this.categoryComboBox.TabIndex = 4;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // shortNameTextBox
            // 
            this.shortNameTextBox.Location = new System.Drawing.Point(183, 61);
            this.shortNameTextBox.Name = "shortNameTextBox";
            this.shortNameTextBox.Size = new System.Drawing.Size(184, 20);
            this.shortNameTextBox.TabIndex = 2;
            this.shortNameTextBox.TextChanged += new System.EventHandler(this.shortNameTextBox_TextChanged);
            this.shortNameTextBox.Enter += new System.EventHandler(this.shortNameTextBox_Enter);
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(183, 34);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(184, 20);
            this.lastNameTextBox.TabIndex = 1;
            this.lastNameTextBox.TextChanged += new System.EventHandler(this.lastNameTextBox_TextChanged);
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(183, 7);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(184, 20);
            this.firstNameTextBox.TabIndex = 0;
            this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Driver Category";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Short Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Last Name";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "First Name";
            // 
            // entryListContextMenuStrip
            // 
            this.entryListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEntryToolStripMenuItem,
            this.toolStripSeparator1,
            this.expandAllToolStripMenuItem,
            this.collapsAllToolStripMenuItem});
            this.entryListContextMenuStrip.Name = "entryListContextMenuStrip";
            this.entryListContextMenuStrip.Size = new System.Drawing.Size(137, 76);
            // 
            // addEntryToolStripMenuItem
            // 
            this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
            this.addEntryToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addEntryToolStripMenuItem.Text = "Add Entry";
            this.addEntryToolStripMenuItem.Click += new System.EventHandler(this.addEntryToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapsAllToolStripMenuItem
            // 
            this.collapsAllToolStripMenuItem.Name = "collapsAllToolStripMenuItem";
            this.collapsAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collapsAllToolStripMenuItem.Text = "Collapse All";
            this.collapsAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // entryContextMenuStrip
            // 
            this.entryContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDriverToolStripMenuItem,
            this.removeEntryToolStripMenuItem,
            this.toolStripSeparator2,
            this.expandAllToolStripMenuItem1,
            this.collapsAllToolStripMenuItem1});
            this.entryContextMenuStrip.Name = "entryContextMenuStrip";
            this.entryContextMenuStrip.Size = new System.Drawing.Size(148, 98);
            // 
            // addDriverToolStripMenuItem
            // 
            this.addDriverToolStripMenuItem.Name = "addDriverToolStripMenuItem";
            this.addDriverToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addDriverToolStripMenuItem.Text = "Add Driver";
            this.addDriverToolStripMenuItem.Click += new System.EventHandler(this.addDriverToolStripMenuItem_Click);
            // 
            // removeEntryToolStripMenuItem
            // 
            this.removeEntryToolStripMenuItem.Name = "removeEntryToolStripMenuItem";
            this.removeEntryToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.removeEntryToolStripMenuItem.Text = "Remove Entry";
            this.removeEntryToolStripMenuItem.Click += new System.EventHandler(this.removeEntryToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
            // 
            // expandAllToolStripMenuItem1
            // 
            this.expandAllToolStripMenuItem1.Name = "expandAllToolStripMenuItem1";
            this.expandAllToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.expandAllToolStripMenuItem1.Text = "Expand All";
            this.expandAllToolStripMenuItem1.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapsAllToolStripMenuItem1
            // 
            this.collapsAllToolStripMenuItem1.Name = "collapsAllToolStripMenuItem1";
            this.collapsAllToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.collapsAllToolStripMenuItem1.Text = "Collapse All";
            this.collapsAllToolStripMenuItem1.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // driverContextMenuStrip
            // 
            this.driverContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeDriverMenuItem,
            this.toolStripSeparator3,
            this.expandAllToolStripMenuItem2,
            this.collapseAllToolStripMenuItem});
            this.driverContextMenuStrip.Name = "driverContextMenuStrip";
            this.driverContextMenuStrip.Size = new System.Drawing.Size(152, 76);
            // 
            // removeDriverMenuItem
            // 
            this.removeDriverMenuItem.Name = "removeDriverMenuItem";
            this.removeDriverMenuItem.Size = new System.Drawing.Size(151, 22);
            this.removeDriverMenuItem.Text = "Remove Driver";
            this.removeDriverMenuItem.Click += new System.EventHandler(this.removeDriverMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
            // 
            // expandAllToolStripMenuItem2
            // 
            this.expandAllToolStripMenuItem2.Name = "expandAllToolStripMenuItem2";
            this.expandAllToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
            this.expandAllToolStripMenuItem2.Text = "Expand All";
            this.expandAllToolStripMenuItem2.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // EntriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 450);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.forceEntryListCheckBox);
            this.Controls.Add(this.entriesTreeView);
            this.Controls.Add(this.carSettingsPanel);
            this.Controls.Add(this.driverSettingsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EntriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entries";
            this.Activated += new System.EventHandler(this.EntriesForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EntriesForm_FormClosing);
            this.Load += new System.EventHandler(this.EntriesForm_Load);
            this.carSettingsPanel.ResumeLayout(false);
            this.carSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restrictorNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ballastNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPositionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carNumberNumericUpDown)).EndInit();
            this.driverSettingsPanel.ResumeLayout(false);
            this.driverSettingsPanel.PerformLayout();
            this.entryListContextMenuStrip.ResumeLayout(false);
            this.entryContextMenuStrip.ResumeLayout(false);
            this.driverContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView entriesTreeView;
        private System.Windows.Forms.Panel carSettingsPanel;
        private System.Windows.Forms.NumericUpDown restrictorNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown ballastNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown gridPositionNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox adminCheckBox;
        private System.Windows.Forms.ComboBox customCarComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox forcedCarModelComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox overrideDriverInfoCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown carNumberNumericUpDown;
        private System.Windows.Forms.CheckBox forceEntryListCheckBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel driverSettingsPanel;
        private System.Windows.Forms.TextBox playerIDTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.TextBox shortNameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip entryListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addEntryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip entryContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addDriverToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip driverContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeDriverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapsAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem collapsAllToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.CheckBox overrideCustomCarModelCheckBox;
    }
}