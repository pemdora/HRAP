using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    public class M_Question
    {
        private int questionId;
        private string body;
        private int nextQuestionId;
        private int numAnswers;

        public M_Question(int id, string body, int next, int answers)
        {
            this.questionId = id;
            this.body = body;
            this.nextQuestionId = next;
            this.numAnswers = answers;
        }

        public int Id { get { return questionId; } }
        public string Body { get { return body; }  }
        public int NextQuestionId { get { return nextQuestionId; } }
        public int NumAnswers { get { return numAnswers; } }

    }
}
