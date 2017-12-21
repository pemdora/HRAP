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

        // CANDIDATES

        [TestMethod]
        public void GetCandidate_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            string expected_name = "yasmeen";
            M_Candidate actual_candidate = M_DataManager.Instance.GetCandidate(2);
            Assert.AreEqual(expected_name, actual_candidate.Name);

        }

        // MATRICE CQ

        [TestMethod]
        public void GetMatriceCQ()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Check the number of qualities (35)
            string[][] mat = M_DataManager.Instance.GetMatrice();
            int expected_length = 35;
            int current_length = mat.Length;
            Assert.AreEqual(expected_length, current_length);
        }


        // SEQUENCES

        [TestMethod]
        public void GetSequence()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            int countSequences = M_DataManager.Instance.CountSequences();
            M_Sequence seq = M_DataManager.Instance.GetSequence(countSequences);

            // Vérifie que l'on récupère 6 éléments dans la dernière sequence
            int expected = 6;
            int current = seq.DialogElements.Count;
            Assert.AreEqual(expected, current);

        }



        [TestMethod]
        public void GetDialogElement_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie que l'élément récupéré est bien une réponse
            M_DialogElement element = M_DataManager.Instance.GetElementById("_1");
            Assert.ReferenceEquals(element, typeof(M_Answer));

        }

        [TestMethod]
        public void GetNextSequenceId_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie que l'on récupère le bon id de la sequence 3
            int expected_id = 5;
            int current_id = M_DataManager.Instance.GetNextSequenceId(3);
            Assert.AreEqual(expected_id, current_id);

        }

        [TestMethod]
        public void GetLastSequenceId()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie que l'on récupère le bon id de la dernière sequence
            int expected_id = 25;
            int current_id = M_DataManager.Instance.GetLastSequenceId();
            Assert.AreEqual(expected_id, current_id);

        }

        [TestMethod]
        public void CountSequences()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie que l'on récupère le bon nombre de séquences
            int expected = 14;
            int current = M_DataManager.Instance.CountSequences();
            Assert.AreEqual(expected, current);

        }


    }
}
