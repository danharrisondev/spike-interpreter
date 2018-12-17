using Interpreter.Token;

namespace Interpreter.Commands
{
    class BranchCommand : CommandBase
    {
        private readonly string _left;
        private readonly string _right;
        private readonly IExecutionContext _executionContext;

        public BranchCommand(string left, string right, IExecutionContext executionContext)
        {
            _left = left;
            _right = right;
            _executionContext = executionContext;
        }

        public override void Run()
        {
            var scope = _executionContext.GetCurrentScope();

            if (scope[_left].Value == new StringToken(_right).Value)
            {
                _executionContext.BeginScope();
                _executionContext.InsideIf = true;
            }
            else
            {
                _executionContext.SkipLines = true;
            }
        }
    }
}
