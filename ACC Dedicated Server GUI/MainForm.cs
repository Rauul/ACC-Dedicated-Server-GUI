using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.AssistRules;
using static ACC_Dedicated_Server_GUI.Configuration;
using static ACC_Dedicated_Server_GUI.Event;
using static ACC_Dedicated_Server_GUI.EventRules;
using static ACC_Dedicated_Server_GUI.Settings;

namespace ACC_Dedicated_Server_GUI
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        SettingsObject settings = new SettingsObject();
        AssistObject assist = new AssistObject();
        EventObject eventObject = new EventObject();
        EventRulesObject eventRules = new EventRulesObject();
        ConfigurationObject configuration = new ConfigurationObject();

        Panel consolePanel = new Panel();

        const int WM_SYSCOMMAND = 274;
        const int SC_MAXIMIZE = 61488;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Application.Exit();
        }

        private void tempTrackBar_Scroll(object sender, EventArgs e)
        {
            tempLabel.Text = tempTrackBar.Value.ToString();
        }

        private void cloudCoverageTrackBar_Scroll(object sender, EventArgs e)
        {
            cloudCoverageLabel.Text = ((float)cloudCoverageTrackBar.Value / 10).ToString("0.0#");
        }

        private void rainTrackBar_Scroll(object sender, EventArgs e)
        {
            rainLabel.Text = ((float)rainTrackBar.Value / 10).ToString("0.0#");
        }

        private void weatherRandomnessTrackBar_Scroll(object sender, EventArgs e)
        {
            weatherRandomnessLabel.Text = weatherRandomnessTrackBar.Value.ToString();
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

        private void LoadConfig()
        {
            if (!Directory.Exists(@"cfg\"))
                Directory.CreateDirectory(@"cfg\");

            string rawJSON;
            string file = @"cfg\settings.json";

            if (File.Exists(file))
            {
                // settings.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                settings = JsonConvert.DeserializeObject<SettingsObject>(rawJSON);

                serverNameTextBox.Text = settings.serverName;
                adminPasswordTextBox.Text = settings.adminPassword;
                joinPasswordTextBox.Text = settings.password;
                spectatorPasswordTextBox.Text = settings.spectatorPassword;
                maxCarSlotsNumericUpDown.Value = settings.maxCarSlots;
                TRRequirementNumericUpDown.Value = settings.trackMedalsRequirement;
                SARequirementNumericUpDown.Value = settings.safetyRatingRequirement;
                RCRequirementNumericUpDown.Value = settings.racecraftRatingRequirement;
                isRaceLockedCheckBox.Checked = settings.isRaceLocked == 1 ? true : false;
                shortFormationCheckBox.Checked = settings.shortFormationLap == 1 ? true : false;
            }

            file = @"cfg\assistRules.json";
            if (File.Exists(file))
            {
                // assistRules.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                assist = JsonConvert.DeserializeObject<AssistObject>(rawJSON);

                idealLineCheckBox.Checked = assist.disableIdealLine == 0;
                autoSteeringCheckBox.Checked = assist.disableAutosteer == 0;
                autoPitLimiterCheckBox.Checked = assist.disableAutoPitLimiter == 0;
                autoShiftingCheckBox.Checked = assist.disableAutoGear == 0;
                autoStartEngineCheckBox.Checked = assist.disableAutoEngineStart == 0;
                autoWipersCheckBox.Checked = assist.disableAutoWiper == 0;
                autoLightsCheckBox.Checked = assist.disableAutoLights == 0;
                autoClutchCheckBox.Checked = assist.disableAutoClutch == 0;
                maxStabilityNumericUpDown.Value = assist.stabilityControlLevelMax;
            }

            file = @"cfg\event.json";
            if (File.Exists(file))
            {
                // event.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                eventObject = JsonConvert.DeserializeObject<EventObject>(rawJSON);

                TrackComboBox.SelectedItem = eventObject.track;
                preRaceWaitTimeNumericUpDown.Value = eventObject.preRaceWaitingTimeSeconds;
                overTimeNumericUpDown.Value = eventObject.sessionOverTimeSeconds;
                tempTrackBar.Value = InTrackBarRange(eventObject.ambientTemp, tempTrackBar);
                cloudCoverageTrackBar.Value = InTrackBarRange((int)(eventObject.cloudLevel * 10), cloudCoverageTrackBar);
                rainTrackBar.Value = InTrackBarRange((int)(eventObject.rain * 10), rainTrackBar);
                weatherRandomnessTrackBar.Value = InTrackBarRange(eventObject.weatherRandomness, weatherRandomnessTrackBar);

                foreach (Session session in eventObject.sessions)
                {
                    switch (session.sessionType.ToUpper())
                    {
                        case "P":
                            pCheckBox.Checked = true;
                            pStartTimeNumericUpDown.Value = session.hourOfDay;
                            pTimeScaleNumericUpDown.Value = session.timeMultiplier;
                            pDurationNumericUpDown.Value = session.sessionDurationMinutes;
                            switch (session.dayOfWeekend)
                            {
                                case 1:
                                    pFridayRadioButton.Checked = true;
                                    break;
                                case 2:
                                    pSaturdayRadioButton.Checked = true;
                                    break;
                                case 3:
                                    pSundayRadioButton.Checked = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Q":
                            qCheckBox.Checked = true;
                            qStartTimeNumericUpDown.Value = session.hourOfDay;
                            qTimeScaleNumericUpDown.Value = session.timeMultiplier;
                            qDurationNumericUpDown.Value = session.sessionDurationMinutes;
                            switch (session.dayOfWeekend)
                            {
                                case 1:
                                    qFridayRadioButton.Checked = true;
                                    break;
                                case 2:
                                    qSaturdayRadioButton.Checked = true;
                                    break;
                                case 3:
                                    qSundayRadioButton.Checked = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "R":
                            rCheckBox.Checked = true;
                            rStartTimeNumericUpDown.Value = session.hourOfDay;
                            rTimeScaleNumericUpDown.Value = session.timeMultiplier;
                            rDurationNumericUpDown.Value = session.sessionDurationMinutes;
                            switch (session.dayOfWeekend)
                            {
                                case 1:
                                    rFridayRadioButton.Checked = true;
                                    break;
                                case 2:
                                    rSaturdayRadioButton.Checked = true;
                                    break;
                                case 3:
                                    rSundayRadioButton.Checked = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            file = @"cfg\eventRules.json";
            if (File.Exists(file))
            {
                // eventRules.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                eventRules = JsonConvert.DeserializeObject<EventRulesObject>(rawJSON);

                pitWindowsLengthNumericUpDown.Value = eventRules.pitWindowLengthSec;
                driverStintTimeNumericUpDown.Value = eventRules.driverStintTimeSec;
                mandatoryPitStopCountNumericUpDown.Value = eventRules.mandatoryPitstopCount;
                maxTotalDrivingTimeNumericUpDown.Value = eventRules.maxTotalDrivingTime;
                maxDriversCountNumericUpDown.Value = eventRules.maxDriversCount;
                refuellingAllowedCheckBox.Checked = eventRules.isRefuellingAllowedInRace;
                refuellingTimeFixedCheckBox.Checked = eventRules.isRefuellingTimeFixed;
                refuellingRequiredCheckBox.Checked = eventRules.isMandatoryPitstopRefuellingRequired;
                tyreChangeRequiredCheckBox.Checked = eventRules.isMandatoryPitstopTyreChangeRequired;
                driverSwapRequiredCheckBox.Checked = eventRules.isMandatoryPitstopSwapDriverRequired;
            }

            file = @"cfg\configuration.json";
            if (File.Exists(file))
            {
                // configuration.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                configuration = JsonConvert.DeserializeObject<ConfigurationObject>(rawJSON);

                UDPNumericUpDown.Value = configuration.udpPort;
                TCPNumericUpDown.Value = configuration.tcpPort;
                maxConnectionsNumericUpDown.Value = configuration.maxConnections;
                lanDiscoveryCheckBox.Checked = configuration.lanDiscovery == 1 ? true : false;
                registerToLobbyCheckBox.Checked = configuration.registerToLobby == 1 ? true : false;
            }
        }

        private void SaveConfig()
        {
            // setting.json
            settings.serverName = serverNameTextBox.Text;
            settings.password = joinPasswordTextBox.Text;
            settings.adminPassword = adminPasswordTextBox.Text;
            settings.spectatorPassword = spectatorPasswordTextBox.Text;
            settings.maxCarSlots = (int)maxCarSlotsNumericUpDown.Value;
            settings.trackMedalsRequirement = (int)TRRequirementNumericUpDown.Value;
            settings.safetyRatingRequirement = (int)SARequirementNumericUpDown.Value;
            settings.racecraftRatingRequirement = (int)RCRequirementNumericUpDown.Value;
            settings.isRaceLocked = isRaceLockedCheckBox.Checked ? 1 : 0;
            settings.shortFormationLap = shortFormationCheckBox.Checked ? 1 : 0;
            settings.configVersion = 1;

            // assistRules.json
            assist.disableIdealLine = idealLineCheckBox.Checked ? 0 : 1;
            assist.disableAutosteer = autoSteeringCheckBox.Checked ? 0 : 1;
            assist.disableAutoPitLimiter = autoPitLimiterCheckBox.Checked ? 0 : 1;
            assist.disableAutoGear = autoShiftingCheckBox.Checked ? 0 : 1;
            assist.disableAutoEngineStart = autoStartEngineCheckBox.Checked ? 0 : 1;
            assist.disableAutoWiper = autoWipersCheckBox.Checked ? 0 : 1;
            assist.disableAutoLights = autoLightsCheckBox.Checked ? 0 : 1;
            assist.disableAutoClutch = autoClutchCheckBox.Checked ? 0 : 1;
            assist.stabilityControlLevelMax = (int)maxStabilityNumericUpDown.Value;

            // event.json
            eventObject.track = TrackComboBox.Text;
            eventObject.preRaceWaitingTimeSeconds = (int)preRaceWaitTimeNumericUpDown.Value;
            eventObject.sessionOverTimeSeconds = (int)overTimeNumericUpDown.Value;
            eventObject.ambientTemp = tempTrackBar.Value;
            eventObject.cloudLevel = (float)cloudCoverageTrackBar.Value / 10;
            eventObject.rain = (float)rainTrackBar.Value / 10;
            eventObject.weatherRandomness = weatherRandomnessTrackBar.Value;
            eventObject.configVersion = 1;

            if (eventObject.sessions == null)
                eventObject.sessions = new List<Session>();
            eventObject.sessions.Clear();

            if (pCheckBox.Checked)
            {
                Session session = new Session();

                session.sessionType = "P";
                session.hourOfDay = (int)pStartTimeNumericUpDown.Value;
                session.timeMultiplier = (int)pTimeScaleNumericUpDown.Value;
                session.sessionDurationMinutes = (int)pDurationNumericUpDown.Value;
                if (pFridayRadioButton.Checked)
                    session.dayOfWeekend = 1;
                else if (pSaturdayRadioButton.Checked)
                    session.dayOfWeekend = 2;
                else
                    session.dayOfWeekend = 3;

                eventObject.sessions.Add(session);
            }

            if (qCheckBox.Checked)
            {
                Session session = new Session();

                session.sessionType = "Q";
                session.hourOfDay = (int)qStartTimeNumericUpDown.Value;
                session.timeMultiplier = (int)qTimeScaleNumericUpDown.Value;
                session.sessionDurationMinutes = (int)qDurationNumericUpDown.Value;
                if (qFridayRadioButton.Checked)
                    session.dayOfWeekend = 1;
                else if (qSaturdayRadioButton.Checked)
                    session.dayOfWeekend = 2;
                else
                    session.dayOfWeekend = 3;

                eventObject.sessions.Add(session);
            }

            if (rCheckBox.Checked)
            {
                Session session = new Session();

                session.sessionType = "R";
                session.hourOfDay = (int)rStartTimeNumericUpDown.Value;
                session.timeMultiplier = (int)rTimeScaleNumericUpDown.Value;
                session.sessionDurationMinutes = (int)rDurationNumericUpDown.Value;
                if (rFridayRadioButton.Checked)
                    session.dayOfWeekend = 1;
                else if (rSaturdayRadioButton.Checked)
                    session.dayOfWeekend = 2;
                else
                    session.dayOfWeekend = 3;

                eventObject.sessions.Add(session);
            }

            // eventRules.json
            eventRules.qualifyStandingType = 1;
            eventRules.pitWindowLengthSec = (int)pitWindowsLengthNumericUpDown.Value;
            eventRules.driverStintTimeSec = (int)driverStintTimeNumericUpDown.Value;
            eventRules.mandatoryPitstopCount = (int)mandatoryPitStopCountNumericUpDown.Value;
            eventRules.maxTotalDrivingTime = (int)maxTotalDrivingTimeNumericUpDown.Value;
            eventRules.maxDriversCount = (int)maxDriversCountNumericUpDown.Value;
            eventRules.isRefuellingAllowedInRace = refuellingAllowedCheckBox.Checked;
            eventRules.isRefuellingTimeFixed = refuellingTimeFixedCheckBox.Checked;
            eventRules.isMandatoryPitstopRefuellingRequired = refuellingRequiredCheckBox.Checked;
            eventRules.isMandatoryPitstopTyreChangeRequired = tyreChangeRequiredCheckBox.Checked;
            eventRules.isMandatoryPitstopSwapDriverRequired = driverSwapRequiredCheckBox.Checked;

            // configuration.json
            configuration.udpPort = (int)UDPNumericUpDown.Value;
            configuration.tcpPort = (int)TCPNumericUpDown.Value;
            configuration.maxConnections = (int)maxConnectionsNumericUpDown.Value;
            configuration.lanDiscovery = lanDiscoveryCheckBox.Checked ? 1 : 0;
            configuration.registerToLobby = registerToLobbyCheckBox.Checked ? 1 : 0;
            configuration.configVersion = 1;

            File.WriteAllText(@"cfg\settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented), Encoding.Unicode);
            File.WriteAllText(@"cfg\assistRules.json", JsonConvert.SerializeObject(assist, Formatting.Indented), Encoding.Unicode);
            File.WriteAllText(@"cfg\event.json", JsonConvert.SerializeObject(eventObject, Formatting.Indented), Encoding.Unicode);
            File.WriteAllText(@"cfg\eventRules.json", JsonConvert.SerializeObject(eventRules, Formatting.Indented), Encoding.Unicode);
            File.WriteAllText(@"cfg\configuration.json", JsonConvert.SerializeObject(configuration, Formatting.Indented), Encoding.Unicode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrackComboBox.SelectedIndex = 0;

            consolePanel.Visible = false;

            Size size = new Size();
            size = panel3.Size;
            size.Height += 27;
            consolePanel.Size = size;

            consolePanel.Parent = panel3;

            Point location = new Point(0, -27);
            consolePanel.Location = location;
#if !DEBUG
            if (!File.Exists("accServer.exe"))
            {
                MessageBox.Show("File 'accServer.exe' not found. Please make sure you installed this program to your 'Assetto Corsa Competizione\\server' folder.\n\nClosing...", "ACC Dedicated Server GUI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
#endif
            try
            {
                LoadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void launchServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName("accServer").Length == 0)
                {
                    SaveConfig();
#if !DEBUG          
                    consolePanel.Visible = true;
                    consolePanel.BringToFront();

                    Process process = new Process();
                    process.StartInfo.FileName = "accServer.exe";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                    process.Start();
                    Thread.Sleep(100);
                    SetParent(process.MainWindowHandle, consolePanel.Handle);
                    SendMessage(process.MainWindowHandle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                    label12.Visible = false;
                }
                else
                {
                    foreach (Process process in Process.GetProcessesByName("accServer"))
                    {
                        process.Kill();
                    }
                    consolePanel.Visible = false;
                    label12.Visible = true;
#endif
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void practiceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            pPanel.Enabled = pCheckBox.Checked;
        }

        private void qualifyingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPanel.Enabled = qCheckBox.Checked;
        }

        private void raceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPanel.Enabled = rCheckBox.Checked;
        }

        private void EntriesToolStripButton_Click(object sender, EventArgs e)
        {
            EntriesForm entriesForm = new EntriesForm();
            entriesForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            BoPForm boPForm = new BoPForm();
            boPForm.Show();
        }

        private void entryListButton_Click(object sender, EventArgs e)
        {
            EntriesForm entriesForm = new EntriesForm();
            entriesForm.Show();
        }

        private void BopButton_Click(object sender, EventArgs e)
        {
            BoPForm boPForm = new BoPForm();
            boPForm.Show();
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
    }
}
