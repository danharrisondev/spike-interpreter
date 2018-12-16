using System.Text.RegularExpressions;

namespace Interpreter.Parsing.Statements
{
    public class CreateVariable
    {
        private static readonly Regex Pattern = new Regex("var (?<name>[A-z0-9]+) = \"(?<value>.+)\"",
            RegexOptions.Compiled);

        public CreateVariable(string statement)
        {
            var parts = Pattern.Match(statement);
            Name = parts.Groups["name"].Value;
            Value = parts.Groups["value"].Value;
        }

        public static bool IsMatch(string statement)
        {
            return Pattern.IsMatch(statement);
        }

        public string Name { get; }
        public string Value { get; }
    }
}