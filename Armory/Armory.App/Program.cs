using System;
using System.Collections.Generic;
using Armory.Library;

namespace Armory.App
{
    class Program
    {
        static void Main(string[] args)
        {

            Locations city = new Locations();
            Orders orders = new Orders();
            Customers customer = new Customers();


            Console.WriteLine("Welcome to Arlington Armory \nPlease make your selection");
            while (true)
            {
                Console.WriteLine("1: Check Inventory");
                Console.WriteLine("2: Place an Order");
                string input = Console.ReadLine();
                switch (input)
                { 
                    case "1":
                        city.CheckInventory();
                        break;

                    case "2":
                        PlaceOrder();
                        break;
                }

                break;
            }

            
            orders.PlaceOrder();
            




           

        }

        private static void PlaceOrder()
        {
            throw new NotImplementedException();
        }
    }
}
