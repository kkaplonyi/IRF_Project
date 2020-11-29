using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_U48GFT.Entities
{
    public class PortfolioItem
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }

        public decimal totalcost { get; set; }

    }
}
