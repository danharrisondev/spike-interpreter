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

        public MethodCallCommand(
            string name,
            IEnumerable<Argument> arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        public override void Run(IExecutionContext context)
        {
            var scope = context.GetCurrentScope();
            var output = context.GetOutput();

            if (_name == "print")
            {
                var argument = _arguments.Single();
                if (Tokens.IsStringToken(argument.Value))
                {
                    output.WriteLine(new StringToken(argument.Value).Value);
                }
                else
                {
                    output.WriteLine(scope[argument.Value].Value);
                }
            }
            else
            {
                throw new Exception("Missing method: " + _name);
            }
        }
    }
}
