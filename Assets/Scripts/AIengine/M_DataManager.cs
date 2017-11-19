using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace HRAP
{
    public class M_DataManager
    {
        private static string questionsPath = @"..\HRAP\Assets\AIData\questions.csv";
        private static string answersPath = @"..\HRAP\Assets\AIData\answers.csv";
        private static string candidatesPath = @"..\HRAP\Assets\AIData\candidates.csv";
        private static string idealProfilesPath = @"..\HRAP\Assets\AIData\idealprofiles.csv";
        private static string skillsPath = @"..\HRAP\Assets\AIData\skills.csv";
        private static string pointsPath = @"..\HRAP\Assets\AIData\points.csv";
        private static string importantpointsPath = @"..\HRAP\Assets\AIData\importantpoints.csv";

        private static M_DataManager instance;

        private M_DataManager() { }

        public static M_DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new M_DataManager();
                }
                return instance;
            }
        }

        
        // Counts number of lines in a file

        private int Count(string file)
        {
            int result = -1;

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

        private List<M_Skill> GetPoints(int pointsID)
        {
            List<M_Skill> skillsList = new List<M_Skill>();

            // 1. Get points in points File

            StreamReader p_reader = new StreamReader(pointsPath);
            string line = p_reader.ReadLine();
            string[] headers = { };
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (count == 0)
                {
                    headers = temp;

                }
                if (count != 0 && Convert.ToInt32(temp[0]) == pointsID)
                {
                    
                    for (int i = 1; i < temp.Length; i++)
                    {
                        skillsList.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i]), false));
                    }
                }

                line = p_reader.ReadLine();
                count++;
            }

            p_reader.Close();

            // Check whether skills are important
            if(skillsList != null)
            {
                p_reader = new StreamReader(importantpointsPath);
                line = p_reader.ReadLine();
                count = 0;

                while (line != null)
                {
                    string[] temp = line.Split(';');

                    if (count != 0 && Convert.ToInt32(temp[0]) == pointsID)
                    {
                        for (int i = 1; i < temp.Length; i++)
                        {
                            if (Convert.ToInt32(temp[i]) != 0)
                            {
                                skillsList[i].IsImportant = true;
                            }
                        }
                    }

                    line = p_reader.ReadLine();
                    count++;
                }

                p_reader.Close();
            }

            return skillsList;
        }

        // QUESTIONS

        public int CountQuestions()
        {
            return Count(questionsPath);
        }

        public  M_Question GetQuestionById(int id)
        {
            StreamReader reader = new StreamReader(questionsPath);
            string line = reader.ReadLine();
            int count = 0;


            while (line != null)
            {
                string[] temp = line.Split(';');
                // first line is headers
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    return new M_Question(id, temp[1], Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3]));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
        }

        public int GetQuestionID(string question)
        {
            StreamReader reader = new StreamReader(questionsPath);
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (temp[1] == question)
                {
                    return Convert.ToInt32(temp[0]);
                }
                line = reader.ReadLine();
            }

            reader.Close();
            return 0;
        }



        // ANSWERS

        public int CountAnswers()
        {
            return Count(answersPath);
        }

        public List<M_Answer> GetAnswersByQuestionId(int questionId)
        {
            List<M_Answer> result = new List<M_Answer>();
            List<int> pointsIDs = new List<int>();

            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {

                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[1]) == questionId)
                {
                    result.Add(new M_Answer(Convert.ToInt32(temp[0]), questionId, temp[2], Convert.ToInt32(temp[3]), null));
                    pointsIDs.Add(Convert.ToInt32(temp[4]));
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in answers
            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].Skills = GetPoints(pointsIDs[i]);
                }
            }
            

            return result;
        }

        public int GetAnswerID(int questionId, string answer)
        {
            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (count != 0)
                {
                    if (Convert.ToInt32(temp[1]) == questionId && temp[2] == answer)
                    {
                        return Convert.ToInt32(temp[0]);
                    }
                }
                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return 0;
        }

        public M_Answer GetAnswer(int id)
        {
            M_Answer result= null;
            int pointsID = 0;
            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    result=  new M_Answer(id, Convert.ToInt32(temp[1]), temp[2], Convert.ToInt32(temp[3]), null);
                    pointsID = Convert.ToInt32(temp[4]);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in answer
            if (result !=null)
            {
                result.Skills = GetPoints(pointsID);
            }
            return result;
        }

       

        // CANDIDATES

        public  int CountCandidates()
        {
            return Count(candidatesPath);
        }

        public M_Candidate GetCandidate(int id)
        {
            M_Candidate result = null;
            int pointsID = 0;

            StreamReader reader = new StreamReader(candidatesPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                     result = new M_Candidate(id, temp[1], temp[2], temp[3], null);
                    pointsID = Convert.ToInt32(temp[4]);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in candidate
            if (result != null)
            {
                result.Skills = GetPoints(pointsID);
            }

            return result;
        }

        // TO DO
        public void AddCandidate(M_Candidate candidate)
        {

            string newLine =    candidate.Id + ";" + 
                                candidate.Name + ";" + 
                                candidate.TargetJob + ";" + 
                                candidate.Result;

            for (int i = 0; i < candidate.Skills.Count; i++)
            {
                newLine += ";" + candidate.Skills[i].Points;
            }

            newLine += "\n";

            File.AppendAllText(candidatesPath, newLine);

        }


        // IDEAL PROFILES

        public M_Experience GetExperience(string exp)
        {
            M_Experience result;
            switch (exp)
            {
                case "NULL":
                    result = M_Experience.NULL;
                    break;
                case "JUNIOR":
                    result = M_Experience.JUNIOR;
                    break;
                case "INTERMEDIATE":
                    result = M_Experience.INTERMEDIATE;
                    break;
                case "EXPERT":
                    result = M_Experience.EXPERT;
                    break;
                default:
                    result = M_Experience.NULL;
                    break;
            }
            return result;
        }

        public M_IdealProfile GetIdealProfile(int id)
        {
            M_IdealProfile result = null;
            int pointsID = 0;
            StreamReader reader = new StreamReader(idealProfilesPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');
                
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    result = new M_IdealProfile(id, temp[1], GetExperience(temp[2]), null);
                    pointsID = Convert.ToInt32(temp[3]);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            if (result != null)
            {
                result.Skills = GetPoints(pointsID);
            }

            return result;
        }

        public int GetIdealProfileID(string name, M_Experience exp)
        {
            StreamReader reader = new StreamReader(idealProfilesPath);
            string line = reader.ReadLine();


            while (line != null)
            {
                string[] temp = line.Split(';');
                if (temp[1] == name && temp[2] == exp.ToString())
                {
                    return Convert.ToInt32(temp[0]);
                }
                line = reader.ReadLine();
            }

            reader.Close();
            return 0;
        }


        // SKILLS

        public int CountSkills()
        {
            return Count(skillsPath);
        }

        public  M_SkillCategory GetSkillCategory(string category)
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

        public  List<M_Skill> InitializeSkills()
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
                    result.Add(new M_Skill(temp[0], GetSkillCategory(temp[1]), 0, false));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;

        }


    }
}
