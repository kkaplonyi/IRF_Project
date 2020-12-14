using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_U48GFT.Entities
{
    public class PortfolioItem
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Totalcost { get; set; }

    }
}
