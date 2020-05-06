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

        private int InTrackBarRange(int value, TrackBar trackBar)
        {
            if (value < trackBar.Minimum)
                return trackBar.Minimum;
            if (value > trackBar.Maximum)
                return trackBar.Maximum;
            return value;
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

        private void BoPForm_Load(object sender, EventArgs e)
        {
            string file = @"cfg\bop.json";
            if (File.Exists(file))
            {
                Encoding encoding = GetEncoding(file);
                string rawJSON = File.ReadAllText(file, encoding);
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
            ballastTrackBar.Enabled = true;
            restrictorTrackBar.Enabled = true;

            string track = trackListBox.SelectedItem.ToString();
            int carModel = carListBox.SelectedIndex;

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => ent.track == track && ent.carModel == carModel);

            ballastNumericUpDown.Value = InTrackBarRange(entry.ballast, ballastTrackBar);
            restrictorNumericUpDown.Value = InTrackBarRange(entry.restrictor, restrictorTrackBar);
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
            ballastTrackBar.Enabled = true;
            restrictorTrackBar.Enabled = true;

            int carModel = carListBox.SelectedIndex;
            string track = trackListBox.SelectedItem.ToString();

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel));

            ballastNumericUpDown.Value = InTrackBarRange(entry.ballast, ballastTrackBar);
            restrictorNumericUpDown.Value = InTrackBarRange(entry.restrictor, restrictorTrackBar);
        }

        private void ballastNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            string track = trackListBox.Text;
            int carModel = carListBox.SelectedIndex;

            ballastTrackBar.Value = (int)ballastNumericUpDown.Value;
            bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel)).ballast = (int)ballastNumericUpDown.Value;
        }

        private void restrictorNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            string track = trackListBox.Text;
            int carModel = carListBox.SelectedIndex;

            restrictorTrackBar.Value = (int)restrictorNumericUpDown.Value;
            bop.entries.Find(ent => (ent.track == track) && (ent.carModel == carModel)).restrictor = (int)restrictorNumericUpDown.Value;
        }
        private void cleanUpAndSaveFile()
        {
            bop.entries.RemoveAll(ent => (ent.ballast == 0) && (ent.restrictor == 0));
            File.WriteAllText(@"cfg\bop.json", JsonConvert.SerializeObject(bop, Formatting.Indented), Encoding.Unicode);
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

        private void ballastTrackBar_Scroll(object sender, EventArgs e)
        {
            ballastNumericUpDown.Value = ballastTrackBar.Value;
        }

        private void restrictorTrackBar_Scroll(object sender, EventArgs e)
        {
            restrictorNumericUpDown.Value = restrictorTrackBar.Value;
        }
    }
}
