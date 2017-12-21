using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class M_SequenceTests
    {
        [TestMethod]
        public void GetFirstSequence()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie qu'il y a bien 2 éléments dans la séquence récupérée
            M_Sequence seq = new M_Sequence();
            int expected_num_element = 2;
            int actual_num_element = seq.DialogElements.Count;
            Assert.AreEqual(expected_num_element, actual_num_element);
        }

        [TestMethod]
        public void GetNextSequence()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Vérifie qu'il y a bien 7 éléments dans la séquence récupérée
            M_Sequence seq = new M_Sequence();
            seq = seq.GetNextSequence();
            int expected_num_element = 7;
            int actual_num_element = seq.DialogElements.Count;
            Assert.AreEqual(expected_num_element, actual_num_element);

        }
    }
}
