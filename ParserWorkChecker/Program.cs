using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTML_Parser;
namespace ParserWorkChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Display(Parser.GetInstance().ParseProfessions());
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
    }
}
