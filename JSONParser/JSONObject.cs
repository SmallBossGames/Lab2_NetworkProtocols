using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JSONParser
{
    public abstract class JSONObject
    {
        private string name = null;

        public string Name
        {
            get => name ?? string.Empty;
            set => name = value ?? string.Empty;
        }

        public abstract object GetValue();

        public override string ToString()
        {
            StringWriter stringWriter = new StringWriter();
            return WriteTo(stringWriter).ToString();
        }

        public virtual TextWriter WriteTo(TextWriter textWriter)
        {
            if (Name != string.Empty)
            {
                textWriter.Write(JSONUtility.quote);
                textWriter.Write(Name);
                textWriter.Write(JSONUtility.quote);
                textWriter.Write(JSONUtility.nameSeparator);
            }
            return textWriter;
        }
    }
}
