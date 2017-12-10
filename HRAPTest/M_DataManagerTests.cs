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

        // SEQUENCES

        [TestMethod]
        public void testXML()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            //M_DataManager.Instance.ReadXml();

            M_Sequence seq = M_DataManager.Instance.GetSequence(2);
            if (seq != null)
            {
                Console.WriteLine(seq.ToString());
            }

            M_DialogElement element = M_DataManager.Instance.GetElementById("2a");
            Console.WriteLine(element.ToString());



        }

       

        // CANDIDATES

        [TestMethod]
        public void GetCandidate_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            string expected_name = "yasmeen";
            M_Candidate actual_candidate = M_DataManager.Instance.GetCandidate(2);
            Assert.AreEqual(expected_name, actual_candidate.Name);
             
        }

        // IDEAL PROFILES

            // TO DO
        [TestMethod]
        public void GetIdealProfile_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            //string expected_name = "chef de projet";
            //M_IdealProfile actual_idealprofile = M_DataManager.Instance.GetIdealProfile(1);
            //Assert.AreEqual(expected_name, actual_idealprofile.Name);

        }

    }
}
