using System;
using Interpreter.Token;

namespace Interpreter.Expression
{
    public class PrintCallExpression
    {
        public PrintCallExpression(string line)
        {
            Argument = line.Substring(
                line.IndexOf("(", StringComparison.Ordinal) + 1,
                line.LastIndexOf(")", StringComparison.Ordinal) - (line.IndexOf("(", StringComparison.Ordinal) + 1));

            IsRawString = Tokens.IsStringToken(Argument);
        }

        public string Argument { get; }

        public bool IsRawString { get; }
    }
}