using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class M_MatriceCQTests
    {
        // TO CLEAN

        [TestMethod]
        public void GetFinalValues()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Generate answers
            M_Answer a = (M_Answer)M_DataManager.Instance.GetElementById("_1");
            M_Answer b = (M_Answer)M_DataManager.Instance.GetElementById("_2");

            // Init qualities points : 0 or 1
            for(int i = 0; i < M_MatriceCQ.Instance.Qualities.Count; i++)
            {
                if (i % 2 == 0)
                {
                    a.QualitiesList[i].Points = 0;
                    b.QualitiesList[i].Points = 0;
                }
                else
                {
                    a.QualitiesList[i].Points = 1;
                    b.QualitiesList[i].Points = 1;
                }
            }

            // Put answers in answerList
            List<M_Answer> answersList = new List<M_Answer>();
            answersList.Add(a);
            answersList.Add(b);

            // Test private method
            /*double[] qualitiesValues = M_MatriceCQ.Instance.GetFinalQualitiesValues(answersList);
            for(int i = 0; i < M_MatriceCQ.Instance.Qualities.Count; i++)
            {
                Console.WriteLine("  final value: " + qualitiesValues[i]);
            }*/

            // Test final competences values
            double[] finalValues = M_MatriceCQ.Instance.GetFinalCompetencesValues(answersList);
            for (int i = 0; i < M_MatriceCQ.Instance.Competences.Count; i++)
            {
                Console.WriteLine("  final value: " + finalValues[i]);
            }

            // Test UpdateCompetence for candidate
            M_Candidate c = new M_Candidate("test", "test");
            c.UpdateCompetences(answersList);

            foreach(M_Competence comp in c.CompetencesList)
            {
                Console.WriteLine(comp.Points);
            }
            

        }
    }
}
