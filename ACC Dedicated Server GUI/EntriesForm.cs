using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.EntryList;

namespace ACC_Dedicated_Server_GUI
{
    public partial class EntriesForm : Form
    {
        EntryListObject entrylist = new EntryListObject();
        List<Car> carList = new List<Car>(MainForm.carList);
        bool closeButtonClicked = false;

        public EntriesForm()
        {
            InitializeComponent();
        }

        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 123 && bom[1] == 13 && bom[2] == 10) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0) return Encoding.UTF32; //UTF-32LE
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 123 && bom[1] == 0) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return new UTF32Encoding(true, true);  //UTF-32BE

            // We actually have no idea what the encoding is if we reach this point, so
            // you may wish to return null instead of defaulting to ASCII
            return Encoding.ASCII;
        }

        private void EntriesForm_Load(object sender, EventArgs e)
        {
            foreach (Car car in carList)
                car.model = car.model.Replace("    ", "");

            forcedCarModelComboBox.Items.Add(new Car("", -1));
            forcedCarModelComboBox.Items.AddRange(carList.ToArray());

            TreeNode entriesTreeNode = new TreeNode();
            entriesTreeNode.Text = "Entries";
            entriesTreeNode.Tag = entrylist;
            entriesTreeView.Nodes.Add(entriesTreeNode);

            string file = @"cfg\entrylist.json";
            if (File.Exists(file))
            {
                Encoding encoding = GetEncoding(file);
                string rawJSON = File.ReadAllText(file, encoding);
                entrylist = JsonConvert.DeserializeObject<EntryListObject>(rawJSON);
                entrylist.entries.Sort(delegate (Entry x, Entry y) { return x.raceNumber.CompareTo(y.raceNumber); });
                LoadTreeView(entrylist);
                forceEntryListCheckBox.Checked = entrylist.forceEntryList == 1 ? true : false;
            }

            if (Directory.Exists(@"cfg\cars\"))
            {
                var fileNames = Directory.GetFiles(@"cfg\cars\", "*.json").Select(f => Path.GetFileName(f));
                foreach (string filen in fileNames)
                {
                    customCarComboBox.Items.Add(filen);
                }
            }
        }

        private void LoadTreeView(EntryListObject entrylist)
        {
            //TreeNode entriesTreeNode = new TreeNode();
            //entriesTreeNode.Text = "Entries";
            //entriesTreeNode.Tag = entrylist;
            //entriesTreeView.Nodes.Add(entriesTreeNode);

            foreach (Entry entry in entrylist.entries)
            {
                TreeNode carTreeNode = new TreeNode();
                Font regularFont = new Font(entriesTreeView.Font, FontStyle.Regular);
                Font boldFont = new Font(entriesTreeView.Font, FontStyle.Bold);
                //carTreeNode.NodeFont = entry.isServerAdmin == 1 ? boldFont : regularFont;
                carTreeNode.Text = entry.raceNumber.ToString();
                if (entry.isServerAdmin == 1)
                    carTreeNode.Text = carTreeNode.Text + " ★";
                carTreeNode.Tag = entry;
                entriesTreeView.TopNode.Nodes.Add(carTreeNode);

                foreach (Driver driver in entry.drivers)
                {
                    TreeNode driverTreeNode = new TreeNode();
                    if (driver.firstName != null && driver.firstName.Length > 0)
                    {
                        driverTreeNode.Text = driver.firstName + " " + driver.lastName;
                    }
                    else
                    {
                        driverTreeNode.Text = driver.playerID;
                    }
                    driverTreeNode.Tag = driver;
                    carTreeNode.Nodes.Add(driverTreeNode);
                }
            }
            entriesTreeView.TopNode.Expand();
        }

        private void entriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Selecting the root node
            var selectedNode = entriesTreeView.SelectedNode;
            if (selectedNode.Text == "Entries")
            {
                carSettingsPanel.Visible = false;
                driverSettingsPanel.Visible = false;
            }
            // Selecting a car
            else if (selectedNode.Parent.Text == "Entries")
            {
                carSettingsPanel.Visible = true;
                driverSettingsPanel.Visible = false;
                LoadCarPanel(selectedNode);
            }
            // Selecting a driver
            else
            {
                carSettingsPanel.Visible = false;
                driverSettingsPanel.Visible = true;
                LoadDriverPanel(selectedNode);
            }
        }

        private void LoadDriverPanel(TreeNode selectedNode)
        {
            Driver driver = (Driver)selectedNode.Tag;

            firstNameTextBox.Text = driver.firstName;
            lastNameTextBox.Text = driver.lastName;
            shortNameTextBox.Text = driver.shortName;
            categoryComboBox.SelectedIndex = driver.driverCategory;
            playerIDTextBox.Text = driver.playerID;
        }

        private void LoadCarPanel(TreeNode selectedNode)
        {
            Entry entry = (Entry)selectedNode.Tag;

            carNumberNumericUpDown.Value = entry.raceNumber;
            foreach (Car car in forcedCarModelComboBox.Items)
            {
                if (car.ID == entry.forcedCarModel)
                {
                    forcedCarModelComboBox.SelectedItem = car;
                    break;
                }
            }
            customCarComboBox.SelectedIndex = 0;
            customCarComboBox.SelectedItem = entry.customCar;
            overrideCustomCarModelCheckBox.Checked = entry.overrideCarModelForCustomCar == 1;
            gridPositionNumericUpDown.Value = entry.defaultGridPosition;
            ballastNumericUpDown.Value = entry.ballastKg;
            restrictorNumericUpDown.Value = entry.restrictor;
            adminCheckBox.Checked = entry.isServerAdmin == 1 ? true : false;
            if (selectedNode.Nodes.Count > 1)
            {
                overrideDriverInfoCheckBox.Checked = true;
                overrideDriverInfoCheckBox.Enabled = false;
            }
            else
            {
                overrideDriverInfoCheckBox.Enabled = true;
                overrideDriverInfoCheckBox.Checked = entry.overrideDriverInfo == 1 ? true : false;
            }
        }

        private void forcedCarModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.forcedCarModel = ((Car)forcedCarModelComboBox.SelectedItem).ID;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void customCarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.customCar = customCarComboBox.Text != "None" ? customCarComboBox.Text : "";
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void carNumberNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.raceNumber = (int)carNumberNumericUpDown.Value;
            entriesTreeView.SelectedNode.Tag = entry;
            entriesTreeView.SelectedNode.Text = carNumberNumericUpDown.Value.ToString();
            if (entry.isServerAdmin == 1)
                entriesTreeView.SelectedNode.Text = entriesTreeView.SelectedNode.Text + " ★";
        }

        private void gridPositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.defaultGridPosition = (int)gridPositionNumericUpDown.Value;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void ballastNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.ballastKg = (int)ballastNumericUpDown.Value;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void restrictorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.restrictor = (int)restrictorNumericUpDown.Value;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void overrideCustomCarModelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.overrideCarModelForCustomCar = overrideCustomCarModelCheckBox.Checked == true ? 1 : 0;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void adminCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.isServerAdmin = adminCheckBox.Checked == true ? 1 : 0;
            Font regularFont = new Font(entriesTreeView.Font, FontStyle.Regular);
            Font boldFont = new Font(entriesTreeView.Font, FontStyle.Bold);
            //entriesTreeView.SelectedNode.NodeFont = adminCheckBox.Checked == true ? boldFont : regularFont;
            entriesTreeView.SelectedNode.Text = entry.raceNumber.ToString();
            if (entry.isServerAdmin == 1)
                entriesTreeView.SelectedNode.Text = entriesTreeView.SelectedNode.Text + " ★";
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void overrideDriverInfoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.overrideDriverInfo = overrideDriverInfoCheckBox.Checked == true ? 1 : 0;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void forceEntryListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            entrylist.forceEntryList = forceEntryListCheckBox.Checked == true ? 1 : 0;
            entriesTreeView.TopNode.Tag = entrylist;
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Driver driver = (Driver)entriesTreeView.SelectedNode.Tag;
            driver.firstName = firstNameTextBox.Text;
            entriesTreeView.SelectedNode.Tag = driver;
            if (driver.firstName != null && driver.firstName.Length > 0)
            {
                entriesTreeView.SelectedNode.Text = driver.firstName + " " + driver.lastName;
            }
            else
            {
                entriesTreeView.SelectedNode.Text = driver.playerID;
            }
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Driver driver = (Driver)entriesTreeView.SelectedNode.Tag;
            driver.lastName = lastNameTextBox.Text;
            entriesTreeView.SelectedNode.Tag = driver;
            if (driver.firstName != null && driver.firstName.Length > 0)
            {
                entriesTreeView.SelectedNode.Text = driver.firstName + " " + driver.lastName;
            }
            else
            {
                entriesTreeView.SelectedNode.Text = driver.playerID;
            }
        }

        private void shortNameTextBox_TextChanged(object sender, EventArgs e)
        {
            Driver driver = (Driver)entriesTreeView.SelectedNode.Tag;
            driver.shortName = shortNameTextBox.Text;
            entriesTreeView.SelectedNode.Tag = driver;
            if (driver.firstName != null && driver.firstName.Length > 0)
            {
                entriesTreeView.SelectedNode.Text = driver.firstName + " " + driver.lastName;
            }
            else
            {
                entriesTreeView.SelectedNode.Text = driver.playerID;
            }
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Driver driver = (Driver)entriesTreeView.SelectedNode.Tag;
            driver.driverCategory = categoryComboBox.SelectedIndex;
            entriesTreeView.SelectedNode.Tag = driver;
        }

        private void playerIDTextBox_TextChanged(object sender, EventArgs e)
        {
            Driver driver = (Driver)entriesTreeView.SelectedNode.Tag;
            driver.playerID = playerIDTextBox.Text;
            entriesTreeView.SelectedNode.Tag = driver;
            if (driver.firstName != null && driver.firstName.Length > 0)
            {
                entriesTreeView.SelectedNode.Text = driver.firstName + " " + driver.lastName;
            }
            else
            {
                entriesTreeView.SelectedNode.Text = driver.playerID;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            cleanUpAndSaveFile();
            closeButtonClicked = true;
            this.Close();
        }

        private void EntriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeButtonClicked)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes before closing?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cleanUpAndSaveFile();
                }
            }
        }

        private void cleanUpAndSaveFile()
        {
            EntryListObject nEntryList = (EntryListObject)entriesTreeView.TopNode.Tag;

            if (nEntryList.entries == null)
                nEntryList.entries = new List<Entry>();

            nEntryList.entries.Clear();

            foreach (TreeNode node in entriesTreeView.TopNode.Nodes)
            {
                Entry entry = (Entry)node.Tag;
                if (entry.drivers == null)
                    entry.drivers = new List<Driver>();

                entry.drivers.Clear();

                foreach (TreeNode dNode in node.Nodes)
                {
                    Driver driver = (Driver)dNode.Tag;
                    entry.drivers.Add(driver);
                }
                nEntryList.entries.Add(entry);
            }

            nEntryList.entries.Sort(delegate (Entry x, Entry y) { return x.raceNumber.CompareTo(y.raceNumber); });
            File.WriteAllText(@"cfg\entrylist.json", JsonConvert.SerializeObject(nEntryList, Formatting.Indented), Encoding.Unicode);
        }

        private void entriesTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure this is the right button.
            if (e.Button != MouseButtons.Right) return;

            // Select this node.
            TreeNode node_here = entriesTreeView.GetNodeAt(e.X, e.Y);
            entriesTreeView.SelectedNode = node_here;

            // See if we got a node.
            if (node_here == null) return;

            // See what kind of object this is and
            // display the appropriate popup menu.
            if (node_here.Tag is Entry)
                entryContextMenuStrip.Show(entriesTreeView, new Point(e.X, e.Y));
            else if (node_here.Tag is EntryListObject)
                entryListContextMenuStrip.Show(entriesTreeView, new Point(e.X, e.Y));
            else if (node_here.Tag is Driver)
                driverContextMenuStrip.Show(entriesTreeView, new Point(e.X, e.Y));
        }

        private void addDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Driver driver = new Driver();
            driver.firstName = "New";
            driver.lastName = "Driver";
            driver.shortName = "";
            driver.playerID = "";
            driver.driverCategory = 0;

            TreeNode node = new TreeNode();
            node.Text = driver.firstName + " " + driver.lastName;
            node.Tag = driver;

            entriesTreeView.SelectedNode.Nodes.Add(node);
            entriesTreeView.SelectedNode = node;

            firstNameTextBox.Focus();
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entry entry = new Entry();
            entry.raceNumber = 0;
            entry.forcedCarModel = -1;
            entry.overrideDriverInfo = 0;
            entry.isServerAdmin = 0;
            entry.defaultGridPosition = 0;
            entry.ballastKg = 0;
            entry.restrictor = 0;
            entry.customCar = "";
            entry.overrideCarModelForCustomCar = 0;
            entry.drivers = new List<Driver>();

            TreeNode node = new TreeNode();
            node.Text = entry.raceNumber.ToString();
            node.Tag = entry;

            entriesTreeView.TopNode.Nodes.Add(node);
            entriesTreeView.SelectedNode = node;

            carNumberNumericUpDown.Focus();
        }

        private void removeEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.SelectedNode.Remove();
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.SelectedNode.Remove();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.ExpandAll();
        }

        private void collapsAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.CollapseAll();
            entriesTreeView.TopNode.Expand();
        }

        private void expandAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            entriesTreeView.ExpandAll();
        }

        private void collapsAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            entriesTreeView.CollapseAll();
            entriesTreeView.TopNode.Expand();
        }

        private void expandAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            entriesTreeView.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.CollapseAll();
            entriesTreeView.TopNode.Expand();
        }


        bool selectByMouse = false;

        private void quickBoxs_Enter(object sender, EventArgs e)
        {
            NumericUpDown curBox = sender as NumericUpDown;
            curBox.Select();
            curBox.Select(0, curBox.Text.Length);
            if (MouseButtons == MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }

        private void quickBoxs_MouseDown(object sender, MouseEventArgs e)
        {
            NumericUpDown curBox = sender as NumericUpDown;
            if (selectByMouse)
            {
                curBox.Select(0, curBox.Text.Length);
                selectByMouse = false;
            }
        }

        private void shortNameTextBox_Enter(object sender, EventArgs e)
        {
            if (shortNameTextBox.Text == "" && firstNameTextBox.Text.Length > 1 && lastNameTextBox.Text.Length > 1)
                shortNameTextBox.Text = (firstNameTextBox.Text.Substring(0, 2) + lastNameTextBox.Text.Substring(0, 2)).ToUpper();
        }


        //move the nodes up/down
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ActiveControl != entriesTreeView)
                return base.ProcessCmdKey(ref msg, keyData);

            //capture up arrow key
            if (keyData == Keys.Up)
            {
                TreeNode node = entriesTreeView.SelectedNode;
                entriesTreeView.SelectedNode.MoveUp();
                node.TreeView.SelectedNode = node;
                return true;
            }
            //capture down arrow key
            if (keyData == Keys.Down)
            {
                TreeNode node = entriesTreeView.SelectedNode;
                entriesTreeView.SelectedNode.MoveDown();
                node.TreeView.SelectedNode = node;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public static class Extensions
    {
        public static void MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                }
            }
        }

        public static void MoveDown(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                }
            }
        }
    }
}
