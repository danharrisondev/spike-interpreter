using System;
using System.Collections.Generic;
using Interpreter.Parsing.CLike;

namespace Interpreter.Parsing
{
    public class CLikeParser
    {
        public IEnumerable<object> Parse(string script)
        {
            var lines = script.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                if (CreateVariable.IsMatch(line))
                    yield return new CreateVariable(line);
                else if (Assignment.IsMatch(line))
                    yield return new Assignment(line);
                else if (MethodCall.IsMatch(line))
                    yield return new MethodCall(line);
                else if (Branch.IsMatch(line))
                    yield return new Branch(line);
                else if (EndBranch.IsMatch(line))
                    yield return new EndBranch();
            }
        }
    }
}