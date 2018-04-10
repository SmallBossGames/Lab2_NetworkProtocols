using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JSONArrayCollection : JSONCollection
    {
        public JSONArrayCollection()
        {
        }

        public JSONArrayCollection(string name) : base(name)
        {
        }

        public JSONArrayCollection(ICollection<JSONObject> jsonObjects) : base(jsonObjects)
        {
        }

        public JSONArrayCollection(string name, ICollection<JSONObject> jsonObjects) : base(name, jsonObjects)
        {
        }

        public override char BeginCollection => JSONUtility.beginArray;

        public override char EndCollection => JSONUtility.endArray;
    }
}
