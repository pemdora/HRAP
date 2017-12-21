using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Candidate : M_Profile
    {
         private string targetJob;
         private string result;

        public M_Candidate(string name, string targetJob) : base(name)
        {
            this.targetJob = targetJob;
            this.result = null;
        }

        public M_Candidate(int id, string name, string targetJob, string result, List<M_Competence> competencesList) : base(id, name, competencesList)
        {
            this.targetJob = targetJob;
            this.result = result;
        }

        public string TargetJob { get { return targetJob; } }
        public string Result { get { return result; } }

        // TO DO
        public void UpdateSkills(M_Answer answer)
        {

        }


    }
}
