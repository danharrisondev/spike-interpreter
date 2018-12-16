namespace Interpreter.Token
{
    public static class Tokens
    {
        public static bool IsStringToken(string tokenString)
        {
            return tokenString.StartsWith("\"") && tokenString.EndsWith("\"");
        }
    }
}