using System;
using System.Collections.Generic;
using System.Text;

namespace POLO
{
    class Errors
    {
        public static void InvalidPath(string path) { Console.WriteLine($@"Error: Path {path} was not found."); }
        public static void InvalidFileExtension(string path) { Console.WriteLine($@"Error: Path {path} is not a valid .POLO file."); }
    }
}
