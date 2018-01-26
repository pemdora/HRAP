using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace HRAP
{
    public class M_RunPy
    {

        public M_RunPy(string path)
        {
            System.Diagnostics.Process.Start(path);
        }
    }
}
