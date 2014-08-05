using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterJugsCSharpConsoleApp
{
    public class WaterJugs
    {
        public int JugA { get; set; }
        public int JugB { get; set; }
    }

    public class WaterJugConstraints
    {
        public int JugA_Max { get; set; }
        public int JugB_Max { get; set; }
        public int TargetGallons { get; set; }
    }
}
