namespace Interpreter.Expression
{
    public class AssignmentExpression
    {
        public AssignmentExpression(string expression)
        {
            var operands = expression.Split(" = ");
            Name = operands[0];
            Value = operands[1];
        }

        public string Name { get; }
        public string Value { get; }
    }
}