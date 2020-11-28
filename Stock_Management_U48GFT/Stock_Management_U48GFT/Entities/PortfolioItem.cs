using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_U48GFT.Entities
{
    class PortfolioItem
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }

        public string totalcost { get; set; }

    }
}
