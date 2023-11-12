using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class TriangleCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var triangleCommand = new TriangleCommand();
            string[] commandParts = { "TRIANGLE", "50", "100" };

            // Act
            bool result = triangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var triangleCommand = new TriangleCommand();
            string[] commandParts = { "TRIANGLE", "50" }; // Missing height

            // Act
            bool result = triangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidBaseLength_ReturnsFalse()
        {
            // Arrange
            var triangleCommand = new TriangleCommand();
            string[] commandParts = { "TRIANGLE", "abc", "100" }; // Non-integer base length

            // Act
            bool result = triangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid base length should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidHeight_ReturnsFalse()
        {
            // Arrange
            var triangleCommand = new TriangleCommand();
            string[] commandParts = { "TRIANGLE", "50", "xyz" }; // Non-integer height

            // Act
            bool result = triangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid height should return false.");
        }

        [Test]
        public void Execute_ValidCommand_NoErrors()
        {
            // Arrange
            var triangleCommand = new TriangleCommand();
            string[] commandParts = { "TRIANGLE", "50", "100" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            // should not throw an exception
            triangleCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);
        }
    }
}
