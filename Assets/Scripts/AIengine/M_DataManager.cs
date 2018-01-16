using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace HRAP
{
    public class M_DataManager
    {
        private static string dialogPath = @"..\HRAP\Assets\AIData\dialog.xml";
        private static string matriceCQPath = @"..\HRAP\Assets\AIData\matriceCQ.csv";
        // private static string candidatesPath = @"..\HRAP\Assets\AIData\candidates.csv";
        private static string idealProfilesPath = @"..\HRAP\Assets\AIData\idealprofiles.csv";
        //private static string competencesPath = @"..\HRAP\Assets\AIData\skills.csv";
        private static string candidates_pointsPath = @"..\HRAP\Assets\AIData\candidates_points.csv";
        private static string idealprofiles_pointsPath = @"..\HRAP\Assets\AIData\idealprofiles_points.csv";
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

        // Matrice CQ
        public string[][] GetMatrice()
        {
            return File.ReadAllLines(matriceCQPath).Where(line => line != "").Select(x => x.Split(';')).ToArray();
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

        private List<M_Competence> GetPoints(string file, int id)
        {
            List<M_Competence> competencesList = new List<M_Competence>();

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
                        competencesList.Add(new M_Competence(headers[i], false, Convert.ToInt32(temp[i]), false));
                    }
                }


                line = p_reader.ReadLine();
                count++;
            }

            p_reader.Close();

            // Check whether skills are important
            if (competencesList != null)
            {
                p_reader = new StreamReader(importantpointsPath);
                line = p_reader.ReadLine();
                count = 0;

                while (line != null)
                {
                    string[] temp = line.Split(';');

                    if (count != 0 && Convert.ToInt32(temp[0]) == id)
                    {
                        for (int i = 1; i < temp.Length; i++)
                        {
                            if (Convert.ToInt32(temp[i]) != 0)
                            {
                                competencesList[i].IsImportant = true;
                            }
                        }
                    }

                    line = p_reader.ReadLine();
                    count++;
                }

                p_reader.Close();
            }

            return competencesList;
        }



        // CANDIDATES

        public int CountCandidates()
        {
            //return Count(candidatesPath);
            return 0;
        }

       /* public M_Candidate GetCandidate(int id)
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
                result.CompetencesList = GetPoints(candidates_pointsPath, id);
            }

            return result;
        }*/

        //TO DO
        public void AddCandidate(M_Candidate candidate)
        {

            string newLine = candidate.Id + ";" +
                                candidate.Name + ";" +
                                candidate.TargetJob + ";" +
                                candidate.Result;

            for (int i = 0; i < candidate.CompetencesList.Count; i++)
            {
                newLine += ";" + candidate.CompetencesList[i].Points;
            }

            newLine += "\n";

            // TO DO : Save candidate in csv file
            //File.AppendAllText(candidatesPath, newLine);

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
                result.CompetencesList = GetPoints(idealprofiles_pointsPath, id);
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


        // DIALOG


        private M_Animation ToAnimation(string toParse)
        {
            M_Animation result;

            switch (toParse)
            {
                case "Geste d'accueil":
                    result = M_Animation.ANIM_SALUTATIONS;
                    break;
                case "Invitation de la main":
                    result = M_Animation.ANIM_GESTE_MAIN;
                    break;
                case "Sourit":
                    result = M_Animation.ANIM_SOURIRE;
                    break;
                case "Ouverture des bras":
                    result = M_Animation.OUVERTURE_BRAS;
                    break;
                case "Clin d'œil":
                    result = M_Animation.ANIM_CLIN_D_OEIL;
                    break;
                default:
                    result = M_Animation.NULL;
                    break;
            }

            return result;
        }

        private M_Camera ToCamera(string toParse)
        {
            M_Camera result;

            switch (toParse)
            {
                case "PE_1":
                    result = M_Camera.PE_1;
                    break;
                case "PA_1":
                    result = M_Camera.PA_1;
                    break;
                case "PA_2":
                    result = M_Camera.PA_2;
                    break;
                case "PR_1":
                    result = M_Camera.PR_1;
                    break;
                case "PR_2":
                    result = M_Camera.PR_2;
                    break;
                case "PR_3":
                    result = M_Camera.PR_3;
                    break;
                case "PR_4":
                    result = M_Camera.PR_4;
                    break;
                case "PR_5":
                    result = M_Camera.PR_5;
                    break;
                case "GP_1":
                    result = M_Camera.GP_1;
                    break;
                default:
                    result = M_Camera.PE_1;
                    break;
            }

            return result;
        }

        private M_Question GenerateQuestion(XmlTextReader reader, int id)
        {
            return new M_Question(
                                reader.GetAttribute("id"),
                                id,
                                reader.GetAttribute("actor"),
                                reader.GetAttribute("text"),
                                ToAnimation(reader.GetAttribute("animation")),
                                ToCamera(reader.GetAttribute("camera")),
                                reader.GetAttribute("next"));
        }

        private M_Answer GenerateAnswer(XmlTextReader reader, int id)
        {
            // Temporary solution for qualities list initialization
            // Each point value is 1
            List<M_Quality> qualities = new List<M_Quality>();
            foreach(string qualityName in M_MatriceCQ.Instance.Qualities)
            {
                qualities.Add(new M_Quality(qualityName, 1));
            }

            return new M_Answer(
                                reader.GetAttribute("id"),
                                "qid",
                                id,
                                reader.GetAttribute("actor"),
                                reader.GetAttribute("text"),
                                ToAnimation(reader.GetAttribute("animation")),
                                ToCamera(reader.GetAttribute("camera")),
                                reader.GetAttribute("next"),
                                qualities);
        }

        private M_Phrase GeneratePhrase(XmlTextReader reader, int id)
        {
            return new M_Phrase(
                                reader.GetAttribute("id"),
                                id,
                                reader.GetAttribute("actor"),
                                reader.GetAttribute("text"),
                                ToAnimation(reader.GetAttribute("animation")),
                                ToCamera(reader.GetAttribute("camera")),
                                reader.GetAttribute("next"));
        }


        // Return a sequence 
        public M_Sequence GetSequence(int id)
        {
            M_Sequence result = null;
            List<M_DialogElement> dialogElements = new List<M_DialogElement>();
            bool seqFound = false;
            bool seqOver = false;
            int readerId = 0;

            XmlTextReader reader = new XmlTextReader(dialogPath);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "dialog")
                    {
                        try
                        {
                            readerId = Convert.ToInt32(reader.GetAttribute("id"));

                            if (seqFound)
                            {
                                seqOver = true;
                                result.DialogElements = dialogElements;
                            }

                            if (readerId == id)
                            {
                                result = new M_Sequence(id, reader.GetAttribute("name"), null);
                                seqFound = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }

                    if (seqFound && !seqOver)
                    {
                        switch (reader.Name)
                        {
                            case "question":
                                dialogElements.Add(GenerateQuestion(reader, id));
                                break;
                            case "answer":
                                dialogElements.Add(GenerateAnswer(reader, id));
                                break;
                            case "line":
                                dialogElements.Add(GeneratePhrase(reader, id));
                                break;
                        }
                    }
                }
            }

            return result;
        }

        // Cette fonction est necessaire pcq les ids ne se suivent pas
        public int GetNextSequenceId(int previousId)
        {
            XmlTextReader reader = new XmlTextReader(dialogPath);
            int readerId = 0;
            bool seqFound = false;


            reader.ReadToFollowing("dialog");
            do
            {
                try
                {
                    readerId = Convert.ToInt32(reader.GetAttribute("id"));
                    if (readerId == previousId)
                    {
                        seqFound = true;
                    }
                    if (seqFound && readerId != previousId)
                    {
                        return readerId;
                    }

                }
                catch { }
            } while (reader.ReadToNextSibling("dialog"));

            return 0;
        }



        public M_DialogElement GetElementById(string elementId)
        {
            XmlTextReader reader = new XmlTextReader(dialogPath);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.GetAttribute("id") == elementId)
                    {
                        // TO DO : set sequence id
                        switch (reader.Name)
                        {
                            case "question":
                                return GenerateQuestion(reader, 0);
                            case "answer":
                                return GenerateAnswer(reader, 0);
                            case "line":
                                return GeneratePhrase(reader, 0);
                        }
                    }
                }
            }
            return null;
        }

        // Count number of sequences in dialog.xml
        public int CountSequences()
        {
            int result = 0;
            XmlTextReader reader = new XmlTextReader(dialogPath);
            reader.ReadToFollowing("dialog");
            do { result++; } while (reader.ReadToNextSibling("dialog"));
            reader.Close();
            return result;
        }


        // A ameliorer
        public int GetLastSequenceId()
        {
            int numSequences = CountSequences();
            int count = 0;
            XmlTextReader reader = new XmlTextReader(dialogPath);

            reader.ReadToFollowing("dialog");
            do
            {
                count++;
                if (numSequences == count)
                {
                    try
                    {
                        return Convert.ToInt32(reader.GetAttribute("id"));
                    }
                    catch { }
                }
                
            } while (reader.ReadToNextSibling("dialog"));

            
            return 0;
        }
        

    }
}
