using System;
using System.Collections.Generic;
using Interpreter.Expression;
using Interpreter.Token;

namespace Interpreter
{
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
                if (line.StartsWith("var"))
                {
                    var variable = new AssignmentExpression(line.Replace("var ", string.Empty));
                    _variables.Add(variable.Name, new StringToken(variable.Value));
                }
                else if (line.StartsWith("set"))
                {
                    var assignment = new AssignmentExpression(line.Replace("set ", string.Empty));
                    _variables[assignment.Name] = new StringToken(assignment.Value);
                }
                else if (line.StartsWith("print"))
                {
                    var argument = line.Substring(line.IndexOf("(") + 1, line.LastIndexOf(")") - (line.IndexOf("(") + 1));

                    if (Tokens.IsStringToken(argument))
                    {
                        var stringToken = new StringToken(argument);
                        _out.WriteLine(stringToken.Value);
                    }
                    else
                    {
                        _out.WriteLine(_variables[argument].Value);
                    }
                }
            }
        }
    }
}