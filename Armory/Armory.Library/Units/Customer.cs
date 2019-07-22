using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Customer
    {
        private string firstName, lastName, location;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Location { get => location; set => location = value; }

        public void PlaceOrder()
        {
            
        }
    }
}
