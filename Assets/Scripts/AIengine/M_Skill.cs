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
        private bool isImportant;
        private List<M_Quality> listQualite;

        public M_Skill(string name, M_SkillCategory category, int points, bool isImportant)
        {
            this.name = name;
            this.category = category;
            this.points = points;
            this.isImportant = isImportant;
            this.listQualite = new List<M_Quality>();
        }
        public M_Skill(string name, M_SkillCategory category, int points, bool isImportant, List<M_Quality> listQualite)
        {
            this.name = name;
            this.category = category;
            this.points = points;
            this.isImportant = isImportant;
            this.listQualite = listQualite;
        }

        public string Name { get { return name; } }
        public M_SkillCategory Category { get { return category; } }
        public int Points { get { return points; } set { points = value; } }
        public bool IsImportant { get { return isImportant; } set { isImportant = value; } }
        public List<M_Quality> ListQualite { get { return listQualite; } }


    }
}
