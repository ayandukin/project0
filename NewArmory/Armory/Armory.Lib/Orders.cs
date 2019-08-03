using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Armory.Library
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string Location { get; set; }
        public int Customer { get; set; }

        public List<Invoice> Invoice { get; set; } = new List<Invoice>();
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
