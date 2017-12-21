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


        // TO DO : Convertir les points des valeurs des réponses en compétences pour le candidat
        public List<M_Competence> ConvertToCompetences(List<M_Quality> qualityList)
        {
            return null;
        }



    }
}
