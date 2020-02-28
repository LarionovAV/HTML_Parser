using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HTML_Parser;
namespace ParserWorkChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteFile(Parser.GetInstance().ParseProfessions(), "output.txt");
        }

        static void Display(Dictionary<string, Dictionary<string, string>> outItem) {
            if (outItem == null) {
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
        static void WriteFile(Dictionary<string, Dictionary<string, string>> outItem, string filename) {
            StreamWriter outstream = new StreamWriter(filename);
            if (outItem == null)
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
