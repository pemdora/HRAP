using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    class M_QCM
    {
        private int length = 5;
        private List<MV_Question> questions;
        // private Dictionary<int, int> candidateAnswers = new Dictionary<int, int>();

        public M_QCM()
        {
            questions = new List<MV_Question>();
            generateRandom();
        }

        public M_QCM(M_IdealProfile job, M_Candidate candidate)
        {

        }


        private void generateRandom()
        {
            int numQuestions = M_DataManager.CountQuestions();
            Random random;
            int randomId;

            for (int i = 0; i < length; i++)
            {
                random = new Random();
                randomId = random.Next(1, numQuestions);
                this.questions.Add(new MV_Question(M_DataManager.GetQuestionById(randomId)));
            }

        }


    }
}
