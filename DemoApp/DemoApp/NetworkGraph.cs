using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    /// <summary>
    /// This class represents a social network as an undirected graph.
    /// For simplicity and to maintain the relevance of the testing results,
    /// nodes will not contain additional data and will be represented by an
    /// integer ID.
    /// As for the way the graph is represented, the adjacency list representation
    /// was chosen over the matrix representation, as the graph of a social network
    /// typically has a very large number of nodes compared to the number of edges.
    /// </summary>
    public class NetworkGraph
    {
        #region data
        public Dictionary<int, HashSet<int>> network;
        #endregion

        public NetworkGraph(Dictionary<int, HashSet<int>> dict) => network = dict;

        public IEnumerable<int> GetAccounts() => network.Keys;


        #region CRUD methods

        /// <summary>
        /// Function that adds an account with a given id
        /// </summary>
        /// <param name="account">The unique identifier(ID) of the social account</param>
        public void AddAccount(int account) 
        {
            if(!network.ContainsKey(account))
            {
                network[account] = new HashSet<int>();
            }
        }

        /// <summary>
        /// Function that adds a friendship relation between the user of account1 and the user of account2
        /// </summary>
        /// <param name="account1">The ID of the first account</param>
        /// <param name="account2">The ID of the second account</param>
        public void AddFriend(int account1, int account2)
        {
            if (network.ContainsKey(account1) && network.ContainsKey(account2))
            {
                network[account1].Add(account2);
                network[account2].Add(account1);
            }
            else throw new Exception("Invalid add request. One of the accounts does not exist.");
        }

        /// <summary>
        /// Function that is called when somenone deletes their account.
        /// </summary>
        /// <param name="account">The ID of the deleted account</param>
        public void RemoveAccount(int account)
        {
            if(!network.ContainsKey(account))
            {
                return;
            }

            foreach(var friend in network[account])
            {
                network[friend].Remove(account);
            }

            network.Remove(account);
        }

        /// <summary>
        ///Method that removes the friend edge
        /// </summary>
        /// <param name="account">The ID of the deleted account</param>
        /// <returns></returns>
        public void Unfriend(int account1, int account2)
        {
            if (!network.ContainsKey(account1) || !network.ContainsKey(account2))
            {
                return;
            }

            network[account1].Remove(account2);
            network[account2].Remove(account1);
        }

        /// <summary>
        /// Method that returns all the fiens of an account
        /// (usefull for friend display)
        /// </summary>
        /// <param name="account">The ID of the deleted account</param>
        /// <returns></returns>
        public HashSet<int> GetFriends(int account)
        {
            if (!network.ContainsKey(account))
            {
                return new HashSet<int>();
            }
            return network[account];
        }
        /// <summary>
        /// Method for finding out is an account exists
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public  bool ContainsAccount(int account) => network.ContainsKey(account);
        public List<int> Accounts
        {
            get
            {
                return new List<int>(network.Keys);
            }
        }

        #endregion
    }
}
