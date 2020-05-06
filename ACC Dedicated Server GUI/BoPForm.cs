using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.BoP;

namespace ACC_Dedicated_Server_GUI
{
    public partial class BoPForm : Form
    {
        BopObject bop = new BopObject();
        bool closeButtonClicked = false;

        public BoPForm()
        {
            InitializeComponent();
        }

        private void BoPForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(@"cfg\bop.json"))
            {
                string rawJSON = File.ReadAllText(@"cfg\bop.json");
                bop = JsonConvert.DeserializeObject<BopObject>(rawJSON);
            }
         
            FillBopTable();
        }

        private void FillBopTable()
        {
            if (bop.entries == null)
                bop.entries = new List<Entry>();

            foreach (var item in trackListBox.Items)
            {
                for (int i = 0; i < carListBox.Items.Count; i++)
                {
                    Entry entry = new Entry();
                    entry.track = item.ToString();
                    entry.carModel = i;
                    entry.ballast = 0;
                    entry.restrictor = 0;

                    if (!bop.entries.Any(e => e.track == entry.track && e.carModel == entry.carModel))
                        bop.entries.Add(entry);
                }
            }
        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carListBox.SelectedIndex < 0)
            {
                carListBox.SelectedIndex = 0;
                return;
            }

            ballastNumericUpDown.Enabled = true;
            restrictorNumericUpDown.Enabled = true;

            string track = trackListBox.SelectedItem.ToString();
            int carModel = carListBox.SelectedIndex;

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => ent.track == track && ent.carModel == carModel);

            ballastNumericUpDown.Value = entry.ballast;
            restrictorNumericUpDown.Value = entry.restrictor;
        }

        private void carListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trackListBox.SelectedIndex < 0)
            {
                trackListBox.SelectedIndex = 0;
                return;
            }

            ballastNumericUpDown.Enabled = true;
            restrictorNumericUpDown.Enabled = true;

            int carModel = carListBox.SelectedIndex;
            string track = trackListBox.SelectedItem.ToString();

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel));

            ballastNumericUpDown.Value = entry.ballast;
            restrictorNumericUpDown.Value = entry.restrictor;
        }

        private void ballastNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            string track = trackListBox.Text;
            int carModel = carListBox.SelectedIndex;

            bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel)).ballast = (int)ballastNumericUpDown.Value;
        }

        private void restrictorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            string track = trackListBox.Text;
            int carModel = carListBox.SelectedIndex;

            bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel)).restrictor = (int)restrictorNumericUpDown.Value;
        }
        private void cleanUpAndSaveFile()
        {
            bop.entries.RemoveAll(ent => (ent.ballast == 0) && (ent.restrictor == 0));
            File.WriteAllText(@"cfg\bop.json", JsonConvert.SerializeObject(bop, Formatting.Indented));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            cleanUpAndSaveFile();
            closeButtonClicked = true;
            this.Close();
        }

        private void BoPForm_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
