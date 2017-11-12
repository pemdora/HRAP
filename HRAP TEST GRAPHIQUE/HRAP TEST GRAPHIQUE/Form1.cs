using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRAP_TEST_GRAPHIQUE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public void test(Dictionary<string,int> data)
        {
            List<string> XValue =new List<string>() ;
            List<int> YValue = new List<int>();
            for (int i = 0; i < data.Count(); i++)
            {
                XValue.Add(data.ElementAt(i).Key);
                YValue.Add(data.ElementAt(i).Value);
            }
            
            chart1.Series["Competence"].Points.DataBindXY(XValue, YValue);
        }
      

        
    }
}
