using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Skill
    {
        private string name;
        private M_SkillCategory category;
        private int points;

        public M_Skill(string name, M_SkillCategory category, int points)
        {
            this.name = name;
            this.category = category;
            this.points = points;
        }

        public string Name { get { return name; } }
        public M_SkillCategory Category { get { return category; } }
        public int Points { get { return points; } set { points = value; } }
    }
}
