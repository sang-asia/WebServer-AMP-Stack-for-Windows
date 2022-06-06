using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Libraries
{
    class String
    {
        public static string[] ParamParts(string input)
        {
            List<string> parts = new List<string> { };
            string current = "";
            bool quoted = false;

            input = input.Trim() + " ";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    if (quoted)
                    {
                        current += input.Substring(i, 1);
                        continue;
                    }
                    else
                    {
                        if (current.Length > 0)
                        {
                            parts.Add(current);
                            current = "";
                        }
                    }
                }
                else if (input[i] == '"')
                {
                    if (quoted)
                    {
                        quoted = false;
                    }
                    else
                    {
                        quoted = true;
                    }

                    continue;
                }
                else
                {
                    current += input.Substring(i, 1);
                }
            }

            return parts.ToArray();
        }
    }
}
