using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class P_Interview
    {
        private M_Candidate candidate;
        private List<M_Quizz> quizzList;

        private int currentQuizz;
        private int currentQuestion;

        private bool isOver;

        public P_Interview(string name, string targetJob)
        {
            this.candidate = new M_Candidate(name, targetJob);
            this.quizzList = new List<M_Quizz>();
            quizzList.Add(new M_Quizz());
            currentQuizz = 0;
            currentQuestion = 0;
            isOver = false;
        }

        public List<string> GetNextQuestion()
        {
            List<string> result = null;

            if (currentQuestion < quizzList[currentQuizz].NumQuestions)
            {
                M_Question question = quizzList[currentQuizz].Questions[currentQuestion];
                List<M_Answer> answers = M_DataManager.Instance.GetAnswersByQuestionId(question.Id);

                if (question != null)
                {
                    result = new List<string>();
                    result.Add(question.Body);
                    foreach (M_Answer a in answers)
                    {
                        result.Add(a.Body);
                    }
                    currentQuestion++;
                }
            }
            
            return result;
        }

        public void SetChosenAnswer(string chosen_answer)
        {
            // TO DO
        }
    }
}
