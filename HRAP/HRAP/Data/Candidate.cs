using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP.Data
{
    class Candidate : Profile
    {
        public Candidate(string type, string name, Dictionary<string, int> skills) : base(type, name, skills)
        {
            this.Type = "Candidate";
        }

        public void UpdateCandidate(Answer answer)
        {
            int length = this.Skills.Count();
            for (int i = 0; i < length; i++)
            {
                KeyValuePair<string, int> a = this.Skills.ElementAt(i);
                KeyValuePair<string, int> b = answer.Skills.ElementAt(i);
                if (a.Key == b.Key)
                {
                    this.Skills[a.Key] += answer.Skills[b.Key];
                }
            }
        }
    }
}
