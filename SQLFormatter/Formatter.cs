using System;
using System.Collections.Generic;
using System.Text;

namespace SQLFormatter
{
    public class Formatter
    {
        private static HashSet<string> _keywords = new HashSet<string>
        {
            "select",
            "from",
            "*"
        };


        public static string FormatSQL(string input)
        {
            
            var start = 0;
            var len = 1;
            var sb = new StringBuilder();
            var inWhiteSpace = false;
            var onNewLine = false;
            while(start + len <= input.Length)
            {
                var subStr = input.Substring(start, len);

                if (_keywords.Contains(subStr))
                {
                    sb.Append(subStr.ToUpper());
                    sb.Append(Environment.NewLine);
                    onNewLine = true;
                    start = start + len;
                    len = 0;
                }
                else if (string.IsNullOrWhiteSpace(subStr))
                {
                    inWhiteSpace = true;
                }
                else if (inWhiteSpace)
                {
                    if (!onNewLine)
                    {
                        sb.Append(" ");   
                    }
                    else
                    {
                        onNewLine = !onNewLine;
                    }

                    start = start -1 + len;
                    len = 0;
                    inWhiteSpace = false;
                }
                else if (subStr.Length > 1 && subStr[^1] == ' ')
                {
                    sb.Append(subStr);
                    start = start + len;
                    len = 0;
                }

                len++;
            }

            return sb.ToString();
        }

    }
}
