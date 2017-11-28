using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Quizz
    {
        private int numQuestions = 5;
        private List<M_Question> questions;

        public M_Quizz()
        {
            questions = new List<M_Question>();
            GenerateRandom();
        }


        public M_Quizz(M_IdealProfile job, M_Candidate candidate)
        {

            List<M_Skill> listSkillImportant = job.GetImportantSkills();

            for (int i = 0; i < listSkillImportant.Count(); i++)
            {
                int idcompetence = M_DataManager.Instance.GetIdCompetenceByName(listSkillImportant[i].Name);
                if (this.questions == null)
                {
                    this.questions = M_DataManager.Instance.GetListQuestion(idcompetence);

                }
                else
                {
                    List<M_Question> q = M_DataManager.Instance.GetListQuestion(idcompetence);
                    for (int j = 0; j < q.Count(); j++)
                    {
                        this.questions.Add(q[j]);
                    }
                }
            }

        }

        public int NumQuestions { get { return numQuestions; } }
        public List<M_Question> Questions { get { return questions; } }

        private void GenerateRandom()
        {
            int numQuestions = M_DataManager.Instance.CountQuestions();
            Random random = new Random();

            for (int i = 0; i < this.numQuestions; i++)
            {
                int randomId = random.Next(1, numQuestions);
                while (QuestionRandom(randomId))
                {
                    randomId = random.Next(1, numQuestions);

                }
                this.questions.Add(M_DataManager.Instance.GetQuestionById(randomId));

            }

        }
        private bool QuestionRandom(int idrandom)
        {
            bool resultat = false;
            for (int i = 0; i < this.questions.Count(); i++)
            {
                if (idrandom == this.questions[i].Id)
                {
                    resultat = true;
                    break;
                }
            }
            return resultat;
        }


    }
}
