﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string[] filesToRead = handleInput.readFiles(); // example D:\Users\MarkG\source\repos\test1.csv
            memoryDB = handleInput.ParseFiles(filesToRead); // D:\Users\MarkG\source\repos\test2.xml
                                                            // D:\Users\MarkG\source\repos\test3.json
            programOutput.ReportList();
            handleInput.ReadRaport(memoryDB);
            Console.Read();

        }
    }

}
