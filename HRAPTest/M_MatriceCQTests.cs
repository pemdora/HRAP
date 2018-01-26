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

        [TestInitialize]
        public void TestInitialize()
        {
            AIengine.datapath = @"..\..\..\Assets\";
        }

        [TestMethod]
        public void GetFinalValues()
        {
        
            // Generate answers
            M_Answer a = (M_Answer)M_DataManager.Instance.GetElementById("5aR1");
            M_Answer b = (M_Answer)M_DataManager.Instance.GetElementById("5aR2");

            // Put answers in answerList
            List<M_Answer> answersList = new List<M_Answer>();
            answersList.Add(a);
  
            //answersList.Add(b);

            // Update answers qualities
            List<M_Answer> updatedList = M_DataManager.Instance.UpdateQualityPoints(answersList);

            // Test private method
            /*double[] qualitiesValues = M_MatriceCQ.Instance.GetFinalQualitiesValues(answersList);
            for(int i = 0; i < M_MatriceCQ.Instance.Qualities.Count; i++)
            {
                Console.WriteLine("  final value: " + qualitiesValues[i]);
            }*/

            // Test final competences values
            double[] finalValues = M_MatriceCQ.Instance.GetFinalCompetencesValues(updatedList);
            for (int i = 0; i < M_MatriceCQ.Instance.Competences.Count; i++)
            {
                Console.WriteLine("  final value: " + finalValues[i]);
            }

            // Test UpdateCompetence for candidate
            M_Candidate c = new M_Candidate("test", "test");
            c.UpdateCompetences(updatedList);

            foreach(M_Competence comp in c.CompetencesList)
            {
                Console.WriteLine(comp.Points);
            }
            

        }
    }
}
