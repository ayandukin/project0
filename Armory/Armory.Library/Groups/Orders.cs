using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    
    public class Orders
    {
        private static int orderNumber = 1;
        private readonly List<Order> orders = new List<Order>();
        private Dictionary<string, int> purchases;
       

        public void PlaceOrder(string location, Customer user, Dictionary<string,int> order)
        {
            orders.Add(new Order(orderNumber ,location, user, order));
            
            orderNumber++;

        }
    }
     

}
