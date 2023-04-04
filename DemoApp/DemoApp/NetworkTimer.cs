using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class NetworkTimer
    {
        /// <summary>
        /// Wrapper function for timing execution
        /// </summary>
        /// <param name="args">Parameters passed to the called function</param>
        /// <param name="fun">Function that has to be timed</param>
        /// <returns></returns>
        public static TimeSpan TimeIt(int p1, int p2, NetworkGraph network, Func<int, int, NetworkGraph, int> fun)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            fun(p1, p2, network);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberNodes"></param>
        /// <param name="fun"></param>
        /// <param name="numberOfMeasurments"></param>
        /// <returns></returns>
        public static TimeSpan MeasureTimePerformance(Func<int, int, NetworkGraph, int> fun)
        {
            int p1 = 0;
            int p2 = 0;
            int nodesNumber = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.Write("->Enter the dimmenssion of the test-network (less than 27 * 10 ^ 6 for accceptable generation time: ");
                validInput = int.TryParse(Console.ReadLine(), out nodesNumber) && nodesNumber < 27000001 && nodesNumber > 1;
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter positive integer less or equal 27000000.");
                }
            }
            validInput = false;
            while (!validInput)
            {
                Console.Write("->Enter the first account id:");
                validInput = int.TryParse(Console.ReadLine(), out p1) && p1 < nodesNumber && p1 > 1;
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter positive integer less than the network dimenssion.");
                }
            }
            validInput = false;
            while (!validInput)
            {
                Console.Write("->Enter the second account id:");
                validInput = int.TryParse(Console.ReadLine(), out p2) && p2 < nodesNumber && p2 > 1;
                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter positive integer less the network dimenssion.");
                }
            }
            NetworkGraphFactory factory = new NetworkGraphFactoryPowerLaw(nodesNumber, 4, 2.3);
            //code for mesuring graph generation
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nStatus: Generating {0} members network...",nodesNumber);
            Console.WriteLine("Status: Timing generation...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Dictionary<int, HashSet<int>> dict = GeneratePowerLawNetworkGraph(nodesNumber, 4, 2.3);
            NetworkGraph network = factory.CreateNetworkGraph();
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Timing results: " + stopwatch.Elapsed);

            //code for measuring algorithm performance
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nStatus: Mesuring time preformance of algortihm...");
            TimeSpan time = TimeIt(p1, p2, network, fun);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time performance result : " + time);
            return time;
        }
    }
}
