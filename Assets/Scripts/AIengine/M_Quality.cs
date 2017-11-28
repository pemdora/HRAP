using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Quality
    {
        private string name;
        private int point;
        private int ponderation;


        public M_Quality(string name, int point)
        {
            this.name = name;
            this.point = point;
            this.ponderation = 0;


        }

        public string Name { get { return name; } }
        public int Point { get { return point; } set { point = value; } }
        public int Ponderation { get { return ponderation; } set { ponderation = value; } }

    }
}
