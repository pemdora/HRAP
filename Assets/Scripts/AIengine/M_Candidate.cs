using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_Candidate : M_Profile
    {
        // private string surname;
        // private string targetJob;

        public M_Candidate(string name) : base( name)
        {
        }

        public void UpdateCandidate(Answer answer)
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
