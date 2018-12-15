using System.Collections.Generic;
using Interpreter;

namespace Tests.Infrastructure
{
    public class MockOut : IOut
    {
        public List<string> WriteLineCalls { get; } = new List<string>();

        public void WriteLine(string message)
        {
            WriteLineCalls.Add(message);
        }
    }
}