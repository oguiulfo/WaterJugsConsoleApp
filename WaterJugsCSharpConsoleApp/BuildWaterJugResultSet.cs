using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterJugsCSharpConsoleApp
{
    public class BuildWaterJugResultSet
    {
        int gallons { get; set; }
        int a_max { get; set; }
        int b_max { get; set; }
        int a { get; set; }
        int b { get; set; }

        public BuildWaterJugResultSet(WaterJugConstraints constraints)
        {
            a_max = constraints.JugA_Max;
            b_max = constraints.JugB_Max;
            gallons = constraints.TargetGallons;
        }

        public List<WaterJugs> CheckAtoB()
        {
            a = b = 0;
            var resultSet = new List<WaterJugs>() { new WaterJugs() };

            var continueLoop = true;
            while (continueLoop)
            {
                if (a == 0)
                    FillA();
                else if (a > 0 && b != b_max)
                    TransferAtoB();
                else if (a > 0 && b == b_max)
                    EmptyB();

                resultSet.Add(new WaterJugs() { JugA = a, JugB = b });

                if (a == gallons || b == gallons)
                    continueLoop = false;
            }

            return resultSet;
        }

        public List<WaterJugs> CheckBtoA()
        {
            a = b = 0;
            var resultSet = new List<WaterJugs>() { new WaterJugs() };

            var continueLoop = true;
            while (continueLoop)
            {
                if (b == 0)
                    FillB();
                else if (b > 0 && a != a_max)
                    TransferBtoA();
                else if (b > 0 && a == a_max)
                    EmptyA();

                resultSet.Add(new WaterJugs() { JugA = a, JugB = b });

                if (a == gallons || b == gallons)
                    continueLoop = false;
            }

            return resultSet;
        }

        void FillA()
        {
            a = a_max;
        }

        void FillB()
        {
            b = b_max;
        }

        void TransferAtoB()
        {
            var continueLoop = true;
            while (continueLoop)
            {
                b += 1;
                a -= 1;
                if (b == b_max || a == 0)
                    continueLoop = false;
            }
        }

        void TransferBtoA()
        {
            var continueLoop = true;
            while (continueLoop)
            {
                a += 1;
                b -= 1;
                if (a == a_max || b == 0)
                    continueLoop = false;
            }
        }

        void EmptyA()
        {
            a = 0;
        }

        void EmptyB()
        {
            b = 0;
        }
    }
}
