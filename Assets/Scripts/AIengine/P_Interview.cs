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
        private List<M_Sequence> sequenceList;
        private M_DialogElement previous;
        private V_Question q; // current question
        private M_QCM qcm;
        public List<M_Answer> candidateAnswers;


        private int currentSequence;
        private int currentElement;
        private bool isWaiting; // is waiting for candidate answer
        private bool isOver;

        public P_Interview(M_Candidate candidate, IHMInterview ihm)
        {
            this.candidate = candidate;
            this.candidateAnswers = new List<M_Answer>();
            this.sequenceList = new List<M_Sequence>();
            sequenceList.Add(new M_Sequence());
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
            if (!isWaiting)
            {
                // If current dialog element is a question

                if (sequenceList[currentSequence].DialogElements[currentElement] is M_Question)
                {
                    // Get question and answers
                    q = GetNextQuestion();

                    // Display question and answers
                    ihm.Activate_buttons_nb_answers(q.NumAnswers);
                    ihm.DisplayQuestion(q.Question);
                    ihm.DisplayAnswers(q.Answers);

                    #region Camera - Animation - Audio 
                    CameraManager.cameraManagerinstance.Display(GetCurrentCamera()); // Change camera position
                    ChooseAnimationToPlay(); // Choose a dynamic animation to play
                    AISpeechManager.speechManagerinstance.LoadandPlayAudio(sequenceList[currentSequence].DialogElements[currentElement].Id); // load and play audio
                    CandidateController.candidateControllerInstance.DiplayCandidateInterface(0f); // AISpeechManager.speechManagerinstance.GetLengthAudioClip() - 4f
                    #endregion

                    // We are waiting for the candidate answer
                    isWaiting = true;
                }

                // If current dialog element is a phrase

                if (sequenceList[currentSequence].DialogElements[currentElement] is M_Phrase)
                {
                    M_Phrase currentPhrase = (M_Phrase)sequenceList[currentSequence].DialogElements[currentElement];

                    // If phrase is first element in sequence, display phrase
                    if (currentElement == 0) currentPhrase.Display = true;

                    // If phrase is next element to previous element, display phrase
                    if (previous != null && currentPhrase.Id == previous.Next) currentPhrase.Display = true;

                    if (currentPhrase.Display)
                    {
                        ihm.Clear();
                        ihm.DisplayComment(currentPhrase.Text);
                        #region Camera - Animation - Audio 
                        CameraManager.cameraManagerinstance.Display(GetCurrentCamera());
                        ChooseAnimationToPlay();
                        AISpeechManager.speechManagerinstance.LoadandPlayAudio(currentPhrase.Id); // AISpeechManager.speechManagerinstance.GetLengthAudioClip() - 4f
                        CandidateController.candidateControllerInstance.DiplayCandidateInterface(0f); // if it is a sentence, wait duration of clip -4s because clip finished 0.5s after she has spoken
                        #endregion

                        // We are waiting for the candidate answer (button click)
                        this.previous = currentPhrase;
                        isWaiting = true;
                    }

                }


                // If current element is last element in sequence, set next sequence

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
                        // there is no more sequence in database
                        this.IsOver = true;
                    }
                }

                // If current element is not last element in sequence, get next element

                if (currentElement < sequenceList[currentSequence].DialogElements.Count - 1)
                {
                    this.currentElement++;
                }
            }

            // otherwise do nothing

        }


        // Return current dialog element animation

        private M_Animation GetCurrentAnimation()
        {
            return sequenceList[currentSequence].DialogElements[currentElement].Animation;
        }


        // Return current dialog element camera 

        private M_Camera GetCurrentCamera()
        {
            return sequenceList[currentSequence].DialogElements[currentElement].Camera;
        }


        // Convert next M_Question to V_Question

        private V_Question GetNextQuestion()
        {
            V_Question result = null;

            // Get M_Question 

            M_Question question = (M_Question)sequenceList[currentSequence].DialogElements[currentElement];

            // If question is found, get answers

            if (question != null)
            {
                int count = currentElement + 1;
                List<M_Answer> answerList = new List<M_Answer>();
                List<string> answersString = new List<string>();

                // While element is not last element and is an answer, add answer in answerList 
                while (count < sequenceList[currentSequence].DialogElements.Count
                    && (sequenceList[currentSequence].DialogElements[count] is M_Answer))
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

        // Add candidate answer in candidateAnswers

        public void SetChosenAnswer(int chosen_answer)
        {
            if (!(sequenceList[currentSequence].DialogElements[currentElement] is M_Phrase) && this.qcm != null)
            {
                M_Answer answerChosen = null;
                answerChosen = this.qcm.Answers[chosen_answer];
                if (!candidateAnswers.Contains(answerChosen))
                    candidateAnswers.Add(answerChosen);

                // Display next element
                for (int i = currentSequence; i < sequenceList.Count; i++)
                {
                    foreach (M_DialogElement e in sequenceList[i].DialogElements)
                    {
                        if (e.Id == answerChosen.Next)
                        {
                            e.Display = true;
                        }
                    }
                }
            }

            isWaiting = false;
        }



        // Return candidate competences at the end of the interview

        public List<V_Competence> GetResult1()
        {
            List<V_Competence> result = new List<V_Competence>();

            // Update the values of answers qualities
            candidateAnswers = M_DataManager.Instance.UpdateQualityPoints(candidateAnswers);

            // Convert qualities to competences
            candidate.UpdateCompetences(candidateAnswers);

            // Add competences in result
            foreach (M_Competence c in candidate.CompetencesList)
            {
                result.Add(new V_Competence(c.Name, c.Points));
            }

            return result;
        }

        public void ChooseAnimationToPlay()
        {
            if (sequenceList[currentSequence].DialogElements[currentElement].Id == "1b") // the interview has began, characters need to sit
            {
                AIBehaviour.aiBehaviourInstance.PlayAnimation(M_Animation.ANIM_SASSOIR, 0f);
                CandidateController.candidateControllerInstance.PlayAnimation(M_Animation.ANIM_MARCHE, 2f);
            }
        }

    }
}
