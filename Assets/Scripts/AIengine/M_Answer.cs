using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Answer:M_DialogElement
    {
        private List<M_Quality> qualitiesList;

        public M_Answer(string id, string actor, string text, M_Animation animation, M_Camera camera, string next, List<M_Quality> qualitiesList) :base(id, actor, text, animation, camera, next)
        {
            this.qualitiesList = qualitiesList;
        }
        
        public List<M_Quality> QualitiesList { get { return qualitiesList; } set { qualitiesList = value; } }
    }
}
