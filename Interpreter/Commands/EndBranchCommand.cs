namespace Interpreter.Commands
{
    public class EndBranchCommand : CommandBase
    {
        private readonly IExecutionContext _executionContext;

        public EndBranchCommand(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public override void Run()
        {
            if (_executionContext.InsideIf)
            {
                _executionContext.EndScope();
                _executionContext.SkipLines = false;
                _executionContext.InsideIf = false;
            }
        }
    }
}
