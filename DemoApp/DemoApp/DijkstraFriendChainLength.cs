using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp
{
    class DijkstraFriendChainLength : FriendChainLength
    {
        public override int GetChainLength(int A, int B, NetworkGraph network)
        {
            if (!network.ContainsAccount(A) || !network.ContainsAccount(B))
            {
                throw new ArgumentException("One or both users do not exist in the social network");
            }
            // dictionary to store the shortest distance for each node
            Dictionary<int, int> distances = new Dictionary<int, int>();

            // initialize all distances to infinity except the source node A
            foreach (int node in network.GetAccounts())
            {
                if (node == A)
                {
                    distances[node] = 0;
                }
                else
                {
                    distances[node] = int.MaxValue;
                }
            }

            // set of unvisited nodes
            HashSet<int> unvisited = new HashSet<int>(network.GetAccounts());

            // perform Dijkstra's algorithm
            while (unvisited.Count > 0)
            {
                // divide the unvisited nodes into batches to be processed in parallel
                List<List<int>> batches = new List<List<int>>();
                int batchSize = (int)Math.Ceiling((double)unvisited.Count / Environment.ProcessorCount);
                List<int> currentBatch = new List<int>(batchSize);
                foreach (int node in unvisited)
                {
                    currentBatch.Add(node);
                    if (currentBatch.Count == batchSize)
                    {
                        batches.Add(currentBatch);
                        currentBatch = new List<int>(batchSize);
                    }
                }
                // add the last batch
                if (currentBatch.Count > 0)
                {
                    batches.Add(currentBatch);
                }

                // process each batch in parallel
                Parallel.ForEach(batches, batch =>
                {
                    // find the node with the smallest distance in the batch
                    int current = -1;
                    int minDistance = int.MaxValue;
                    foreach (int node in batch)
                    {
                        if (distances[node] < minDistance)
                        {
                            current = node;
                            minDistance = distances[node];
                        }
                    }

                    // update the distances of its neighbors
                    foreach (int friend in network.GetFriends(current))
                    {
                        int newDistance = distances[current] + 1;
                        if (newDistance < distances[friend])
                        {
                            distances[friend] = newDistance;
                        }
                    }
                });
                // remove the processed nodes from the set of unvisited nodes
                foreach (List<int> batch in batches)
                {
                    foreach (int node in batch)
                    {
                        unvisited.Remove(node);
                    }
                }
            }

            // check if the destination node has been reached
            if (distances[B] == int.MaxValue)
            {
                // if the destination node was not reached, then there is no friend chain between the two users
                return -1;
            }
            else
            {
                return distances[B];
            }
        }
    }
}
