using System;
using System.Collections.Generic;
using Interpreter.Expressions;
using Interpreter.Tokens;

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
                    var variable = new CreateVariableExpression(line.Replace("var ", string.Empty));
                    _variables.Add(variable.Name, new StringToken(variable.Value));
                }
                else if (line.StartsWith("print"))
                {
                    var argument = line.Replace("print ", string.Empty);

                    if (Tokens.Tokens.IsStringToken(argument))
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
            }
        }
    }
}