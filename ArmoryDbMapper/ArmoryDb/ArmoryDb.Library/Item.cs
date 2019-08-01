using System;
using System.Collections.Generic;
using System.Text;

namespace ArmoryDb.Library
{
    public class Item
    {
        private string name;

        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Invoice> Invoice { get; set; } = new List<Invoice>();
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();
        public bool Automatic { get; set; }
    }
}
