using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    public class JSONParser
    {
        int pos;

        string parseText;

        char CurrentChar => parseText[pos];

        bool IsEndText => pos >= parseText.Length;

        public JSONObject Parse(string text)
        {
            pos = 0;

            if (text == null)
                throw new FormatException();

            parseText = text.Trim();

            if (text == string.Empty)
                throw new FormatException();

            return ParseWithoutName();
        }

        private JSONObject ParseWithoutName()
        {
            SkipWhiteSpace();

            if (CurrentChar == JSONUtility.beginArray || CurrentChar == JSONUtility.beginObject)
                return ParseCollection();

            if (CurrentChar == JSONUtility.quote)
                return ParseString();

            if (Char.IsDigit(CurrentChar) || CurrentChar == '-')
                return ParseNumber();

            if (CurrentChar == 't' || CurrentChar == 'f' || CurrentChar == 'n')
                return ParseBool();

            if (CurrentChar == 'T' || CurrentChar == 'F' || CurrentChar == 'N')
                return ParseBool();

            throw new FormatException();
        }

        private JSONString ParseString()
        {
            if (CurrentChar != JSONUtility.quote)
                throw new FormatException();

            var stringBuilder = new StringBuilder();

            pos++;

            while(CurrentChar!=JSONUtility.quote && parseText[pos-1] != '\\')
            {
                if (IsEndText) throw new FormatException();

                stringBuilder.Append(CurrentChar);

                pos++;
            }

            pos++;

            var result = new JSONString
            {
                Value = stringBuilder.ToString()
            };

            return result;
        }

        private JSONBool ParseBool()
        {
            if (CurrentChar != 't' && CurrentChar != 'f' && CurrentChar != 'n' 
                && CurrentChar != 'T' && CurrentChar != 'F' && CurrentChar != 'N')
                throw new FormatException();

            var sb = new StringBuilder();

            while((CurrentChar>='a'&& CurrentChar<='z') || (CurrentChar >= 'A' && CurrentChar <= 'Z'))
            {
                sb.Append(CurrentChar);
                pos++;
            }

            var pool = sb.ToString().Trim().ToLower();

            if (pool != "true" && pool != "false" && pool != "null")
                throw new FormatException();

            return new JSONBool(null, pool);
        }

        private JSONNumber ParseNumber()
        {
            var capturedValue = CaptureNumber();

            var value = double.Parse(capturedValue, JSONUtility.cultureInfo);

            return new JSONNumber(value);
        }

        private JSONCollection ParseCollection()
        {
            bool isArray;
            JSONCollection result;

            SkipWhiteSpace();

            switch (CurrentChar)
            {
                case JSONUtility.beginObject:
                    isArray = false;
                    result = new JSONObjectCollection();
                    break;
                case JSONUtility.beginArray:
                    isArray = true;
                    result = new JSONArrayCollection();
                    break;
                default:
                    throw new FormatException();
            }

            do
            {
                pos++;
                SkipWhiteSpace();

                string name = null;

                if (!isArray)
                    name = ParseName();

                var obj = ParseWithoutName();

                if (obj == null)
                    throw new Exception();

                if (!isArray)
                    obj.Name = name;

                result.Add(obj);

                SkipWhiteSpace();
            }
            while (CurrentChar == JSONUtility.valueSeparator);

            if(isArray)
            {
                if (CurrentChar != JSONUtility.endArray)
                    throw new FormatException();
            }
            else
            {
                if (CurrentChar != JSONUtility.endObject)
                    throw new FormatException();
            }

            pos++;

            return result;
        }

        private string ParseName()
        {
            if(IsEndText)
                throw new FormatException("Cannot find object item's name.");

            if (CurrentChar != JSONUtility.quote)
                throw new FormatException();

            var name = new StringBuilder();

            pos++;

            while (CurrentChar != JSONUtility.quote && parseText[pos - 1] != '\\')
            {
                if (IsEndText) throw new FormatException();

                name.Append(CurrentChar);

                pos++;
            }

            pos++;
            SkipWhiteSpace();

            if (IsEndText || CurrentChar != JSONUtility.nameSeparator)
                throw new FormatException();

            pos++;

            return name.ToString();
        }

        private void SkipWhiteSpace()
        {
            while (!IsEndText && Char.IsWhiteSpace(CurrentChar))
                pos++;

            if (IsEndText)
                throw new FormatException();
        }

        private string CaptureNumber()
        {
            byte dotCount = 0;
            var result = new StringBuilder();

            if (!char.IsNumber(CurrentChar) && CurrentChar != '-')
                throw new FormatException();

            if(CurrentChar == '-')
            {
                pos++;
                result.Append(CurrentChar);
            }

            for(;dotCount<2 && (char.IsDigit(CurrentChar)|| CurrentChar=='.'); pos++)
            {
                if (CurrentChar == '.')
                    dotCount++;

                result.Append(CurrentChar);
            }

            if (dotCount > 1)
                throw new FormatException();

            return result.ToString();
        }
    }
}
