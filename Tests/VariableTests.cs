using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class VariableCommandTests
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
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "=", "50" };

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "=" }; // Missing value

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidVariableName_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "123", "=", "50" }; // Invalid variable name

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid variable name should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidOperator_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "INVALID", "50" }; // Invalid operator

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid operator should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidValue_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "=", "abc" }; // Non-integer value

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid value should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidOperatorAndValue_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "INVALID", "abc" }; // Invalid operator and value

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid operator and value should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidOperatorAndSecondValue_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "=", "50", "+", "abc" }; // Invalid operator and second value

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid operator and second value should return false.");
        }

        [Test]
        public void SyntaxCheck_TooManyArguments_ReturnsFalse()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "Count", "=", "50", "+", "1", "extra" }; // Extra argument

            // Act
            bool result = variableCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Too many arguments should return false.");
        }

        [Test]
        public void Execute_ValidCommand_SetsVariable()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "Count", "=", "50" };

            // Act
            variableCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsTrue(variables.ContainsKey("Count"), "Variable should be set.");
            Assert.AreEqual(50, variables["Count"], "Variable should have the correct value.");
        }

        [Test]
        public void Execute_InvalidValue_DoesNotSetVariable()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "Count", "=", "abc" }; // Non-integer value

            // Act
            variableCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsFalse(variables.ContainsKey("Count"), "Variable should not be set.");
        }

        [Test]
        public void Execute_AssignVariable_NoErrors()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "Count", "=", "50" };

            // Act
            // Should not throw an exception
            ReplaceVariableValues(ref commandParts, variables);
            variableCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
        }

        [Test]
        public void Execute_AssignVariableWithCalculation_NoErrors()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int> { { "x", 10 }, { "y", 5 } };
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "Result", "=", "x", "+", "y" };

            // Act
            // Should not throw an exception
            ReplaceVariableValues(ref commandParts, variables);
            variableCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
        }

        [Test]
        public void Execute_AssignVariableWithInvalidOperator_ShouldThrowException()
        {
            // Arrange
            var variableCommand = new VariableCommand();
            var variables = new Dictionary<string, int> { { "x", 10 }, { "y", 5 } };
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;

            // Invalid operator in calculation
            string[] commandParts = { "Result", "=", "x", "++", "y" };

            // Act
            // Should throw an exception
            ReplaceVariableValues(ref commandParts, variables);
            variableCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsFalse(variables.ContainsKey("Result"), "Variable should not be set.");
        }

        [Test]
        public void ParseVariables_VariableInCommand_ReplacesVariable()
        {
            // Arrange
            var variables = new Dictionary<string, int> { { "Count", 20 } };
            string[] commandParts = { "CIRCLE", "Count" };

            // Act
            VariableCommand.ParseVariables(ref commandParts, variables);

            // Assert
            Assert.AreEqual("CIRCLE", commandParts[0], "Command should not be changed.");
            Assert.AreEqual("20", commandParts[1], "Variable should be replaced.");
        }

        [Test]
        public void ParseVariables_UndefinedVariableInCommand_ShowsError()
        {
            // Arrange
            var variables = new Dictionary<string, int>();
            string[] commandParts = { "CIRCLE", "UndefinedVariable" };

            // Act
            bool result = VariableCommand.ParseVariables(ref commandParts, variables, false);

            // Assert
            Assert.IsFalse(result, "Parsing should fail for undefined variable.");
        }

        [Test]
        public void IsVariableAssignment_ValidAssignment_ReturnsTrue()
        {
            // Arrange
            string[] commandParts = { "Count", "=", "50" };

            // Act
            bool result = VariableCommand.IsVariableAssignment(commandParts);

            // Assert
            Assert.IsTrue(result, "Should be a valid variable assignment.");
        }

        [Test]
        public void IsVariableAssignment_InvalidAssignment_ReturnsFalse()
        {
            // Arrange
            string[] commandParts = { "Count", "INVALID", "50" }; // Invalid operator

            // Act
            bool result = VariableCommand.IsVariableAssignment(commandParts);

            // Assert
            Assert.IsFalse(result, "Should not be a valid variable assignment.");
        }

        [Test]
        public void IsVariableAssignment_TooManyArguments_ReturnsFalse()
        {
            // Arrange
            string[] commandParts = { "Count", "=", "50", "+", "1", "extra" }; // Extra argument

            // Act
            bool result = VariableCommand.IsVariableAssignment(commandParts);

            // Assert
            Assert.IsFalse(result, "Should not be a valid variable assignment.");
        }
    }
}
