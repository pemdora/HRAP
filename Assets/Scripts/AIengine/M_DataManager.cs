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
        private static string skillsPath = @"..\HRAP\Assets\AIData\competence.csv";
        private static string answers_pointsPath = @"..\HRAP\Assets\AIData\answers_points.csv";
        private static string candidates_pointsPath = @"..\HRAP\Assets\AIData\candidates_points.csv";
        private static string idealprofiles_pointsPath = @"..\HRAP\Assets\AIData\idealprofiles_points.csv";
        private static string matriceQualityCompetencePath = @"..\HRAP\Assets\AIData\qualityCompetence.csv";
        private static string importantCompetencePath = @"..\HRAP\Assets\AIData\importantCompetence.csv";

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

        private List<M_Quality> GetPointsQuality(string file, int id, int idCompentence)
        {
            List<M_Quality> qualityList = new List<M_Quality>();

            // 1. Get points in points File

            StreamReader p_reader = new StreamReader(file);
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

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {

                    for (int i = 1; i < temp.Length; i++)
                    {
                        qualityList.Add(new M_Quality(headers[i], Convert.ToInt32(temp[i])));
                    }
                }


                line = p_reader.ReadLine();
                count++;
            }

            p_reader.Close();

            // Check whether skills are important
            if (qualityList != null)
            {
                p_reader = new StreamReader(matriceQualityCompetencePath);
                line = p_reader.ReadLine();
                count = 0;

                while (line != null)
                {
                    string[] temp = line.Split(';');

                    if (count != 0 && Convert.ToInt32(temp[0]) == idCompentence)
                    {
                        for (int i = 0; i < qualityList.Count(); i++)
                        {
                            if (Convert.ToInt32(temp[i]) != 0)
                            {
                                qualityList[i].Ponderation = Convert.ToInt32(temp[i + 1]);
                            }
                        }
                    }

                    line = p_reader.ReadLine();
                    count++;
                }

                p_reader.Close();
            }


            return qualityList;

        }

        //calcul pour une competence retourne le nombre de point total avec les ponderation associé
        public int calculPointSkill(List<M_Quality> listQualite)
        {
            int score = 0;
            for (int i = 0; i < listQualite.Count(); i++)
            {
                score += listQualite[i].Ponderation * listQualite[i].Point;
            }
            return score;
        }

        //retourne une liste de competence associé au profile ideal ou au canididat
        private List<M_Skill> getPointSkill(string file, int idProfil, int id)
        {
            List<M_Skill> listSkill = new List<M_Skill>();

            StreamReader p_reader = new StreamReader(importantCompetencePath);
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

                if (count != 0 && Convert.ToInt32(temp[0]) == idProfil)
                {

                    for (int i = 1; i < temp.Length; i++)
                    {
                        if (Convert.ToInt32(temp[i]) == 0)
                        {
                            listSkill.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), calculPointSkill(GetPointsQuality(file, id, i)), false, GetPointsQuality(file, id, i)));

                        }
                        if (Convert.ToInt32(temp[i]) == 1)
                        {
                            listSkill.Add(new M_Skill(headers[i], GetSkillCategory(headers[i]), calculPointSkill(GetPointsQuality(file, id, i)), true, GetPointsQuality(file, id, i)));

                        }

                    }
                }


                line = p_reader.ReadLine();
                count++;
            }

            p_reader.Close();

            return listSkill;
        }


        // QUESTIONS

        public int CountQuestions()
        {
            return Count(questionsPath);
        }

        public M_Question GetQuestionById(int id)
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
                    return new M_Question(id, temp[1], Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3]), Convert.ToInt32(temp[4]));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return null;
        }

        public int GetIdCompetenceByIdQuestion(int id)
        {

            StreamReader reader = new StreamReader(questionsPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');
                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    return Convert.ToInt32(temp[4]);
                }
                line = reader.ReadLine();
                count += 1;
            }

            reader.Close();

            return 0;
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
            int count = 0;

            while (line != null)
            {

                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[1]) == questionId)
                {
                    result.Add(new M_Answer(Convert.ToInt32(temp[0]), questionId, temp[2], Convert.ToInt32(temp[3]), null));
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in answers
            if (result != null)
            {
                foreach (M_Answer a in result)
                {
                    a.QualityList = GetPointsQuality(answers_pointsPath, a.Id, GetIdCompetenceByIdQuestion(questionId));

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
            M_Answer result = null;
            StreamReader reader = new StreamReader(answersPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    result = new M_Answer(id, Convert.ToInt32(temp[1]), temp[2], Convert.ToInt32(temp[3]), null);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in answer
            if (result != null)
            {
                result.QualityList = GetPointsQuality(answers_pointsPath, id, GetIdCompetenceByIdQuestion(result.QuestionId));
            }
            return result;
        }



        // CANDIDATES

        public int CountCandidates()
        {
            return Count(candidatesPath);
        }

        public M_Candidate GetCandidate(int id, M_Experience exp)
        {
            M_Candidate result = null;

            StreamReader reader = new StreamReader(candidatesPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    result = new M_Candidate(id, temp[1], temp[2], temp[3], null);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            // Set points in candidate
            if (result != null)
            {
                //un candidat est associé à un profil ideal pour l'evaluation de celui ci 

                result.Skills = getPointSkill(candidates_pointsPath, GetIdealProfileID(result.Name, exp), id);
            }

            return result;
        }

        public List<M_Question> GetListQuestion(int idCompetence)
        {
            List<M_Question> question = new List<M_Question>();

            StreamReader reader = new StreamReader(questionsPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[4]) == idCompetence)
                {
                    question.Add(new M_Question(Convert.ToInt32(temp[0]), temp[1], Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3]), idCompetence));
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            return question;
        }

        // TO DO
        public void AddCandidate(M_Candidate candidate)
        {

            string newLine = candidate.Id + ";" +
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

        public int GetIdCompetenceByName(string name)
        {
            int result = 0;
            StreamReader reader = new StreamReader(skillsPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && temp[1] == name)
                {
                    result = Convert.ToInt32(temp[0]);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            return result;
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
            StreamReader reader = new StreamReader(idealProfilesPath);
            string line = reader.ReadLine();
            int count = 0;

            while (line != null)
            {
                string[] temp = line.Split(';');

                if (count != 0 && Convert.ToInt32(temp[0]) == id)
                {
                    result = new M_IdealProfile(id, temp[1], GetExperience(temp[2]), null);
                }


                line = reader.ReadLine();
                count++;
            }

            reader.Close();

            if (result != null)
            {

                result.Skills = getPointSkill(idealprofiles_pointsPath, id, id);
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

        public M_SkillCategory GetSkillCategory(string category)
        {
            M_SkillCategory result;
            switch (category)
            {
                case "Leadership":
                    result = M_SkillCategory.HUMAINE;
                    break;
                case "Controle emotionnel":
                    result = M_SkillCategory.HUMAINE;
                    break;
                case "Sociabilite":
                    result = M_SkillCategory.HUMAINE;
                    break;
                default:
                    result = M_SkillCategory.TECHNIQUE;
                    break;
            }
            return result;
        }

        public List<M_Skill> InitializeSkills()
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
                    result.Add(new M_Skill(temp[1], GetSkillCategory(temp[1]), 0, false));
                }

                line = reader.ReadLine();
                count++;
            }

            reader.Close();
            return result;

        }



    }
}
