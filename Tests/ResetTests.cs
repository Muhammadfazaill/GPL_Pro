using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class ResetCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidSyntax_ReturnsTrue()
        {
            // Arrange
            var resetCommand = new ResetCommand();
            string[] commandParts = { "RESET" };

            // Act
            bool result = resetCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "Valid syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var resetCommand = new ResetCommand();
            string[] commandParts = { "RESET", "extra" }; // Invalid arguments

            // Act
            bool result = resetCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void Execute_ResetsStateToDefaultValues()
        {
            // Arrange
            var resetCommand = new ResetCommand();
            string[] commandParts = { "RESET" };
            int x = 10, y = 20;
            Color penColor = Color.Red;
            bool fillShapes = true;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            resetCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);

            // Assert
            Assert.That(0 == x, "X coordinate should be reset to 0.");
            Assert.That(0 == y, "Y coordinate should be reset to 0.");
            Assert.That(Color.Black == penColor, "Pen color should be reset to Black.");
            Assert.IsFalse(fillShapes, "fillShapes should be reset to false.");
        }
    }
}
