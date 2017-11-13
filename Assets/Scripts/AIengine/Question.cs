using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HRAP
{
    class Question
    {
        private int questionId;
        private string questionStr;
        private int numAnswers;

        public Question(int id, string str, int answers)
        {
            this.questionId = id;
            this.questionStr = str;
            this.numAnswers = answers;
        }

        public int Id { get { return questionId; } }
        public string String { get { return questionStr; }  }
        public int NumAnswers { get { return numAnswers; } }

    }
}
