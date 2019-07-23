using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Armory.Library
{
    public class Locations
    {
        private readonly List<Location> city = new List<Location>();

        public Locations()
        {
            city.Add(new Location { Name = "Arlington" });
            city.Add(new Location { Name = "Fort Worth" });
            city.Add(new Location { Name = "Dallas" });
        }
        
        public bool MakePurchase(string town, string gun, int num)
        {
            var temp = city.First(x => x.Name.Equals(town));
            return temp.Purchase(gun, num)? true : false;
        }



        public string SelectLocation()
        {
            string loc="";
            Console.WriteLine("Select city");
            Console.WriteLine("1: Arlingon");
            Console.WriteLine("2: Fort Worth");
            Console.WriteLine("3: Dallas");
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        loc = city[0].Name;
                        break;
                    case "2":
                        loc = city[1].Name;
                        break;
                    case "3":
                        loc = city[2].Name;
                        break;

                }

                break;
            }
            return loc;
        }


        public Dictionary<string, int> SelectItems(string town)
        {
            Console.WriteLine("The following items are available");
            var temp = city.First(x => x.Name.Equals(town));
            {
                temp.ListInventory();
            }
            string item;
            int num;
            Dictionary<string, int> purchase = new Dictionary<string, int>();
            Console.WriteLine("Type name of item");
            item = Console.ReadLine();
            Console.WriteLine("Type the number of items you wish to purchase");
            num = int.Parse(Console.ReadLine());

            if (temp.Purchase(item, num))
            {
                Console.WriteLine("Added to cart");
                purchase.Add(item, num);
            }
            return purchase;
            
        }








        public void CheckInventory()
        {
            while (true)
            {
                foreach(var x in city)
                    Console.WriteLine($"1: {x.Name}");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        city[0].ListInventory();
                        break;
                    case "2":
                        city[1].ListInventory();
                        break;
                    case "3":
                        city[2].ListInventory();
                        break;
                }

                break;
            }
        }
    }
}
