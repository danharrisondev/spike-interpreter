using NUnit.Framework;
using Tests.Infrastructure;

namespace Tests
{
    public class PrintTests
    {
        private MockOut _mockOut;
        private Interpreter.Interpreter _interpreter;

        [SetUp]
        public void Set_up_interpreter()
        {
            _mockOut = new MockOut();
            _interpreter = new Interpreter.Interpreter(_mockOut);
        }

        [Test]
        public void Print_writes_to_output()
        {
            _interpreter.Evaluate(@"print(""hello world"")");
            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("hello world"));
        }

        [Test]
        public void Print_can_be_called_multiple_times()
        {
            _interpreter.Evaluate(
                @"print(""hello world"")
print(""goodbye world"")");

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("hello world"));
            Assert.That(_mockOut.WriteLineCalls[1], Is.EqualTo("goodbye world"));
        }

        [Test]
        public void Print_can_take_a_variable_as_an_argument()
        {
            _interpreter.Evaluate(
                @"var message = ""greetings""
print(message)");

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("greetings"));
        }

        [Test]
        public void Can_create_then_assign_then_print()
        {
            _interpreter.Evaluate(
                @"var message = ""greetings""
set message = ""seasons greetings""
print(message)");

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("seasons greetings"));
        }
    }
}