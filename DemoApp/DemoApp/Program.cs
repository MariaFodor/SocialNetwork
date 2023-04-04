using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    class Program
    {
        /// <summary>
        /// In the main it's made one simulation for the maximum acceptable network dimension with A and B randomly benerated
        /// Than a UI menu is initialized to support further measurments
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            NetworkGraphFactory factory = new NetworkGraphFactoryPowerLaw(27000000, Environment.ProcessorCount, 2.3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Status: Generating 27 * 10^6 members network...");
            Console.WriteLine("Status: Timing generation...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NetworkGraph network = factory.CreateNetworkGraph();

            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Timing results: " + stopwatch.Elapsed);

            Random random = new Random();
            var p1 = random.Next(1, 27000000 + 1);
            var p2 = random.Next(1, 27000000 + 1);
            FriendChainLength bFSFriendChain = new BFSFriendChainLength();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nStatus: Mesuring time preformance of BFS algortihm...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time performance result : " + NetworkTimer.TimeIt(p1, p2, network, bFSFriendChain.GetChainLength));

            FriendChainLength dijkstraFriendChain = new DijkstraFriendChainLength();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nStatus: Mesuring time preformance of Dijkstra algortihm...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time performance result : " + NetworkTimer.TimeIt(p1, p2, network, dijkstraFriendChain.GetChainLength));

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nStatus:Initializing UI console menu...");
            bool exit = false;
            int option;
            while(!exit)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Menu options: \n\t\t\t 1-Mesaure BFS performance," +
                    " \n\t\t\t 2-Measure Dijkstra performance" +
                    "\n\t\t\t 3-Exit");
                Console.Write("->option: ");
                option = int.Parse(Console.ReadLine());
                Console.WriteLine("You entered: " + option);
                switch (option)
                {
                    case 1:
                        NetworkTimer.MeasureTimePerformance(bFSFriendChain.GetChainLength);
                        break;
                    case 2:
                        NetworkTimer.MeasureTimePerformance(dijkstraFriendChain.GetChainLength);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
