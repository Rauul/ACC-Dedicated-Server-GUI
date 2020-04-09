using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private void LoadConfig()
        {
            string rawJSON = File.ReadAllText(@"cfg\settings.json");
            settings = JsonConvert.DeserializeObject<SettingsObject>(rawJSON);
            rawJSON = File.ReadAllText(@"cfg\assistRules.json");
            assist = JsonConvert.DeserializeObject<AssistObject>(rawJSON);
            rawJSON = File.ReadAllText(@"cfg\event.json");
            eventObject = JsonConvert.DeserializeObject<EventObject>(rawJSON);


            serverNameTextBox.Text = settings.serverName;
            adminPasswordTextBox.Text = settings.adminPassword;
            joinPasswordTextBox.Text = settings.password;
            spectatorPasswordTextBox.Text = settings.spectatorPassword;
            maxCarsTrackBar.Value = settings.maxCarSlots;
            TRRequirementsTrackBar.Value = settings.trackMedalsRequirement;
            SARequirementTrackBar.Value = settings.safetyRatingRequirement;
            RCRequirementTrackBar.Value = settings.racecraftRatingRequirement;


            idealLineCheckBox.Checked = assist.disableIdealLine == 0;
            autoSteeringCheckBox.Checked = assist.disableAutosteer == 0;
            autoPitLimiterCheckBox.Checked = assist.disableAutoPitLimiter == 0;
            autoShiftingCheckBox.Checked = assist.disableAutoGear == 0;
            autoStartEngineCheckBox.Checked = assist.disableAutoEngineStart == 0;
            autoWipersCheckBox.Checked = assist.disableAutoWiper == 0;
            autoLightsCheckBox.Checked = assist.disableAutoLights == 0;
            autoClutchCheckBox.Checked = assist.disableAutoClutch == 0;
            stabilityControlLevelTrackBar.Value = assist.stabilityControlLevelMax;

            tracksListBox.SelectedItem = eventObject.track;

            pStartTimeTrackBar.Value = eventObject.sessions[0].hourOfDay;
            pTimeScaleTrackBar.Value = eventObject.sessions[0].timeMultiplier;
            pDurationTrackBar.Value = eventObject.sessions[0].sessionDurationMinutes / 5;
            switch (eventObject.sessions[0].dayOfWeekend)
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

            qStartTimeTrackBar.Value = eventObject.sessions[1].hourOfDay;
            qTimeScaleTrackBar.Value = eventObject.sessions[1].timeMultiplier;
            qDurationTrackBar.Value = eventObject.sessions[1].sessionDurationMinutes / 5;
            switch (eventObject.sessions[1].dayOfWeekend)
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

            rStartTimeTrackBar.Value = eventObject.sessions[2].hourOfDay;
            rTimeScaleTrackBar.Value = eventObject.sessions[2].timeMultiplier;
            rDurationTrackBar.Value = eventObject.sessions[2].sessionDurationMinutes / 5;
            switch (eventObject.sessions[2].dayOfWeekend)
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

            waitTimeTrackBar.Value = eventObject.preRaceWaitingTimeSeconds / 30;
            overTimeTrackBar.Value = eventObject.sessionOverTimeSeconds / 30;
            tempTrackBar.Value = eventObject.ambientTemp;
            cloudCoverageTrackBar.Value = (int)(eventObject.cloudLevel * 10);
            rainTrackBar.Value = (int)(eventObject.rain * 10);
            weatherRandomnessTrackBar.Value = eventObject.weatherRandomness;
        }

        private void SaveConfig()
        {
            SettingsObject newSettings = new SettingsObject();
            settings.serverName = serverNameTextBox.Text;
            settings.password = joinPasswordTextBox.Text;
            settings.adminPassword = adminPasswordTextBox.Text;
            settings.spectatorPassword = spectatorPasswordTextBox.Text;
            settings.maxCarSlots = maxCarsTrackBar.Value;
            settings.trackMedalsRequirement = TRRequirementsTrackBar.Value;
            settings.safetyRatingRequirement = SARequirementTrackBar.Value;
            settings.racecraftRatingRequirement = RCRequirementTrackBar.Value;

            AssistObject newAssist = new AssistObject();
            assist.disableIdealLine = idealLineCheckBox.Checked ? 0 : 1;
            assist.disableAutosteer = autoSteeringCheckBox.Checked ? 0 : 1;
            assist.disableAutoPitLimiter = autoPitLimiterCheckBox.Checked ? 0 : 1;
            assist.disableAutoGear = autoShiftingCheckBox.Checked ? 0 : 1;
            assist.disableAutoEngineStart = autoStartEngineCheckBox.Checked ? 0 : 1;
            assist.disableAutoWiper = autoWipersCheckBox.Checked ? 0 : 1;
            assist.disableAutoLights = autoLightsCheckBox.Checked ? 0 : 1;
            assist.disableAutoClutch = autoClutchCheckBox.Checked ? 0 : 1;
            assist.stabilityControlLevelMax = stabilityControlLevelTrackBar.Value;

            EventObject newEventObject = new EventObject();
            eventObject.track = tracksListBox.SelectedItem.ToString();
            eventObject.preRaceWaitingTimeSeconds = waitTimeTrackBar.Value * 30;
            eventObject.sessionOverTimeSeconds = overTimeTrackBar.Value * 30;
            eventObject.ambientTemp = tempTrackBar.Value;
            eventObject.cloudLevel = (float)cloudCoverageTrackBar.Value / 10;
            eventObject.rain = (float)rainTrackBar.Value / 10;
            eventObject.weatherRandomness = weatherRandomnessTrackBar.Value;

            // Practice
            Session practice = new Session();
            eventObject.sessions[0].sessionType = "P";
            eventObject.sessions[0].hourOfDay = pStartTimeTrackBar.Value;
            eventObject.sessions[0].timeMultiplier = pTimeScaleTrackBar.Value;
            eventObject.sessions[0].sessionDurationMinutes = pDurationTrackBar.Value * 5;
            if (pFridayRadioButton.Checked)
                eventObject.sessions[0].dayOfWeekend = 1;
            else if (pSaturdayRadioButton.Checked)
                eventObject.sessions[0].dayOfWeekend = 2;
            else
                eventObject.sessions[0].dayOfWeekend = 3;

            // Qualifying
            Session qualifying = new Session();
            eventObject.sessions[1].sessionType = "Q";
            eventObject.sessions[1].hourOfDay = qStartTimeTrackBar.Value;
            eventObject.sessions[1].timeMultiplier = qTimeScaleTrackBar.Value;
            eventObject.sessions[1].sessionDurationMinutes = qDurationTrackBar.Value * 5;
            if (qFridayRadioButton.Checked)
                eventObject.sessions[1].dayOfWeekend = 1;
            else if (qSaturdayRadioButton.Checked)
                eventObject.sessions[1].dayOfWeekend = 2;
            else
                eventObject.sessions[1].dayOfWeekend = 3;

            // Race
            Session race = new Session();
            eventObject.sessions[2].sessionType = "R";
            eventObject.sessions[2].hourOfDay = rStartTimeTrackBar.Value;
            eventObject.sessions[2].timeMultiplier = rTimeScaleTrackBar.Value;
            eventObject.sessions[2].sessionDurationMinutes = rDurationTrackBar.Value * 5;
            if (rFridayRadioButton.Checked)
                eventObject.sessions[2].dayOfWeekend = 1;
            else if (rSaturdayRadioButton.Checked)
                eventObject.sessions[2].dayOfWeekend = 2;
            else
                eventObject.sessions[2].dayOfWeekend = 3;

            File.WriteAllText(@"cfg\settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            File.WriteAllText(@"cfg\assistRules.json", JsonConvert.SerializeObject(assist, Formatting.Indented));
            File.WriteAllText(@"cfg\event.json", JsonConvert.SerializeObject(eventObject, Formatting.Indented));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                SaveConfig();
                Process.Start("accServer.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
