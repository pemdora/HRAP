using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_IdealProfile : M_Profile
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

    }
}
