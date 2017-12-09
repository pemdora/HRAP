using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_QCM
    {
        private M_Question question;
        private List<M_Answer> answers;

        public M_QCM(M_Question question, List<M_Answer> answers)
        {
            this.question = question;
            this.answers = answers;
        }

        public M_Question Question { get { return question; } }
        public List<M_Answer> Answers { get { return answers; } }
    }
}
