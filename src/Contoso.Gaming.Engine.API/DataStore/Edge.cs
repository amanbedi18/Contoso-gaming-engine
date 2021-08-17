using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.DataStore
{
    public class Edge
    {
        public string Src => src;
        public string Nbr => nbr;
        public int Wt => wt;

        private readonly string src;
        private readonly string nbr;
        private readonly int wt;

        public Edge(string src, string nbr, int wt)
        {
            this.src = src;
            this.nbr = nbr;
            this.wt = wt;
        }
    }
}
