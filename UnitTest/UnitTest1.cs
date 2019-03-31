using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App_for_handling_orders;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CSVParse()
        {
            Parser parser = new Parser();

            List<Request> result = parser.CSVParse("test1.csv");
            ReportController reportContoller = new ReportController(result);

            Assert.AreEqual(4, reportContoller.RequestQuantity());
            Assert.AreEqual(3, reportContoller.ClientRequestQuantity("1"));
            Assert.AreEqual(125, reportContoller.SumOfRequests());
            Assert.AreEqual(115, reportContoller.SumOfClientRequests("1"));
            Assert.AreEqual(31.25, reportContoller.AverageWorth());
            Assert.AreEqual(10, reportContoller.AverageClientWorth("2"));
            Assert.AreEqual(8, reportContoller.QuantityByName()["Chleb"]);
            Assert.AreEqual(7, reportContoller.ClientQuantityByName("1")["Chleb"]);

            Request actualRequest = reportContoller.RequestInSection(60, 80);
            Assert.AreEqual("1", actualRequest.clientId );
            Assert.AreEqual(2, actualRequest.requestId );
            Assert.AreEqual("Chleb", actualRequest.name );
            Assert.AreEqual(5, actualRequest.quantity);
            Assert.AreEqual(15, actualRequest.price);
        }

        [TestMethod]
        public void XMLParse()
        {
            Parser parser = new Parser();

            List<Request> result = parser.XMLParse("test2.xml");
            ReportController reportContoller = new ReportController(result);

            Assert.AreEqual(4, reportContoller.RequestQuantity() );
            Assert.AreEqual(1, reportContoller.ClientRequestQuantity("4"));
            Assert.AreEqual(134, reportContoller.SumOfRequests());
            Assert.AreEqual(131, reportContoller.SumOfClientRequests("3") );
            Assert.AreEqual(33.5, reportContoller.AverageWorth() );
            Assert.AreEqual(3, reportContoller.AverageClientWorth("4") );
            Assert.AreEqual(1, reportContoller.QuantityByName()["Woda"]);
            Assert.AreEqual(7, reportContoller.ClientQuantityByName("3")["Chleb"]);

            Request actualRequest = reportContoller.RequestInSection(25, 27);
            Assert.AreEqual(actualRequest.clientId, "3");
            Assert.AreEqual(1, actualRequest.requestId );
            Assert.AreEqual("Ser", actualRequest.name );
            Assert.AreEqual(2, actualRequest.quantity );
            Assert.AreEqual(13, actualRequest.price );
            
        }

        [TestMethod]
        public void JSONParse()
        {
            Parser parser = new Parser();

            List<Request> result = parser.JSONParse("test3.json");
            ReportController reportContoller = new ReportController(result);

            Assert.AreEqual(reportContoller.RequestQuantity(), 4);
            Assert.AreEqual(reportContoller.ClientRequestQuantity("6"), 2);
            Assert.AreEqual(reportContoller.SumOfRequests(), 125);
            Assert.AreEqual(reportContoller.SumOfClientRequests("7"), 10);
            Assert.AreEqual(reportContoller.AverageWorth(), 31.25);
            Assert.AreEqual(reportContoller.AverageClientWorth("5"), 10);
            Assert.AreEqual(reportContoller.QuantityByName()["Mleko"], 5);
            Assert.AreEqual(reportContoller.ClientQuantityByName("6")["Chleb"], 2);

            Request actualRequest = reportContoller.RequestInSection(74, 76);
            Assert.AreEqual(actualRequest.clientId, "6");
            Assert.AreEqual(actualRequest.requestId, 2);
            Assert.AreEqual(actualRequest.name, "Mleko");
            Assert.AreEqual(actualRequest.quantity, 5);
            Assert.AreEqual(actualRequest.price, 15);

        }

    }
}
