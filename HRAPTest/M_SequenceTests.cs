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
        public void test1()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            M_Sequence seq = new M_Sequence();
            seq = seq.GetNextSequence();

            foreach(M_DialogElement e in seq.DialogElements)
            {
                Console.WriteLine(e.Text);
            }

            Console.WriteLine(seq.DialogElements.Count);

        }
    }
}
