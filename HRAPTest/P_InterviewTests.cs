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
        [TestMethod]
        public void test2()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // 1. On instancie P_Interview avec le nom du candidat et son poste
           /* M_Candidate candidate = new M_Candidate("steve", "chef de projet");
            P_Interview interview = new P_Interview(candidate);
            
            //V_Question q = interview.GetNextQuestion();
            Console.WriteLine("go");
            int count = 0;
            while (!interview.IsOver)
            {
                interview.Launch();
                interview.SetChosenAnswer(1);
                count++;
            }*/
        }
    }
}
