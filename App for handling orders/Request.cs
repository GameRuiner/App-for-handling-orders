using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_for_handling_orders
{
    class Request
    {
        private string name;
        private string quantity;
        private double price;
        private string clientId;
        private long requestId;



        public Request(string clientId, long requestId, string name, string quantity, double price) 
        {
            this.clientId = clientId;
            this.requestId = requestId;
            this.name = name;
            this.quantity = quantity;
            this.price = price;

        }
    }
}
