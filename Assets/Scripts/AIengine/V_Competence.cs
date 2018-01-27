using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class V_Competence
    {
        private string name;
        private double points;

        public V_Competence(string name, double points)
        {
            this.name = name;
            this.points = points;
        }

        public string Name { get { return name; } }
        public double Points { get { return points; } set { points = value; } }
    }
}
