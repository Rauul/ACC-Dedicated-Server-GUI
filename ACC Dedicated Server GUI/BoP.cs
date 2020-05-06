using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Dedicated_Server_GUI
{
    class BoP
    {

        public class BopObject
        {
            public List<Entry> entries { get; set; }
        }

        public class Entry
        {
            public string track { get; set; }
            public int carModel { get; set; }
            public int ballast { get; set; }
            public int restrictor { get; set; }
        }
    }
}
