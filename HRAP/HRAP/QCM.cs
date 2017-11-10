using System;
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

        //list IDQuestion
        List<int> IDQuestionPoser = new List<int>();
        
        Dictionary<int, int> BilanQuestionReponse = new Dictionary<int, int>();

        public QCM()
        {
            //initialisation des questions en lien avec les capacité a evaluer
            for(int i = 0; i < 14; i++)
            {
                IDQuestionPoser.Add(i + 1);
            }
            

        }

        public void Evaluation(int IDReponse)
        {
            string answersPath = @"..\..\Data\answers.csv";

            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            int count = 0;

                while (line != null)
            {
                string[] temp = line.Split(';');
                
                // first line is titles
                if (count != 0 && Convert.ToInt32(temp[0]) == IDReponse)
                {
                    for (int i = 3; i < 7; i++)
                    {
                        ScoreMotivation += Convert.ToInt32(temp[i]);
                    }
                    for (int i = 7; i < 12; i++)
                    {
                        ScoreControleEmmotionnel += Convert.ToInt32(temp[i]);
                    }
                    for (int i = 12; i < 19; i++)
                    {
                        ScoreLeadership += Convert.ToInt32(temp[i]);
                    }
                    for (int i = 19; i < 27; i++)
                    {
                        ScoreSociabilite += Convert.ToInt32(temp[i]);
                    }

                }
                
                               
                line = reader.ReadLine();
                count++;
            }

            reader.Close();
      

        }

        public int PoseQuestion(int IDQuestion)
        {
            int IDReponse=0;

            Console.WriteLine(DataManager.getQuestionById(IDQuestion).String);
            List<Answer> answers = DataManager.getAnswersByQuestionId(IDQuestion);
            
            for(int i = 0; i < answers.LongCount(); i++)
            {
                Console.WriteLine(i+1+": "+answers[i].String);
               
            }
            while (IDReponse == 0)
            {
                try
                {
                    IDReponse = answers[Convert.ToInt32(Console.ReadLine()) - 1].Id;
                }
                catch
                {
                    Console.WriteLine("Veuillez saisir un numero de reponse valide");

                }

            }
            
            return IDReponse;
        }

        public void getBestQuestion()
        {


        }


        public void LancementQCM()
        {
            /*Random r = new Random();
            int QuestionAleatoir = r.Next(1,14);
            while(QuestionAleatoir==12 || QuestionAleatoir == 14)
            {
                QuestionAleatoir = r.Next(1, 14);
            }*/

            int QuestionAleatoir = 1;

            if (ScoreMotivation==0 && ScoreLeadership==0 && ScoreControleEmmotionnel==0 && ScoreSociabilite == 0)
            {
                
                int reponse = PoseQuestion(QuestionAleatoir);
                Evaluation(reponse);
                IDQuestionPoser.RemoveAt(QuestionAleatoir - 1);
                BilanQuestionReponse.Add(QuestionAleatoir, reponse);

            }
            while (true)
            {
                int reponse = 0;
                int question = 0;

                if (QuestionAleatoir == 11)
                {
                    reponse = PoseQuestion(12);
                    Evaluation(reponse);
                    IDQuestionPoser.RemoveAt(QuestionAleatoir - 1);
                    BilanQuestionReponse.Add(QuestionAleatoir, reponse);
                }
                if (QuestionAleatoir == 13)
                {
                    reponse = PoseQuestion(14);
                    Evaluation(reponse);
                    IDQuestionPoser.RemoveAt(QuestionAleatoir - 1);
                    BilanQuestionReponse.Add(QuestionAleatoir, reponse);
                }

                else
                {
                    if (ScoreMotivation < 5)
                    {
                                                
                    }
                    if (ScoreLeadership < 5)
                    {
                        
                    }
                    if (ScoreControleEmmotionnel < 5)
                    {
                       
                    }
                    if (ScoreSociabilite < 5)
                    {
                        
                    }
                    else
                    {
                        question = QuestionAleatoir + 1;
                        reponse = PoseQuestion(question);
                        QuestionAleatoir += 1;

                    }

                    Evaluation(reponse);
                    IDQuestionPoser.RemoveAt(question - 1);
                    BilanQuestionReponse.Add(question, reponse);

                }




                if (IDQuestionPoser.LongCount() < 10)
                {

                    int ScoreTotal = ScoreControleEmmotionnel + ScoreLeadership + ScoreMotivation + ScoreSociabilite;
                    Console.WriteLine("Le QCM est terminé\nVotre score Total est de : " + ScoreTotal);
                    Console.WriteLine("Motivation : " + ScoreMotivation);
                    Console.WriteLine("Leadership : " + ScoreLeadership);
                    Console.WriteLine("Emotions : " + ScoreControleEmmotionnel);
                    Console.WriteLine("Sociabilité : " + ScoreSociabilite);

                    break;


                }
            }
        }

    }
}
