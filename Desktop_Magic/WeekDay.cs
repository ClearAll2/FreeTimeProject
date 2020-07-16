using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM
{
    class WeekDay
    {
        public bool mon;
        public bool tue;
        public bool wed;
        public bool thu;
        public bool fri;
        public bool sat;
        public bool sun;

        public WeekDay (bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun)
        {
            this.mon = mon;
            this.tue = tue;
            this.wed = wed;
            this.thu = thu;
            this.fri = fri;
            this.sat = sat;
            this.sun = sun;
        }
    }
}
