using System;
using System.Collections.Generic;

namespace ACC_Dedicated_Server_GUI
{
    class EntryList
    {

        public class EntryListObject
        {
            public List<Entry> entries { get; set; }
            public int forceEntryList { get; set; }
        }

        public class Entry : IComparable<Entry>
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


            public int SortByNameAscending(string name1, string name2)
            {
                return name1.CompareTo(name2);
            }

            public int CompareTo(Entry other)
            {
                // A null value means that this object is greater.
                if (other == null)
                    return 1;

                else
                    return this.raceNumber.CompareTo(other.raceNumber);
            }
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
