using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerId { get; set; }
        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
