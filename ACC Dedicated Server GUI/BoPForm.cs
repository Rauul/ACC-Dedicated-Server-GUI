using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.BoP;
using System.Threading;

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

            foreach (Car car in MainForm.carList)
                carListBox.Items.Add(car);
            foreach (Track track in MainForm.trackList)
                trackListBox.Items.Add(track);

            FillBopTable();
            carListBox.SelectedIndex = 0;
            trackListBox.SelectedIndex = 0;
            UpdateTrackList();
        }

        private void FillBopTable()
        {
            if (bop.entries == null)
                bop.entries = new List<Entry>();

            foreach (Track track in trackListBox.Items)
            {
                for (int i = 0; i < carListBox.Items.Count; i++)
                {
                    Entry entry = new Entry();
                    entry.track = track.alias;
                    entry.carModel = i;
                    entry.ballast = 0;
                    entry.restrictor = 0;

                    if (!bop.entries.Any(e => e.track == entry.track && e.carModel == entry.carModel))
                        bop.entries.Add(entry);
                }
            }
        }

        private void UpdateTrackList()
        {
            trackListBox.BeginUpdate();

            int selectedIndex = trackListBox.SelectedIndex;
            List<Entry> entries = new List<Entry>(bop.entries);
            entries.RemoveAll(ent => (ent.ballast == 0) && (ent.restrictor == 0));

            for (int i = 0; i < trackListBox.Items.Count; i++)
            {
                Track track = (Track)trackListBox.Items[i];
                if (track.name.Contains("★") && !entries.Any(e => e.track == track.alias))
                {
                    track.name = track.name.Replace("★ ", "    ");
                    trackListBox.Items.RemoveAt(i);
                    trackListBox.Items.Insert(i, track);
                }
                else if (!track.name.Contains("★") && entries.Any(e => e.track == track.alias))
                {
                    track.name = track.name.Replace("    ", "★ ");
                    trackListBox.Items.RemoveAt(i);
                    trackListBox.Items.Insert(i, track);
                }
            }

            trackListBox.SelectedIndex = selectedIndex;
            trackListBox.EndUpdate();
        }

        private void UpdateCarList()
        {
            carListBox.BeginUpdate();

            int selectedIndex = carListBox.SelectedIndex;
            Track track = (Track)trackListBox.SelectedItem;
            List<Entry> entries = new List<Entry>(bop.entries);
            entries.RemoveAll(ent => ent.track != track.alias);
            entries.RemoveAll(ent => (ent.ballast == 0) && (ent.restrictor == 0));

            for (int i = 0; i < carListBox.Items.Count; i++)
            {
                Car car = (Car)carListBox.Items[i];
                if (car.model.Contains("★") && !entries.Any(e => e.carModel == car.ID))
                {
                    car.model = car.model.Replace("★ ", "    ");
                    carListBox.Items.RemoveAt(i);
                    carListBox.Items.Insert(i, car);
                }
                else if (!car.model.Contains("★") && entries.Any(e => e.carModel == car.ID))
                {
                    car.model = car.model.Replace("    ", "★ ");
                    carListBox.Items.RemoveAt(i);
                    carListBox.Items.Insert(i, car);
                }
            }
            carListBox.SelectedIndex = selectedIndex;
            carListBox.EndUpdate();
        }

        private void UpdateCurrentTrack()
        {
            trackListBox.BeginUpdate();

            Track track = (Track)trackListBox.SelectedItem;
            int selectedIndex = trackListBox.SelectedIndex;

            List<Entry> entries = new List<Entry>(bop.entries);
            entries.RemoveAll(ent => (ent.ballast == 0) && (ent.restrictor == 0));

            if (track.name.Contains("★") && !entries.Any(e => e.track == track.alias))
            {
                track.name = track.name.Replace("★ ", "    ");
                trackListBox.Items.RemoveAt(selectedIndex);
                trackListBox.Items.Insert(selectedIndex, track);
            }
            else if (!track.name.Contains("★") && entries.Any(e => e.track == track.alias))
            {
                track.name = track.name.Replace("    ", "★ ");
                trackListBox.Items.RemoveAt(selectedIndex);
                trackListBox.Items.Insert(selectedIndex, track);
            }

            trackListBox.SelectedIndex = selectedIndex;
            trackListBox.EndUpdate();
        }

        private void UpdateCurrentCar()
        {
            carListBox.BeginUpdate();

            Car car = (Car)carListBox.SelectedItem;
            Track track = (Track)trackListBox.SelectedItem;
            int selectedIndex = carListBox.SelectedIndex;

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => ent.track == track.alias && ent.carModel == car.ID);
            entry.ballast = ballastTrackBar.Value;
            entry.restrictor = restrictorTrackBar.Value;

            if (ballastTrackBar.Value + restrictorTrackBar.Value > 0)
            {
                if (!car.model.Contains("★ "))
                {
                    car.model = car.model.Replace("    ", "★ ");
                    carListBox.Items.RemoveAt(selectedIndex);
                    carListBox.Items.Insert(selectedIndex, car);
                }
            }
            else if (car.model.Contains("★ "))
            {
                car.model = car.model.Replace("★ ", "    ");
                carListBox.Items.RemoveAt(selectedIndex);
                carListBox.Items.Insert(selectedIndex, car);
            }

            carListBox.SelectedIndex = selectedIndex;
            carListBox.EndUpdate();
        }

        private void UpdateTrackBars()
        {
            
            Car car = (Car)carListBox.SelectedItem;
            Track track = (Track)trackListBox.SelectedItem;

            Entry entry = new Entry();
            entry = bop.entries.Find(ent => ent.track == track.alias && ent.carModel == car.ID);

            ballastTrackBar.Value = InTrackBarRange(entry.ballast, ballastTrackBar);
            restrictorTrackBar.Value = InTrackBarRange(entry.restrictor, restrictorTrackBar);
        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carListBox.SelectedIndex < 0)
            {
                carListBox.SelectedIndex = 0;
                return;
            }
            if (trackListBox.SelectedIndex < 0)
            {
                trackListBox.SelectedIndex = 0;
                return;
            }

            ballastTrackBar.Enabled = true;
            restrictorTrackBar.Enabled = true;

            UpdateTrackBars();
            UpdateCarList();
        }

        private void carListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trackListBox.SelectedIndex < 0)
            {
                trackListBox.SelectedIndex = 0;
                return;
            }
            if (carListBox.SelectedIndex < 0)
            {
                carListBox.SelectedIndex = 0;
                return;
            }

            ballastTrackBar.Enabled = true;
            restrictorTrackBar.Enabled = true;

            UpdateTrackBars();
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

            e.Cancel = true;
            this.Hide();
        }

        private void ballastTrackBar_Scroll(object sender, EventArgs e)
        {
            ballastLabel.Text = ballastTrackBar.Value.ToString();
        }

        private void restrictorTrackBar_Scroll(object sender, EventArgs e)
        {
            restrictorLabel.Text = restrictorTrackBar.Value.ToString();
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

        private void ballastTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCurrentCar();
            UpdateCurrentTrack();
        }

        private void restrictorTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCurrentCar();
            UpdateCurrentTrack();
        }

        private void ballastTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ballastLabel.Text = ballastTrackBar.Value.ToString();
        }

        private void restrictorTrackBar_ValueChanged(object sender, EventArgs e)
        {
            restrictorLabel.Text = restrictorTrackBar.Value.ToString();
        }

        private void BoPForm_Activated(object sender, EventArgs e)
        {
            closeButtonClicked = false;
            FillBopTable();
        }
    }
}
