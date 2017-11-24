using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class M_QuizzTests
    {
        [TestMethod]
        public void test1()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            M_Quizz quizz = new M_Quizz();
            
            foreach(M_Question question in quizz.Questions)
            {
                Console.WriteLine(question.Body);
            }

        }
    }
}
