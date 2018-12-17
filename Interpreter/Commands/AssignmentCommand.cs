using Interpreter.Token;

namespace Interpreter.Commands
{
    public class AssignmentCommand : CommandBase
    {
        private readonly string _name;
        private readonly string _value;

        public AssignmentCommand(
            string name,
            string value)
        {
            _name = name;
            _value = value;
        }

        public override void Run(IExecutionContext context)
        {
            var scope = context.GetCurrentScope();
            scope[_name] = new StringToken(_value);
        }
    }
}
