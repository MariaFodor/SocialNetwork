using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class NetworkGraphFactoryPowerLaw : NetworkGraphFactory
    {
        private int numAccounts;
        private int numThreads;
        private double alpha;

        public NetworkGraphFactoryPowerLaw(int numAccounts, int numThreads, double alpha)
        {
            this.numAccounts = numAccounts;
            this.numThreads = numThreads;
            this.alpha = alpha;
        }
        /// <summary>
        /// Method that generates a social-network like graph
        /// in which the number of friends follows a power law distribution
        /// </summary>
        public override NetworkGraph CreateNetworkGraph()
        {
            var friendsDict = new Dictionary<int, HashSet<int>>();
            var partSize = (int)Math.Ceiling((double)numAccounts / numThreads);

            Parallel.For(0, numThreads, i =>
            {
                var startIndex = i * partSize + 1;
                var endIndex = Math.Min((i + 1) * partSize, numAccounts);
                var random = new Random();

                for (int j = startIndex; j <= endIndex; j++)
                {
                    var friendCount = Math.Round(Math.Pow(random.NextDouble(), -1.0 / (alpha - 1.0)));
                    friendCount = Math.Min(friendCount, numAccounts);
                    var friends = new HashSet<int>();

                    while (friends.Count < (friendCount))
                    {
                        var friend = random.Next(1, numAccounts + 1);
                        if (friend != j && !friends.Contains(friend))
                        {
                            friends.Add(friend);
                        }
                    }

                    lock (friendsDict)
                    {
                        friendsDict.Add(j, friends);
                    }
                }
            });
            return new NetworkGraph(friendsDict);
        }
    }
}
