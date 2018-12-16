namespace Interpreter.Parsing.Statements
{
    public class EndBranch
    {
        public static bool IsMatch(string statement)
        {
            return statement == "endif";
        }
    }
}