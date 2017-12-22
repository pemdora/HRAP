using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Competence
    {
        private string name;
        private bool isTech;
        private double points;
        private bool isImportant;

        public M_Competence(string name, bool isTech, double points, bool isImportant)
        {
            this.name = name;
            this.isTech = isTech;
            this.points = points;
            this.isImportant = isImportant;
        }

        public string Name { get { return name; } }
        public bool IsTech { get { return isTech; } }
        public double Points { get { return points; } set { points = value; } }
        public bool IsImportant { get { return isImportant; } set { isImportant = value; } }

    }
}
