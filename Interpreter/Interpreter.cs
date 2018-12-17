using System.Collections.Generic;
using Interpreter.Commands;
using Interpreter.Parsing.CLike;
using Interpreter.Token;

namespace Interpreter
{
    public interface IExecutionContext
    {
        void BeginScope();
        void EndScope();
        Dictionary<string, StringToken> GetCurrentScope();
        bool SkipLines { get; set; }
        bool InsideIf { get; set; }
        IOut GetOutput();
    }

    public class Interpreter : IExecutionContext
    {
        private readonly IOut _out;
        private readonly Stack<Dictionary<string, StringToken>> _scopeStack = new Stack<Dictionary<string, StringToken>>();

        public Interpreter(IOut mockOutput)
        {
            _out = mockOutput;
        }

        public void Evaluate(IEnumerable<object> statements)
        {
            BeginScope();

            foreach (var statement in statements)
            {
                if (statement is Branch)
                {
                    Branch branch = (Branch) statement;

                    var branchCommand = new BranchCommand(branch.Left, branch.Right);
                    branchCommand.Run(this);
                }
                else if (statement is EndBranch)
                {
                    var endBranchCommand = new EndBranchCommand();
                    endBranchCommand.Run(this);
                }
                
                /* Todo: This breaks the pattern because we can't roll
                    up these commands into a list before looping through to run them.
                 */
                else if (SkipLines)
                {
                    var doNothingCommand = new DoNothingCommand();
                    doNothingCommand.Run(this);
                }

                else if (statement is CreateVariable)
                {
                    var createVariable = (CreateVariable) statement;

                    var createVariableCommand = new CreateVariableCommand(createVariable.Name,
                        createVariable.Value);

                    createVariableCommand.Run(this);
                }
                else if (statement is Assignment)
                {
                    var assignment = (Assignment) statement;

                    var assignmentCommand = new AssignmentCommand(assignment.Name,
                        assignment.Value);

                    assignmentCommand.Run(this);
                }
                else if (statement is MethodCall)
                {
                    var methodCall = (MethodCall) statement;

                    var methodCallCommand = new MethodCallCommand("print",
                        methodCall.Arguments);

                    methodCallCommand.Run(this);
                }
            }

            EndScope();
        }

        public void BeginScope()
        {
            if (_scopeStack.Count > 0)
            {
                var parentScope = _scopeStack.Peek();
                var newScope = new Dictionary<string, StringToken>(parentScope);
                _scopeStack.Push(newScope);
            }
            else
            {
                _scopeStack.Push(new Dictionary<string, StringToken>());
            }
        }

        public void EndScope()
        {
            _scopeStack.Pop();
        }

        public Dictionary<string, StringToken> GetCurrentScope()
        {
            return _scopeStack.Peek();
        }

        public bool SkipLines { get; set; }
        public bool InsideIf { get; set; }

        public IOut GetOutput()
        {
            return _out;
        }
    }
}