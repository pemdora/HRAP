using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP_TEST_GRAPHIQUE
{
    
        class Candidate : Profile
        {

            public Candidate(int id,string type, string name, Dictionary<string, int> skills) : base(id,type, name, skills)
            {

            }
            //Rajoute les competences du candidats en focntion de la reponse donné
            public void UpdateCandidate(Answer answer)
            {
                int length = skills.Count();
                for (int i = 0; i < length; i++)
                {
                    KeyValuePair<string, int> a = skills.ElementAt(i);
                    KeyValuePair<string, int> b = answer.Skills.ElementAt(i);
                    if (a.Key == b.Key)
                    {
                        skills[a.Key] += answer.Skills[b.Key];
                    }
                }
            }



        }
    }
