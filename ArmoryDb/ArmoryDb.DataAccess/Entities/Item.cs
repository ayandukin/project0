using System;
using System.Collections.Generic;

namespace ArmoryDb.DataAccess.Entities
{
    public partial class Item
    {
        public Item()
        {
            Inventory = new HashSet<Inventory>();
            Invoice = new HashSet<Invoice>();
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
