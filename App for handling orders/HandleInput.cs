using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;


namespace App_for_handling_orders
{
    public class HandleInput
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
                            result.AddRange(CSVParse(file));
                            break;

                        case "xml":
                            result.AddRange(XMLParse(file));
                            break;

                        case "json":
                            result.AddRange(JSONParse(file));
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
                        reportController.Sort(sort);
                        reportController.QuantityByName();
                        break;
                    case "j":
                        Console.WriteLine("Podaj id klienta");
                        id = Console.ReadLine();
                        reportController.Sort(sort);
                        reportController.ClientQuantityByName(id);
                        break;
                    case "k":
                        Console.WriteLine("Najniższa cena");
                        double bottom = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.WriteLine("Najwyższa cena");
                        double top = Double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        reportController.Sort(sort);
                        reportController.RequestInSection(bottom, top);
                        break;
                    case "l":
                        sort = NewSort(sort);
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

        class TempObj
        {
            List<Request> requests { get; set; }
        }

        public List<Request> JSONParse(string file)
        {
            List<Request> result = new List<Request>();
            try
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();

                    TempObj tempObj = new TempObj();

                    JObject jObject = JObject.Parse(json);
                    IList<JToken> results = jObject["requests"].Children().ToList();
                    foreach (JToken jResult in results)
                    {
                        Request request = jResult.ToObject<Request>();
                        result.Add(request);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd w formacie danych w pliku " + file + " " + e.Message);
            }

            return result;
        }

        public List<Request> XMLParse(string file)
        {
            List<Request> result = new List<Request>();
            XDocument doc = XDocument.Load(file);

            foreach (XElement el in doc.Root.Elements())
            {
                try
                {
                    string ClientId = el.Element("clientId").Value;
                    long RequestId = long.Parse(el.Element("requestId").Value);
                    string Name = el.Element("name").Value;
                    int Quantity = int.Parse(el.Element("quantity").Value);
                    double Price = Double.Parse(el.Element("price").Value, CultureInfo.InvariantCulture);
                    Request request = new Request(ClientId, RequestId, Name, Quantity, Price);
                    result.Add(request);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Błąd w formacie danych w pliku " + file + " " + e.Message);
                }
            }
            return result;
        }

        public List<Request> CSVParse(string file)
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
                        Console.WriteLine("Błąd w formacie danych w pliku " + e.Message);
                    }
            }
            return result;
            }
    }
}
