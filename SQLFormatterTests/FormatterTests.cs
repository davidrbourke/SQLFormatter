using FluentAssertions;
using SQLFormatter;
using Xunit;

namespace SQLFormatterTests
{
    public class FormatterTests
    {

        [Fact]
        public void Format_Commas_OnNewLine()
        {
            var formatted = QueryFormatter.Format("select cola, colb, colc from dbo.table where cola = \"test\"");
            Assert.Equal(@"SELECT
cola
,colb
,colc
FROM
dbo.table
WHERE
cola = ""test""", formatted);
        }
    }
}
