using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace HRAPTest
{
    [TestClass]
    public class M_DataManagerTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
            AIengine.datapath= @"..\..\..\Assets\";
        }

        // CANDIDATES


        [TestMethod]
        public void AddCandidate()
        {
            M_Candidate candidate = new M_Candidate("Steve", "Chef de Projet");
            M_DataManager.Instance.AddCandidate(candidate);
        }

        // MATRICE CQ

        [TestMethod]
        public void GetMatriceCQ()
        {
            // Check the number of qualities (35)
            string[][] mat = M_DataManager.Instance.GetMatrice();
            int expected_length = 35;
            int current_length = mat.Length;
            Assert.AreEqual(expected_length, current_length);
        }

        // ANSWER QUALITY POINTS

        [TestMethod]
        public void UpdateAnswerPoints()
        {
            // Créé une liste de réponses
            M_Answer a = (M_Answer)M_DataManager.Instance.GetElementById("5aR1");
            M_Answer b = (M_Answer)M_DataManager.Instance.GetElementById("5aR2");

            List<M_Answer> answersList = new List<M_Answer>();
            answersList.Add(a);
            answersList.Add(b);

            // Récupère la liste de réponses mise à jour
            List<M_Answer> list = M_DataManager.Instance.UpdateQualityPoints(answersList);
            
            // Valeurs de la réponse "a" récupérées dans le csv
            int[] expectedValues = { 0, 0, 3, 3, 3, 3, 0, 0, -3, 0, 0, 0, 0, -3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 0, 0, 0, 0 };
            
            int i = 0;

            // teste si les points sont équivalents à ceux récupérés dans la fonction UpdateQualityPoints()
            foreach(M_Quality q in list[0].QualitiesList)
            {
                Assert.AreEqual(expectedValues[i], q.Points);
                i++;
            }

        }


        // SEQUENCES

        [TestMethod]
        public void GetSequence()
        {
            int countSequences = M_DataManager.Instance.CountSequences();
            M_Sequence seq = M_DataManager.Instance.GetSequence(5);

            // Vérifie que l'on récupère 6 éléments dans la dernière sequence
            int expected = 7;
            int current = seq.DialogElements.Count;
            Assert.AreEqual(expected, current);

        }



        [TestMethod]
        public void GetDialogElement_WithValidID()
        {
            // Vérifie que l'élément récupéré est bien une réponse
            M_DialogElement element = M_DataManager.Instance.GetElementById("2aR2");
            Assert.ReferenceEquals(element, typeof(M_Answer));

        }

        [TestMethod]
        public void GetNextSequenceId_WithValidID()
        {
            // Vérifie que l'on récupère le bon id de la sequence 3
            int expected_id = 5;
            int current_id = M_DataManager.Instance.GetNextSequenceId(3);
            Assert.AreEqual(expected_id, current_id);

        }

        [TestMethod]
        public void GetLastSequenceId()
        {
            // Vérifie que l'on récupère le bon id de la dernière sequence
            int expected_id = 26;
            int current_id = M_DataManager.Instance.GetLastSequenceId();
            Assert.AreEqual(expected_id, current_id);

        }

        [TestMethod]
        public void CountSequences()
        {
            // Vérifie que l'on récupère le bon nombre de séquences
            int expected = 15;
            int current = M_DataManager.Instance.CountSequences();
            Assert.AreEqual(expected, current);
            
        }


    }
}
