using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace App_for_handling_orders
{
    public class HandleInput
    {
        public string[] readFiles()
        {
            string result = Console.ReadLine();
            return result.Trim().Split(null);
        }

        public List<Request> ParseFiles(string[] files )
        {
            List<Request> result = new List<Request>();
            Parser parser = new Parser();

            foreach (string file in files)
            {
                try
                {
                    string format = file.Split('\\').Last<string>().Split('.')[1];

                    switch(format)
                    {
                        case "csv":
                            result.AddRange(parser.CSVParse(file));
                            break;

                        case "xml":
                            result.AddRange(parser.XMLParse(file));
                            break;

                        case "json":
                            result.AddRange(parser.JSONParse(file));
                            break;

                        default:
                            throw new Exception("Nie prawidłowy format " + format + " ");

                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Błąd w formacie pliku " +  " " + file);
                }
            }
            return result;
        }

        internal void ReadRaport(List<Request> memoryDB)
        {
            ReportController reportController = new ReportController(memoryDB);
            ProgramOutput programOutput = new ProgramOutput(memoryDB);
            string id;
            string sort = "clientId";
            while (true)
            {
                string letter = Console.ReadLine();

                switch (letter)
                {
                    case "a":
                        Console.WriteLine(reportController.RequestQuantity());
                        break;
                    case "b":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        Console.WriteLine(reportController.ClientRequestQuantity(id));
                        break;
                    case "c":
                        Console.WriteLine(reportController.SumOfRequests());
                        break;
                    case "d":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        Console.WriteLine(reportController.SumOfClientRequests(id));
                        break;
                    case "e":
                        programOutput.memoryDB = reportController.Sort(sort);
                        programOutput.RequestList();
                        break;
                    case "f":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        programOutput.memoryDB = reportController.Sort(sort);
                        programOutput.ClientRequestList(id);
                        break;
                    case "g":
                        Console.WriteLine(reportController.AverageWorth());
                        break;
                    case "h":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        Console.WriteLine(reportController.AverageClientWorth(id));
                        break;
                    case "i":
                        reportController.QuantityByName();
                        break;
                    case "j":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        reportController.ClientQuantityByName(id);
                        break;
                    case "k":
                        Console.WriteLine("Najniższa cena");
                        double bottom = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.WriteLine("Najwyższa cena");
                        double top = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        reportController.RequestInSection(bottom, top);
                        break;
                    case "l":
                        sort = NewSort(sort);
                        reportController.Sort(sort);
                        break;
                        
                }
            }

        }

        private string NewSort(string oldsort)
        {
            Console.WriteLine("Wybierz odpowiednią literę");
            Console.WriteLine("a klient Id");
            Console.WriteLine("b zapyt Id");
            Console.WriteLine("c nazwa");
            Console.WriteLine("d ilość");
            Console.WriteLine("e cena");
            string letter = Console.ReadLine();

            string result;

            switch(letter)
            {
                case "a":
                    result = "clientId";
                    break;
                case "b":
                    result = "requestId";
                    break;
                case "c":
                    result = "name";
                    break;
                case "d":
                    result = "quantity";
                    break;
                case "e":
                    result = "price";
                    break;
                default:
                    result = oldsort;
                    break;
            }

            return result;
        }
        
    }
}
