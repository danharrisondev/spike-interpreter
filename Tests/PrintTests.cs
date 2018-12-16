using System.Collections.Generic;
using Interpreter.Parsing.Statements;
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
            _interpreter.Evaluate(new List<object>
            {
                new MethodCall(@"print(""hello world"")")
            });

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("hello world"));
        }

        [Test]
        public void Print_can_be_called_multiple_times()
        {
            _interpreter.Evaluate(new List<object>
            {
                new MethodCall(@"print(""hello world"")"),
                new MethodCall(@"print(""goodbye world"")")
            });

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("hello world"));
            Assert.That(_mockOut.WriteLineCalls[1], Is.EqualTo("goodbye world"));
        }

        [Test]
        public void Print_can_take_a_variable_as_an_argument()
        {
            _interpreter.Evaluate(new List<object>
            {
                new CreateVariable(@"var message = ""greetings"""),
                new MethodCall("print(message)")
            });

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("greetings"));
        }

        [Test]
        public void Can_create_then_assign_then_print()
        {
            _interpreter.Evaluate(new List<object>
            {
                new CreateVariable(@"var message = ""greetings"""),
                new Assignment(@"set message = ""seasons greetings"""),
                new MethodCall("print(message)")
            });

            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("seasons greetings"));
        }

        [Test]
        public void If_condition_is_true_then_print()
        {
            _interpreter.Evaluate(new List<object>
            {
                new CreateVariable(@"var message = ""hello"""),
                new Branch(@"if message == ""hello"""),
                new MethodCall(@"print(""message is hello"")"),
                new EndBranch(),
                new Branch(@"if message == ""goodbye"""),
                new MethodCall(@"print(""message is goodbye"")"),
                new EndBranch()
            });

            Assert.That(_mockOut.WriteLineCalls.Count, Is.EqualTo(1));
            Assert.That(_mockOut.WriteLineCalls[0], Is.EqualTo("message is hello"));
        }
    }
}