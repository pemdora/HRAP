﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP
{
    class QCM
    {
        int ScoreMotivation;
        int ScoreLeadership;
        int ScoreControleEmmotionnel;
        int ScoreSociabilite;

        int NombreQuestionPoser;
        Dictionary<int, int> BilanQuestionReponse = new Dictionary<int, int>();
               
        public QCM()
        {
           

        }

        public void Evaluation(Answer answer,Candidate candidat)
        {
            candidat.UpdateCandidate(answer);

            for (int i = 0; i < 4; i++)
            {                
                ScoreMotivation += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 4; i < 9; i++)
            {                
                ScoreControleEmmotionnel += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 9; i < 16; i++)
            {               
                ScoreLeadership += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 16; i < 24; i++)
            {
                ScoreSociabilite += candidat.Skills.ElementAt(i).Value;
            }



        }

        public static Answer PoseQuestion(int IDQuestion)
        {       
           

            Console.WriteLine(DataManager.getQuestionById(IDQuestion).String);
            List<Answer> answers = DataManager.getAnswersByQuestionId(IDQuestion);
            
            for(int i = 0; i < answers.LongCount(); i++)
            {
                Console.WriteLine(i+1+": "+answers[i].String);
               
            }
            bool reponseUser = true;
            while (reponseUser)
            {
                try
                {
                    Answer Reponse = answers[Convert.ToInt32(Console.ReadLine())- 1];
                    reponseUser = false;
                    return Reponse;

                }
                catch
                {
                    Console.WriteLine("Veuillez saisir un numero de reponse valide");
                    reponseUser = true;

                }

            }
            
            return null;
        }

       


        public void LancementQCM()
        {
            Console.WriteLine("Votre nom?");
            string nom = Console.ReadLine();

            Console.WriteLine("Pour quel post postulez vous?");
            string type = Console.ReadLine();

            Candidate candidat = new Candidate(DataManager.getIDProfile(),type, nom, DataManager.InitialisationSkill());
            
            int QuestionAleatoir = 1;

            if (ScoreMotivation==0 && ScoreLeadership==0 && ScoreControleEmmotionnel==0 && ScoreSociabilite == 0)
            {
                
                Answer reponse = PoseQuestion(QuestionAleatoir);
                NombreQuestionPoser += 1;
                Evaluation(reponse, candidat);
                BilanQuestionReponse.Add(QuestionAleatoir, reponse.Id);
                


            }
            while (true)
            {
                Answer reponse;
                

                if (QuestionAleatoir == 11)
                {
                    reponse = PoseQuestion(12);
                    Evaluation(reponse, candidat);
                    BilanQuestionReponse.Add(12, reponse.Id);
                    QuestionAleatoir += 1;
                    NombreQuestionPoser += 1;

                }
                if (QuestionAleatoir == 13)
                {
                    reponse = PoseQuestion(14);
                    Evaluation(reponse, candidat);
                    BilanQuestionReponse.Add(14, reponse.Id);
                    QuestionAleatoir += 1;
                    NombreQuestionPoser += 1;
                }

                else
                {
                    
                    QuestionAleatoir += 1;
                    reponse = PoseQuestion(QuestionAleatoir);
                    NombreQuestionPoser += 1;


                    Evaluation(reponse,candidat);
                    BilanQuestionReponse.Add(QuestionAleatoir, reponse.Id);

                }




                if (NombreQuestionPoser == 14)
                {

                    int ScoreTotal = ScoreControleEmmotionnel + ScoreLeadership + ScoreMotivation + ScoreSociabilite;
                    Console.WriteLine("Le QCM est terminé\nVotre score Total est de : " + ScoreTotal);
                    Console.WriteLine("Motivation : " + ScoreMotivation);
                    Console.WriteLine("Leadership : " + ScoreLeadership);
                    Console.WriteLine("Emotions : " + ScoreControleEmmotionnel);
                    Console.WriteLine("Sociabilité : " + ScoreSociabilite);
                    Console.WriteLine();
                    for(int i = 0; i < candidat.Skills.Count(); i++)
                    {
                       Console.WriteLine(candidat.Skills.ElementAt(i).Key + ":" + candidat.Skills.ElementAt(i).Value);
                    }




                   
                    break;


                }
            }

            DataManager.setCandidatIntoFile(candidat);
        }

    }
}
