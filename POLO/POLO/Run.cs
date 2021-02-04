using System;
using System.Collections.Generic;
using System.IO;

namespace POLO
{
    class Run
    {
        static void Main(string[] args)
        {
            POLO polo = new POLO();
            //string path = @"C:\Users\Thomas\Desktop\POLO\POLO FILES\Main.POLO";
            Console.Write("Enter path for main POLO file:\n>>> ");
            string path = Console.ReadLine();
            if (!polo.validPath(path))
            {
                Environment.Exit(0);
            }
            string file = File.ReadAllText(path);
            List<char> command = polo.Command(path);
            int output = polo.ExecuteNumeric(command);
            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
