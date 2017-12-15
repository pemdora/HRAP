using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    public abstract class M_Profile
    {
        private int id;
        private string name;
        protected List<M_Competences> skillsList;

        public M_Profile(string name)
        {
            this.id = InitializeId();
            this.name = name;
            this.skillsList = InitializeSkills();
        }

        public M_Profile(int id, string name, List<M_Competences> skills)
        {
            this.id = id;
            this.name = name;
            this.skillsList = skills;
        }

        // Access attributes
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public List<M_Competences> Skills { get { return skillsList; } set { skillsList = value; } }

        private int InitializeId()
        {
            return M_DataManager.Instance.CountCandidates() + 1;
        }

        private List<M_Competences> InitializeSkills()
        {
            return M_DataManager.Instance.InitializeSkills();
        }

        
        public List<M_Competences> CompareSkillsTo(M_Profile otherProfile)
        {
            List<M_Competences> result = new List<M_Competences>();
            int count = 0;
            foreach (M_Competences s in skillsList)
            {
                s.Points -= otherProfile.Skills[count].Points;
                result.Add(s);
                count++;
            }
            return result;
        }

        
    }
}
