using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRAP_TEST_GRAPHIQUE
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            QCM1 qcm= new QCM1();
            
            Application.Run(qcm);
            
           /// Application.Run(new Form1());
            

          
        }
    }
}
