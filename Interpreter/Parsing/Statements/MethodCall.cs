using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Interpreter.Parsing.Statements
{
    public class MethodCall
    {
        private static readonly Regex Pattern = new Regex("(?<name>[A-z0-9]+)\\((?<arguments>[A-z0-9 ,\"]*)\\)", RegexOptions.Compiled);

        public MethodCall(string statement)
        {
            var parts = Pattern.Match(statement);
            Name = parts.Groups["name"].Value;
            Arguments = ArgumentCollection.Parse(parts.Groups["arguments"].Value);
        }

        public static bool IsMatch(string statement)
        {
            return Pattern.IsMatch(statement);
        }

        public string Name { get; }
        public IEnumerable<Argument> Arguments { get; }
    }
}
