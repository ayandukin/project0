using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    public class Locations
    {
        private readonly List<Location> city = new List<Location>();
        //public List<Location> GetLocations() => city;

        public Locations()
        {
            city.Add(new Location { Name = "Arlington" });
            city.Add(new Location { Name = "Fort Worth" });
            city.Add(new Location { Name = "Dallas" });
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
