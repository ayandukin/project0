using System;
using System.Collections.Generic;
using Armory.Library;

namespace Armory.App
{
    class Program
    {
        static Locations city = new Locations();
        static Orders orders = new Orders();
        static Customers customer = new Customers();

        private static string _location;
        static void Main(string[] args)
        {

            


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
                        location = city.SelectLocation();
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
