using System;
using System.Collections.Generic;
using System.Text;

namespace SQLFormatter
{
    public class QueryFormatter
    {
        private static HashSet<string> _keywords = new HashSet<string>
        {
            "select",
            "from",
            "where"
        };

        public static string Format(string unformattedSQL)
        {
            var posA = 0;
            var posB = 1;

            // todo: remove all new lines 1st


            var fsb = new StringBuilder();
            var subStrLen = posB - posA;
            while (posA + subStrLen <= unformattedSQL.Length)
            {

                var subStr = unformattedSQL.Substring(posA, subStrLen);
                
                // upper case keywords
                foreach(var keyword in _keywords)
                {
                    var iOfKeyword = subStr.IndexOf(keyword);
                    if (iOfKeyword != -1)
                    {
                        if (iOfKeyword > 0)
                            fsb.AppendLine(subStr.Substring(0, iOfKeyword-1));

                        fsb.AppendLine(subStr.Substring(iOfKeyword, keyword.Length).ToUpper());
                        var nextPosB = GetNextStartPositionAfterNewline(unformattedSQL, posB + 1);

                        posA = nextPosB;
                        posB = posA + 1;
                        break;
                    }
                }

                // Handle comma
                if (subStr[^1] == ',')
                {
                    fsb.AppendLine(subStr.Substring(0, subStrLen - 1));
                    fsb.Append(",");

                    // Need to remove whitespace after comma
                    var nextPosB = GetNextStartPositionAfterNewline(unformattedSQL, posB + 1);
                    
                    posA = nextPosB;
                    posB = posA+1;
                }
                                
                posB++;

                subStrLen = posB - posA;

                // Check if at end
                if (posA + subStrLen == unformattedSQL.Length)
                {
                    fsb.Append(unformattedSQL.Substring(posA, subStrLen));
                }
            }

            var formattedSQL = fsb.ToString();
            return formattedSQL;
        }

        private static int GetNextStartPositionAfterNewline(string unformattedSQL, int nextPosB)
        {
            while (nextPosB < unformattedSQL.Length - 1 && unformattedSQL[nextPosB] == ' ')
            {
                nextPosB++;
            }
            return nextPosB;
        }

    }
}
