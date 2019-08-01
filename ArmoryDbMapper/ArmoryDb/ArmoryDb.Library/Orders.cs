using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ArmoryDb.Library
{
    public class Orders
    {
        private int orderID, customer;
        private string location;

        public int OrderId { get; set; }
        public string Location { get; set; }
        public int Customer { get; set; }

        public List<Invoice> Invoice { get; set; } = new List<Invoice>();
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
