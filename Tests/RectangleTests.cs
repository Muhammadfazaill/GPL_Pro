using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class RectangleCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var rectangleCommand = new RectangleCommand();
            string[] commandParts = { "RECTANGLE", "50", "100" };

            // Act
            bool result = rectangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var rectangleCommand = new RectangleCommand();
            string[] commandParts = { "RECTANGLE", "50" }; // Missing height

            // Act
            bool result = rectangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidWidth_ReturnsFalse()
        {
            // Arrange
            var rectangleCommand = new RectangleCommand();
            string[] commandParts = { "RECTANGLE", "abc", "100" }; // Non-integer width

            // Act
            bool result = rectangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid width should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidHeight_ReturnsFalse()
        {
            // Arrange
            var rectangleCommand = new RectangleCommand();
            string[] commandParts = { "RECTANGLE", "50", "xyz" }; // Non-integer height

            // Act
            bool result = rectangleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid height should return false.");
        }

        [Test]
        public void Execute_ValidCommand_NoErrors()
        {
            // Arrange
            var rectangleCommand = new RectangleCommand();
            string[] commandParts = { "RECTANGLE", "50", "100" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            // should not throw an exception
            rectangleCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);
        }
    }
}
