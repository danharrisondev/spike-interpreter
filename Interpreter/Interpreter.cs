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
                    var call = new MethodCallWithStringParameterOrVariableExpression(line);

                    if (call.IsRawString)
                    {
                        _out.WriteLine(new StringToken(call.Argument).Value);
                    }
                    else
                    {
                        _out.WriteLine(_variables[call.Argument].Value);
                    }
                }
            }
        }
    }
}