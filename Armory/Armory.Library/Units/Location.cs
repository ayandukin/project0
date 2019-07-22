using System;
using System.Collections.Generic;

namespace Armory.Library
{
    public class Location
    {
        private string name;

        public string Name { get => name; set => name = value; }

        private Dictionary<string, int> inventory = new Dictionary<string, int>();

        public Location()
        {
            inventory.Add("S&W M500", 10);
            inventory.Add("Remington 870", 10);
            inventory.Add("Colt Python", 10);
            inventory.Add("Glock 19", 10);
            inventory.Add("Ruger LCP", 10);
            inventory.Add("COP Derringer", 10);
            inventory.Add("SPAS 12", 10);
        }


        public void ListInventory()
        {
            Console.WriteLine($"Stock of {name} Armory");
            foreach (var item in inventory)
            {
                Console.WriteLine(item);
            }
        }

        public void Purchase(string gun, int num)
        {
            inventory[gun] -= num;
        }
    }
}
