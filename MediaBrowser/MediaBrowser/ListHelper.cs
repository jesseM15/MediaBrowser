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

        public static SortedDictionary<string, string> OrderByLastNames(List<string> inputs)
        {
            SortedDictionary<string, string> output = new SortedDictionary<string, string>();
            foreach (string input in inputs)
            {
                output.Add(ConvertToLastNameFirstFormat(input), input);
            }
            return output;
        }

        private static string ConvertToLastNameFirstFormat(string input)
        {
            List<string> names = input.Split(' ').ToList();
            string output = names.Last();
            if (names.Count > 1)
            {
                output += ", ";
                for (int n = 0; n < names.Count - 1; n++)
                {
                    output += names[n];
                    if (n < names.Count - 1)
                    {
                         names[n] += " ";
                    }
                }
            }
            return output;
        }

        public static List<string> RemoveParentheses(List<string> input)
        {
            for (int n = 0; n < input.Count; n++)
            {
                while (input[n].Contains("("))
                {
                    for (int o = 0; o < input[n].Length; o++)
                    {
                        if (input[n].Substring(o, 1).Equals("("))
                        {
                            input[n] = input[n].Remove(o);
                        }
                    }
                }
            }
            return input;
        }

    }
}
