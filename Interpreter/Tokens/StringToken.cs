namespace Interpreter.Tokens
{
    public class StringToken
    {
        private readonly string _value;

        public StringToken(string value)
        {
            _value = value;
        }

        public string Value => _value.Replace("\"", string.Empty);
    }
}