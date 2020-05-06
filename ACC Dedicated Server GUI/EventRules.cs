using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Dedicated_Server_GUI
{
    class EventRules
    {
        public class EventRulesObject
        {
            public int qualifyStandingType { get; set; }
            public int pitWindowLengthSec { get; set; }
            public int driverStintTimeSec { get; set; }
            public int mandatoryPitstopCount { get; set; }
            public int maxTotalDrivingTime { get; set; }
            public int maxDriversCount { get; set; }
            public bool isRefuellingAllowedInRace { get; set; }
            public bool isRefuellingTimeFixed { get; set; }
            public bool isMandatoryPitstopRefuellingRequired { get; set; }
            public bool isMandatoryPitstopTyreChangeRequired { get; set; }
            public bool isMandatoryPitstopSwapDriverRequired { get; set; }
        }
    }
}
