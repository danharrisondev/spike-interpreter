using System;
using System.Collections.Generic;
using System.Linq;
using Interpreter.Parsing.CLike;
using Interpreter.Token;

namespace Interpreter.Commands
{
    public class MethodCallCommand : CommandBase
    {
        private readonly string _name;
        private readonly IEnumerable<Argument> _arguments;
        private readonly Dictionary<string, StringToken> _scope;
        private readonly IOut _output;

        public MethodCallCommand(
            string name,
            IEnumerable<Argument> arguments,
            IExecutionContext executionContext)
        {
            _name = name;
            _arguments = arguments;
            _scope = executionContext.GetCurrentScope();
            _output = executionContext.GetOutput();
        }

        public override void Run()
        {
            if (_name == "print")
            {
                var argument = _arguments.Single();
                if (Tokens.IsStringToken(argument.Value))
                {
                    _output.WriteLine(new StringToken(argument.Value).Value);
                }
                else
                {
                    _output.WriteLine(_scope[argument.Value].Value);
                }
            }
            else
            {
                throw new Exception("Missing method: " + _name);
            }
        }
    }
}
