using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    abstract class M_Profile
    {
        private int id;
        private string name;
        protected List<M_Skill> skillsList;

        public M_Profile(string name)
        {
            this.id = InitializeId();
            this.name = name;
            this.skillsList = InitializeSkills();
        }

        public M_Profile(int id, string name, List<M_Skill> skills)
        {
            this.id = id;
            this.name = name;
            this.skillsList = skills;
        }

        // Access attributes
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public List<M_Skill> Skills { get { return skillsList; } }

       private int InitializeId()
        {
            return M_DataManager.Instance.CountCandidates() + 1;
        }

        private List<M_Skill> InitializeSkills()
        {
            return M_DataManager.Instance.InitializeSkills();
        }

    }
}
