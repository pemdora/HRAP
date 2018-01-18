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
        private static string dialogPath = AIengine.datapath + @"\AIData\dialog.xml";
        private static string matriceCQPath = AIengine.datapath + @"\AIData\matriceCQ.csv"; 
        private static string candidatesPath = AIengine.datapath + @"\AIData\candidates.csv"; 


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

        // **********************  MATRICE CQ  **********************

        public string[][] GetMatrice()
        {
            return File.ReadAllLines(matriceCQPath).Where(line => line != "").Select(x => x.Split(';')).ToArray();
        }




        // **********************  CANDIDATES  **********************

        // Counts number of candidates in candidates.csv

        public int CountCandidates()
        {
            int result = -1;

            StreamReader reader = new StreamReader(candidatesPath);
            string line = reader.ReadLine();

            while (line != null)
            {
                line = reader.ReadLine();
                result += 1;
            }

            reader.Close();

            return result;

        }



        // Add a candidate in candidates.csv
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
            File.AppendAllText(candidatesPath, newLine);

        }



        // **********************  DIALOG  **********************


        // Convert a string to enum Animation
        private M_Animation ToAnimation(string toParse)
        {
            M_Animation result;

            switch (toParse)
            {
                case "Geste d'accueil": result = M_Animation.ANIM_SALUTATIONS; break;
                case "Invitation de la main": result = M_Animation.ANIM_GESTE_MAIN; break;
                case "Sourit": result = M_Animation.ANIM_SOURIRE; break;
                case "Ouverture des bras": result = M_Animation.OUVERTURE_BRAS; break;
                case "Clin d'œil": result = M_Animation.ANIM_CLIN_D_OEIL; break;
                default: result = M_Animation.NULL; break;
            }

            return result;
        }

        // Convert a string to enum Camera
        private M_Camera ToCamera(string toParse)
        {
            M_Camera result;

            switch (toParse)
            {
                case "PE_1": result = M_Camera.PE_1; break;
                case "PA_1": result = M_Camera.PA_1; break;
                case "PA_2": result = M_Camera.PA_2; break;
                case "PR_1": result = M_Camera.PR_1; break;
                case "PR_2": result = M_Camera.PR_2; break;
                case "PR_3": result = M_Camera.PR_3; break;
                case "PR_4": result = M_Camera.PR_4; break;
                case "PR_5": result = M_Camera.PR_5; break;
                case "GP_1": result = M_Camera.GP_1; break;
                default: result = M_Camera.PE_1; break;
            }

            return result;
        }

        // Generate a question from xml
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

        // Generate an answer from xml
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

        // Generate a phrase from xml
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
