using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace HRAP
{
    abstract class DataManager
    {
        private static string questionsPath = @"..\HRAP\Assets\AIData\questions.csv";
        private static string answersPath = @"..\HRAP\Assets\AIData\answers.csv";
        private static string profilesPath = @"..\HRAP\Assets\AIData\profiles.csv";

        // QUESTIONS

        public static Question GetQuestionById(int id)
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

        public static List<Answer> GetAnswersByQuestionId(int questionId)
        {
            List<Answer> result = new List<Answer>();

            
            
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
                    Dictionary<string, int> skills = new Dictionary<string, int>();
                    //modifier les colonnes 
                    for (int i=3; i < 27; i++)
                    {
                        skills.Add(titles[i], Convert.ToInt32(temp[i]));
                    }
                    result.Add(new Answer(Convert.ToInt32(temp[0]), questionId, temp[2], skills));
                }

                
                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;
        }

        public static int GetIDProfile()
        {
            
            StreamReader reader = new StreamReader(profilesPath);
            string line = reader.ReadLine();
            int idprofile = 0;
           
            while (line != null)
            {               
                line = reader.ReadLine();
                idprofile+=1;
            }

            reader.Close();

            return idprofile;

        }

        public static Dictionary<string, int> InitialisationSkill()
        {
            Dictionary<string, int> skills = new Dictionary<string, int>();

            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();

            string[] temp = line.Split(';');
            for (int i = 3; i < 27; i++)
            {
                skills.Add(temp[i], 0);

            }

            
            reader.Close();

            return skills;
        }

        //Enregistre le candidat dans le fichier profiles.csv
        public static void SetCandidatIntoFile(Candidate candidat)
        {

            string newLine = candidat.Id + ";"+candidat.Type+ ";" +candidat.Name;
            for(int i = 0; i < candidat.Skills.Count; i++)
            {
                newLine += ";" + candidat.Skills.ElementAt(i).Value; 
            }

            newLine += "\n";

            File.AppendAllText(profilesPath, newLine);



            


        }
        


        //IA

        public void GetBestQuestion()
        {
            

        }

    }
}
