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
        public void test3()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            M_IdealProfile ip = new M_IdealProfile("chef de projet", M_Experience.EXPERT);
            Console.WriteLine(ip.Name);

            ip.Skills[1].IsImportant = true;
            ip.Skills[7].IsImportant = true;

            List<M_Competences> list1 = ip.GetImportantSkills();
            List<M_Competences> list2 = ip.GetNotImportantSkills();

            foreach(M_Competences s in list1)
            {
                Console.WriteLine("list 1 : " + s.Name);
            }

            foreach (M_Competences s in list2)
            {
                Console.WriteLine("list 2 : " + s.Name);
            }
        }
    }
}
