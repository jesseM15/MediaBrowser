using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBrowser
{
    public static class ListHelper
    {
        public static string CreateCommaSeperatedString(List<string> input)
        {
            string output = "";
            for (int i = 0; i < input.Count; i++)
            {
                output += input[i];
                if (i + 1 != input.Count)
                {
                    output += ",";
                }
            }
            return output;
        }

        public static List<string> ListTrim(List<string> input)
        {
            for (int n = 0; n < input.Count; n++)
            {
                input[n] = input[n].Trim();
            }
            return input;
        }
    }
}
