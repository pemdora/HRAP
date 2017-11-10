using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP
{
    abstract class DataManager
    {
        private static string questionsPath = @"..\..\Data\questions.csv";
        private static string answersPath = @"..\..\Data\answers.csv";
        private static string profilesPath = @"..\..\Data\profiles.csv";

        // QUESTIONS

        public static Question getQuestionById(int id)
        {
            StreamReader reader = new StreamReader(questionsPath);
            string line = reader.ReadLine();
            int count = 0;


            while (line != null)
            {
                string[] temp = line.Split(';');
                // first line is titles
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    return new Question(id, temp[1], Convert.ToInt32(temp[2]));
                }
               
                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
        }

        // ANSWERS

        public static List<Answer> getAnswersByQuestionId(int questionId)
        {
            List<Answer> result = new List<Answer>();
            //creation d'une liste de skill ou chaque skill corresponds à une qualité (motivation,leadership...)
            Dictionary<string, int> skills = new Dictionary<string, int>();
            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            string[] titles = { };
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (count == 0)
                {
                    titles = temp;
                    
                }
                if (count != 0 && Convert.ToInt32(temp[1]) == questionId)
                {
                    //modifier les colonnes 
                    for(int i=3; i < 16; i++)
                    {
                        skills.Add(titles[i], Convert.ToInt32(temp[i]));
                    }
                    result.Add(new Answer(Convert.ToInt32(temp[0]), questionId, temp[2], skills));
                }

                skills.Clear();
                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;
        }
    }
}
