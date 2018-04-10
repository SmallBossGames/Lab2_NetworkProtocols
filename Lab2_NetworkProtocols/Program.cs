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
            ForCGICalc calc = new ForCGICalc();
            var value = calc.GetValue();
            var doc = new StringBuilder();

            doc.Append("Content-Type: application/json")
                .AppendLine()
                .AppendLine()
                .Append(value);

            File.AppendAllText("D:/hui.txt", doc.ToString());

            Console.Write(doc.ToString());
        }
    }
}
