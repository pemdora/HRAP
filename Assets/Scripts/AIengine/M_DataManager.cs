using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace HRAP
{
    abstract class M_DataManager
    {
        private static string questionsPath = @"..\HRAP\Assets\AIData\questions.csv";
        private static string answersPath = @"..\HRAP\Assets\AIData\answers.csv";
        private static string candidatesPath = @"..\HRAP\Assets\AIData\candidates.csv";
        private static string idealProfilesPath = @"..\HRAP\Assets\AIData\idealprofiles.csv";
        private static string skillsPath = @"..\HRAP\Assets\AIData\skills.csv";

        public static int Count(string file)
        {
            int result = 0;

            StreamReader reader = new StreamReader(file);
            string line = reader.ReadLine();

            while (line != null)
            {
                line = reader.ReadLine();
                result += 1;
            }

            reader.Close();

            return result;

        }

        // QUESTIONS

        public static int CountQuestions()
        {
            return Count(questionsPath);
        }

        public static M_Question GetQuestionById(int id)
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
                    return new M_Question(id, temp[1], Convert.ToInt32(temp[2]));
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
            string[] headers = { };
            int count = 0;

            while (line != null)
            {


                string[] temp = line.Split(';');
                if (count == 0)
                {
                    headers = temp;

                }
                if (count != 0 && Convert.ToInt32(temp[1]) == questionId)
                {
                    List<M_Skill> skills = new List<M_Skill>();
                    for (int i = 3; i < 27; i++)
                    {
                        skills.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i])));
                    }
                    result.Add(new Answer(Convert.ToInt32(temp[0]), questionId, temp[2], skills));
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;
        }

        // PROFILES

        public static int CountProfiles()
        {
            return Count(candidatesPath);
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
        public static void SetCandidatIntoFile(M_Candidate candidat)
        {

            string newLine = candidat.Id + ";" + candidat.Name;
            for (int i = 0; i < candidat.Skills.Count; i++)
            {
                newLine += ";" + candidat.Skills[i].Points;
            }

            newLine += "\n";

            File.AppendAllText(candidatesPath, newLine);

        }

        // SKILLS

        public static M_SkillCategory GetSkillCategory(string category)
        {
            M_SkillCategory result;
            switch (category)
            {
                case "motivation":
                    result = M_SkillCategory.MOTIVATION;
                    break;
                case "controle emotionnel":
                    result = M_SkillCategory.CONTROLE_EMOTIONNEL;
                    break; 
                case "leadership":
                    result = M_SkillCategory.LEADERSHIP;
                    break;
                case "sociabilite":
                    result = M_SkillCategory.SOCIABILITE;
                    break;
                default:
                    result = M_SkillCategory.NULL;
                    break;
            }
            return result;
        }

        public static List<M_Skill> initializeSkills()
        {
            List<M_Skill> result = new List<M_Skill>();

            StreamReader reader = new StreamReader(skillsPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                // first line is headers
                if (count != 0)
                {
                    result.Add(new M_Skill(temp[0], GetSkillCategory(temp[1]), 0));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;

        }

        //IA

        public void GetBestQuestion()
        {


        }

    }
}
