using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Answer:M_DialogElement
    {
        private string questionId;
        private List<M_Competences> skillsList;

        public M_Answer(string id, string questionId, int seqID, string actor, string text, M_Animation animation, M_Camera camera, string next, List<M_Competences> skillsList) :base(id, seqID, actor, text, animation, camera, next)
        {
            this.questionId = questionId;
            this.skillsList = skillsList;
        }
        
        public string QuestionId { get { return questionId; } }
        public List<M_Competences> Skills { get { return skillsList; } set { skillsList = value; } }
    }
}
