using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic.FileIO;

namespace App_for_handling_orders
{
    public class Parser
    {
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
                catch (Exception )
                {
                    Console.WriteLine("Błąd w formacie danych w pliku " + file);
                }
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
                catch (Exception )
                {
                    Console.WriteLine("Błąd w formacie danych w pliku " + file );
                }
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
            catch (Exception )
            {
                Console.WriteLine("Błąd w formacie danych w pliku " + file);
            }

            return result;
        }
    }
}
