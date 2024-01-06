using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class IfCommandTests
    {
        private void ReplaceVariableValues(ref string[] commandParts, Dictionary<string, int> variables)
        {
            for (int i = 0; i < commandParts.Length; i++)
            {
                if (variables.ContainsKey(commandParts[i]))
                {
                    // Replace variable with its value
                    commandParts[i] = variables[commandParts[i]].ToString();
                }
            }
        }

        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "IF", "20", ">", "10" };

            // Act
            bool result = ifCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "IF", "x", ">", "10", "extra" }; // Extra argument

            // Act
            bool result = ifCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidComparisonOperator_ReturnsFalse()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "IF", "x", "INVALID", "10" }; // Invalid operator

            // Act
            bool result = ifCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid comparison operator should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidFirstValue_ReturnsFalse()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "IF", "abc", ">", "10" }; // Non-integer first value

            // Act
            bool result = ifCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid first value should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidSecondValue_ReturnsFalse()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "IF", "x", ">", "abc" }; // Non-integer second value

            // Act
            bool result = ifCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid second value should return false.");
        }

        [Test]
        public void Execute_ConditionTrue_NoErrors()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int> { { "x", 15 } };
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "IF", "x", ">", "10" };

            // Initialize isExecutingSpecialCommandStack with a false peek
            isExecutingSpecialCommandStack.Push(false);

            // Act
            // Should not throw an exception
            ReplaceVariableValues(ref commandParts, variables);
            ifCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
        }

        [Test]
        public void Execute_ConditionFalse_SkipsCommands()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int> { { "x", 5 } };
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;

            // Nested command to simulate skipping
            specialCommandsStack.Push("IF");
            isExecutingSpecialCommandStack.Push(true);

            string[] commandParts = { "IF", "x", ">", "10" };

            // Act
            // Should not throw an exception
            ifCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
        }

        [Test]
        public void Execute_EndIfCommand_PopsStacks()
        {
            // Arrange
            var ifCommand = new IfCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;

            specialCommandsStack.Push("IF");
            isExecutingSpecialCommandStack.Push(false);

            string[] commandParts = { "ENDIF" };

            // Act
            // Should not throw an exception
            ifCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsEmpty(isExecutingSpecialCommandStack, "Stack should be empty after executing ENDIF.");
            Assert.IsEmpty(specialCommandsStack, "Stack should be empty after executing ENDIF.");
        }
    }
}
