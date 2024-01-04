/// <summary>
/// Interface for a special command. Special commands are commands that need access to the variables and methods. They don't need graphics.
/// </summary>
/// <remarks>
/// The interface defines two methods:
/// SyntaxCheck - checks the syntax of the command
/// Execute - executes the command
/// </remarks>

public interface ISpecialCommand
{
    bool SyntaxCheck(
        string[] commandParts,
        ref Dictionary<string, int> variables,
        ref Dictionary<string, string[]> methods,
        bool showError = true
    );
    void Execute(
        string[] commandParts,
        ref Dictionary<string, int> variables,
        ref Dictionary<string, string[]> methods,
        ref Stack<bool> isExecutingSpecialCommandStack,
        ref Stack<string> specialCommandsStack,
        ref int currentLineIndex
    );
}
