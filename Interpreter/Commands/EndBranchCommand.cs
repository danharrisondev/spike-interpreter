namespace Interpreter.Commands
{
    public class EndBranchCommand : CommandBase
    {
        public override void Run(IExecutionContext context)
        {
            if (context.InsideIf)
            {
                context.EndScope();
                context.SkipLines = false;
                context.InsideIf = false;
            }
        }
    }
}
