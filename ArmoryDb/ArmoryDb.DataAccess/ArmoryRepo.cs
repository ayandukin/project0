using ArmoryDb.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace ArmoryDb.DataAccess
{
    public static class ArmoryRepo
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private static DbContextOptions<ArmoryDbContext> options = new DbContextOptionsBuilder<ArmoryDbContext>()
                .UseSqlServer(SecretConfiguration.ConnectionString).Options;

        public static void OrderHistoryByLocation(string location)
        {
            Console.WriteLine("checking order history...");
            logger.Info("checking order history...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    dbContext.Orders.Any(x => x.Location.Equals(location));
                    Console.WriteLine("The items are");
                    foreach (Orders order in dbContext.Orders)
                        if (order.Location.Equals(location))
                        {
                            Console.WriteLine($"Order number:{order.OrderId} by customer {GetCustomerName(order.Customer)} purchased at {order.Location}");
                        }
                }
                catch
                {
                    logger.Warn("No orders yet");
                }
            }
        }

        public static void OrderHistoryByCustomer(string first, string last)
        {
            Console.WriteLine("checking order history...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    int custID = CheckCustomer(first, last);
                    dbContext.Orders.Any(x => x.Customer.Equals(custID));
                    Console.WriteLine("The items are");
                    foreach (Orders order in dbContext.Orders)
                        if (order.Customer.Equals(custID))
                        {
                            Console.WriteLine($"Order number:{order.OrderId} by customer {first} {last} purchased at {order.Location}");
                        }
                }
                catch
                {
                    Console.WriteLine("No orders at this location yet");
                }
            }
        }

        public static void DisplayItems()
        {
            Console.WriteLine("loading...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    dbContext.Item.Any();
                    Console.WriteLine("The items are");
                    foreach (Item item in dbContext.Item)
                        Console.WriteLine($"{item.Name}");
                }
                catch {
                    Console.WriteLine("No items found!");
                }
            }
        }

        public static void DisplayInventory()
        {
            Console.WriteLine("checking inventory...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    dbContext.Inventory.Any();
                    Console.WriteLine("The items are");
                    foreach (Inventory item in dbContext.Inventory)
                        Console.WriteLine($"{item.Location} {item.Item} {item.Quantity}");
                }
                catch
                {
                    Console.WriteLine("No items found!");
                }
            }
        }

        public static bool CheckInventory(string location, string item, int number)
        {
            Console.WriteLine("checking inventory...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    Console.WriteLine($"Checking {location} and {item} item");
                    Inventory temp = dbContext.Inventory.First(x => x.Location.Equals(location) && x.Item.Equals(item));
                    if (temp.Quantity >= number)
                    {
                        Console.WriteLine($"Sufficient quantity of {temp.Item} in inventory");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"There are only {temp.Quantity} items");
                        return false;
                    }
                }
                catch
                {
                    Console.WriteLine($"No {location} items at {location}");
                    return false;
                }

            }
        }

        public static void Purchase (string first, string last, string location, Dictionary<string, int> purchase)
        {
            using (var dbContext = new ArmoryDbContext(options))
            {
                var newOrder = new Orders
                {
                    Location = location,
                    Customer = CheckCustomer(first, last)
                };
                dbContext.Add(newOrder);
                dbContext.SaveChanges();

                int orderNumber = newOrder.OrderId;
                Console.WriteLine($"Order number {orderNumber} has been placed");

                foreach (var x in purchase)
                {
                    if (CheckInventory(location, x.Key, x.Value))
                    {
                        CreateInvoice(orderNumber, x.Key, x.Value);
                        ReduceInventory(location, x.Key, x.Value);
                    }
                }
                dbContext.SaveChanges();
            }

        }


        private static void CreateInvoice(int orderNumber, string item, int number)
        {
            using (var dbContext = new ArmoryDbContext(options))
            {
                var newInvoice = new Invoice
                {
                    OrderId = orderNumber,
                    Item = item,
                    Quantity = number
                };
                dbContext.Add(newInvoice);
                dbContext.SaveChanges();

                int invoiceNumber = newInvoice.InvoiceId;
                Console.WriteLine($"Invoice number {invoiceNumber} for {number} of {item} has been placed");

            }
        }

        private static void ReduceInventory(string location, string item, int number)
        {
            using (var dbContext = new ArmoryDbContext(options))
            {
                Inventory temp = dbContext.Inventory.First(x => x.Location.Equals(location) && x.Item.Equals(item));
                temp.Quantity -= number;
                dbContext.SaveChanges();
            }
        }

        public static void Restock()
        {
            Console.WriteLine("restocking...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                Console.WriteLine("The items are");
                foreach (Inventory item in dbContext.Inventory)
                {
                    item.Quantity += 10;
                    Console.WriteLine($"{item.Location} {item.Item} {item.Quantity}");
                }
                dbContext.SaveChanges();
            }
        }

        public static int CheckCustomer(string first, string last)
        {
            Console.WriteLine("searching...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                try
                {
                    var customer = dbContext.Customer.First(x => x.FirstName.Equals(first) && x.LastName.Equals(last));
                    Console.WriteLine($"Customer: {first} {last} already on database");
                    return customer.CustomerId;
                }
                catch
                {
                    Console.WriteLine($"Adding customer {first} {last}");
                    return AddCustomer(first, last);
                }

            }
        }

        public static string GetCustomerName(int customerNum)
        {
            Console.WriteLine("searching...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                string fullName;
                try
                {
                    var customer = dbContext.Customer.First(x => x.CustomerId.Equals(customerNum));
                    fullName = customer.FirstName + "";
                    fullName += " " + customer.LastName;
                    return fullName;
                }
                catch
                {
                    return "Customer not found";
                }

            }
        }

        private static int AddCustomer(string first, string last)
        {
            Console.WriteLine("adding user...");
            using (var dbContext = new ArmoryDbContext(options))
            {
                var newUser = new Customer
                {
                    FirstName = first,
                    LastName = last
                };
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                Console.WriteLine($"Customer: {first} {last} added");
                return newUser.CustomerId;
            }

        }

    }

}
