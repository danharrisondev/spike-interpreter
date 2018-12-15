using System;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(new ConsoleOut());
            interpreter.Evaluate(@"print hello world
print goodbye world");
        }
    }

    class ConsoleOut : IOut
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
