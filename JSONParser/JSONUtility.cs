using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace JSONParser
{
    public static class JSONUtility
    {
        public const char
            quote = '"',
            nameSeparator = ':',
            valueSeparator = ',',
            beginArray = '[',
            endArray = ']',
            beginObject = '{',
            endObject = '}',
            space = ' ';

        internal static readonly CultureInfo cultureInfo = new CultureInfo("en-US", false);

    }
}
