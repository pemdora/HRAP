using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Phrase : M_DialogElement
    {

        public M_Phrase(string id, string actor, string text, M_Animation animation, M_Camera camera, string next):base(id, actor, text, animation, camera, next)
        {
            this.Display = false;
        }

        
    }
}
