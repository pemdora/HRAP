using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    class M_Question
    {
        private int questionId;
        private string body;
        private int numAnswers;

        public M_Question(int id, string body, int answers)
        {
            this.questionId = id;
            this.body = body;
            this.numAnswers = answers;
        }

        public int Id { get { return questionId; } }
        public string String { get { return body; }  }
        public int NumAnswers { get { return numAnswers; } }

    }
}
