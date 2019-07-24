using System;
using System.Collections.Generic;
using System.Linq;
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

        public Customer AddCustomer(string first, string last)
        {
            customer.Add(new Customer { FirstName = first, LastName = last});
            Console.WriteLine($"{first} {last} added");
            return customer.First(x => x.FirstName.Equals(first) && x.LastName.Equals(last));
        }
        
        public Customer CkeckCustomer()
        {
            string first, last;
            Console.WriteLine("Please enter your first name");
            first = Console.ReadLine();
            Console.WriteLine("Please enter your last name");
            last = Console.ReadLine();

            if(customer.Any(x => x.FirstName.Equals(first) && x.LastName.Equals(last)))
            {
                var cust = customer.First(x => x.FirstName.Equals(first) && x.LastName.Equals(last));
                Console.WriteLine($"Welcome back {first} {last}");
                return cust;
            }
            else
            {
                return AddCustomer(first, last);
            }

        }
    }
}
