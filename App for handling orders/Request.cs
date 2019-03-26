using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_for_handling_orders
{
    public class Request
    {
        public string name { get; }
        public int quantity { get; }
        public double price { get; }
        public string clientId { get; }
        public long requestId { get; }



        public Request(string clientId, long requestId, string name, int quantity, double price) 
        {
            this.clientId = clientId;
            this.requestId = requestId;
            this.name = name;
            this.quantity = quantity;
            this.price = price;

        }
    }
}
