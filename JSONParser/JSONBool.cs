using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JSONParser
{

    public class JSONBool : JSONObject
    {
        bool? value = null;

        public bool? Value { get => value; set => this.value = value; }

        public override object GetValue() => value;

        JSONBool() { }

        public JSONBool(string name, string strValue)
        {
            Name = name;

            value = null;

            if (strValue == null) return;

            strValue = strValue.Trim().ToLower();

            switch (strValue)
            {
                case "null":
                    value = null;
                    break;
                case "true":
                    value = true;
                    break;
                case "false":
                    value = false;
                    break;
                default:
                    throw new FormatException();
            }

        }

        public JSONBool(string name, bool? value)
        {
            Name = name;
            this.value = value;
        }

        public JSONBool(bool? value)
        {
            this.value = value;
        }

        public override TextWriter WriteTo(TextWriter textWriter)
        {
            base.WriteTo(textWriter);

            if (value != null)
                textWriter.Write(value);
            else
                textWriter.Write("null");

            return textWriter;
        }
    }
}
