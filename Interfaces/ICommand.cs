/// <summary>
/// Interface for commands. All commands must implement this interface.
/// </summary>
/// <remarks>
/// The interface defines two methods:
/// SyntaxCheck - checks the syntax of the command
/// Execute - executes the command
/// </remarks>

public interface ICommand
{
    bool SyntaxCheck(string[] commandParts, bool showError = true);
    void Execute(string[] commandParts, ref int x, ref int y, ref Color penColor, ref bool fillShapes, Graphics graphics);
}
