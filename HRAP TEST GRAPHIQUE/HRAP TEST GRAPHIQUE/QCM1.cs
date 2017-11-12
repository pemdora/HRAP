using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRAP_TEST_GRAPHIQUE
{
    partial class QCM1 : Form
    {
        int ScoreMotivation;
        int ScoreLeadership;
        int ScoreControleEmmotionnel;
        int ScoreSociabilite;

        
        Dictionary<int, int> BilanQuestionReponse = new Dictionary<int, int>();

        int NUMquestion = 0;
        
        Candidate candidat = new Candidate(DataManager.getIDProfile(), "Chef", "candidata", DataManager.InitialisationSkill());

        public QCM1()
        {
            InitializeComponent();

            labelProductName.Text = DataManager.getQuestionById(1).String;
            List<Answer> answers = DataManager.getAnswersByQuestionId(1);
            checkBox1.Text = answers[0].String;
            checkBox2.Text = answers[1].String;
            checkBox3.Text = answers[2].String;
            NUMquestion += 1;



        }


        public void LancementQCM(int IDQuestion,int rep)
        {
            
            Answer reponse = PoseQuestion(IDQuestion, rep);
            NUMquestion += 1;

            if (reponse != null)
            {
                Evaluation(reponse, candidat);
                BilanQuestionReponse.Add(IDQuestion, reponse.Id);

            }
            else
            {
                BilanQuestionReponse.Add(IDQuestion, -1);
            }

            if (NUMquestion==10)
            {
                Form1 frm = new Form1();
                frm.test(candidat.Skills);
                frm.Show();
            }
            

        }

        public Answer PoseQuestion(int IDQuestion,int rep)
        {

            labelProductName.Text = DataManager.getQuestionById(NUMquestion).String;
            List<Answer> answers = DataManager.getAnswersByQuestionId(NUMquestion);
            if (answers.LongCount() == 2)
            {
                checkBox1.Text = answers[0].String;
                checkBox2.Text = answers[1].String;
                checkBox3.Text = "Je ne sais pas";

            }
            else
            {
                checkBox1.Text = answers[0].String;
                checkBox2.Text = answers[1].String;
                checkBox3.Text = answers[2].String;

            }
            

            try
            {
                return answers[rep];
            }

            catch
            {
                return null;
            }


        }

        public void Evaluation(Answer answer, Candidate candidat)
        {
            candidat.UpdateCandidate(answer);

            for (int i = 0; i < 4; i++)
            {
                ScoreMotivation += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 4; i < 9; i++)
            {
                ScoreControleEmmotionnel += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 9; i < 16; i++)
            {
                ScoreLeadership += candidat.Skills.ElementAt(i).Value;
            }
            for (int i = 16; i < 24; i++)
            {
                ScoreSociabilite += candidat.Skills.ElementAt(i).Value;
            }



        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int rep = 0;
            LancementQCM(NUMquestion,rep);

            checkBox1.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            int rep = 1;
            LancementQCM(NUMquestion,rep);
            
            checkBox2.Checked = false;


        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            int rep = 2;
            LancementQCM(NUMquestion,rep);
            checkBox3.Checked = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataManager.setCandidatIntoFile(candidat);

            Form1 frm = new Form1();
            frm.test(candidat.Skills);
            frm.Show();

        }
    }
}
