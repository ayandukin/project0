using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Order
    {
        private readonly int number;
        private string firstName, lastName, location;

        public int Number { get => number; }

        public Order(int num)
        {
            number = num;
        }

        public void OrderDetails()
        {
            
        }


    }
}
