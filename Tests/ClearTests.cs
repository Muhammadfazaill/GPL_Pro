using NUnit.Framework;
using System.Windows.Forms;

[TestFixture]
public class ClearCommandTests
{
    private ClearCommand _clearCommand = new ClearCommand();

    [Test]
    public void SyntaxCheck_WithNoArguments_ReturnsTrue()
    {
        // Arrange
        string[] commandParts = new string[] { "CLEAR" };

        // Act
        bool result = _clearCommand.SyntaxCheck(commandParts, false);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void SyntaxCheck_WithOneArgument_ReturnsFalse()
    {
        // Arrange
        string[] commandParts = new string[] { "CLEAR", "arg1" };

        // Act
        bool result = _clearCommand.SyntaxCheck(commandParts, false);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void SyntaxCheck_WithMultipleArguments_ReturnsFalse()
    {
        // Arrange
        string[] commandParts = new string[] { "CLEAR", "arg1", "arg2" };

        // Act
        bool result = _clearCommand.SyntaxCheck(commandParts, false);

        // Assert
        Assert.IsFalse(result);
    }
}