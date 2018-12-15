using System;
using System.IO;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(new ConsoleOut());

            using (var script = new StreamReader(args[0]))
            {
                interpreter.Evaluate(script.ReadToEnd());
            }
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
