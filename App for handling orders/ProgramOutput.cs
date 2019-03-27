using System;
using System.Collections.Generic;
using System.Linq;

namespace App_for_handling_orders
{
    public class ProgramOutput
    {
        public List<Request> memoryDB { get;  set; }

        public ProgramOutput()
        {
            
        }

        public ProgramOutput(List<Request> memoryDB)
        {
            this.memoryDB = memoryDB;
        }

        public void Greetings ()
        {
            Console.WriteLine("Podaj listę plików: ");
        }

        public void ReportList()
        {
            Console.WriteLine("a. Ilość zamówień");
            Console.WriteLine("b. Ilość zamówień dla klienta o wskazanym identyfikatorze");
            Console.WriteLine("c. Łączna kwota zamówień");
            Console.WriteLine("d. Łączna kwota zamówień dla klienta o wskazanym identyfikatorze");
            Console.WriteLine("e. Lista wszystkich zamówień");
            Console.WriteLine("f. Lista zamówień dla klienta o wskazanym identyfikatorze");
            Console.WriteLine("g. Średnia wartość zamówienia");
            Console.WriteLine("h. Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze");
            Console.WriteLine("i.Ilość zamówień pogrupowanych po nazwie");
            Console.WriteLine("j. Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze");
            Console.WriteLine("k. Zamówienia w podanym przedziale cenowym");
            Console.WriteLine("l. Ustalenia sortowania ");
        }

        internal void RequestList()
        {
            Console.WriteLine("KlientID ZapytId Nazwa Ilość Cena");
            foreach (Request r in memoryDB)
            {
                Console.WriteLine(r.clientId + " " + r.requestId + " " + r.name + " " + r.quantity + " " + r.price);
            }
        }

        internal void ClientRequestList(string id)
        {
            Console.WriteLine("KlientID ZapytId Nazwa Ilość Cena");
            var requests = from Request r in memoryDB
                           where r.clientId == id
                           select r;
            foreach (Request r in requests)
            {
                Console.WriteLine(r.clientId + " " + r.requestId + " " + r.name + " " + r.quantity + " " + r.price);
            }
        }


    }
}