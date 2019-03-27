using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App_for_handling_orders;
using System.Collections.Generic;
using text2.xml;

namespace AppTest
{
    [TestClass]
    public class UnitTest1
    {
        HandleInput handleInput = new HandleInput();
        ProgramOutput programOutput = new ProgramOutput();
        List<Request> memoryDB;
        //string[] filesToRead = { "@test2.xml" }; 

        [TestMethod]
        public void TestXMLParser()
        {
            string file = "D:\Users\MarkG\source\repos\test1.csv";
            memoryDB = handleInput.XMLParse(file);
        }
    }
}
