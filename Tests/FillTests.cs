using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class FillCommandTests
    {
        [Test]
        public void SyntaxCheck_FillOn_ReturnsTrue()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL", "ON" };

            // Act
            bool result = fillCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "FILL ON syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_FillOff_ReturnsTrue()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL", "OFF" };

            // Act
            bool result = fillCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsTrue(result, "FILL OFF syntax should return true.");
        }

        [Test]
        public void SyntaxCheck_InvalidArgumentsCount_ReturnsFalse()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL" }; // Missing argument

            // Act
            bool result = fillCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid argument count should return false.");
        }

        [Test]
        public void SyntaxCheck_InvalidFillValue_ReturnsFalse()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL", "INVALID" }; // Invalid fill value

            // Act
            bool result = fillCommand.SyntaxCheck(commandParts, false);

            // Assert
            Assert.IsFalse(result, "Invalid fill value should return false.");
        }

        [Test]
        public void Execute_FillOn_SetsFillShapesToTrue()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL", "ON" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            fillCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);

            // Assert
            Assert.IsTrue(fillShapes, "FILL ON should set fillShapes to true.");
        }

        [Test]
        public void Execute_FillOff_SetsFillShapesToFalse()
        {
            // Arrange
            var fillCommand = new FillCommand();
            string[] commandParts = { "FILL", "OFF" };
            int x = 10, y = 20;
            Color penColor = Color.Black;
            bool fillShapes = true;

            // Create a mock Graphics object
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            fillCommand.Execute(commandParts, ref x, ref y, ref penColor, ref fillShapes, graphics);

            // Assert
            Assert.IsFalse(fillShapes, "FILL OFF should set fillShapes to false.");
        }
    }
}
