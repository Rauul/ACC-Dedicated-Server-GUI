using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Dedicated_Server_GUI
{
    class EntryList
    {

        public class EntryListObject
        {
            public List<Entry> entries { get; set; }
            public int forceEntryList { get; set; }
        }

        public class Entry
        {
            public List<Driver> drivers { get; set; }
            public int raceNumber { get; set; }
            public int forcedCarModel { get; set; }
            public int overrideDriverInfo { get; set; }
            public int isServerAdmin { get; set; }
            public int defaultGridPosition { get; set; }
            public int ballastKg { get; set; }
            public int restrictor { get; set; }
            public string customCar { get; set; }
            public int overrideCarModelForCustomCar { get; set; }
        }

        public class Driver
        {
            public string playerID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string shortName { get; set; }
            public int driverCategory { get; set; }
        }

    }
}
