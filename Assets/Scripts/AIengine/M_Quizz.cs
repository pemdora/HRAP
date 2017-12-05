using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Quizz
    {
        private int numQuestions = 5; // ne sert plus a rien
        private List<M_Question> questions;

        public M_Quizz()
        {
            questions = new List<M_Question>();
            GenerateRandom();
        }

        public M_Quizz(M_IdealProfile job, M_Candidate candidate)
        {

        }

        public int NumQuestions { get { return numQuestions; } } // ne sert plus a rien
        public List<M_Question> Questions { get { return questions; }}

        private void GenerateRandom()
        {
            int numQuestions = M_DataManager.Instance.CountQuestions();
            Random random = new Random();

            for (int i = 0; i < this.numQuestions; i++)
            {                
                int randomId = random.Next(1, numQuestions);
                this.questions.Add(M_DataManager.Instance.GetQuestionById(randomId));
            }

        }


    }
}
