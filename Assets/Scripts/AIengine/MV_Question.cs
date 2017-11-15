using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class MV_Question
    {
        private M_Question question;
        private List<Answer> answers;

        public MV_Question(M_Question question)
        {
            this.question = question;
            this.answers = M_DataManager.Instance.GetAnswersByQuestionId(question.Id);
        }

        public MV_Question(M_Question question, List<Answer> answers)
        {
            this.question = question;
            this.answers = answers;
        }

    }
}
