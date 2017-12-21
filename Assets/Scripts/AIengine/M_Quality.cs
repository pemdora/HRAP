using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Quality
    {
        private string name;
        private int points;

        public M_Quality(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public string Name { get { return name; } }
        public int Points { get { return points; } set { points = value; } }

        public override string ToString()
        {
            return "Name: " + name + " Points: " + points;
        }

    }
}
