using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmoryDb.DataAccess
{
    public static class Mapper
    {
        public static Entities.Customer Map(Library.Customer customer) => new Entities.Customer
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Orders = customer.Orders.Select(Map).ToList()
        };
        public static Library.Customer Map(Entities.Customer customer) => new Library.Customer
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Orders = customer.Orders.Select(Map).ToList()
        };
        public static Entities.Inventory Map(Library.Inventory inventory) => new Entities.Inventory
        {
            InvId = inventory.InvId,
            Item = inventory.Item,
            Location = inventory.Location,
            Quantity = inventory.Quantity
        };
        public static Library.Inventory Map(Entities.Inventory inventory) => new Library.Inventory
        {
            InvId = inventory.InvId,
            Item = inventory.Item,
            Location = inventory.Location,
            Quantity = inventory.Quantity
        };
        public static Library.Invoice Map(Entities.Invoice invoice) => new Library.Invoice
        {
            InvoiceId = invoice.InvoiceId,
            OrderId = invoice.OrderId,
            Item = invoice.Item,
            Quantity = invoice.Quantity,
            Price = invoice.Price
        };
        public static Entities.Invoice Map(Library.Invoice invoice) => new Entities.Invoice
        {
            InvoiceId = invoice.InvoiceId,
            OrderId = invoice.OrderId,
            Item = invoice.Item,
            Quantity = invoice.Quantity,
            Price = invoice.Price
        };
        public static Entities.Item Map(Library.Item item) => new Entities.Item
        {
            Name = item.Name,
            Price = item.Price,
            Automatic = item.Automatic,
            Invoice = item.Invoice.Select(Map).ToList(),
            Inventory = item.Inventory.Select(Map).ToList()
        };
        public static Library.Item Map(Entities.Item item) => new Library.Item
        {
            Name = item.Name,
            Price = item.Price,
            Automatic = item.Automatic,
            Invoice = item.Invoice.Select(Map).ToList(),
            Inventory = item.Inventory.Select(Map).ToList()
        };
        public static Entities.Location Map(Library.Location location) => new Entities.Location
        {
            Name = location.Name,
            Orders = location.Orders.Select(Map).ToList(),
            Inventory = location.Inventory.Select(Map).ToList()
        };
        public static Library.Location Map(Entities.Location location) => new Library.Location
        {
            Name = location.Name,
            Orders = location.Orders.Select(Map).ToList(),
            Inventory = location.Inventory.Select(Map).ToList()
        };
        public static Entities.Orders Map(Library.Orders orders) => new Entities.Orders
        {
            OrderId = orders.OrderId,
            Location = orders.Location,
            Customer = orders.Customer,
            PurchaseDate = orders.PurchaseDate,
            //Price = orders.Price,
            Invoice = orders.Invoice.Select(Map).ToList(),
        };
        public static Library.Orders Map(Entities.Orders orders) => new Library.Orders
        {
            OrderId = orders.OrderId,
            Location = orders.Location,
            Customer = orders.Customer,
            PurchaseDate = orders.PurchaseDate,
            //Price = orders.Price,
            Invoice = orders.Invoice.Select(Map).ToList()
        };
    }
}
