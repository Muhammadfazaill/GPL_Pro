using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class CircleCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var circleCommand = new CircleCommand();
            string[] commandParts = { "CIRCLE", "50" };

            // Act
            bool result = circleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var circleCommand = new CircleCommand();
            string[] commandParts = { "CIRCLE" }; // Missing radius

            // Act
            bool result = circleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidRadius_ReturnsFalse()
        {
            // Arrange
            var circleCommand = new CircleCommand();
            string[] commandParts = { "CIRCLE", "abc" }; // Non-integer radius

            // Act
            bool result = circleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid radius should return false.");
        }

        [Test]
        public void SyntaxCheck_NegativeRadius_ReturnsFalse()
        {
            // Arrange
            var circleCommand = new CircleCommand();
            string[] commandParts = { "CIRCLE", "-10" }; // Negative radius

            // Act
            bool result = circleCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Negative radius should return false.");
        }

        [Test]
        public void Execute_ValidCommand_NoErrors()
        {
            // Arrange
            var circleCommand = new CircleCommand();
            string[] commandParts = { "CIRCLE", "50" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            // should not throw an exception
            circleCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);
        }
    }
}
