using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_MatriceCQ
    {
        private static M_MatriceCQ instance;

        private List<string> competences;
        private List<string> qualities;
        private int[][] ponderations;

        private M_MatriceCQ()
        {

            string[][] mat = M_DataManager.Instance.GetMatrice();
            this.competences = new List<string>();
            this.qualities = new List<string>();
            this.ponderations = new int[mat.Length][];

            // initialize array
            for (int i = 0; i < mat.Length; i++)
            {
                this.ponderations[i] = new int[mat[0].Length];
            }

            // classify data
            for (int i = 0; i < mat.Length; i++)
            {
                for (int j = 0; j < mat[0].Length; j++)
                {
                    if (i == 1 && j > 1) this.competences.Add(mat[i][j]);

                    if (i > 1 && j == 0) this.qualities.Add(mat[i][j]);

                    if (i > 1 && j > 1)
                    {
                        if (mat[i][j] != "") this.ponderations[i - 2][j - 2] = Convert.ToInt32(mat[i][j]);
                        else this.ponderations[i - 2][j - 2] = 0;
                    }
                }
            }
        }

        public static M_MatriceCQ Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new M_MatriceCQ();
                }
                return instance;
            }
        }

        public List<string> Competences { get { return competences; } }
        public List<string> Qualities { get { return qualities; } }


        // Fait la moyenne des points des qualités des réponses
        private double[] GetFinalQualitiesValues(List<M_Answer> answers)
        {
            int numQualities = this.qualities.Count;

            int[] counters = new int[numQualities];
            double[] finalValues = new double[numQualities];

            // On récupère les points des qualités de chaque réponses
            foreach (M_Answer answer in answers)
            {
                for (int i = 0; i < numQualities; i++)
                {
                    if (answer.QualitiesList[i].Points != 0)
                    {
                        counters[i]++;
                        finalValues[i] += answer.QualitiesList[i].Points;
                    }
                }
            }
            

            // On fait la moyenne des points
            for (int i = 0; i < numQualities; i++)
            {
                if (counters[i] != 0)
                {
                    finalValues[i] = finalValues[i] / counters[i];
                }
                else
                {
                    // La qualité n'a pas été évaluée
                    finalValues[i] = Double.NaN;
                }
            }

            return finalValues;
        }

        // Utilise la matrice CQ pour convertir les qualités en compétences
        public double[] GetFinalCompetencesValues(List<M_Answer> answers)
        {
            int numCompetences = this.competences.Count;
            double[] finalValues = new double[numCompetences];

            int numQualities = this.qualities.Count;
            double[] qualitiesValues = GetFinalQualitiesValues(answers);
            double sum;
            double count;

            for (int j = 0; j < numCompetences; j++)
            {
                sum = 0;
                count = 0;
                for (int i = 0; i < numQualities; i++)
                {
                    if (!Double.IsNaN(qualitiesValues[i]) && ponderations[i][j]!=0)
                    {
                        sum += qualitiesValues[i] * ponderations[i][j];
                        count++;
                    }
                }
                if (count == 0) count = 1;
                finalValues[j] = sum / count;

            }
            return finalValues;
        }

    }
}
