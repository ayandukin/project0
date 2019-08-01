using System;
using System.Collections.Generic;
using System.Text;
namespace Armory.Library
{
    class Invoice
    {
        private static int invoiceID = 1;
        private int orderID, quantity;
        private string item;
        
        public Invoice(int orderNumber, KeyValuePair<string, int> purchase)
        {
            orderID = orderNumber;
            item = purchase.Key;
            quantity = purchase.Value;

        }
    }
}