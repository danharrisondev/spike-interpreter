using System.Linq;
using Interpreter.Parsing;
using Interpreter.Parsing.CLike;
using NUnit.Framework;

namespace Tests
{
    public class CLikeParserTests
    {
        private CLikeParser _parser;

        [SetUp]
        public void Set_up_parser()
        {
            _parser = new CLikeParser();
        }

        [Test]
        public void Can_parse_create_variable_statement()
        {
            var statementString = @"var message = ""seasons greetings""";
            var statement = ParseStatement<CreateVariable>(statementString);
            Assert.That(statement, Is.TypeOf<CreateVariable>());
            Assert.That(statement.Name, Is.EqualTo("message"));
            Assert.That(statement.Value, Is.EqualTo("seasons greetings"));
        }

        [Test]
        public void Can_parse_assignment_statement()
        {
            var statementString = @"set message = ""updated""";
            var statement = ParseStatement<Assignment>(statementString);
            Assert.That(statement, Is.TypeOf<Assignment>());
            Assert.That(statement.Name, Is.EqualTo("message"));
            Assert.That(statement.Value, Is.EqualTo("updated"));
        }

        [Test]
        public void Can_parse_method_call_statement()
        {
            var statementString = @"print(""hello world"")";
            var statement = ParseStatement<MethodCall>(statementString);
            Assert.That(statement, Is.TypeOf<MethodCall>());
            Assert.That(statement.Name, Is.EqualTo("print"));
            Assert.That(statement.Arguments.Count, Is.EqualTo(1));
            Assert.That(statement.Arguments.ToList()[0].Value, Is.EqualTo(@"""hello world"""));
        }

        [Test]
        public void Can_parse_if_statement()
        {
            var statementString = @"if message == ""hello""";
            var statement = ParseStatement<Branch>(statementString);
            Assert.That(statement, Is.TypeOf<Branch>());
            Assert.That(statement.Left, Is.EqualTo("message"));
            Assert.That(statement.Right, Is.EqualTo(@"""hello"""));
        }

        [Test]
        public void Can_parse_endif_statement()
        {
            var statementString = "endif";
            var statement = ParseStatement<EndBranch>(statementString);
            Assert.That(statement, Is.TypeOf<EndBranch>());
        }

        private T ParseStatement<T>(string script)
        {
            var parseResult = _parser.Parse(script);
            var statement = (T) parseResult.Single();
            return statement;
        }
    }
}
