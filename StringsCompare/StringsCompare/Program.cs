using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter amounts of iterations to beep ");
            int nrc = int.Parse(Console.ReadLine());
            Console.WriteLine("Do you need strings output? Y/N");
            string answer = Console.ReadLine();
            bool tm = false;
            if (answer == "Y" || answer == "y" || answer == "YES" || answer == "Yes" || answer == "yes") tm = true;
            else if (answer == "N" || answer == "n" || answer == "NO" || answer == "No" || answer == "no") tm = false;
            else Console.WriteLine("Misunderstood. Interpreted as N");
            InlineCheck IC = new InlineCheck(nrc, tm);
            string line;
            int counter = 0;

            DateTime start = new DateTime();
            DateTime finish = new DateTime();
            start = DateTime.Now;

            using (StreamReader sr = new StreamReader("..\\..\\generated.txt"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    IC.addLine(line);
                    counter++;
                }
            }

            finish = DateTime.Now;
            Console.WriteLine("" + counter + " strings: " + (finish - start));
            Console.ReadKey();
        }
    }
}