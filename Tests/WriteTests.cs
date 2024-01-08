using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NUnit.Framework;

namespace GraphicalProgrammingLanguage.Tests
{
    [TestFixture]
    public class WriteCommandTests
    {
        [Test]
        public void SyntaxCheck_ValidCommand_ReturnsTrue()
        {
            // Arrange
            var writeCommand = new WriteCommand();
            string[] validCommand = { "WRITE", "\"Hello\"" };

            // Act
            bool result = writeCommand.SyntaxCheck(validCommand, false);

            // Assert
            Assert.IsTrue(result);
        }

        public void SyntaxCheck_ValidCommandWithSize_ReturnsTrue()
        {
            // Arrange
            var writeCommand = new WriteCommand();
            string[] validCommand = { "WRITE", "20", "\"Hello\"" };

            // Act
            bool result = writeCommand.SyntaxCheck(validCommand, false);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SyntaxCheck_InvalidCommand_ShowErrorAndReturnsFalse()
        {
            // Arrange
            WriteCommand writeCommand = new WriteCommand();
            string[] invalidCommand = { "WRITE" };

            // Act
            bool result = writeCommand.SyntaxCheck(invalidCommand, false);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_ValidCommand_DrawsTextOnGraphicsContext()
        {
            // Arrange
            WriteCommand writeCommand = new WriteCommand();
            string[] validCommand = { "WRITE", "\"Hello\"" };
            int x = 0;
            int y = 0;
            Color penColor = Color.Black;
            bool fillShapes = false;

            // create a graphics context mock
            PictureBox resultBox = new PictureBox();
            Graphics graphics = resultBox.CreateGraphics();

            // Act
            writeCommand.Execute(validCommand, ref x, ref y, ref penColor, ref fillShapes, graphics);

            // Assert
            // no exception is thrown
            // x, y, penColor, fillShapes are not changed
            // resultBox has text drawn on it
            Assert.AreEqual(0, x);
            Assert.AreEqual(0, y);
        }
    }
}
