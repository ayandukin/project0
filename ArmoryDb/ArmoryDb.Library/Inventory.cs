using System;
using System.Collections.Generic;
using System.Text;

namespace ArmoryDb.Library
{
    public class Inventory
    {
        public int InvId { get; set; }
        public string Item { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }
}
