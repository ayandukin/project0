using System;
using System.Collections.Generic;
using Armory.Library;

namespace Armory.App
{
    class Program
    {
        static Locations city = new Locations();
        static Orders order = new Orders();
        static Customers customer = new Customers();
        static Dictionary<string, int> purchase = new Dictionary<string, int>();

        private static string _location;
        private static Customer user;
        static void Main(string[] args)
        {

            


            Console.WriteLine("Welcome to Arlington Armory \nPlease make your selection");
            while (true)
            {
                Console.WriteLine("1: Check Inventory");
                Console.WriteLine("2: Place an Order");
                Console.WriteLine("3: Check Customer");
                string input = Console.ReadLine();
                switch (input)
                { 
                    case "1":
                        city.CheckInventory();
                        break;

                    case "2":
                        _location = city.SelectLocation();
                        purchase=city.SelectItems(_location);
                        user=customer.CkeckCustomer();
                        order.PlaceOrder(_location, user, purchase);

                        break;

                    case "3":
                        customer.CkeckCustomer();
                        break;
                }

                break;
            }


        }

        private static void PlaceOrder(string town, string gun, int num)
        {
            city.MakePurchase(town, gun, num);
        }

        
    }
}
