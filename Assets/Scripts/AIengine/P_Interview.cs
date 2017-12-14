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
        private CameraManager cameraManager;
        public List<M_Sequence> sequenceList;
        //  private Dictionary<int, int> candidateAnswers;// not used
        private V_Question q;


        private int currentSequence;
        private int currentElement;
        private bool isWaiting;
        private bool isOver;

        public P_Interview(M_Candidate candidate, IHMInterview ihm, CameraManager cameraManager)
        {
            this.candidate = candidate;
            this.sequenceList = new List<M_Sequence>();
            sequenceList.Add(new M_Sequence());
            //this.candidateAnswers = new Dictionary<int, int>();// TO DO
            this.currentSequence = 0;
            this.currentElement = 0;
            this.isWaiting = false;
            this.isOver = false;

            this.ihm = ihm;
            this.cameraManager = cameraManager;
        }


        public bool IsOver { get { return isOver; } set { isOver = value; } }

        public void Launch()
        {
            // if the candidate has answer 
            if (!isWaiting)
            {

                //TODO : Corriger l'envoie du nombre de réponse (int) il est pas bon une fois passé à la vue
                // cameraManager.Display(getCurrentCamera())
                // animation


                if (Object.ReferenceEquals(
                    sequenceList[currentSequence].DialogElements[currentElement].GetType(),
                    typeof(M_Question)))
                {
                    q = GetNextQuestion();
                    
                    Console.WriteLine("Q: " + q.Question);
                    Console.WriteLine("Num Answers: " + q.NumAnswers);
                    foreach (string s in q.Answers)
                    {
                        Console.WriteLine("A: " + s);
                    }

                    
                    ihm.Activate_buttons_nb_answers(q.NumAnswers);
                    ihm.DisplayQuestion(q.Question);
                    ihm.DisplayAnswers(q.Answers);
                }

                if (Object.ReferenceEquals(
                    sequenceList[currentSequence].DialogElements[currentElement].GetType(),
                    typeof(M_Phrase)))
                {
                    Console.WriteLine("P: " + sequenceList[currentSequence].DialogElements[currentElement].Text);
                    ihm.Clear();
                    ihm.DisplayComment(sequenceList[currentSequence].DialogElements[currentElement].Text);
                }




                // Set next sequence
                if (currentElement == sequenceList[currentSequence].DialogElements.Count - 1)
                {
                    if (sequenceList[currentSequence].GetNextSequence() != null)
                    {
                        this.sequenceList.Add(sequenceList[currentSequence].GetNextSequence());
                        this.currentElement = -1;
                        this.currentSequence++;
                    }
                    else
                    {
                        Console.WriteLine(currentSequence);
                        this.IsOver = true;
                    }
                }

                // Set next element
                if (currentElement < sequenceList[currentSequence].DialogElements.Count - 1)
                {
                    this.currentElement++;
                }

                // We are waiting for the candidate answer
                isWaiting = true;
            }
            // otherwise do nothing
        }

        private M_Animation GetCurrentAnimation()
        {
            return sequenceList[currentSequence].DialogElements[currentElement].Animation;

        }

        private M_Camera GetCurrentCamera()
        {
            return sequenceList[currentSequence].DialogElements[currentElement].Camera;

        }

        private V_Question GetNextQuestion()
        {
            V_Question result = null;

            M_Question question = (M_Question)sequenceList[currentSequence].DialogElements[currentElement];

            if (question != null)
            {

                int count = currentElement + 1;
                List<string> answersString = new List<string>();

                    while (count < sequenceList[currentSequence].DialogElements.Count
                        && Object.ReferenceEquals(
                        sequenceList[currentSequence].DialogElements[count].GetType(),
                        typeof(M_Answer)))
                    {
                        answersString.Add(sequenceList[currentSequence].DialogElements[count].Text);
                        count++;
                    }
                int numAnswers = count - (currentElement + 1);
                result = new V_Question(question.Text, numAnswers, answersString);
            }


            return result;
        }

        public void SetChosenAnswer(int chosen_answer)
        {
            // TO DO

            /*string id = sequenceList[currentSequence].DialogElements[currentElement].Id;
            List<M_Answer> answers = M_DataManager.Instance.GetAnswersByQuestionId(id);
            // Error : Un élément avec la même clé a déjà été ajouté
            // TODO : Ne pas générer les questions déjà posées
            // TODO : S'il n'y a plus de question, stopper la boucle
            //this.candidateAnswers.Add(id, answers[chosen_answer].Id);
            this.candidate.UpdateSkills(answers[chosen_answer]);*/
            AIengine.Affichage("Envoie des résultats");
            isWaiting = false;
        }

        public string GetResult()
        {
            return "tu crains, le job est pour moi";
        }

    }
}
