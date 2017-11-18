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
                    for (int i = 4; i < 28; i++)
                    {
                        skills.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i])));
                    }
                    result.Add(new M_Answer(Convert.ToInt32(temp[0]), questionId, temp[2], Convert.ToInt32(temp[3]), skills));
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;
        }

        public int GetAnswerID(int questionId, string answer)
        {
            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (Convert.ToInt32(temp[1]) == questionId && temp[2] == answer)
                {
                    return Convert.ToInt32(temp[0]);
                }
                line = reader.ReadLine();
            }

            reader.Close();
            return 0;
        }

        public M_Answer GetAnswer(int id)
        {
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
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    List<M_Skill> skills = new List<M_Skill>();
                    for (int i = 4; i < 28; i++)
                    {
                        skills.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i])));
                    }
                    return new M_Answer(id, Convert.ToInt32(temp[1]), temp[2], Convert.ToInt32(temp[3]), skills);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
        }

       

        // CANDIDATES

        public  int CountCandidates()
        {
            return Count(candidatesPath);
        }

        public M_Candidate GetCandidate(int id)
        {
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
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    List<M_Skill> skills = new List<M_Skill>();
                    for (int i = 4; i < 28; i++)
                    {
                        skills.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i])));
                    }
                    return new M_Candidate(id, temp[1], temp[2], temp[3], skills);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
        }

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

    /*    public string GetExperienceToString(M_Experience exp)
        {
            string result;
            switch (exp)
            {
                case M_Experience.NULL:
                    result = "NULL";
                    break;
                case M_Experience.JUNIOR:
                    result = "JUNIOR";
                    break;
                case M_Experience.INTERMEDIATE:
                    result = "INTERMEDIATE";
                    break;
                case M_Experience.EXPERT:
                    result = "EXPERT";
                    break;
                default:
                    result = "NULL";
                    break;
            }
            return result;
        }*/

        public M_IdealProfile GetIdealProfile(int id)
        {
            StreamReader reader = new StreamReader(idealProfilesPath);
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
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    List<M_Skill> skills = new List<M_Skill>();
                    for (int i = 4; i < 28; i++)
                    {
                        skills.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), Convert.ToInt32(temp[i])));
                    }
                    return new M_IdealProfile(id, temp[1], GetExperience(temp[2]), skills);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
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
                    result.Add(new M_Skill(temp[0], GetSkillCategory(temp[1]), 0));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;

        }


    }
}
