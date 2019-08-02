using ArmoryDb.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace ArmoryDb.DataAccess
{
    /// <summary>
    /// This repository manages data access to the ArmoryDb database
    /// The repository is static as there is no reason to create multiple objects of this class; static methods always callable
    /// </summary>
    public static class ArmoryRepo
    {
        
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private static DbContextOptions<ArmoryDbContext> options = new DbContextOptionsBuilder<ArmoryDbContext>()
                .UseSqlServer(SecretConfiguration.ConnectionString).Options;
        private static ArmoryDbContext _dbContext = new ArmoryDbContext(options);

        /// <summary>
        /// Returns list of orders placed at specific location
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Collection of orders, each with a collection of corresponding invoices</returns>
        public static IEnumerable<Entities.Orders> OrderHistoryByLocation(string location)
        {
            IQueryable<Orders> orders = _dbContext.Orders.Include(r => r.Invoice).Where(r => r.Location.Equals(location));
            return orders;
        }
        /// <summary>
        /// Returns list of orders placed by customer
        /// </summary>
        /// <param name="first">Customer's first name</param>
        /// <param name="last">Customer's last name</param>
        /// <returns>Collection of orders, each with a collection of corresponding invoices</returns>
        public static IEnumerable<Entities.Orders> OrderHistoryByCustomer(string first, string last)
        {
            IQueryable<Orders> orders = _dbContext.Orders.Include(r => r.CustomerNavigation).Include(r => r.Invoice).Where(x => x.CustomerNavigation.FirstName == first && x.CustomerNavigation.LastName==last);
            return orders;
        }
        /// <summary>
        /// Returns list of items with their corresponding prices
        /// </summary>
        /// <returns>Collection of orders</returns>
        public static IEnumerable<Entities.Item> DisplayItems()
        {
            IQueryable<Item> items = _dbContext.Item;
            return items;
        }
        /// <summary>
        /// Ruturns inventory list of available items and their quantities in specific location
        /// </summary>
        /// /// <param name="location">store location</param>
        /// <returns>inventory collection for specified location</returns>
        public static IEnumerable<Entities.Inventory> DisplayInventory(string location)
        {
            IQueryable<Inventory> inventory = _dbContext.Inventory.Where(x => x.Location == location);
            return inventory;
        }
        /// <summary>
        /// Checks if there are enough items in inventory to fulfil the order
        /// </summary>
        /// <param name="location">store location</param>
        /// <param name="item">item name</param>
        /// <param name="number">quantity</param>
        /// <returns>true if enough inventory in stock to fulfil order</returns>
        public static bool CheckInventory(string location, string item, int number)
        {
            Inventory inv = _dbContext.Inventory.First(x => x.Item == item && x.Location == location);
            return inv.Quantity >= number? true : false;
        }
        /// <summary>
        /// Places an order. Order contains a collection of invoices, each for individual item and quantity.
        /// Once all invoices are created; the total order price is updated
        /// </summary>
        /// <param name="first">Customer's first name</param>
        /// <param name="last">Customer's last name</param>
        /// <param name="location">store location</param>
        /// <param name="purchase">Dictionary of purchases (key:item names, value: quantity)</param>
        /// <returns></returns>
        public static Orders Purchase (string first, string last, string location, Dictionary<string, int> purchase)
        {
            if (!CheckCustomer(first, last))
                AddCustomer(first, last);
            Orders order = new Orders { Location = location, Customer = GetCustomerID(first, last), Price = 0 };
            _dbContext.Add(order);
            foreach(var x in purchase)
            {
                ReduceInventory(location, x.Key, x.Value);
                Item it = _dbContext.Item.Find(x.Key);
                order.Invoice.Add(new Invoice { Item = x.Key, Quantity = x.Value, Price = (it.Price * x.Value) });
            }
            foreach (var x in order.Invoice)
                order.Price += x.Price;
            _dbContext.SaveChanges();
            return order;
        }
        /// <summary>
        /// Called only by Purchase method, so is set to private; reduces inventory after items have been purchased
        /// </summary>
        /// <param name="location">store location</param>
        /// <param name="item">item name</param>
        /// <param name="number">quantity</param>
        private static void ReduceInventory(string location, string item, int number)
        {
            Inventory inv = _dbContext.Inventory.First(x => x.Item == item && x.Location == location);
            inv.Quantity -= number;
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Utility method to replenish low inventory; raises all inventory items at a location by a hardcoded number
        /// </summary>
        /// <param name="location">store location</param>
        public static void Restock(string location)
        {
            IQueryable<Inventory> inv = _dbContext.Inventory;
            foreach (var x in inv.Where(x=> x.Location == location))
                x.Quantity += 5;
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Checks if the customer is already in the database
        /// </summary>
        /// <param name="first">Customer's first name</param>
        /// <param name="last">Customer's last name</param>
        /// <returns>True if customer is already in the database</returns>
        public static bool CheckCustomer(string first, string last)
        {
            return _dbContext.Customer.Any(r => r.FirstName.Equals(first) && r.LastName.Equals(last)) ? true : false;
        }
        /// <summary>
        /// Get customer's reference by customer ID 
        /// </summary>
        /// <param name="customerId">Customer's ID</param>
        /// <returns>Customer reference</returns>
        public static Customer GetCustomerName(int customerId)
        {
            return _dbContext.Customer.Find(customerId);
        }
        /// <summary>
        /// Get Customer's ID based on first and last names
        /// </summary>
        /// <param name="first">Customer's first name</param>
        /// <param name="last">Customer's last name</param>
        /// <returns>Customer's ID number</returns>
        public static int GetCustomerID(string first, string last)
        {
            Customer cust = _dbContext.Customer.First(x => x.FirstName == first && x.LastName == last);
            return cust.CustomerId;
        }
        /// <summary>
        /// Returns Item reference based on inventory item's name
        /// </summary>
        /// <param name="name">name of the item</param>
        /// <returns>Item reference</returns>
        public static Item GetItem(string name)
        {
            return _dbContext.Item.First(x => x.Name == name);
        }
        /// <summary>
        /// Called only by Purchase method, so is set to private; adds new customer to customer database 
        /// </summary>
        /// <param name="first">Customer's first name</param>
        /// <param name="last">Customer's last name</param>
        private static void AddCustomer(string first, string last)
        {
            logger.Info($"Adding customer");
            Customer customer = new Customer { FirstName = first, LastName = last };
            _dbContext.Add(customer);
            _dbContext.SaveChanges();
        }

    }

}
