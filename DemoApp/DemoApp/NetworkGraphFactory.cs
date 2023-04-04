using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    /// <summary>
    /// Factory abstract class
    /// </summary>
    abstract class NetworkGraphFactory
    {
        public abstract NetworkGraph CreateNetworkGraph();
    }
}
