using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Answer
    {
        private int answerId;
        private int questionId;
        private string body;
        private int nextQuestionId;
        private List<M_Skill> skillsList;

        public M_Answer(int id, int questionId, string body, int nextQuestionId, List<M_Skill> skillsList)
        {
            this.answerId = id;
            this.questionId = questionId;
            this.body = body;
            this.nextQuestionId = nextQuestionId;
            this.skillsList = skillsList;
        }

        public int Id { get { return answerId; } }
        public int QuestionId { get { return questionId; } }
        public string String { get { return body; } }
        public int NextQuestionId { get { return nextQuestionId; } }
        public List<M_Skill> Skills { get { return skillsList; } }
    }
}
