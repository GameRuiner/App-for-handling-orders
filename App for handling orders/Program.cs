using System;
using System.Collections.Generic;

namespace App_for_handling_orders
{
    class Program
    {

        static void Main(string[] args)
        {
            HandleInput handleInput = new HandleInput();
            ProgramOutput programOutput = new ProgramOutput();
            List<Request> memoryDB;

            programOutput.Greetings();
            string[] filesToRead = handleInput.readFiles(); //  D:\Users\MarkG\source\repos\test1.csv  D:\Users\MarkG\source\repos\test2.xml D:\Users\MarkG\source\repos\test3.json   
            memoryDB = handleInput.ParseFiles(filesToRead); 

            programOutput.ReportList();
            handleInput.ReadRaport(memoryDB);
            Console.Read();

        }
    }

}
