using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_Candidate : M_Profile
    {
         private string targetJob;
         private string result;

        public M_Candidate(string name, string targetJob) : base(name)
        {
            this.targetJob = targetJob;
            this.result = null;
        }

        public M_Candidate(int id, string name, string targetJob, string result, List<M_Skill> skillsList) : base(id, name, skillsList)
        {
            this.targetJob = targetJob;
            this.result = result;
        }

        public string TargetJob { get { return targetJob; } }

        public string Result { get { return result; } }

        public void UpdateSkills(M_Answer answer)
        {
            for (int i = 0; i < this.skillsList.Count(); i++)
            {
                if (this.skillsList[i].Name == answer.Skills[i].Name)
                {
                    this.skillsList[i].Points += answer.Skills[i].Points;
                }
            }
        }


    }
}
