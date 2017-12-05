using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class P_Interview
    {
        private M_Candidate candidate;
        private IHMInterview ihm;
        private List<M_Quizz> quizzList;
        private Dictionary<int, int> candidateAnswers;


        private int currentQuizz;
        private int currentQuestion;
        private bool isWaiting;
        private bool isOver;

        public P_Interview(M_Candidate candidate, IHMInterview ihm)
        {
            this.candidate = candidate;
            this.quizzList = new List<M_Quizz>();
            this.quizzList.Add(new M_Quizz());
            this.candidateAnswers = new Dictionary<int, int>();
            this.currentQuizz = 0;
            this.currentQuestion = 0;
            this.isWaiting = false;
            this.isOver = false;

            this.ihm = ihm;
        }

        public bool IsOver { get { return isOver; } set { isOver = value; }  }

        public void Launch() 
        {
            // if the candidate has answer 
            if (!isWaiting)
            {
                // TEST
                //AIengine.Affichage("Affichage des questions");
                // TODO : Envoi  de la question dans la vue
                // Console.WriteLine("question : " + currentQuestion);
                // Console.WriteLine("quizz : " + currentQuizz);
                // Console.WriteLine(GetNextQuestion().Question);

                //TODO : Corriger l'envoie du nombre de réponse (int) il est pas bon une fois passé à la vue
                ihm.Activate_buttons_nb_answers(GetNextQuestion().NumAnswers);
                ihm.DisplayQuestion(GetNextQuestion().Question);//IHM
                ihm.DisplayAnswers(GetNextQuestion().Answers);//IHM

                // Set next question
                if (currentQuestion == quizzList[currentQuizz].NumQuestions - 1)
                {
                    this.quizzList.Add(new M_Quizz());
                    this.currentQuestion = 0;
                    this.currentQuizz++;
                }

                if (currentQuestion < quizzList[currentQuizz].NumQuestions - 1)
                {
                    this.currentQuestion++;
                }

                // We are waiting for the candidate answer
                isWaiting = true;
            }
            // otherwise do nothing
        }

        public V_Question GetNextQuestion()
        {
            V_Question result = null;

            M_Question question = quizzList[currentQuizz].Questions[currentQuestion];
            List<M_Answer> answers = M_DataManager.Instance.GetAnswersByQuestionId(question.Id);

            if (question != null)
            {
                List<string> answersToString = new List<string>();
                foreach (M_Answer a in answers)
                {
                    answersToString.Add(a.Body);
                }

                result = new V_Question(question.Body, question.NumAnswers, answersToString);
            }

            return result;
        }

        public void SetChosenAnswer(int chosen_answer)
        {
            int id = quizzList[currentQuizz].Questions[currentQuestion].Id;
            List<M_Answer> answers = M_DataManager.Instance.GetAnswersByQuestionId(id);
            // Error : Un élément avec la même clé a déjà été ajouté
            // TODO : Ne pas générer les questions déjà posées
            // TODO : S'il n'y a plus de question, stopper la boucle
            //this.candidateAnswers.Add(id, answers[chosen_answer].Id);
            this.candidate.UpdateSkills(answers[chosen_answer]);
            AIengine.Affichage("Envoie des résultats");
            isWaiting = false;
        }

        public string GetResult()
        {
            return "tu crains, le job est pour moi";
        }
        
    }
}
