namespace Interpreter.Commands
{
    public abstract class CommandBase
    {
        public abstract void Run(IExecutionContext context);
    }
}