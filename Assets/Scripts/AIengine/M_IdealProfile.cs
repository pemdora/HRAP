﻿using System;
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

        public M_IdealProfile(int id, string name, M_Experience experience, List<M_Skill> skillsList) : base(id, name, skillsList)
        {
            this.experience = experience;
        }

        public M_Experience Experience { get { return experience; } set { experience = value; } }

        public List<M_Skill> GetImportantSkills()
        {
            List<M_Skill> result = new List<M_Skill>();

            foreach (M_Skill s in skillsList)
            {
                if (s.IsImportant)
                {
                    result.Add(s);
                }
            }

            return result;
        }

        public List<M_Skill> GetNotImportantSkills()
        {
            List<M_Skill> result = new List<M_Skill>();

            foreach (M_Skill s in skillsList)
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
