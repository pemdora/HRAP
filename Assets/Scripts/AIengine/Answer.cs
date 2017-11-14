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
        private string answerStr;
        private Dictionary<string, int> skills;

        public Answer(int id, int questionId, string str, Dictionary<string, int> skills)
        {
            this.answerId = id;
            this.questionId = questionId;
            this.answerStr = str;
            this.skills = skills;
        }

        public int Id { get { return answerId; } }
        public int QuestionId { get { return questionId; } }
        public string String { get { return answerStr; } }
        public Dictionary<string, int> Skills { get { return skills; } }
    }
}
