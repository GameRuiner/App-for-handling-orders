using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace App_for_handling_orders
{
    class HandleInput
    {
        public string[] readFiles()
        {
            string result = Console.ReadLine();
            return result.Split(' ');
        }

        public List<Request> ParseFiles(string[] files )
        {
            List<Request> result = new List<Request>();

            foreach (string file in files)
            {
                try
                {
                    string format = file.Split('\\').Last<string>().Split('.')[1];

                    switch(format)
                    {
                        case "csv":
                            //result.Concat( CSVParse(file));
                            result.AddRange(CSVParse(file));
                            break;

                        case "xml":
                           // result.Add(XMLParse(file));
                            break;

                        case "json":
                          //  result.Add(JSONParse(file));
                            break;

                        default:
                            throw new Exception("nie prawidłowy format " + format + " ");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Błąd w formacie pliku " +  " " + e);
                }
            }
            return result;
        }

        internal void ReadRaport(List<Request> memoryDB)
        {
            ReportController reportController = new ReportController(memoryDB);
            ProgramOutput programOutput = new ProgramOutput(memoryDB);
            string id;
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
                        programOutput.RequestList();
                        break;
                    case "f":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
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
                }
            }

        }

        private List<Request> JSONParse(string file)
        {
            throw new NotImplementedException();
        }

        private List<Request> XMLParse(string file)
        {
            throw new NotImplementedException();
        }

        private List<Request> CSVParse(string file)
        {
            List<Request> result = new List<Request>(); 
            TextFieldParser parser = new TextFieldParser(file);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                
                string[] fields = parser.ReadFields();
                if (fields[0] == "Client_Id")
                    continue;

                try
                    {
                        string ClientId = fields[0];
                        long RequestId = long.Parse(fields[1]);
                        string Name = fields[2];
                        int Quantity = int.Parse(fields[3]);
                        double Price = Double.Parse(fields[4], CultureInfo.InvariantCulture);
                        Request request = new Request(ClientId, RequestId, Name, Quantity, Price);
                        result.Add(request);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Błąd w formacie danych w pliku " + e);
                    }
            }
            return result;
            }
    }
}
