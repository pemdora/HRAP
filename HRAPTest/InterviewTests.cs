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

            // 1. On instancie P_Interview avec le nom du candidat et son poste
            P_Interview interview = new P_Interview("steve", "chef de projet");

            // 2. On récupère une liste de strings (ordre : question, rep1, rep2 )
            List<string> q = interview.GetNextQuestion();

            // 3. On affiche chaque string dans la console
            foreach(string s in q)
            {
                Console.WriteLine(s);
            }

            // 4. On enregistre la reponse du candidat
            interview.SetChosenAnswer("la reponse du candidat");

        }
    }
}
