using System;
using System.Collections.Generic;

namespace Interpreter
{
    public static class Tokens
    {
        public static bool IsStringToken(string tokenString)
        {
            return tokenString.StartsWith("\"") && tokenString.EndsWith("\"");
        }
    }

    public class StringToken
    {
        private readonly string _value;

        public StringToken(string value)
        {
            _value = value;
        }

        public string Value => _value.Replace("\"", string.Empty);
    }

    public class Interpreter
    {
        private readonly IOut _out;
        private readonly Dictionary<string, StringToken> _variables = new Dictionary<string, StringToken>();

        public Interpreter(IOut mockOutput)
        {
            _out = mockOutput;
        }

        public void Evaluate(string script)
        {
            var lines = script.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                if (line.StartsWith("print"))
                {
                    var argument = line.Replace("print ", string.Empty);

                    if (Tokens.IsStringToken(argument))
                    {
                        var stringToken = new StringToken(argument);
                        _out.WriteLine(stringToken.Value);
                    }
                    else
                    {
                        var variableName = line.Replace("print ", string.Empty);
                        _out.WriteLine(_variables[variableName].Value);
                    }
                }
                else
                {
                    var operands = line.Split(" = ");
                    var variableName = operands[0];
                    var value = operands[1];
                    _variables.Add(variableName, new StringToken(value));
                }
            }
        }
    }
}