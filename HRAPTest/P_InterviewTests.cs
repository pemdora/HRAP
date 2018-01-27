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
                 interview.SetChosenAnswer(1);
                 count++;
             }*/

        }
    }
}