using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using KAPITypes;
using Kompas6Constants;
using Kompas6Constants3D;
using Kompas6API5;
using KompasAPI7;

namespace KompasWrapper
{
    public class Class1
    {
        public void Main()
        {
            KompasObject kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
            ksDocument2D document2d = (ksDocument2D)kompas.ActiveDocument2D();
            document2d.ksLineSeg(0, 0, 30, 30, 1);
        }
    }
}
