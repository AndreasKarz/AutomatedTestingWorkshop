using System.Data;
using System.Text.RegularExpressions;


namespace FunkyBDD.SxS.Framework.Specflow
{
    public static class SpecflowHelpers
    {
        private const string RegexPrefix = "regex:";

        public static List<string> CompareTables(Table sTable, DataTable tTable)
        {
            var result = new List<string>();

            try
            {
                for (var i = 0; i < sTable.Rows.Count; i++)
                {
                    for (var j = 0; j < sTable.Header.Count; j++)
                    {
                        var expectedValue = sTable.Rows[i].ElementAt(j).Value;
                        expectedValue = expectedValue.Replace("{nl}", "\r\n");
                        var value = tTable.Rows[i][j].ToString();

                        if (!ValuesMatch(expectedValue, value))
                        {
                            result.Add($"Row {(i + 1):D3} / Col {(j + 1):D3} | {expectedValue} != {value}");
                        }
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                result.Add("Tabelle zeigt WENIGER Zeilen an als erwartet!");
            }

            if (sTable.Rows.Count < tTable.Rows.Count)
            {
                result.Add("Tabelle zeigt MEHR Zeilen an als erwartet!");
            }

            return result;
        }

        public static bool ValuesMatch(string expected, string value)
        {
            if (expected.StartsWith(RegexPrefix))
            {
                return Regex.Match(
                    value, expected.Substring(RegexPrefix.Length), RegexOptions.IgnoreCase
                ).Success;
            }

            return string.Equals(expected, value, StringComparison.InvariantCulture);
        }
    }
}
