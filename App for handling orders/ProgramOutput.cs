using System;

namespace App_for_handling_orders
{
    public class ProgramOutput
    {
        public ProgramOutput()
        {

        }

        public void Greetings ()
        {
            Console.WriteLine("Podaj listę plików: ");
        }

        public void ReportList ()
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
        } 
    }
}