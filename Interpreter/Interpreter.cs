using System;
using System.Collections.Generic;
using Interpreter.Expression;
using Interpreter.Token;

namespace Interpreter
{
    public class Interpreter
    {
        private readonly IOut _out;
        private readonly Stack<Dictionary<string, StringToken>> _scopeStack = new Stack<Dictionary<string, StringToken>>();

        public Interpreter(IOut mockOutput)
        {
            _out = mockOutput;
        }

        public void Evaluate(string script)
        {
            bool skipLines = false;
            bool insideIf = false;

            BeginScope();

            var lines = script.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                if (line.StartsWith("if"))
                {
                    var branch = line.Replace("if ", string.Empty);
                    var condition = branch.Split(" == ");
                    if (GetCurrentScope()[condition[0]].Value == new StringToken(condition[1]).Value)
                    {
                        BeginScope();
                        insideIf = true;
                    }
                    else
                    {
                        skipLines = true;
                    }
                }
                else if (line.StartsWith("endif"))
                {
                    if (insideIf)
                    {
                        EndScope();
                        skipLines = false;
                        insideIf = false;
                    }
                }
                else if (skipLines)
                {
                    continue;
                }
                else if (line.StartsWith("var"))
                {
                    var variable = new AssignmentExpression(line.Replace("var ", string.Empty));
                    GetCurrentScope().Add(variable.Name, new StringToken(variable.Value));
                }
                else if (line.StartsWith("set"))
                {
                    var assignment = new AssignmentExpression(line.Replace("set ", string.Empty));
                    GetCurrentScope()[assignment.Name] = new StringToken(assignment.Value);
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
                        _out.WriteLine(GetCurrentScope()[call.Argument].Value);
                    }
                }
            }

            EndScope();
        }

        private void BeginScope()
        {
            if (_scopeStack.Count > 0)
            {
                var parentScope = _scopeStack.Peek();
                var newScope = new Dictionary<string, StringToken>(parentScope);
                _scopeStack.Push(newScope);
            }
            else
            {
                _scopeStack.Push(new Dictionary<string, StringToken>());
            }
        }

        private Dictionary<string, StringToken> GetCurrentScope()
        {
            return _scopeStack.Peek();
        }

        private void EndScope()
        {
            _scopeStack.Pop();
        }
    }
}