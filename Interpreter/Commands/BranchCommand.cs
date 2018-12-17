using Interpreter.Token;

namespace Interpreter.Commands
{
    class BranchCommand : CommandBase
    {
        private readonly string _left;
        private readonly string _right;

        public BranchCommand(string left, string right)
        {
            _left = left;
            _right = right;
        }

        public override void Run(IExecutionContext context)
        {
            var scope = context.GetCurrentScope();

            if (scope[_left].Value == new StringToken(_right).Value)
            {
                context.BeginScope();
                context.InsideIf = true;
            }
            else
            {
                context.SkipLines = true;
            }
        }
    }
}
