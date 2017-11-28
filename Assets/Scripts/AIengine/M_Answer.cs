using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Answer
    {
        private int answerId;
        private int questionId;
        private string body;
        private int nextQuestionId;
        private List<M_Quality> qualityList;

        public M_Answer(int id, int questionId, string body, int nextQuestionId, List<M_Quality> qualityList)
        {
            this.answerId = id;
            this.questionId = questionId;
            this.body = body;
            this.nextQuestionId = nextQuestionId;
            this.qualityList = qualityList;
        }

        public int Id { get { return answerId; } }
        public int QuestionId { get { return questionId; } }
        public string Body { get { return body; } }
        public int NextQuestionId { get { return nextQuestionId; } }
        public List<M_Quality> QualityList { get { return qualityList; } set { qualityList = value; } }
    }
}
