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
        protected List<M_Competence> competencesList;

        public M_Profile(string name)
        {
            this.id = InitializeId();
            this.name = name;
            this.competencesList = InitializeCompetences();
        }

        public M_Profile(int id, string name, List<M_Competence> competenceList)
        {
            this.id = id;
            this.name = name;
            this.competencesList = competenceList;
        }

        // Access attributes
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public List<M_Competence> CompetencesList { get { return competencesList; } set { competencesList = value; } }

        private int InitializeId()
        {
            return M_DataManager.Instance.CountCandidates() + 1;
        }

        private List<M_Competence> InitializeCompetences()
        {
            List<M_Competence> competences = new List<M_Competence>();
            List<String> competencesNames = M_MatriceCQ.Instance.Competences;
            foreach (string name in competencesNames)
            {
                competences.Add(new M_Competence(name, false, 1,false));
            }
            return competences;
        }

        
    }
}
