using System;
using System.Collections.Generic;
using System.Text;

namespace ArmoryDb.Library
{
    public class Customer
    {
        private int customerID;
        private string firstName, lastName;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int CustomerId { get; set; }
        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
