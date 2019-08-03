using System;
using System.Collections.Generic;

namespace Armory.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public string Name { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
