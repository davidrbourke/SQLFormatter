using FluentAssertions;
using SQLFormatter;
using Xunit;

namespace SQLFormatterTests
{
    public class FormatterTests
    {
        [Fact]
        public void FormatSQL_Simple_UppercaseKeywords()
        {
            var result = Formatter.FormatSQL("select * from dbo.table ");

            result.Should().Be(@"SELECT
*
FROM
dbo.table ");
        }
    }
}
