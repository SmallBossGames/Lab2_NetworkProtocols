using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JSONString:JSONObject
    {
        string value = null;

        public string Value
        {
            get => value;
            set => this.value = value ?? string.Empty;
        }

        public JSONString() { }

        public JSONString(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public JSONString(string name)
        {
            Name = name;
        }

        public override object GetValue() => value;

        public override TextWriter WriteTo(TextWriter textWriter)
        {
            base.WriteTo(textWriter);

            textWriter.Write(JSONUtility.quote);
            textWriter.Write(Value);
            textWriter.Write(JSONUtility.quote);

            return textWriter;
        }
    }
}
