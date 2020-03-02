using System;
using System.Collections.Generic;
using System.IO;
using HTML_Parser;
namespace ParserWorkChecker
{
    static class Program
    {
        static void Main(string[] args)
        {
            Display(Parser.GetInstance().ParseProffessions());
        }

        static void Display(Dictionary<string, Dictionary<string, string>> outItem) {
            if (outItem.Count == 0) {
                Console.WriteLine("Данные не получены");
                Console.ReadKey();
                return;
            }

            foreach (var item in outItem) {
                Console.WriteLine(item.Key);
                foreach (var subItem in item.Value) {
                    Console.WriteLine("└────" + subItem.Key + " --> " + subItem.Value);
                }
            }

            Console.ReadKey();
        }
        static void Display(Dictionary<string, string> outItem)
        {
            if (outItem.Count == 0)
            {
                Console.WriteLine("Данные не получены");
                Console.ReadKey();
                return;
            }

            foreach (var item in outItem)
            {
                Console.WriteLine(item.Key + " --> " + item.Value);   
            }

            Console.ReadKey();
        }
        static void WriteFile(Dictionary<string, Dictionary<string, string>> outItem, string filename) {
            StreamWriter outstream = new StreamWriter(filename);
            if (outItem.Count == 0)
            {
                outstream.WriteLine("Данные не получены");
                outstream.Close();
                return;
            }

            foreach (var item in outItem)
            {
                outstream.WriteLine(item.Key);
                foreach (var subItem in item.Value)
                {
                    outstream.WriteLine("└────" + subItem.Key + " --> " + subItem.Value);
                }
           
            }
            outstream.Close();
        }
    }
}
