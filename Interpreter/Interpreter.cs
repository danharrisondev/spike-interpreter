using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Parsing.CLike;
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

        public void Evaluate(IEnumerable<object> statements)
        {
            bool skipLines = false;
            bool insideIf = false;

            BeginScope();

            foreach (var statement in statements)
            {
                if (statement is Branch)
                {
                    Branch branch = (Branch) statement;
                    if (GetCurrentScope()[branch.Left].Value == new StringToken(branch.Right).Value)
                    {
                        BeginScope();
                        insideIf = true;
                    }
                    else
                    {
                        skipLines = true;
                    }
                }
                else if (statement is EndBranch)
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
                else if (statement is CreateVariable)
                {
                    var createVariable = (CreateVariable) statement;
                    GetCurrentScope().Add(createVariable.Name, new StringToken(createVariable.Value));
                }
                else if (statement is Assignment)
                {
                    var assignment = (Assignment) statement;
                    GetCurrentScope()[assignment.Name] = new StringToken(assignment.Value);
                }
                else if (statement is MethodCall)
                {
                    var methodCall = (MethodCall) statement;

                    if (methodCall.Name == "print")
                    {
                        var argument = methodCall.Arguments.Single();
                        if (Tokens.IsStringToken(argument.Value))
                        {
                            _out.WriteLine(new StringToken(argument.Value).Value);
                        }
                        else
                        {
                            _out.WriteLine(GetCurrentScope()[argument.Value].Value);
                        }
                    }
                    else
                    {
                        throw new Exception("Missing method: " + methodCall.Name);
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