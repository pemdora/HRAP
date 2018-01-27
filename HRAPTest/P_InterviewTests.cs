using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class P_InterviewTests
    {


        [TestInitialize]
        public void TestInitialize()
        {
            AIengine.datapath = @"..\..\..\Assets\";
        }

        [TestMethod]
        public void LaunchInterview()
        {
            // A TESTER SANS IHM

           /*  M_Candidate candidate = new M_Candidate("steve", "chef de projet");
             P_Interview interview = new P_Interview(candidate);
             int count = 0;
             while (!interview.IsOver)
             {
                 interview.Launch();
                 interview.SetChosenAnswer(0);
                 count++;
             }

             List<V_Competence> r = interview.GetResult1();
              Console.WriteLine(r.ToString());
              foreach (M_Competence c in candidate.CompetencesList)
              {
                  Console.WriteLine(c.Points);
              }

            List<M_Answer> p = new List<M_Answer>();
                p=M_DataManager.Instance.UpdateQualityPoints(interview.candidateAnswers);
            foreach (M_Answer a in interview.candidateAnswers)
            {
                //Console.WriteLine(a.Id);
                foreach (M_Quality q in a.QualitiesList)
                    Console.WriteLine(q);

                Console.WriteLine("***************************");
            }*/
        }
    }
}