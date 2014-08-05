using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterJugsCSharpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a max capacity for Jug A: ");
            int valueJugA;
            string lineJugA = Console.ReadLine();
            while (!Int32.TryParse(lineJugA, out valueJugA))
            {
                Console.WriteLine("Not a valid number, try again.");
                lineJugA = Console.ReadLine();
            }
            Console.WriteLine("Enter a max capacity for Jug B: ");
            int valueJugB;
            string lineJugB = Console.ReadLine();
            while (!Int32.TryParse(lineJugB, out valueJugB))
            {
                Console.WriteLine("Not a valid number, try again.");
                lineJugB = Console.ReadLine();
            }
            Console.WriteLine("Enter target in gallons: ");
            int valueTarget;
            string lineTarget = Console.ReadLine();
            while (!Int32.TryParse(lineTarget, out valueTarget))
            {
                Console.WriteLine("Not a valid number, try again.");
                lineTarget = Console.ReadLine();
            }

            var constraints = new WaterJugConstraints()
            {
                JugA_Max = valueJugA,
                JugB_Max = valueJugB,
                TargetGallons = valueTarget
            };

            if (constraints.JugA_Max > 0 || constraints.JugB_Max > 0 || constraints.TargetGallons > 0)
            {
                // This is a brute force search - aka the "repeatedly fill one jug" method - includes GCD check
                // other methods (using Production Rules; search using BFS, DFS): http://kartikkukreja.wordpress.com/2013/10/11/water-jug-problem/
                try
                {
                    var gcd = System.Numerics.BigInteger.GreatestCommonDivisor(constraints.JugA_Max, constraints.JugB_Max);
                    var isMultiple = constraints.TargetGallons % gcd == 0;

                    if (isMultiple)
                    {
                        var ds = new BuildWaterJugResultSet(constraints);
                        var stepsA = Task.Run(() => ds.CheckAtoB()).Result;
                        var stepsB = Task.Run(() => ds.CheckBtoA()).Result;

                        Console.WriteLine("(A->B)");
                        for (int i = 0; i < stepsA.Count; i++)
                            Console.WriteLine("Step " + (i + 1) + ": [ " + stepsA[i].JugA + " | " + stepsA[i].JugB + " ]");

                        Console.WriteLine("(B->A)");
                        for (int i = 0; i < stepsB.Count; i++)
                            Console.WriteLine("Step " + (i + 1) + ": [ " + stepsB[i].JugA + " | " + stepsB[i].JugB + " ]");

                        if (stepsA.Count < stepsB.Count)
                            Console.WriteLine("(A->B) is fastest with " + stepsA.Count + " steps total!");
                        else
                            Console.WriteLine("(B->A) is fastest with " + stepsB.Count + " steps total!");
                    }
                    else
                    {
                        Console.WriteLine("ERROR: There is no solution. The greatest common divisor of {0} and {1} is not a multiple of {2}",
                            constraints.JugA_Max, constraints.JugB_Max, constraints.TargetGallons);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("ERROR: Unable to calculate the greatest common divisor." + Environment.NewLine + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("ERROR: All values must be greater than zero.");
            }

            Console.ReadLine();
        }
    }
}
