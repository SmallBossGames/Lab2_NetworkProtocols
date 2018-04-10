using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using JSONParser;
using System.IO;

namespace Lab2_NetworkProtocols
{
    class ForCGICalc
    {
        enum Operator { plus, minus, multiple, divide }

        readonly JSONParser.JSONParser parser = new JSONParser.JSONParser();

        const string
            variableName = "QUERY_STRING",
            //Имена необходимых переменных окружения
            val1Name = "value1",
            val2Name = "value2",
            operatName = "operator",
            jsonName = "jsonParam";


        public ForCGICalc()
        {

        }

        public string GetValue()
        {
            var parsedJsonSource = GetVariables()[jsonName];

            var parsedJson = parser.Parse(parsedJsonSource) as JSONObjectCollection;

            double
                val1 = (double)parsedJson[val1Name].GetValue(),
                val2 = (double)parsedJson[val2Name].GetValue();

            string operatStr = (string)parsedJson[operatName].GetValue();
            Operator operat;

            switch (operatStr)
            {
                case "-":
                    operat = Operator.minus;
                    break;
                case "+":
                    operat = Operator.plus;
                    break;
                case "*":
                    operat = Operator.multiple;
                    break;
                case "/":
                    operat = Operator.divide;
                    break;
                default:
                    throw new FormatException();
            }

            var result = CalcThis(val1, val2, operat);

            var resultJson = new JSONObjectCollection
                {
                    new JSONNumber(result)
                };

            return resultJson.ToString();
        }

        double CalcThis(double val1, double val2, Operator operat)
        {
            switch (operat)
            {
                case Operator.plus:
                    return val1 + val2;
                case Operator.minus:
                    return val1 - val2;
                case Operator.multiple:
                    return val1 * val2;
                case Operator.divide:
                    return val1 / val2;
                default:
                    throw new Exception("Incorrect data");
            }
        }

        Dictionary<string, string> GetVariables()
        {
            var varDictonary = new Dictionary<string, string>();

            var inputString = Environment.GetEnvironmentVariable("QUERY_STRING");
            
            var variables = inputString.Split('&');

            foreach (var a in variables)
            {
                var pool = a.Split('=');
                varDictonary.Add(pool[0], pool[1]);
            }

            return varDictonary;
        }


        /// <summary>
        /// Получить документ с результатом вычислений 
        /// {0} - значение 1; {1} - оператор; {2} - значение 2; {3} - сумма
        /// </summary>
        /// <returns></returns>
        string GetDocumentRight(string outSource, string val1, string operat, string val2,  string sum)
        {
            var htmlDoc = File.ReadAllText(outSource);
            return string.Format(htmlDoc, val1, operat, val2, sum);
        }

        /// <summary>
        /// Получить документ со сведениями об ошибке;
        /// {0} - текст ошибки
        /// </summary>
        /// <param name="outSource">HTML документ</param>
        /// <param name="exception">Текст ошибки</param>
        /// <returns></returns>
        string GetDocumentWrong(string outSource, string exception)
        {
            var htmlDoc = File.ReadAllText(outSource);
            return string.Format(htmlDoc, exception);
        }

    }
}
