using System;
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

            programOutput.Greetings();
            string[] filesToRead = handleInput.readFiles(); // example D:\Users\MarkG\source\repos\test1.csv;
            handleInput.ParseFiles(filesToRead);
            programOutput.ReportList();
            handleInput.ReadRaport();
            Console.Read();

        }
    }

}
