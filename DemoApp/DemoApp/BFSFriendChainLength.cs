using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp
{
    class BFSFriendChainLength : FriendChainLength
    { 
        public override int GetChainLength(int A, int B, NetworkGraph network)
        {
            if (!network.ContainsAccount(A) || !network.ContainsAccount(B))
            {
                throw new ArgumentException("One or both users do not exist in the social network");
            }

            // BFS queue and add source node to it
            Queue<int> queue = new Queue<int>(50000);
            queue.Enqueue(A);

            // dictionary to store the visited status and shortest distance for each node
            var visited = new ConcurrentDictionary<int, int>();
            visited[A] = 0;

            // list to store tasks
            List<Task> tasks = new List<Task>();

            // perform BFS traversal until the destination node is found or the queue is empty
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                // check if the destination node has been reached
                if (current == B)
                {
                    // wait for all tasks to complete before returning the result
                    Task.WaitAll(tasks.ToArray());
                    // return distance 
                    return visited[current];
                }
                // add unvisited neighbor nodes to the queue and update their visited status and distance
                foreach (int friend in network.GetFriends(current))
                {
                    if (!visited.ContainsKey(friend))
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            //add unvisited node to the ConcurentDictionary and than adds one node to the distance
                            visited[friend] = visited[current] + 1;
                            queue.Enqueue(friend);
                        }));
                    }
                }
            }
            // wait for all tasks to complete before returning -1
            Task.WaitAll(tasks.ToArray());
            // if the destination node was not reached, then there is no friend chain between the two users
            return -1;
        }
    }
}
