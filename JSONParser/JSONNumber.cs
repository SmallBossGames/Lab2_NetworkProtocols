using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JSONNumber : JSONObject
    {
        private double value;

        public double Value { get => value; set => this.value = value; }

        public override object GetValue() => value;

        public JSONNumber()
        {
            value = 0.0;
        }

        public JSONNumber(string name)
        {
            Name = name;
            Value = 0.0;
        }

        public JSONNumber(string name, double value)
        {
            this.value = value;
            this.Name = name;
        }

        public JSONNumber(double value)
        {
            Value = value;
        }

        public override TextWriter WriteTo(TextWriter textWriter)
        {

            base.WriteTo(textWriter)
                .Write(Value.ToString("g", JSONUtility.cultureInfo));

            return textWriter;
        }
    }
}
