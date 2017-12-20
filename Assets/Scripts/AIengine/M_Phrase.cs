using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Phrase : M_DialogElement
    {
        bool display;

        public M_Phrase(string id, int seqID, string actor, string text, M_Animation animation, M_Camera camera, string next):base(id, seqID, actor, text, animation, camera, next)
        {
            this.display = false;
        }

        public bool Display { get { return display; } set { display = value; } }
    }
}
