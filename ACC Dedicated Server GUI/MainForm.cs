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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ACC_Dedicated_Server_GUI.AssistRules;
using static ACC_Dedicated_Server_GUI.Event;
using static ACC_Dedicated_Server_GUI.Settings;

namespace ACC_Dedicated_Server_GUI
{
    public partial class MainForm : Form
    {
        SettingsObject settings = new SettingsObject();
        AssistObject assist = new AssistObject();
        EventObject eventObject = new EventObject();

        public MainForm()
        {
            InitializeComponent();
        }

        private void medalsRequirementsTrackBar_Scroll(object sender, EventArgs e)
        {
            TRRequirementLabel.Text = TRRequirementsTrackBar.Value.ToString();
        }

        private void ratingRequirementTrackBar_Scroll(object sender, EventArgs e)
        {
            SARequirementLabel.Text = SARequirementTrackBar.Value.ToString();
        }

        private void maxCarsTrackBar_Scroll(object sender, EventArgs e)
        {
            maxCarsLabel.Text = maxCarsTrackBar.Value.ToString();
        }

        private void RCRequirementTrackBar_Scroll(object sender, EventArgs e)
        {
            RCRequirementLabel.Text = RCRequirementTrackBar.Value.ToString();
        }

        private void stabilityControlLevelTrackBar_Scroll(object sender, EventArgs e)
        {
            stabilityControlLevelLabel.Text = stabilityControlLevelTrackBar.Value.ToString();
        }

        private void practiceStartTimeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (pStartTimeTrackBar.Value < 10)
                pStartTimeLabel.Text = "0" + pStartTimeTrackBar.Value.ToString() + ":00";
            else
                pStartTimeLabel.Text = pStartTimeTrackBar.Value.ToString() + ":00";
        }

        private void practiceTimeScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            pTimeScaleLabel.Text = pTimeScaleTrackBar.Value.ToString();
        }

        private void practiceDurationTrackBar_Scroll(object sender, EventArgs e)
        {
            pDurationLabel.Text = (pDurationTrackBar.Value * 5).ToString();
        }

        private void qStartTimeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (qStartTimeTrackBar.Value < 10)
                qStartTimeLabel.Text = "0" + qStartTimeTrackBar.Value.ToString() + ":00";
            else
                qStartTimeLabel.Text = qStartTimeTrackBar.Value.ToString() + ":00";
        }

        private void qTimeScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            qTimeScaleLabel.Text = qTimeScaleTrackBar.Value.ToString();
        }

        private void qDurationTrackBar_Scroll(object sender, EventArgs e)
        {
            qDurationLabel.Text = (qDurationTrackBar.Value * 5).ToString();
        }

        private void rStartTimeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (rStartTimeTrackBar.Value < 10)
                rStartTimeLabel.Text = "0" + rStartTimeTrackBar.Value.ToString() + ":00";
            else
                rStartTimeLabel.Text = rStartTimeTrackBar.Value.ToString() + ":00";
        }

        private void rTimeScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            rTimeScaleLabel.Text = rTimeScaleTrackBar.Value.ToString();
        }

        private void rDurationTrackBar_Scroll(object sender, EventArgs e)
        {
            rDurationLabel.Text = (rDurationTrackBar.Value * 5).ToString();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Application.Exit();
        }

        private void waitTimeTrackBar_Scroll(object sender, EventArgs e)
        {
            waitTimeLabel.Text = (waitTimeTrackBar.Value * 30).ToString();
        }

        private void overTimeTrackBar_Scroll(object sender, EventArgs e)
        {
            overTimeLabel.Text = (overTimeTrackBar.Value * 30).ToString();
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

        private void LoadConfig()
        {
            string rawJSON = File.ReadAllText(@"cfg\settings.json");
            settings = JsonConvert.DeserializeObject<SettingsObject>(rawJSON);
            rawJSON = File.ReadAllText(@"cfg\assistRules.json");
            assist = JsonConvert.DeserializeObject<AssistObject>(rawJSON);
            rawJSON = File.ReadAllText(@"cfg\event.json");
            eventObject = JsonConvert.DeserializeObject<EventObject>(rawJSON);

            // settings.json
            serverNameTextBox.Text = settings.serverName;
            adminPasswordTextBox.Text = settings.adminPassword;
            joinPasswordTextBox.Text = settings.password;
            spectatorPasswordTextBox.Text = settings.spectatorPassword;
            maxCarsTrackBar.Value = InTrackBarRange(settings.maxCarSlots, maxCarsTrackBar);
            TRRequirementsTrackBar.Value = InTrackBarRange(settings.trackMedalsRequirement, TRRequirementsTrackBar);
            SARequirementTrackBar.Value = InTrackBarRange(settings.safetyRatingRequirement, SARequirementTrackBar);
            RCRequirementTrackBar.Value = InTrackBarRange(settings.racecraftRatingRequirement, RCRequirementTrackBar);

            // assistRules.json
            idealLineCheckBox.Checked = assist.disableIdealLine == 0;
            autoSteeringCheckBox.Checked = assist.disableAutosteer == 0;
            autoPitLimiterCheckBox.Checked = assist.disableAutoPitLimiter == 0;
            autoShiftingCheckBox.Checked = assist.disableAutoGear == 0;
            autoStartEngineCheckBox.Checked = assist.disableAutoEngineStart == 0;
            autoWipersCheckBox.Checked = assist.disableAutoWiper == 0;
            autoLightsCheckBox.Checked = assist.disableAutoLights == 0;
            autoClutchCheckBox.Checked = assist.disableAutoClutch == 0;
            stabilityControlLevelTrackBar.Value = InTrackBarRange(assist.stabilityControlLevelMax, stabilityControlLevelTrackBar);

            // event.json
            tracksListBox.SelectedItem = eventObject.track;
            waitTimeTrackBar.Value = InTrackBarRange(eventObject.preRaceWaitingTimeSeconds / 30, waitTimeTrackBar);
            overTimeTrackBar.Value = InTrackBarRange(eventObject.sessionOverTimeSeconds / 30, overTimeTrackBar);
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
                        pStartTimeTrackBar.Value = InTrackBarRange(session.hourOfDay, pStartTimeTrackBar);
                        pTimeScaleTrackBar.Value = InTrackBarRange(session.timeMultiplier, pTimeScaleTrackBar);
                        pDurationTrackBar.Value = InTrackBarRange(session.sessionDurationMinutes / 5, pDurationTrackBar);
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
                        qStartTimeTrackBar.Value = InTrackBarRange(session.hourOfDay, qStartTimeTrackBar);
                        qTimeScaleTrackBar.Value = InTrackBarRange(session.timeMultiplier, qTimeScaleTrackBar);
                        qDurationTrackBar.Value = InTrackBarRange(session.sessionDurationMinutes / 5, qDurationTrackBar);
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
                        rStartTimeTrackBar.Value = InTrackBarRange(session.hourOfDay, rStartTimeTrackBar);
                        rTimeScaleTrackBar.Value = InTrackBarRange(session.timeMultiplier, rTimeScaleTrackBar);
                        rDurationTrackBar.Value = InTrackBarRange(session.sessionDurationMinutes / 5, rDurationTrackBar);
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

        private void SaveConfig()
        {
            // setting.json
            settings.serverName = serverNameTextBox.Text;
            settings.password = joinPasswordTextBox.Text;
            settings.adminPassword = adminPasswordTextBox.Text;
            settings.spectatorPassword = spectatorPasswordTextBox.Text;
            settings.maxCarSlots = maxCarsTrackBar.Value;
            settings.trackMedalsRequirement = TRRequirementsTrackBar.Value;
            settings.safetyRatingRequirement = SARequirementTrackBar.Value;
            settings.racecraftRatingRequirement = RCRequirementTrackBar.Value;

            // assistRules.json
            assist.disableIdealLine = idealLineCheckBox.Checked ? 0 : 1;
            assist.disableAutosteer = autoSteeringCheckBox.Checked ? 0 : 1;
            assist.disableAutoPitLimiter = autoPitLimiterCheckBox.Checked ? 0 : 1;
            assist.disableAutoGear = autoShiftingCheckBox.Checked ? 0 : 1;
            assist.disableAutoEngineStart = autoStartEngineCheckBox.Checked ? 0 : 1;
            assist.disableAutoWiper = autoWipersCheckBox.Checked ? 0 : 1;
            assist.disableAutoLights = autoLightsCheckBox.Checked ? 0 : 1;
            assist.disableAutoClutch = autoClutchCheckBox.Checked ? 0 : 1;
            assist.stabilityControlLevelMax = stabilityControlLevelTrackBar.Value;

            // event.json
            eventObject.track = tracksListBox.SelectedItem.ToString();
            eventObject.preRaceWaitingTimeSeconds = waitTimeTrackBar.Value * 30;
            eventObject.sessionOverTimeSeconds = overTimeTrackBar.Value * 30;
            eventObject.ambientTemp = tempTrackBar.Value;
            eventObject.cloudLevel = (float)cloudCoverageTrackBar.Value / 10;
            eventObject.rain = (float)rainTrackBar.Value / 10;
            eventObject.weatherRandomness = weatherRandomnessTrackBar.Value;

            eventObject.sessions.Clear();

            if (pCheckBox.Checked)
            {
                Session session = new Session();

                session.sessionType = "P";
                session.hourOfDay = pStartTimeTrackBar.Value;
                session.timeMultiplier = pTimeScaleTrackBar.Value;
                session.sessionDurationMinutes = pDurationTrackBar.Value * 5;
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
                session.hourOfDay = qStartTimeTrackBar.Value;
                session.timeMultiplier = qTimeScaleTrackBar.Value;
                session.sessionDurationMinutes = qDurationTrackBar.Value * 5;
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
                session.hourOfDay = rStartTimeTrackBar.Value;
                session.timeMultiplier = rTimeScaleTrackBar.Value;
                session.sessionDurationMinutes = rDurationTrackBar.Value * 5;
                if (rFridayRadioButton.Checked)
                    session.dayOfWeekend = 1;
                else if (rSaturdayRadioButton.Checked)
                    session.dayOfWeekend = 2;
                else
                    session.dayOfWeekend = 3;

                eventObject.sessions.Add(session);
            }

            File.WriteAllText(@"cfg\settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            File.WriteAllText(@"cfg\assistRules.json", JsonConvert.SerializeObject(assist, Formatting.Indented));
            File.WriteAllText(@"cfg\event.json", JsonConvert.SerializeObject(eventObject, Formatting.Indented));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("accServer.exe"))
            {
                MessageBox.Show("File 'accServer.exe' not found. Please make sure you installed this program to your 'Assetto Corsa Competizione\\server' folder.\n\nClosing...", "ACC Dedicated Server GUI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
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
                    Process.Start("accServer.exe");
                }
                else
                {
                    foreach (Process process in Process.GetProcessesByName("accServer"))
                    {
                        process.Kill();
                    }
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
    }
}
