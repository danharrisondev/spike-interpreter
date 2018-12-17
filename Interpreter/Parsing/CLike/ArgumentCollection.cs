using System.Collections.Generic;
using System.Linq;

namespace Interpreter.Parsing.CLike
{
    public static class ArgumentCollection
    {
        public static IEnumerable<Argument> Parse(string argumentString)
        {
            if (argumentString == string.Empty)
                return Enumerable.Empty<Argument>();

            if (argumentString.Contains(","))
                return argumentString.Split(",").Select(arg => new Argument(arg));

            return new List<Argument>(1) { new Argument(argumentString) };
        }
    }
}