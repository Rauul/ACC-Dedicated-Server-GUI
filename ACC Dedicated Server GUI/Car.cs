using System;

namespace ACC_Dedicated_Server_GUI
{
    public class Car : IComparable<Car>
    {
        public Car(string model, int ID)
        {
            this.model = model;
            this.ID = ID;
        }

        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }

        public int CompareTo(Car other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.model.CompareTo(other.model);
        }

        public string model { get; set; }
        public int ID { get; set; }
    }
}
