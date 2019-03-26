using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_for_handling_orders
{
    class ReportController
    {
        List<Request> memoryDB;

        public ReportController(List<Request> memoryDB)
        {
            this.memoryDB = memoryDB;
        }

        internal int RequestQuantity()
        {
            return memoryDB.Count;
        }

        internal int ClientRequestQuantity(string id)
        {
            var selectedRequest = from Request r in memoryDB 
                                  where r.clientId == id 
                                  select r;
            return selectedRequest.Count();
        }

        internal double SumOfRequests()
        {
            double result = 0;
            foreach (Request r in memoryDB) {
                result += r.price * r.quantity;
              }
            return result;
        }

        internal double SumOfClientRequests(string id)
        {
            double result = 0;
            var clientR = from Request r in memoryDB
                          where r.clientId == id
                          select r;
            foreach (Request r in clientR)
            {
                result += r.price * r.quantity;
            }
            return result;
        }

        internal double AverageWorth()
        {
            return SumOfRequests() / RequestQuantity();
        }

        internal double AverageClientWorth(string id)
        {
            return SumOfClientRequests(id) / ClientRequestQuantity(id);
        }

        internal void QuantityByName()
        {
            Dictionary<string, int> quantitityByName = new Dictionary<string, int>();
            foreach (Request r in memoryDB)
            {
                if (quantitityByName.ContainsKey(r.name)) {
                    quantitityByName[r.name] += r.quantity;
                } else
                {
                    quantitityByName.Add(r.name, r.quantity);
                }
            }

            foreach (var name in quantitityByName)
            {
                Console.WriteLine(name.Key + " " + name.Value);
            }
        }

        internal void ClientQuantityByName(string id)
        {
            var requests = from r in memoryDB
                           where r.clientId == id
                           select r;

            Dictionary<string, int> quantitityByName = new Dictionary<string, int>();
            foreach (Request r in requests)
            {
                if (quantitityByName.ContainsKey(r.name))
                {
                    quantitityByName[r.name] += r.quantity;
                }
                else
                {
                    quantitityByName.Add(r.name, r.quantity);
                }
            }

            foreach (var name in quantitityByName)
            {
                Console.WriteLine(name.Key + " " + name.Value);
            }

        }

        internal void RequestInSection(double bottom, double top)
        {
            var requests = from r in memoryDB
                          where (r.price * r.quantity) >= bottom &&
                                (r.price * r.quantity) <= top
                          select r;
            foreach (Request r in requests)
            {
                Console.WriteLine("KlientID ZapytId Nazwa Ilość Cena");
                Console.WriteLine(r.clientId + " " + r.requestId + " " + r.name + " " + r.quantity + " " + r.price);
            }
        }
    }
}
