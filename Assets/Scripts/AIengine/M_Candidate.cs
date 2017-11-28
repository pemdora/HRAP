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

        public M_Candidate(int id, string name, string targetJob, string result, List<M_Skill> skillList) : base(id, name, skillList)
        {
            this.targetJob = targetJob;
            this.result = result;
        }

        public string TargetJob { get { return targetJob; } }
        public string Result { get { return result; } }

        public void UpdateSkills(M_Answer answer)
        {
            int idCompetence = M_DataManager.Instance.GetIdCompetenceByIdQuestion(answer.QuestionId);

            int score = 0;

            for (int i = 0; i < answer.QualityList.Count(); i++)
            {
                score += answer.QualityList[i].Point * answer.QualityList[i].Ponderation;


            }
            this.skillsList[idCompetence - 1].Points += score;
        }

    }
}
