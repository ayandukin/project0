using System;
using System.Collections.Generic;

namespace ArmoryDb.DataAccess.Entities
{
    public partial class Inventory
    {
        public int InvId { get; set; }
        public string Item { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }

        public virtual Item ItemNavigation { get; set; }
        public virtual Location LocationNavigation { get; set; }
    }
}
