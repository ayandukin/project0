using System;
using System.Collections.Generic;

namespace Armory.Library
{
    public class Location
    {
        private string name;
        public string Name { get => name; set => name = value; }

        public List<Inventory> Inventory { get; set; } = new List<Inventory>();

        public List<Orders> Orders { get; set; } = new List<Orders>();

    }
}
