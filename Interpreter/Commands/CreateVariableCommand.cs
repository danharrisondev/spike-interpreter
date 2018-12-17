using System.Collections.Generic;
using Interpreter.Token;

namespace Interpreter.Commands
{
    public class CreateVariableCommand : CommandBase
    {
        private readonly string _name;
        private readonly string _value;
        private readonly Dictionary<string, StringToken> _scope;

        public CreateVariableCommand(
            string name,
            string value,
            Dictionary<string, StringToken> scope)
        {
            _name = name;
            _value = value;
            _scope = scope;
        }

        public override void Run()
        {
            /* Todo: Consider StringToken and how it fits into
             this pattern. Will need to be updated when the scope
             accepts more than strings */
            _scope.Add(_name, new StringToken(_value));
        }
    }

    public abstract class CommandBase
    {
        public abstract void Run();
    }
}
