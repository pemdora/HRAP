using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_Sequence
    {
        private int seqId;
        private string name;
        private List<M_DialogElement> dialogElements;

        public M_Sequence(int seqId, string name, List<M_DialogElement> dialogElements)
        {
            this.seqId = seqId;
            this.name = name;
            this.dialogElements = dialogElements;
        }

        public int SeqId { get { return seqId; } }
        public string Name { get { return name; } }
        public List<M_DialogElement> DialogElements { get { return dialogElements; } set { dialogElements = value; } }

        public M_Sequence()
        {
            M_Sequence sequence = M_DataManager.Instance.GetSequence(1);
            this.seqId = sequence.seqId;
            this.name = sequence.name;
            this.dialogElements = sequence.dialogElements;
        }

        public M_Sequence GetNextSequence()
        {
            int nextSequenceId = M_DataManager.Instance.GetNextSequenceId(seqId);
            if (nextSequenceId < M_DataManager.Instance.GetLastSequenceId())
            {
                return M_DataManager.Instance.GetSequence(nextSequenceId);
            }
            return null;            
        }


        public override string ToString()
        {
            string result = "";
            result += seqId;
            result += "   ";
            result += name;
            foreach (M_DialogElement e in dialogElements)
            {
                result += "\n";
                result += e.ToString();
            }
            return result;

        }

    }
}
