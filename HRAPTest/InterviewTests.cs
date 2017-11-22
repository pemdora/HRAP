using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class InterviewTests
    {
        [TestMethod]
        public void test2()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            P_Interview interview = new P_Interview("steve", "chef de projet");

            List<string> q = interview.GetNextQuestion();

            foreach(string s in q)
            {
                Console.WriteLine(s);
            }

            // Pour envoyer la reponse du candidat
            interview.SetChosenAnswer("la reponse du candidat");

        }
    }
}
