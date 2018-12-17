using System.Collections.Generic;
using Interpreter.Token;

namespace Interpreter.Commands
{
    public class CreateVariableCommand : CommandBase
    {
        private readonly string _name;
        private readonly string _value;

        public CreateVariableCommand(
            string name,
            string value)
        {
            _name = name;
            _value = value;
        }

        public override void Run(IExecutionContext context)
        {
            var scope = context.GetCurrentScope();

            /* Todo: Consider StringToken and how it fits into
             this pattern. Will need to be updated when the scope
             accepts more than strings */
            scope.Add(_name, new StringToken(_value));
        }
    }
}
