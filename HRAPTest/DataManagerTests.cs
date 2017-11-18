using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HRAPTest
{
    [TestClass]
    public class DataManagerTests
    {
        [TestMethod]
        public void CountQuestions()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            int expected = 14;
            int numQuestions = M_DataManager.Instance.CountQuestions();
            Assert.AreEqual(expected, numQuestions);
            
        }

        [TestMethod]
        public void GetQuestion_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Get a question in DB

            int id = 9;
            string body = "Comment je definis mes objectifs :";
            int next = 0;
            int numAnswers = 4;

            M_Question expected = new M_Question(id,body, next,numAnswers);
            M_Question actual = M_DataManager.Instance.GetQuestionById(id);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Body, actual.Body);

        }

        [TestMethod]
        public void GetQuestionID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            // Get a question in DB
            M_Question expected = M_DataManager.Instance.GetQuestionById(9);

            int actual_id = M_DataManager.Instance.GetQuestionID(expected.Body);

            Assert.AreEqual(expected.Id, actual_id);

        }
    }
}
