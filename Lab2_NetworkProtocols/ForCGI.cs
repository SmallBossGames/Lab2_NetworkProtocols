using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace Lab2_NetworkProtocols
{
    class ForCGICalc
    {
        enum Operator { plus, minus, multiple, divide }

        readonly string 
            outDocumentSource,
            wrongDocumentSource;
        const string
            variableName = "QUERY_STRING",
            //Имена необходимых переменных окружения
            val1Name = "value1",
            val2Name = "value2",
            operatName = "operator";


        public ForCGICalc(string outDocumentSource, string wrongDocumentSource)
        {
            this.outDocumentSource = outDocumentSource;
            this.wrongDocumentSource = wrongDocumentSource;

        }

        public string GetValue()
        {
            try
            {
                var variables = GetVariables();

                var val1Str = variables[val1Name];
                var val2Str = variables[val2Name];
                string operatString = variables[operatName];

                var val1 = Double.Parse(val1Str);
                var val2 = Double.Parse(val2Str);

                Operator operat;
                switch (operatString)
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
                        throw new Exception();
                }

                var result = CalcThis(val1, val2, operat);
                return GetDocumentRight(outDocumentSource, val1Str, val2Str, operatString, result.ToString());
            }
            catch
            {
                return GetDocumentWrong(wrongDocumentSource, "Data is incorrect");
            }
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

            var inputString = HttpUtility.UrlDecode(Environment.GetEnvironmentVariable("QUERY_STRING"));
            
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
