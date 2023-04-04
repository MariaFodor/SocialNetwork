using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    /// <summary>
    /// Interface used to implement the Strategy design pattern
    /// for testing the performances of various algorithms that compute the shortest
    /// chain of friends between two users in a social network.
    /// </summary>
    public abstract class FriendChainLength
    {

        /// <summary>
        /// Computes the length of the shortest chain of friends between
        /// two users A and B in the social network
        /// </summary>
        /// <param name="A">ID of the first user</param>
        /// <param name="B">ID of the second user</param>
        /// <returns></returns>
        public abstract int GetChainLength(int A, int B, NetworkGraph network);
       
    }
}
