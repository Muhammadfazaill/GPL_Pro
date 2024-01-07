using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class MethodCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidMethodDefinition_ReturnsTrue()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "METHOD", "DrawLine(x1,y1,x2,y2)" };

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid method definition syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_ValidMethodCall_ReturnsTrue()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            methods["DrawLine"] = new string[] { "x1,y1,x2,y2", "5", "10" }; // Simulating a defined method
            string[] commandParts = { "DrawLine(10,10,100,100)" };

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsTrue(result, "Valid method call syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidMethodDefinition_ReturnsFalse()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "METHOD", "DrawLine(x1,y1,x2,y2", "+", "ENDMETHOD" }; // Missing closing bracket

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid method definition syntax should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidMethodCall_ReturnsFalse()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "DrawLine(10,10,100,100" }; // Missing closing bracket

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Invalid method call syntax should return false.");
        }

        [Test]
        public void SyntaxCheck_UndefinedMethodCall_ReturnsFalse()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "UndefinedMethod(10,10,100,100)" };

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Undefined method call should return false.");
        }

        [Test]
        public void SyntaxCheck_MethodCallWithInvalidParameters_ReturnsFalse()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            string[] commandParts = { "DrawLine(10,'a',100,100)" }; // Non-integer parameter

            // Act
            bool result = methodCommand.SyntaxCheck(commandParts, ref variables, ref methods, false);

            // Assert
            Assert.IsFalse(result, "Method call with invalid parameters should return false.");
        }

        [Test]
        public void Execute_ValidMethodDefinition_AddsMethodToDictionary()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "METHOD", "DrawLine(x1,y1,x2,y2)" };

            isExecutingSpecialCommandStack.Push(false);

            // Act
            methodCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsTrue(methods.ContainsKey("DrawLine"), "Method should be added to the dictionary.");
            Assert.AreEqual("x1,y1,x2,y2", methods["DrawLine"][0], "Method parameters should be stored.");
            Assert.AreEqual("0", methods["DrawLine"][1], "Method start line should be stored.");
        }

        [Test]
        public void Execute_ValidMethodCall_JumpsToMethodStartLine()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            methods["DrawLine"] = new string[] { "x1,y1,x2,y2", "5", "10" }; // Simulating a defined method
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false);
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "DrawLine(10,20,30,40)" };

            // Act
            methodCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual("METHOD", specialCommandsStack.Peek(), "METHOD should be pushed to specialCommandsStack.");
            Assert.AreEqual(5, currentLineIndex, "Should jump to the start line of the method.");
        }

        [Test]
        public void Execute_ValidMethodCallWithParameters_StoresTemporaryVariables()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            methods["DrawLine"] = new string[] { "x1,y1,x2,y2", "5", "10" }; // Simulating a defined method
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false);
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            string[] commandParts = { "DrawLine(10,20,x,y)" };
            variables["x"] = 30;
            variables["y"] = 40;

            // Act
            methodCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.IsTrue(variables.ContainsKey("x1_METHOD"), "Temporary variable x1_METHOD should be stored.");
            Assert.AreEqual(10, variables["x1_METHOD"], "Temporary variable x1_METHOD should be set to 10.");
            Assert.IsTrue(variables.ContainsKey("y1_METHOD"), "Temporary variable y1_METHOD should be stored.");
            Assert.AreEqual(20, variables["y1_METHOD"], "Temporary variable y1_METHOD should be set to 20.");
            Assert.IsTrue(variables.ContainsKey("x2_METHOD"), "Temporary variable x2_METHOD should be stored.");
            Assert.AreEqual(30, variables["x2_METHOD"], "Temporary variable x2_METHOD should be set to 30.");
            Assert.IsTrue(variables.ContainsKey("y2_METHOD"), "Temporary variable y2_METHOD should be stored.");
            Assert.AreEqual(40, variables["y2_METHOD"], "Temporary variable y2_METHOD should be set to 40.");
        }

        [Test]
        public void Execute_EndMethod_PopsStacksAndSetsEndLine()
        {
            // Arrange
            var methodCommand = new MethodCommand();
            var variables = new Dictionary<string, int>();
            var methods = new Dictionary<string, string[]>();
            var isExecutingSpecialCommandStack = new Stack<bool>();
            isExecutingSpecialCommandStack.Push(false); // default value
            var specialCommandsStack = new Stack<string>();
            int currentLineIndex = 0;
            // create a method first with commadparts
            string[] commandParts = { "METHOD", "DrawLine(x1,y1,x2,y2)" };

            // Act
            methodCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual(2, isExecutingSpecialCommandStack.Count, "isExecutingSpecialCommandStack should have 2 items.");
            Assert.AreEqual(1, specialCommandsStack.Count, "specialCommandsStack should have 1 item.");
            Assert.AreEqual("METHOD", specialCommandsStack.Peek(), "METHOD should be pushed to specialCommandsStack.");
            // verify the method is added to the dictionary
            Assert.IsTrue(methods.ContainsKey("DrawLine"), "Method should be added to the dictionary.");
            Assert.AreEqual("x1,y1,x2,y2", methods["DrawLine"][0], "Method parameters should be stored.");
            Assert.AreEqual("0", methods["DrawLine"][1], "Method start line should be stored.");

            // Arrange
            commandParts = new string[] { "ENDMETHOD" };
            currentLineIndex = 2;

            // Act
            methodCommand.Execute(commandParts, ref variables, ref methods, ref isExecutingSpecialCommandStack, ref specialCommandsStack, ref currentLineIndex);

            // Assert
            Assert.AreEqual(1, isExecutingSpecialCommandStack.Count, "isExecutingSpecialCommandStack should be empty.");
            Assert.AreEqual(0, specialCommandsStack.Count, "specialCommandsStack should be empty.");
            Assert.AreEqual("2", methods["DrawLine"][2], "End line should be set in the methods dictionary.");
        }
    }
}
