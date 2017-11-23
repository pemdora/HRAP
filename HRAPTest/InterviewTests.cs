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
            V_Question q = interview.GetNextQuestion();

            // 3. On affiche la question, le nb de réponses et les réponses dans la console
            Console.WriteLine(q.Question);
            Console.WriteLine(q.NumAnswers);
            foreach(string s in q.Answers)
            {
                Console.WriteLine(s);
            }

            // 4. On enregistre la reponse du candidat
            interview.SetChosenAnswer("la reponse du candidat");

        }
    }
}
