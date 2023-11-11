using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class ColorCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var colorCommand = new ColorCommand();
            string[] commandParts = { "COLOR", "RED" };

            // Act
            bool result = colorCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var colorCommand = new ColorCommand();
            string[] commandParts = { "COLOR" }; // Missing color argument

            // Act
            bool result = colorCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidColorName_ReturnsFalse()
        {
            // Arrange
            var colorCommand = new ColorCommand();
            string[] commandParts = { "COLOR", "INVALID" }; // Invalid color name

            // Act
            bool result = colorCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid color name should return false.");
        }

        [Test]
        public void SyntaxCheck_ValidColorNames_ReturnsTrue()
        {
            // Arrange
            var colorCommand = new ColorCommand();
            string[] validColors = { "BLACK", "BLUE", "RED", "GREEN" };

            foreach (var colorName in validColors)
            {
                string[] commandParts = { "COLOR", colorName };

                // Act
                bool result = colorCommand.SyntaxCheck(commandParts, false);

                // Assert
                Assert.IsTrue(result, $"{colorName} should be a valid color name.");
            }
        }

        [Test]
        public void Execute_ChangesPenColor()
        {
            // Arrange
            var colorCommand = new ColorCommand();
            string[] commandParts = { "COLOR", "RED" };
            int x = 10, y = 20;
            Color initialPenColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            colorCommand.Execute(commandParts, ref x, ref y, ref initialPenColor, ref fillShapes, graphics);

            // Assert
            Assert.That(Color.Red == initialPenColor, "Pen color should change to RED.");
        }
    }
}
