using System;
using System.IO;
using Interpreter.Parsing;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(new StandardOut());

            using (var script = new StreamReader(args[0]))
            {
                var parser = new Parser();
                var parseResult = parser.Parse(script.ReadToEnd());
                interpreter.Evaluate(parseResult);
            }
        }
    }

    class StandardOut : IOut, IDisposable
    {
        private readonly StreamWriter _writer;

        public StandardOut()
        {
            _writer = new StreamWriter(Console.OpenStandardOutput());
        }

        public void WriteLine(string message)
        {
            _writer.WriteLine(message);
            _writer.Flush();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
