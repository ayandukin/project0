using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Order
    {
        private readonly int orderNumber;
        private readonly string firstName, lastName, location;
        private readonly Dictionary<string, int> purchases;
        private readonly List<Invoice> invoices = new List<Invoice>();


        public int Number { get => orderNumber; }

        public Order(int orderNumber, string location, Customer user, Dictionary<string, int> order)
        {
            this.orderNumber = orderNumber;
            this.location = location;
            firstName = user.FirstName;
            lastName = user.LastName;
            purchases = order;
            CreateInvoices();
        }

        public void CreateInvoices()
        {
            foreach (KeyValuePair<string, int> x in purchases)
            {

                invoices.Add(new Invoice(orderNumber, x));
            }
        }
        


        public void OrderDetails()
        {
            Console.WriteLine($"{orderNumber}: purchased by {firstName} {lastName} at {location}");
            foreach (var item in purchases)
                Console.WriteLine(item);
        }


    }
}
