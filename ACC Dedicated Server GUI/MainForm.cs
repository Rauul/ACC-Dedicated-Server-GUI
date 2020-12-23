using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        EntriesForm entriesForm = new EntriesForm();
        BoPForm boPForm = new BoPForm();
        Process process = new Process();

        Panel consolePanel = new Panel();

        const int WM_SYSCOMMAND = 274;
        const int SC_MAXIMIZE = 61488;

        //public static List<Car> carList = new List<Car>()
        //{
        //    new Car("    Porsche 991 GT3", 0),
        //    new Car("    Mercedes AMG GT3", 1),
        //    new Car("    Ferrari 488 GT3", 2),
        //    new Car("    Audi R8 LMS", 3),
        //    new Car("    Lamborghini Huracan GT3", 4),
        //    new Car("    McLaren 650S GT3", 5),
        //    new Car("    Nissan GT-R Nismo GT3 (2018)", 6),
        //    new Car("    BMW M6 GT3", 7),
        //    new Car("    Bentley Continental GT3 (2018)", 8),
        //    new Car("    Porsche 991 II GT3 Cup", 9),
        //    new Car("    Nissan GT-R Nismo GT3 (2017)", 10),
        //    new Car("    Bentley Continental GT3 (2016)", 11),
        //    new Car("    Aston Martin Vantage V12 GT3", 12),
        //    new Car("    Lamborghini Gallardo R-EX", 13),
        //    new Car("    Jaguar G3", 14),
        //    new Car("    Lexus RC F GT3", 15),
        //    new Car("    Lamborghini Huracan Evo (2019)", 16),
        //    new Car("    Honda NSX GT3", 17),
        //    new Car("    Lamborghini Huracan SuperTrofeo", 18),
        //    new Car("    Audi R8 LMS Evo (2019)", 19),
        //    new Car("    Aston Martin Vantage V8 GT3", 20),
        //    new Car("    Honda NSX Evo (2019)", 21),
        //    new Car("    McLaren 720S GT3", 22),
        //    new Car("    Porsche 911 II GT3 R (2019)", 23)
        //};

        //public static List<CarClass>

        public static List<Car> carList = new List<Car>()
        {
            new Car("    Aston Martin Vantage V8 GT3", 20),
            new Car("    Aston Martin Vantage V12 GT3", 12),
            new Car("    Audi R8 LMS", 3),
            new Car("    Audi R8 LMS Evo (2019)", 19),
            new Car("    Bentley Continental GT3 (2016)", 11),
            new Car("    Bentley Continental GT3 (2018)", 8),
            new Car("    BMW M6 GT3", 7),
            new Car("    Ferrari 488 GT3", 2),
            new Car("    Ferrari 488 GT3 Evo (2020)", 24),
            new Car("    Honda NSX Evo (2019)", 21),
            new Car("    Honda NSX GT3", 17),
            new Car("    Jaguar G3", 14),
            new Car("    Lamborghini Gallardo R-EX", 13),
            new Car("    Lamborghini Huracan Evo (2019)", 16),
            new Car("    Lamborghini Huracan GT3", 4),
            new Car("    Lamborghini Huracan SuperTrofeo", 18),
            new Car("    Lexus RC F GT3", 15),
            new Car("    McLaren 650S GT3", 5),
            new Car("    McLaren 720S GT3", 22),
            new Car("    Mercedes AMG GT3", 1),
            new Car("    Mercedes AMG GT3 (2020)", 25),
            new Car("    Nissan GT-R Nismo GT3 (2017)", 10),
            new Car("    Nissan GT-R Nismo GT3 (2018)", 6),
            new Car("    Porsche 911 II GT3 R (2019)", 23),
            new Car("    Porsche 991 GT3", 0),
            new Car("    Porsche 991 II GT3 Cup", 9),

            new Car("    Alpine A1110 GT4", 50),
            new Car("    Aston Martin Vantage GT4", 51),
            new Car("    Audi R8 LMS GT4", 52),
            new Car("    BMW M4 GT4", 53),
            new Car("    Chevrolet Camaro GT4", 55),
            new Car("    Ginetta G55 GT4", 56),
            new Car("    KTM X-Bow GT4", 57),
            new Car("    Maserati MC GT4", 58),
            new Car("    McLaren 570S GT4", 59),
            new Car("    Mercedes AMG GT4", 60),
            new Car("    Porsche 718 Cayman GT4", 61)
        };

        public static List<Track> trackList = new List<Track>()
        {
            new Track("    Barcelona","barcelona"),
            new Track("    Barcelona 2019","barcelona_2019"),
            new Track("    Barcelona 2020","barcelona_2020"),
            new Track("    Brands Hatch","brands_hatch"),
            new Track("    Brands Hatch 2019","brands_hatch_2019"),
            new Track("    Brands Hatch 2020","brands_hatch_2020"),
            new Track("    Hungaroring","hungaroring"),
            new Track("    Hungaroring 2019","hungaroring_2019"),
            new Track("    Hungaroring 2020","hungaroring_2020"),
            new Track("    Imola 2020","imola_2020"),
            new Track("    Kyalami 2019","kyalami_2019"),
            new Track("    Laguna Seca 2019","laguna_seca_2019"),
            new Track("    Misano","misano"),
            new Track("    Misano 2019","misano_2019"),
            new Track("    Misano 2020","misano_2020"),
            new Track("    Monza","monza"),
            new Track("    Monza 2019","monza_2019"),
            new Track("    Monza 2020","monza_2020"),
            new Track("    Mount Panorama 2019","mount_panorama_2019"),
            new Track("    Nurburgring","nurburgring"),
            new Track("    Nurburgring 2019","nurburgring_2019"),
            new Track("    Nurburgring 2020","nurburgring_2020"),
            new Track("    Paul Ricard","paul_ricard"),
            new Track("    Paul Ricard 2019","paul_ricard_2019"),
            new Track("    Paul Ricard 2020","paul_ricard_2020"),
            new Track("    Silverstone","silverstone"),
            new Track("    Silverstone 2019","silverstone_2019"),
            new Track("    Silverstone 2020","silverstone_2020"),
            new Track("    Spa-Francorchamps","spa"),
            new Track("    Spa-Francorchamps 2019","spa_2019"),
            new Track("    Spa-Francorchamps 2020","spa_2020"),
            new Track("    Suzuka 2019","suzuka_2019"),
            new Track("    Zandvoort","zandvoort"),
            new Track("    Zandvoort 2019","zandvoort_2019"),
            new Track("    Zandvoort 2020","zandvoort_2020"),
            new Track("    Zolder","zolder"),
            new Track("    Zolder 2019","zolder_2019"),
            new Track("    Zolder 2020","zolder_2020")
        };


        public MainForm()
        {
            InitializeComponent();
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

        private decimal InNumUpDnRange(int value, NumericUpDown numUpDn)
        {
            if (value < numUpDn.Minimum)
                return numUpDn.Minimum;
            if (value > numUpDn.Maximum)
                return numUpDn.Maximum;
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
                centraEntryListPathTextBox.Text = settings.centralEntryListPath;
                carClassComboBox.SelectedItem = settings.carGroup != null && settings.carGroup.Length > 0 ? "    " + settings.carGroup : "    FreeForAll";
                maxCarSlotsNumericUpDown.Value = InNumUpDnRange(settings.maxCarSlots, maxCarSlotsNumericUpDown);
                TRRequirementNumericUpDown.Value = InNumUpDnRange(settings.trackMedalsRequirement, TRRequirementNumericUpDown);
                SARequirementNumericUpDown.Value = InNumUpDnRange(settings.safetyRatingRequirement, SARequirementNumericUpDown);
                RCRequirementNumericUpDown.Value = InNumUpDnRange(settings.racecraftRatingRequirement, RCRequirementNumericUpDown);
                isRaceLockedCheckBox.Checked = settings.isRaceLocked == 1 ? true : false;
                shortFormationCheckBox.Checked = settings.shortFormationLap == 1 ? true : false;
                dumpEntryListCheckBox.Checked = settings.dumpEntryList == 1 ? true : false;
                dumpLeaderboardsCheckBox.Checked = settings.dumpLeaderboards == 1 ? true : false;
                randomizeTrackCheckBox.Checked = settings.randomizeTrackWhenEmpty == 1 ? true : false;
                autoDQCheckBox.Checked = settings.allowAutoDQ == 1 ? true : false;
                isPrepPhaseLockedCheckBox.Checked = settings.isLockedPrepPhase == 1 ? true : false;
                switch (settings.formationLapType)
                {
                    case 0:
                        formationLapTypeComboBox.SelectedItem = "Old limiter lap";
                        break;
                    case 1:
                        formationLapTypeComboBox.SelectedItem = "Free, only usable for private servers";
                        break;
                    case 4:
                        formationLapTypeComboBox.SelectedItem = "Free formation lap + 1 ghosted cars lap";
                        break;
                    case 5:
                        formationLapTypeComboBox.SelectedItem = "Short formation lap with UI + 1 ghosted cars lap";
                        break;
                    default:
                        formationLapTypeComboBox.SelectedItem = "Default formation lap UI";
                        break;
                }
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
                maxStabilityNumericUpDown.Value = InNumUpDnRange(assist.stabilityControlLevelMax, maxStabilityNumericUpDown);
            }

            file = @"cfg\event.json";
            if (File.Exists(file))
            {
                // event.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                eventObject = JsonConvert.DeserializeObject<EventObject>(rawJSON);

                for (int i = 0; i < TrackComboBox.Items.Count; i++)
                {
                    if (((Track)TrackComboBox.Items[i]).alias == eventObject.track)
                    {
                        TrackComboBox.SelectedIndex = i;
                        break;
                    }
                }
                preRaceWaitTimeNumericUpDown.Value = InNumUpDnRange(eventObject.preRaceWaitingTimeSeconds, preRaceWaitTimeNumericUpDown);
                postRaceWaitTimeNumericUpDown.Value = InNumUpDnRange(eventObject.postRaceSeconds, postRaceWaitTimeNumericUpDown);
                overTimeNumericUpDown.Value = InNumUpDnRange(eventObject.sessionOverTimeSeconds, overTimeNumericUpDown);
                tempTrackBar.Value = InTrackBarRange(eventObject.ambientTemp, tempTrackBar);
                cloudCoverageTrackBar.Value = InTrackBarRange((int)(eventObject.cloudLevel * 10), cloudCoverageTrackBar);
                rainTrackBar.Value = InTrackBarRange((int)(eventObject.rain * 10), rainTrackBar);
                weatherRandomnessTrackBar.Value = InTrackBarRange(eventObject.weatherRandomness, weatherRandomnessTrackBar);
                simracerWeatherConditionsCheckBox.Checked = eventObject.simracerWeatherConditions == 1 ? true : false;
                fixedConditionQualificationCheckBox.Checked = eventObject.isFixedConditionQualification == 1 ? true : false;

                foreach (Session session in eventObject.sessions)
                {
                    string sessionType = "";
                    switch (session.sessionType)
                    {
                        case "P":
                            sessionType = "Practice";
                            break;
                        case "Q":
                            sessionType = "Qualifying";
                            break;
                        case "R":
                            sessionType = "Race";
                            break;
                        default:
                            break;
                    }

                    string dayOfWeekend = "";
                    switch (session.dayOfWeekend)
                    {
                        case 1:
                            dayOfWeekend = "Friday";
                            break;
                        case 2:
                            dayOfWeekend = "Saturday";
                            break;
                        case 3:
                            dayOfWeekend = "Sunday";
                            break;
                        default:
                            break;
                    }   

                    sessionGridView.Rows.Add(new object[] { 
                        sessionType,
                        dayOfWeekend,
                        session.timeMultiplier,                        
                        session.hourOfDay,
                        session.sessionDurationMinutes
                    });
                }
            }

            file = @"cfg\eventRules.json";
            if (File.Exists(file))
            {
                // eventRules.json
                Encoding encoding = GetEncoding(file);
                rawJSON = File.ReadAllText(file, encoding);
                eventRules = JsonConvert.DeserializeObject<EventRulesObject>(rawJSON);

                pitWindowsLengthNumericUpDown.Value = InNumUpDnRange(eventRules.pitWindowLengthSec, pitWindowsLengthNumericUpDown);
                driverStintTimeNumericUpDown.Value = InNumUpDnRange(eventRules.driverStintTimeSec, driverStintTimeNumericUpDown);
                mandatoryPitStopCountNumericUpDown.Value = InNumUpDnRange(eventRules.mandatoryPitstopCount, mandatoryPitStopCountNumericUpDown);
                maxTotalDrivingTimeNumericUpDown.Value = InNumUpDnRange(eventRules.maxTotalDrivingTime, maxTotalDrivingTimeNumericUpDown);
                maxDriversCountNumericUpDown.Value = InNumUpDnRange(eventRules.maxDriversCount, maxDriversCountNumericUpDown);
                tyreSetsNumericUpDown.Value = InNumUpDnRange(eventRules.tyreSetCount, tyreSetsNumericUpDown);
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

                UDPNumericUpDown.Value = InNumUpDnRange(configuration.udpPort, UDPNumericUpDown);
                TCPNumericUpDown.Value = InNumUpDnRange(configuration.tcpPort, TCPNumericUpDown);
                maxConnectionsNumericUpDown.Value = InNumUpDnRange(configuration.maxConnections, maxConnectionsNumericUpDown);
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
            settings.centralEntryListPath = centraEntryListPathTextBox.Text;
            settings.carGroup = carClassComboBox.Text.Replace("    ", "");
            settings.maxCarSlots = (int)maxCarSlotsNumericUpDown.Value;
            settings.trackMedalsRequirement = (int)TRRequirementNumericUpDown.Value;
            settings.safetyRatingRequirement = (int)SARequirementNumericUpDown.Value;
            settings.racecraftRatingRequirement = (int)RCRequirementNumericUpDown.Value;
            switch (formationLapTypeComboBox.SelectedItem)
            {
                case "Old limiter lap":
                    settings.formationLapType = 0;
                    break;
                case "Free, only usable for private servers":
                    settings.formationLapType = 1;
                    break;
                case "Free formation lap + 1 ghosted cars lap":
                    settings.formationLapType = 4;
                    break;
                case "Short formation lap with UI + 1 ghosted cars lap":
                    settings.formationLapType = 5;
                    break;
                default:
                    settings.formationLapType = 3;
                    break;
            }
            settings.isRaceLocked = isRaceLockedCheckBox.Checked ? 1 : 0;
            settings.isLockedPrepPhase = isPrepPhaseLockedCheckBox.Checked ? 1 : 0;
            settings.shortFormationLap = shortFormationCheckBox.Checked ? 1 : 0;
            settings.dumpEntryList = dumpEntryListCheckBox.Checked ? 1 : 0;
            settings.dumpLeaderboards = dumpLeaderboardsCheckBox.Checked ? 1 : 0;
            settings.randomizeTrackWhenEmpty = randomizeTrackCheckBox.Checked ? 1 : 0;
            settings.allowAutoDQ = autoDQCheckBox.Checked ? 1 : 0;
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
            eventObject.track = ((Track)TrackComboBox.SelectedItem).alias;
            eventObject.preRaceWaitingTimeSeconds = (int)preRaceWaitTimeNumericUpDown.Value;
            eventObject.postRaceSeconds = (int)postRaceWaitTimeNumericUpDown.Value;
            eventObject.sessionOverTimeSeconds = (int)overTimeNumericUpDown.Value;
            eventObject.ambientTemp = tempTrackBar.Value;
            eventObject.cloudLevel = (float)cloudCoverageTrackBar.Value / 10;
            eventObject.rain = (float)rainTrackBar.Value / 10;
            eventObject.weatherRandomness = weatherRandomnessTrackBar.Value;
            eventObject.isFixedConditionQualification = fixedConditionQualificationCheckBox.Checked ? 1 : 0;
            eventObject.simracerWeatherConditions = simracerWeatherConditionsCheckBox.Checked ? 1 : 0;
            eventObject.configVersion = 1;

            if (eventObject.sessions == null)
                eventObject.sessions = new List<Session>();
            eventObject.sessions.Clear();




            foreach (DataGridViewRow dgvr in sessionGridView.Rows)
            {
                if (dgvr.Index + 1 >= sessionGridView.Rows.Count ||
                    dgvr.Cells[0].Value == null)
                    break;

                Session session = new Session();

                session.sessionType = dgvr.Cells[0].Value.ToString().Substring(0, 1);
                session.timeMultiplier = int.Parse(dgvr.Cells[2].Value.ToString());
                session.hourOfDay = int.Parse(dgvr.Cells[3].Value.ToString());
                session.sessionDurationMinutes = int.Parse(dgvr.Cells[4].Value.ToString());

                switch (dgvr.Cells[1].Value.ToString())
                {
                    case "Friday":
                        session.dayOfWeekend = 1;
                        break;
                    case "Saturday":
                        session.dayOfWeekend = 2;
                        break;
                    case "Sunday":
                        session.dayOfWeekend = 3;
                        break;
                    default:
                        break;
                }

                eventObject.sessions.Add(session);
            }

            // eventRules.json
            eventRules.qualifyStandingType = 1;
            eventRules.pitWindowLengthSec = (int)pitWindowsLengthNumericUpDown.Value;
            eventRules.driverStintTimeSec = (int)driverStintTimeNumericUpDown.Value;
            eventRules.mandatoryPitstopCount = (int)mandatoryPitStopCountNumericUpDown.Value;
            eventRules.maxTotalDrivingTime = (int)maxTotalDrivingTimeNumericUpDown.Value;
            eventRules.maxDriversCount = (int)maxDriversCountNumericUpDown.Value;
            eventRules.tyreSetCount = (int)tyreSetsNumericUpDown.Value;
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
            //carList.Sort(delegate (Car x, Car y) { return x.model.CompareTo(y.model); });
            TrackComboBox.Items.AddRange(trackList.ToArray());
            TrackComboBox.SelectedIndex = 0;
            carClassComboBox.SelectedIndex = 0;
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
                MessageBox.Show("File 'accServer.exe' not found. Please make sure you installed this program to your 'Assetto Corsa Competizione Dedicated Server\\server' folder.\n\nClosing...", "ACC Dedicated Server GUI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
#if DEBUG
            string fileName = "testFlood";
#else
            string fileName = "accServer";
#endif
            try
            {
                if (consolePanel.Visible)
                {
                    consolePanel.Visible = false;
                    label12.Visible = true;
                    if (!process.HasExited)
                        process.Kill();
                }
                else
                {
                    SaveConfig();
                    consolePanel.Visible = true;
                    consolePanel.BringToFront();

                    process.StartInfo.FileName = fileName + ".exe";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                    process.Start();
                    while (string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        Thread.Sleep(10);
                        process.Refresh();
                    }
                    SetParent(process.MainWindowHandle, consolePanel.Handle);
                    SendMessage(process.MainWindowHandle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                    label12.Visible = false;
                    this.BringToFront();
                    this.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void entryListButton_Click(object sender, EventArgs e)
        {
            entriesForm.Show();
        }

        private void BopButton_Click(object sender, EventArgs e)
        {
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

        private void centralEntryListPathButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                centraEntryListPathTextBox.Text = dialog.FileName.Replace(@"\", @"/");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (consolePanel.Visible)
                    process.Kill();
            }
            catch (Exception)
            {

            }
        }

        private void donationButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=P6JSQXRX543V8&currency_code=EUR&source=url");
        }

        private bool IsFormOpen(string formName)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == formName)
                    return true;
            }
            return false;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (IsFormOpen("EntriesForm"))
                entriesForm.Activate();

            else if (IsFormOpen("BoPForm"))
                boPForm.Activate();
        }

        #region Custom checkstates
        private void checkBox_CheckChanged(CheckBox checkBox)
        {
            checkBox.CheckState = checkBox.Checked ? CheckState.Indeterminate : CheckState.Unchecked;
        }

        private void isRaceLockedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(isRaceLockedCheckBox);
        }

        private void shortFormationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(shortFormationCheckBox);
        }

        private void registerToLobbyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(registerToLobbyCheckBox);
        }

        private void lanDiscoveryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(lanDiscoveryCheckBox);
        }

        private void dumpLeaderboardsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(dumpLeaderboardsCheckBox);
        }

        private void dumpEntryListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(dumpEntryListCheckBox);
        }

        private void randomizeTrackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(randomizeTrackCheckBox);
        }

        private void autoDQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoDQCheckBox);
        }

        private void idealLineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(idealLineCheckBox);
        }

        private void autoSteeringCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoSteeringCheckBox);
        }

        private void autoPitLimiterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoPitLimiterCheckBox);
        }

        private void autoShiftingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoShiftingCheckBox);
        }

        private void autoStartEngineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoStartEngineCheckBox);
        }

        private void autoWipersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoWipersCheckBox);
        }

        private void autoLightsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoLightsCheckBox);
        }

        private void autoClutchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(autoClutchCheckBox);
        }

        private void refuellingAllowedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(refuellingAllowedCheckBox);
        }

        private void refuellingTimeFixedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(refuellingTimeFixedCheckBox);
        }

        private void refuellingRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(refuellingRequiredCheckBox);
        }

        private void tyreChangeRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(tyreChangeRequiredCheckBox);
        }

        private void driverSwapRequiredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(driverSwapRequiredCheckBox);
        }

        private void simracerWeatherConditionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(simracerWeatherConditionsCheckBox);
        }

        private void fixedConditionQualificationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(fixedConditionQualificationCheckBox);
        }
        #endregion

        private void isPrepPhaseLockedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckChanged(isPrepPhaseLockedCheckBox);
        }

        private void sessionGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.RowIndex + 1 < senderGrid.Rows.Count)
            {
                //TODO - Button Clicked - Execute Code Here
                sessionGridView.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
