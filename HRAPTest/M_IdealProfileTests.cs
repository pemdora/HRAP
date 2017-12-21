using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class M_IdealProfileTests
    {
        [TestMethod]
        public void GetImportantSkills()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            M_IdealProfile ip = new M_IdealProfile("chef de projet", M_Experience.EXPERT);
            Console.WriteLine(ip.Name);

            ip.CompetencesList[1].IsImportant = true;
            ip.CompetencesList[7].IsImportant = true;

            List<M_Competence> list1 = ip.GetImportantCompetences();

            int expected_num_important = 2;
            int actual_num_important = list1.Count;

            Assert.AreEqual(expected_num_important, actual_num_important);
        }

        [TestMethod]
        public void GetNotImportantSkills()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            M_IdealProfile ip = new M_IdealProfile("chef de projet", M_Experience.EXPERT);
            Console.WriteLine(ip.Name);

            ip.CompetencesList[1].IsImportant = true;
            ip.CompetencesList[7].IsImportant = true;

            List<M_Competence> competenceList = ip.GetNotImportantCompetences();

            int expected_num_important = 2;
            int actual_num_important = competenceList.Count;

            Assert.AreEqual(expected_num_important, actual_num_important);
        }
    }
}
