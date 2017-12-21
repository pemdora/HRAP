using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    public abstract class M_Profile
    {
        private int id;
        private string name;
        protected List<M_Competences> competencesList;

        public M_Profile(string name)
        {
            this.id = InitializeId();
            this.name = name;
            this.competencesList = InitializeCompetences();
        }

        public M_Profile(int id, string name, List<M_Competences> competenceList)
        {
            this.id = id;
            this.name = name;
            this.competencesList = competenceList;
        }

        // Access attributes
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public List<M_Competences> CompetencesList { get { return competencesList; } set { competencesList = value; } }

        private int InitializeId()
        {
            return M_DataManager.Instance.CountCandidates() + 1;
        }

        private List<M_Competences> InitializeCompetences()
        {
            return M_DataManager.Instance.InitializeCompetences();
        }

        
        public List<M_Competences> CompareCompetencesTo(M_Profile otherProfile)
        {
            List<M_Competences> result = new List<M_Competences>();
            int count = 0;
            foreach (M_Competences s in competencesList)
            {
                s.Points -= otherProfile.CompetencesList[count].Points;
                result.Add(s);
                count++;
            }
            return result;
        }

        
    }
}
