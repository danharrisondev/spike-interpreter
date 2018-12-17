using System.Text.RegularExpressions;

namespace Interpreter.Parsing.CLike
{
    public class Branch
    {
        private static readonly Regex Pattern = new Regex("if (?<left>[A-z0-9]+) == (?<right>[A-z0-9\\\"]+)",
            RegexOptions.Compiled);

        public Branch(string statement)
        {
            var parts = Pattern.Match(statement);
            Left = parts.Groups["left"].Value;
            Right = parts.Groups["right"].Value;
        }

        public static bool IsMatch(string statement)
        {
            return Pattern.IsMatch(statement);
        }

        public string Left { get; }
        public string Right { get; }
    }
}