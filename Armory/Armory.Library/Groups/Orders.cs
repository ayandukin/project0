using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.Library
{
    
    public class Orders
    {
        private static int NextOrderNum = 1;
        private readonly List<Order> orders = new List<Order>();
        public void PlaceOrder()
        {
            orders.Add(new Order (NextOrderNum));
            NextOrderNum++;
        }

        
    }
     

}
