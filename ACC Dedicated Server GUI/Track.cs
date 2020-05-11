using System;

namespace ACC_Dedicated_Server_GUI
{
    public class Track : IComparable<Track>
    {
        public Track(string name, string alias)
        {
            this.name = name;
            this.alias = alias;
        }

        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }

        public int CompareTo(Track other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.name.CompareTo(other.name);
        }

        public string name { get; set; }
        public string alias { get; set; }
    }
}
