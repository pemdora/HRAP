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
        private int id_skill;

        public M_Question(int id, string body, int next, int answers, int id_skill)
        {
            this.questionId = id;
            this.body = body;
            this.nextQuestionId = next;
            this.numAnswers = answers;
            this.id_skill = id_skill;

        }

        public int Id { get { return questionId; } }
        public string Body { get { return body; } }
        public int NextQuestionId { get { return nextQuestionId; } }
        public int NumAnswers { get { return numAnswers; } }
        public int Id_skill { get { return id_skill; } }

    }
}
