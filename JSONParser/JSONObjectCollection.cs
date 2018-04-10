using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JSONObjectCollection : JSONCollection
    {
        public JSONObjectCollection()
        {
        }

        public JSONObjectCollection(string name) : base(name)
        {
        }

        public JSONObjectCollection(ICollection<JSONObject> jsonObjects) : base(jsonObjects)
        {
        }

        public JSONObjectCollection(string name, ICollection<JSONObject> jsonObjects) : base(name, jsonObjects)
        {
        }

        public override char BeginCollection => JSONUtility.beginObject;

        public override char EndCollection => JSONUtility.endObject;

        public JSONObject this[string name]
        {
            get
            {
                foreach (var obj in this)
                    if (obj.Name == name)
                        return obj;

                return null;
            }
        }
    }
}
