using System;
using System.Collections.Generic;

namespace Armory.DataAccess.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int OrderId { get; set; }
        public string Location { get; set; }
        public int Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual Location LocationNavigation { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
