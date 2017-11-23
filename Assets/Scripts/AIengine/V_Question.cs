using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class V_Question
    {
        private string question;
        private int numAnswers;
        private List<string> answers;

        public V_Question(string question, int numAnswers, List<string> answers)
        {
            this.question = question;
            this.numAnswers = numAnswers;
            this.answers = answers;
        }

        public string Question { get { return question; } }
        public int NumAnswers { get { return numAnswers; } }
        public List<string> Answers { get { return answers; } }
    }
}
