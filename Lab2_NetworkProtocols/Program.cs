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

            var pathOr = Directory.GetCurrentDirectory();
            string pathRight = Path.Combine(pathOr, "templateRight.html");
            string pathWrong = Path.Combine(pathOr, "templateWrong.html");
            ForCGICalc calc = new ForCGICalc(pathRight, pathWrong);

            //Environment.SetEnvironmentVariable("QUERY_STRING", "value1=1&value2=6&operator=*");

            var output = calc.GetValue();
            Console.Write(calc.GetValue());

        }
    }
}
