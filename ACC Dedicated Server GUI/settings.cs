using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Dedicated_Server_GUI
{
    class Settings
    {

        public class SettingsObject
        {
            public string serverName { get; set; }
            public string adminPassword { get; set; }
            public int trackMedalsRequirement { get; set; }
            public int safetyRatingRequirement { get; set; }
            public int racecraftRatingRequirement { get; set; }
            public string password { get; set; }
            public int maxCarSlots { get; set; }
            public string spectatorPassword { get; set; }
            public int configVersion { get; set; }
        }

    }
}
