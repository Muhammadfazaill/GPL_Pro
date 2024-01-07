using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class WhileCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidWhileCommand_ReturnsTrue()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "WHILE", "x", "<", "10" };

            // Act
            bool result = whileCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid WHILE command syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_ValidEndWhileCommand_ReturnsTrue()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "ENDWHILE" };

            // Act
            bool result = whileCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid ENDWHILE command syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidWhileCommand_MissingComparisonOperator_ReturnsFalse()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "WHILE", "x" };

            // Act
            bool result = whileCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid WHILE command syntax should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidWhileCommand_InvalidComparisonOperator_ReturnsFalse()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "WHILE", "x", ">", "10", "extraPart" };

            // Act
            bool result = whileCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid WHILE command syntax should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidEndWhileCommand_ExtraArguments_ReturnsFalse()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "ENDWHILE", "extraPart" };

            // Act
            bool result = whileCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid ENDWHILE command syntax should return false.");
        }

        [Test]
        public void Execute_ValidWhileCommand_CorrectlySetsCondition()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false);
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "WHILE", "x", "<", "10" };
            variables["x"] = 5;

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsFalse(isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be false for a true condition.");
            Assert.AreEqual("WHILE", specialCommandsStack.Peek(), "WHILE should be pushed to specialCommandsStack.");
            Assert.AreEqual(0, currentLineIndex, "currentLineIndex should not change for a true condition.");
        }

        [Test]
        public void Execute_ValidWhileLoop_ExecutesCommandsOnTrueCondition()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false); // Simulating true condition in WHILE
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;

            // put variables in dictionary
            variables["x"] = 5;

            // execute a valid WHILE command
            string[] commandParts = { "WHILE", "x", "<", "10" };

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual(false, isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be false for a true condition.");
            Assert.AreEqual("WHILE", specialCommandsStack.Peek(), "WHILE should be pushed to specialCommandsStack.");

            // execute a valid ENDWHILE command
            currentLineIndex = 2;
            commandParts = new string[] { "ENDWHILE" };

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
            currentLineIndex++; // simulate incrementing the currentLineIndex

            // Assert
            Assert.AreEqual(false, isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be false for a true condition.");
            Assert.AreEqual(0, specialCommandsStack.Count, "specialCommandsStack should be empty after executing ENDWHILE.");

            // the currentLineIndex should be 0 again after executing ENDWHILE because the WHILE condition is true
            Assert.AreEqual(0, currentLineIndex, "currentLineIndex should be 0 again after executing ENDWHILE because the WHILE condition is true.");
        }

        [Test]
        public void Execute_ValidWhileLoop_CorrectlySkipsCommandsOnFalseCondition()
        {
            // Arrange
            var whileCommand = new WhileCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false); // Simulating true condition in WHILE
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;

            // put variables in dictionary
            variables["x"] = 5;

            // execute a valid WHILE command
            string[] commandParts = { "WHILE", "x", ">", "10" };

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual(true, isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be true for a false condition.");
            Assert.AreEqual("WHILE", specialCommandsStack.Peek(), "WHILE should be pushed to specialCommandsStack.");

            currentLineIndex = 2; // simulate incrementing the currentLineIndex

            // running a DRAW command inside the WHILE loop, it should be skipped
            commandParts = new string[] { "DRAW", "x", "y" };

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual(true, isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be true for a false condition.");
            Assert.AreEqual("WHILE", specialCommandsStack.Peek(), "WHILE should be pushed to specialCommandsStack.");

            // execute a valid ENDWHILE command
            currentLineIndex = 3;
            commandParts = new string[] { "ENDWHILE" };

            // Act
            whileCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);
            currentLineIndex++; // simulate incrementing the currentLineIndex

            // Assert
            Assert.AreEqual(false, isExecutingSpecialCommandStack.Peek(), "isExecutingSpecialCommand flag should be false for a false condition.");
            Assert.AreEqual(0, specialCommandsStack.Count, "specialCommandsStack should be empty after executing ENDWHILE.");

            // the currentLineIndex should be 4 again after executing ENDWHILE because the WHILE condition is false
            Assert.AreEqual(4, currentLineIndex, "currentLineIndex should be 4 again after executing ENDWHILE because the WHILE condition is false.");
        }
    }
}
