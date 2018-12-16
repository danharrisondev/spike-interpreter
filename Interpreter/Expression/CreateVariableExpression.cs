namespace Interpreter.Expression
{
    public class CreateVariableExpression
    {
        public CreateVariableExpression(string expression)
        {
            var operands = expression.Split(" = ");
            Name = operands[0];
            Value = operands[1];
        }

        public string Name { get; }
        public string Value { get; }
    }
}