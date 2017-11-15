using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_Quizz
    {
        private int length = 5;
        private List<MV_Question> questions;
        // private Dictionary<int, int> candidateAnswers = new Dictionary<int, int>();

        public M_Quizz()
        {
            questions = new List<MV_Question>();
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
                this.questions.Add(new MV_Question(M_DataManager.Instance.GetQuestionById(randomId)));
            }

        }


    }
}
