using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public abstract class M_DialogElement
    {
        private string id;
        private int seqID;
        private string actor;
        private string text;
        private M_Animation animation;
        private M_Camera camera;
        private string next;
        private bool display;

        public M_DialogElement(string id, int seqID, string actor, string text, M_Animation animation, M_Camera camera, string next)
        {
            this.id = id;
            this.seqID = seqID;
            this.actor = actor;
            this.text = text;
            this.animation = animation;
            this.camera = camera;
            this.next = next;
            this.display = true;
        }

        public string Id { get { return id; } }
        public int SeqID { get { return seqID; } }
        public string Actor { get { return actor; } }
        public string Text { get { return text; } }
        public M_Animation Animation { get { return animation; } }
        public M_Camera Camera { get { return camera; } }
        public string Next { get { return next; } }
        public bool Display { get { return display; } set { display = value; } }

        public override string ToString()
        {
            string result = "";
            result += id;
            result += "   ";
            result += text;

            return result;

        }
    }
}
