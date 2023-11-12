using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class MoveCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var moveCommand = new MoveCommand();
            string[] commandParts = { "MOVE", "10", "20" };

            // Act
            bool result = moveCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidSyntax_ReturnsFalse()
        {
            // Arrange
            var moveCommand = new MoveCommand();
            string[] commandParts = { "MOVE", "10" }; // Missing Y position

            // Act
            bool result = moveCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid syntax should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidXPosition_ReturnsFalse()
        {
            // Arrange
            var moveCommand = new MoveCommand();
            string[] commandParts = { "MOVE", "abc", "20" }; // Non-integer X position

            // Act
            bool result = moveCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid X position should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidYPosition_ReturnsFalse()
        {
            // Arrange
            var moveCommand = new MoveCommand();
            string[] commandParts = { "MOVE", "10", "xyz" }; // Non-integer Y position

            // Act
            bool result = moveCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid Y position should return false.");
        }

        [Test]
        public void Execute_ValidCommand_UpdatesCoordinates()
        {
            // Arrange
            var moveCommand = new MoveCommand();
            string[] commandParts = { "MOVE", "30", "40" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            moveCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);

            // Assert
            Assert.That(30 == x, "X coordinate should be updated to 30.");
            Assert.That(40 == y, "Y coordinate should be updated to 40.");
        }
    }
}
