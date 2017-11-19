using System;
using System.IO;
using HRAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

        // ANSWERS

        [TestMethod]
        public void GetAnswers_WithValidQuestionID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            string expected_answer1 = "Oui";
            string expected_answer2 = "Non";


            List<M_Answer> answersList = new List<M_Answer>();
            answersList = M_DataManager.Instance.GetAnswersByQuestionId(11);
            string actual_answer1 = answersList[0].Body;
            string actual_answer2 = answersList[1].Body;

            Assert.AreEqual(expected_answer1, actual_answer1);
            Assert.AreEqual(expected_answer2, actual_answer2);

            //Assert.Fail(answersList[0].Skills[3].Points.ToString());
        }

        [TestMethod]
        public void GetAnswerID_WithValidQuestionID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            int expected_id = 35;
            string s = "Oui";
            int actual_answerID = M_DataManager.Instance.GetAnswerID(11,s);

            Assert.AreEqual(expected_id, actual_answerID);
        }

        [TestMethod]
        public void GetAnswer_WithValidID()
        {
            Directory.SetCurrentDirectory(@"..\..\..");

            string expected_answer = "Oui";

            M_Answer actual_answer = M_DataManager.Instance.GetAnswer(35);
            Assert.AreEqual(expected_answer, actual_answer.Body);
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
