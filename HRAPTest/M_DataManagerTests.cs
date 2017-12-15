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

        // MATRICE CQ

        [TestMethod]
        public void testMatriceCQ()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            string[][] mat = M_DataManager.Instance.GetMatrice();

              for (int i=0; i < mat.Length; i++)
              {

                      Console.WriteLine(mat[i][0]);

              }

        }


        // SEQUENCES

        [TestMethod]
        public void testXML()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            int countSequences = M_DataManager.Instance.CountSequences();
            M_Sequence seq = M_DataManager.Instance.GetSequence(countSequences);
            if (seq != null)
            {
                Console.WriteLine(seq.ToString());
            }
            
            M_DialogElement element = M_DataManager.Instance.GetElementById("2a");
            Console.WriteLine(element.GetType());
            Console.WriteLine(countSequences);

            // must display 5
            Console.WriteLine("Next sq of 3: "+M_DataManager.Instance.GetNextSequenceId(3));

            // must display 25
            Console.WriteLine("Last seq id : " + M_DataManager.Instance.GetLastSequenceId());


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
