using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public abstract class JSONCollection : JSONObject, IList<JSONObject>
    {

        private List<JSONObject> list = new List<JSONObject>();

        protected bool isArray;

        public JSONCollection() { }

        public JSONCollection(string name)
        {
            Name = name;
        }

        public JSONCollection(string name, ICollection<JSONObject> jsonObjects)
        {
            Name = name;
            list.AddRange(jsonObjects);
        }

        public JSONCollection(ICollection<JSONObject> jsonObjects)
        {
            list.AddRange(jsonObjects);
        }

        public JSONObject this[int index] { get => list[index]; set => list[index] = value; }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        internal List<JSONObject> List { get => list; set => list = value; }

        public bool IsArray => isArray;

        public abstract char BeginCollection { get; }

        public abstract char EndCollection { get; }

        public void Add(JSONObject item) => list.Add(item);

        public void Clear() => list.Clear();

        public bool Contains(JSONObject item) => list.Contains(item);

        public void CopyTo(JSONObject[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<JSONObject> GetEnumerator() => list.GetEnumerator();

        public override object GetValue() => list;

        public int IndexOf(JSONObject item) => list.IndexOf(item);

        public void Insert(int index, JSONObject item) => list.Insert(index, item);

        public bool Remove(JSONObject item) => list.Remove(item);

        public void RemoveAt(int index) => list.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public override TextWriter WriteTo(TextWriter textWriter)
        {
            base.WriteTo(textWriter);

            textWriter.Write(BeginCollection);

            for(var i = 0; i< Count; i++)
            {
                if (i > 0)
                    textWriter.Write(JSONUtility.valueSeparator);

                this[i].WriteTo(textWriter);
            }
            textWriter.Write(EndCollection);

            return textWriter;
        }
    }
}
