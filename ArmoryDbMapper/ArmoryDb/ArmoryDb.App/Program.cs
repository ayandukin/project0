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


        /// <summary>
        /// Main program, also the Console application; uses do-while loops to lock user in each menu until exit conditions are met
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string input;

            Console.WriteLine("Welcome to Arlington Armory");
            
            do
            {
                Console.WriteLine("\nPlease type \"check orders\" to check order history" +
                "\ntype \"place order\" to to place a new order" +
                "\ntype \"inventory\" to access inventory functions" +
                "\nor type \"exit\" at any time to exit to desktop");
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "check orders":
                        do
                        {
                            Console.WriteLine("\nType \"orders by site\" to view orders by locations" +
                                "\ntype \"orders by customer\" to view orders by customer" +
                                "\ntype \"back\" to go to main menu");
                            input = Console.ReadLine().ToLower();
                            switch (input)
                            {
                                case "orders by site":
                                    do
                                    {
                                        Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to main menu");
                                        input = Console.ReadLine();
                                        switch (input)
                                        {
                                            //Validates location input
                                            case "Arlington":
                                            case "Dallas":
                                            case "Fort Worth":
                                                var w = ArmoryRepo.OrderHistoryByLocation(input).ToList();
                                                foreach (var item in w)
                                                {
                                                    Console.WriteLine($"Order number:{item.OrderId} by customer {ArmoryRepo.GetCustomerName(item.Customer).FirstName} {ArmoryRepo.GetCustomerName(item.Customer).LastName} for ${item.Price} purchased at {item.Location} on {item.PurchaseDate}");
                                                    foreach (var q in item.Invoice)
                                                        Console.WriteLine($"    {q.Item}, {q.Quantity} items, total ${q.Price}");
                                                }
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

                                case "orders by customer":
                                    do
                                    {
                                        Console.WriteLine("\nType customer's name in the following format:" +
                                            "\nFirstName LastName" +
                                            "\ntype \"back\" to go to main menu");
                                        input = Console.ReadLine();
                                        switch (input)
                                        {
                                            case "back":
                                                break;

                                            case "exit":
                                                break;
        //uses space char to separate FirstName from LastName
        //does not work for names with multiple spaces, such as Sir Iasaac Newton
        //acceptable restriction
                                            default:
                                                string[] name = input.Split();
                                                if (name.Length == 2)
                                                {
                                                    if (ArmoryRepo.CheckCustomer(name[0], name[1]))
                                                    {
                                                        var w = ArmoryRepo.OrderHistoryByCustomer(name[0], name[1]);
                                                        foreach (var item in w)
                                                        {
                                                            Console.WriteLine($"{item.OrderId} by {item.CustomerNavigation.FirstName} {item.CustomerNavigation.LastName} for total of ${item.Price} at {item.Location} on {item.PurchaseDate}");
                                                            foreach (var q in item.Invoice)
                                                                Console.WriteLine($"    {q.Item}, {q.Quantity} items, total ${q.Price}");
                                                        }
                                                    }
                                                    else
                                                        Console.WriteLine($"No order history for {name[0]} {name[1]}");
                                                }
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
                                    Console.WriteLine("\nUnrecognized command, please try again");
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
                                "\ntype \"back\" to go to main menu");
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
                                    {
                                        if (ArmoryRepo.CheckCustomer(name[0], name[1]))
                                            Console.WriteLine($"{name[0]} {name[1]} already on database");
                                        else
                                            Console.WriteLine($"{name[0]} {name[1]} added to database");
                                        do
                                        {
                                            Dictionary<string, int> orderItems = new Dictionary<string, int>();
                                            Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to main menu");
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
                                                        "\ntype \"back\" to go to main menu" +
                                                        "\ntype \"done\" when ready to checkout");
                                                        var items = ArmoryRepo.DisplayItems();
                                                        foreach (var x in items)
                                                            Console.WriteLine($"{x.Name}: ${x.Price}");
                                                        input = Console.ReadLine();
                                                        switch (input)
                                                        {
                                                            case "done":
                                                                foreach (var y in orderItems)
                                                                    Console.WriteLine(y);
                                                                var x = ArmoryRepo.Purchase(name[0], name[1], location, orderItems);
                                                                Console.WriteLine($"Order {x.OrderId} for ${x.Price} placed");
                                                                foreach(var q in x.Invoice)
                                                                    Console.WriteLine($"    {q.Item}, {q.Quantity} items, total ${q.Price}");
                                                                break;

                                                            case "back":
                                                                break;

                                                            case "exit":
                                                                break;
             //several nested try-catch statements to return proper error info
             //business rules impose restictions on automatic firearms
             //assures user purchases quantity within constraints
             //allows adding multiple item types into a single order
             //prints order info and corresponding invoices after purchase
                                                            default:
                                                                try
                                                                {
                                                                    string[] s = input.Split(',');
                                                                    int i = int.Parse(s[1]);
                                                                    if (i < 1)
                                                                    {
                                                                        Console.WriteLine("Please enter at least one item");
                                                                        break;
                                                                    }
                                                                    if (Rules.IsAutomatic(s[0]) && i > 1)
                                                                    {
                                                                        Console.WriteLine("Automatic firearms are limited to one per purchase");
                                                                        break;
                                                                    }
                                                                    try
                                                                    {
                                                                        if (ArmoryRepo.CheckInventory(location, s[0], i))
                                                                        {
                                                                            orderItems.Add(s[0], i);
                                                                            foreach (var y in orderItems)
                                                                                Console.WriteLine(y);
                                                                        }
                                                                        else
                                                                            Console.WriteLine("\nNot enough in stock to fill order");
                                                                    }
                                                                    catch
                                                                    {
                                                                        Console.WriteLine("\nNot in stock at this location");
                                                                    }
                                                                    Console.WriteLine("\nWould you like to purchase another item?");
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("\nPlease check your spelling");
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
                                "\ntype \"back\" to go to main menu");
                            input = Console.ReadLine().ToLower();
                            switch (input)
                            {
                                case "check inventory":
                                    do
                                    {
                                        Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to main menu");
                                        input = Console.ReadLine();
                                        switch (input)
                                        {
                                            case "Arlington":
                                            case "Dallas":
                                            case "Fort Worth":
                                                var items = ArmoryRepo.DisplayInventory(input);
                                                foreach (var x in items)
                                                    Console.WriteLine($"{x.Item}: {x.Quantity}");
                                                break;

                                            case "back":
                                                break;

                                            case "exit":
                                                break;

                                            default:
                                                Console.WriteLine("\nUnrecognized command, please try again");
                                                break;
                                        }
                                    }
                                    while (input != "exit" && input != "back");
                                    break;

                                case "restock inventory":
                                    do
                                    {
                                        Console.WriteLine("\nType location name" +
                                            "\ncurrent locations are \"Arlington\", \"Dallas\", and \"Fort Worth\"" +
                                            "\ntype \"back\" to go to main menu");
                                        input = Console.ReadLine();
                                        switch (input)
                                        {
                                            case "Arlington":
                                            case "Dallas":
                                            case "Fort Worth":
                                                ArmoryRepo.Restock(input);
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
