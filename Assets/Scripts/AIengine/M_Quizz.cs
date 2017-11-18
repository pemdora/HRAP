using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Quizz
    {
        private int length = 5;
        private List<M_Question> questions;

        public M_Quizz()
        {
            questions = new List<M_Question>();
            GenerateRandom();
        }

        public M_Quizz(M_IdealProfile job, M_Candidate candidate)
        {

        }


        private void GenerateRandom()
        {

            int numQuestions = M_DataManager.Instance.CountQuestions();
            Random random;
            int randomId;

            for (int i = 0; i < length; i++)
            {
                random = new Random();
                randomId = random.Next(1, numQuestions);
                this.questions.Add(M_DataManager.Instance.GetQuestionById(randomId));
            }

        }


    }
}
