namespace Interpreter.Parsing.CLike
{
    public class EndBranch
    {
        public static bool IsMatch(string statement)
        {
            return statement == "endif";
        }
    }
}