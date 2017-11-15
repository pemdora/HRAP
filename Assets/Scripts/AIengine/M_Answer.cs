using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class Answer
    {
        private int answerId;
        private int questionId;
        private string body;
        private List<M_Skill> skillsList;

        public Answer(int id, int questionId, string body, List<M_Skill> skillsList)
        {
            this.answerId = id;
            this.questionId = questionId;
            this.body = body;
            this.skillsList = skillsList;
        }

        public int Id { get { return answerId; } }
        public int QuestionId { get { return questionId; } }
        public string String { get { return body; } }
        public List<M_Skill> Skills { get { return skillsList; } }
    }
}
