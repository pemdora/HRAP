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
        [TestMethod]
        public void test2()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // 1. On instancie P_Interview avec le nom du candidat et son poste
            P_Interview interview = new P_Interview("steve", "chef de projet");

            interview.Launch();

        }
    }
}
