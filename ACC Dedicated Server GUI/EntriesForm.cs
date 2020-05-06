using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.EntryList;

namespace ACC_Dedicated_Server_GUI
{
    public partial class EntriesForm : Form
    {
        EntryListObject entrylist = new EntryListObject();
        bool closeButtonClicked = false;
        public EntriesForm()
        {
            InitializeComponent();
        }

        private void EntriesForm_Load(object sender, EventArgs e)
        {
            TreeNode entriesTreeNode = new TreeNode();
            entriesTreeNode.Text = "Entries";
            entriesTreeNode.Tag = entrylist;
            entriesTreeView.Nodes.Add(entriesTreeNode);

            if (File.Exists(@"cfg\entrylist.json"))
            {
                string rawJSON = File.ReadAllText(@"cfg\entrylist.json");
                entrylist = JsonConvert.DeserializeObject<EntryListObject>(rawJSON);
                LoadTreeView(entrylist);
                forceEntryListCheckBox.Checked = entrylist.forceEntryList == 1 ? true : false;
            }

            if (Directory.Exists(@"cfg\cars\"))
            {
                var fileNames = Directory.GetFiles(@"cfg\cars\", "*.json").Select(f => Path.GetFileName(f));
                foreach (string file in fileNames)
                {
                    customCarComboBox.Items.Add(file);
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
            entriesTreeView.TreeViewNodeSorter = new NodeSorter();
            entriesTreeView.Sort();
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
            forcedCarModelComboBox.SelectedIndex = entry.forcedCarModel < 1 ? 0 : entry.forcedCarModel;
            customCarComboBox.SelectedIndex = 0;
            customCarComboBox.SelectedItem = entry.customCar;
            overrideCarModelComboBox.SelectedIndex = entry.overrideCarModelForCustomCar < 1 ? 0 : entry.overrideCarModelForCustomCar;
            gridPositionNumericUpDown.Value = entry.defaultGridPosition;
            ballastNumericUpDown.Value = entry.ballastKg;
            restrictorNumericUpDown.Value = entry.restrictor;
            adminCheckBox.Checked = entry.isServerAdmin == 1 ? true : false;
            overrideDriverInfoCheckBox.Checked = entry.overrideDriverInfo == 1 ? true : false;
        }

        private void forcedCarModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.forcedCarModel = forcedCarModelComboBox.SelectedIndex - 1;
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void customCarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.customCar = customCarComboBox.Text != "None" ? customCarComboBox.Text : "";
            entriesTreeView.SelectedNode.Tag = entry;
        }

        private void overrideCarModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Entry entry = (Entry)entriesTreeView.SelectedNode.Tag;
            entry.overrideCarModelForCustomCar = overrideCarModelComboBox.SelectedIndex - 1;
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
            entriesTreeView.TreeViewNodeSorter = new NodeSorter();
            entriesTreeView.Sort();

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

            File.WriteAllText(@"cfg\entrylist.json", JsonConvert.SerializeObject(nEntryList, Formatting.Indented));
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
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entry entry = new Entry();
            entry.raceNumber = 1;
            entry.forcedCarModel = 0;
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
        }

        private void removeEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.SelectedNode.Remove();
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            entriesTreeView.SelectedNode.Remove();
        }

        // Create a node sorter that implements the IComparer interface.
        public class NodeSorter : IComparer
        {
            // Compare the length of the strings, or the strings
            // themselves, if they are the same length.
            public int Compare(object x, object y)
            {
                TreeNode tx = x as TreeNode;
                TreeNode ty = y as TreeNode;

                if (tx.Parent.Text == "Entries")
                    return (((Entry)tx.Tag).raceNumber - ((Entry)ty.Tag).raceNumber);

                // Compare the length of the strings, returning the difference.
                //if (tx.Text.Length != ty.Text.Length)
                //    return tx.Text.Length - ty.Text.Length;

                // If they are the same length, call Compare.
                return string.Compare(tx.Text, ty.Text);
            }
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
    }
}
