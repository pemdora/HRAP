using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HRAPTest
{
    [TestClass]
    public class M_RunPyTests
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

       [TestMethod]
        public void GetFinalValues()
        {
            M_RunPy p = new M_RunPy(@"..\..\..\PythonAssessment\build\AIAssessment win\job_assessment.exe", " ", "");
        }

    }
}
