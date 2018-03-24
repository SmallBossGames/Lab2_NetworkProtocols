using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace Lab2_NetworkProtocols
{
    class Program
    {
        static void Main(string[] args)
        {
            ForCGICalc calc = new ForCGICalc("out.html", "Wrong.html");

            //Environment.SetEnvironmentVariable("QUERY_STRING", "value1=1&value2=6&operator=*");

            var output = calc.GetValue();
            Console.Write(calc.GetValue());
            Console.ReadKey();
        }
    }
}
