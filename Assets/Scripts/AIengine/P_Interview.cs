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
        private M_DialogElement previous;
        private V_Question q;
        private M_QCM qcm;


        private int currentSequence;
        private int currentElement;
        private bool isWaiting;
        private bool isOver;

        public P_Interview(M_Candidate candidate, IHMInterview ihm)
        {
            this.candidate = candidate;
            this.sequenceList = new List<M_Sequence>();
            sequenceList.Add(new M_Sequence());
            //this.candidateAnswers = new Dictionary<int, int>();// TO DO
            this.currentSequence = 0;
            this.currentElement = 0;
            this.qcm = null;
            this.previous = null;
            this.isWaiting = false;
            this.isOver = false;

            this.ihm = ihm;
        }

        public bool IsOver { get { return isOver; } set { isOver = value; } }

        public void Launch()
        {
            // if the candidate has answer 
            if (!isWaiting)
            {

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

                    CameraManager.cameraManagerinstance.Display(GetCurrentCamera());
                    SpeechManager.speechManagerinstance.PlayAudio(sequenceList[currentSequence].DialogElements[currentElement].Id);
                    ihm.Activate_buttons_nb_answers(q.NumAnswers);
                    ihm.DisplayQuestion(q.Question);
                    ihm.DisplayAnswers(q.Answers);
                    // We are waiting for the candidate answer
                    isWaiting = true;
                }

                if (Object.ReferenceEquals(
                    sequenceList[currentSequence].DialogElements[currentElement].GetType(),
                    typeof(M_Phrase)))
                {
                    M_Phrase currentPhrase = (M_Phrase)sequenceList[currentSequence].DialogElements[currentElement];
                    if (currentElement == 0 ) currentPhrase.Display = true;
                    if(previous !=null && currentPhrase.Id==previous.Next) currentPhrase.Display = true;
                    if (currentPhrase.Display)
                    {
                        Console.WriteLine("P: " + currentPhrase.Text);
                         ihm.Clear();
                         CameraManager.cameraManagerinstance.Display(GetCurrentCamera()); // if we have no question mask the quesion interface
                         SpeechManager.speechManagerinstance.PlayAudio(currentPhrase.Id);
                         ihm.DisplayComment(currentPhrase.Text);

                        // We are waiting for the candidate answer
                        this.previous = currentPhrase;
                        isWaiting = true;
                    }

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
                List<M_Answer> answerList = new List<M_Answer>();
                List<string> answersString = new List<string>();

                    while (count < sequenceList[currentSequence].DialogElements.Count
                        && Object.ReferenceEquals(
                        sequenceList[currentSequence].DialogElements[count].GetType(),
                        typeof(M_Answer)))
                    {
                        M_Answer answer = (M_Answer)sequenceList[currentSequence].DialogElements[count];
                    answerList.Add(answer);
                        answersString.Add(answer.Text);
                        count++;
                    }
                int numAnswers = count - (currentElement + 1);
                this.qcm = new M_QCM(question, answerList);
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
            try
            {
                M_Answer answerChosen = null;
                answerChosen = qcm.Answers[chosen_answer];

                foreach (M_Phrase p in sequenceList[currentSequence].DialogElements)
                {
                    if (p.Id == answerChosen.Next)
                    {
                        p.Display = true;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
                
            isWaiting = false;
        }

        public string GetResult()
        {
            return "tu crains, le job est pour moi";
        }

    }
}
