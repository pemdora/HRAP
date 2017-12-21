using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_IdealProfile : M_Profile
    {
        M_Experience experience;

        public M_IdealProfile(string name, M_Experience experience) : base(name)
        {
            this.experience = experience;
        }

        public M_IdealProfile(int id, string name, M_Experience experience, List<M_Competence> skillsList) : base(id, name, skillsList)
        {
            this.experience = experience;
        }

        public M_Experience Experience { get { return experience; } }

        public List<M_Competence> GetImportantCompetences()
        {
            List<M_Competence> result = new List<M_Competence>();

            foreach (M_Competence s in competencesList)
            {
                if (s.IsImportant)
                {
                    result.Add(s);
                }
            }

            return result;
        }

        public List<M_Competence> GetNotImportantCompetences()
        {
            List<M_Competence> result = new List<M_Competence>();

            foreach (M_Competence s in competencesList)
            {
                if (!s.IsImportant)
                {
                    result.Add(s);
                }
            }

            return result;
        }

    }
}
