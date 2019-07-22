using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Customers
    {
        private readonly List<Customer> customer = new List<Customer>();

        public Customers()
        {
            customer.Add(new Customer { FirstName = "Bill", LastName = "Jobs" });
            customer.Add(new Customer { FirstName = "Gordon", LastName = "Croft" });
            customer.Add(new Customer { FirstName = "Steve", LastName = "Gates" });
            customer.Add(new Customer { FirstName = "Lara", LastName = "Freeman" });
        }

        public void AddCustomer(string first, string last)
        {
            customer.Add(new Customer { FirstName = first, LastName = last});
        }
        
    }
}
