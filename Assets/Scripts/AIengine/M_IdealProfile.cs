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

        public M_IdealProfile(int id, string name, M_Experience experience, List<M_Competences> skillsList) : base(id, name, skillsList)
        {
            this.experience = experience;
        }

        public M_Experience Experience { get { return experience; } }

        public List<M_Competences> GetImportantSkills()
        {
            List<M_Competences> result = new List<M_Competences>();

            foreach (M_Competences s in skillsList)
            {
                if (s.IsImportant)
                {
                    result.Add(s);
                }
            }

            return result;
        }

        public List<M_Competences> GetNotImportantSkills()
        {
            List<M_Competences> result = new List<M_Competences>();

            foreach (M_Competences s in skillsList)
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
