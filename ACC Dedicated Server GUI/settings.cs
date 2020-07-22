namespace ACC_Dedicated_Server_GUI
{
    class Settings
    {

        public class SettingsObject
        {
            public string serverName { get; set; }
            public string adminPassword { get; set; }
            public string password { get; set; }
            public string spectatorPassword { get; set; }
            public string centralEntryListPath { get; set; }
            public string carGroup { get; set; }
            public int trackMedalsRequirement { get; set; }
            public int safetyRatingRequirement { get; set; }
            public int racecraftRatingRequirement { get; set; }
            public int maxCarSlots { get; set; }
            public int isRaceLocked { get; set; }
            public int isLockedPrepPhase { get; set; }
            public int shortFormationLap { get; set; }
            public int dumpLeaderboards { get; set; }
            public int dumpEntryList { get; set; }
            public int randomizeTrackWhenEmpty { get; set; }
            public int allowAutoDQ { get; set; }
            public int formationLapType { get; set; }
            public int configVersion { get; set; }
        }
    }
}
