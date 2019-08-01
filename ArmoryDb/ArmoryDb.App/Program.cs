using ArmoryDb.DataAccess;
using ArmoryDb.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace ArmoryDb.App
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string input;

            Console.WriteLine("Welcome to Arlington Armory");
            
            do
            {
                
                Console.WriteLine("\nPlease type \"check order\" to check order history" +
                "\ntype \"place order\" to to place a new order" +
                "\ntype \"inventory\" to access inventory functions" +
                "\nor type \"exit\" at any time to exit to desktop");
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "check order":
                        do
                        {
                            Console.WriteLine("\nType \"orders by site\" to view orders by locations" +
                                "\ntype \"orders by user\" to view orders by customer" +
                                "\ntype \"back\" to go to previous screen");
                            input = Console.ReadLine().ToLower();
                            switch (input)
                            {
                                case "orders by site":
                                    do
                                    {
                                        Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to previous screen");
                                        input = Console.ReadLine();
                                        switch (input)
                                        {
                                            case "Arlington":
                                            case "Dallas":
                                            case "Fort Worth":
                                                ArmoryRepo.OrderHistoryByLocation(input);
                                                break;

                                            case "back":
                                                break;

                                            case "exit":
                                                break;

                                            default:
                                                Console.WriteLine("unrecognized command, please try again");
                                                break;
                                        }
                                    }
                                    while (input != "exit" && input != "back");
                                    break;

                                case "orders by user":
                                    do
                                    {
                                        Console.WriteLine("\nType customer's name in the following format:" +
                                            "\nFirstName LastName" +
                                            "\ntype \"back\" to go to previous screen");
                                        input = Console.ReadLine();
                                        
                                        
                                        switch (input)
                                        {
                                            case "back":
                                                break;

                                            case "exit":
                                                break;

                                            default:
                                                string[] name = input.Split();
                                                if (name.Length == 2)
                                                    ArmoryRepo.OrderHistoryByCustomer(name[0], name[1]);
                                                else
                                                    Console.WriteLine("please try again");
                                                break;
                                        }
                                    }
                                    while (input != "exit" && input != "back");
                                    break;

                                case "back":
                                    break;

                                case "exit":
                                    Console.WriteLine("good bye");
                                    break;

                                default:
                                    Console.WriteLine("unrecognized command, please try again");
                                    break;
                            }
                        }
                        while (input != "exit" && input != "back");
                        break;

                    case "place order":
                        do
                        {
                            
                            Console.WriteLine("\nType customer's name in the following format:" +
                                "\nFirstName LastName" +
                                "\ntype \"back\" to go to previous screen");
                            input = Console.ReadLine();

                            switch (input)
                            {
                                case "back":
                                    break;

                                case "exit":
                                    break;

                                default:
                                    string[] name = input.Split();
                                    ArmoryRepo.CheckCustomer(name[0], name[1]);
                                    if (name.Length == 2)
                                    {
                                        do
                                        {
                                            Dictionary<string, int> orderItems = new Dictionary<string, int>();
                                            Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to previous screen");
                                            input = Console.ReadLine();

                                            switch (input)
                                            {
                                                case "Arlington":
                                                case "Dallas":
                                                case "Fort Worth":
                                                    string location = input;
                                                    do
                                                    {
                                                        Console.WriteLine("\nType the item from the list, " +
                                                        "\nthen, followed by coma, the number of items to purchase, in the following format:" +
                                                        "\nRuger LCP,3" +
                                                        "\ntype \"back\" to go to previous screen" +
                                                        "\ntype \"done\" when ready to checkout");
                                                        ArmoryRepo.DisplayItems();
                                                        input = Console.ReadLine();
                                                        switch (input)
                                                        {
                                                            case "done":
                                                                foreach (var y in orderItems)
                                                                    Console.WriteLine(y);
                                                                ArmoryRepo.Purchase(name[0], name[1], location, orderItems);
                                                                break;

                                                            case "back":
                                                                break;

                                                            case "exit":
                                                                break;
                                                                
                                                            default:
                                                                try
                                                                {
                                                                    string[] s = input.Split(',');
                                                                    int i = int.Parse(s[1]);
                                                                    if (ArmoryRepo.CheckInventory(location, s[0], i))
                                                                    {
                                                                        Console.WriteLine($"Item is:{s[0]}, number is {i}");
                                                                        orderItems.Add(s[0], i);
                                                                        foreach (var y in orderItems)
                                                                            Console.WriteLine(y);
                                                                    }
                                                                    Console.WriteLine("Would you like to purchase another item?");
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("Please check your spelling");
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    while (input != "exit" && input != "back" && input != "done");
                                                    break;

                                                case "back":
                                                    break;

                                                case "exit":
                                                    break;

                                                default:
                                                    Console.WriteLine("please try again");
                                                    break;
                                            }
                                        }
                                        while (input != "exit" && input != "back");
                                    }
                                        
                                    else
                                        Console.WriteLine("please try again");
                                    break;
                            }
                        }
                        while (input != "exit" && input != "back");
                        break;

                    case "inventory":
                        do
                        {
                            Console.WriteLine("\nType \"check inventory\" to view current inventory " +
                                "\ntype \"restock inventory\" to restock inventory" +
                                "\ntype \"back\" to go to previous screen");
                            input = Console.ReadLine().ToLower();
                            switch (input)
                            {
                                case "check inventory":
                                    Console.WriteLine("checking inventory");
                                    ArmoryRepo.DisplayInventory();
                                    break;

                                case "restock inventory":
                                    Console.WriteLine("restocking inventory");
                                    ArmoryRepo.Restock();
                                    break;

                                case "back":
                                    break;

                                case "exit":
                                    break;

                                default:
                                    Console.WriteLine("unrecognized command, please try again");
                                    break;
                            }
                        }
                        while (input != "exit" && input != "back");
                        break;

                    case "exit":
                        break;

                    case "back":
                        break;

                    default:
                        Console.WriteLine("unrecognized command, please try again");
                        break;
                }

            }
            while (input != "exit");
            Console.WriteLine("\nThank you for visiting Arlington Armory. Have a great day");
        }
    }
}
