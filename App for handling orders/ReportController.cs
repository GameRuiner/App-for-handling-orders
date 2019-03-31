using System;
using System.Collections.Generic;
using System.Linq;

namespace App_for_handling_orders
{
    public class ReportController
    {
        List<Request> memoryDB;

        public List<Request> Sort(string sort)
        {
            switch(sort)
            {
                case "clientId":
                    memoryDB = memoryDB.OrderBy(o => o.clientId).ToList();
                    break;
                case "requestId":
                    memoryDB = memoryDB.OrderBy(o => o.requestId).ToList();
                    break;
                case "name":
                    memoryDB = memoryDB.OrderBy(o => o.name).ToList();
                    break;
                case "quantity":
                    memoryDB = memoryDB.OrderBy(o => o.quantity).ToList();
                    break;
                case "price":
                    memoryDB = memoryDB.OrderBy(o => o.price).ToList();
                    break;
            }
            return memoryDB;
        }

        public ReportController(List<Request> memoryDB)
        {
            this.memoryDB = memoryDB;
        }

        public int RequestQuantity()
        {
            return memoryDB.Count;
        }

        public int ClientRequestQuantity(string id)
        {
            var selectedRequest = from Request r in memoryDB 
                                  where r.clientId == id 
                                  select r;
            return selectedRequest.Count();
        }

        public double SumOfRequests()
        {
            double result = 0;
            foreach (Request r in memoryDB) {
                result += r.price * r.quantity;
              }
            return result;
        }

        public double SumOfClientRequests(string id)
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

        public double AverageWorth()
        {
            return SumOfRequests() / RequestQuantity();
        }

        public double AverageClientWorth(string id)
        {
            return SumOfClientRequests(id) / ClientRequestQuantity(id);
        }

        public Dictionary<string, int> QuantityByName()
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

            return quantitityByName;
        }

        public Dictionary<string, int> ClientQuantityByName(string id)
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

            return quantitityByName;
        }

        public Request RequestInSection(double bottom, double top)
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

            return requests.First();
        }
    }
}
