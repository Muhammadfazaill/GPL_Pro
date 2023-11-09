using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Parses and executes commands for drawing shapes and graphics operations.
/// </summary>
public class CommandParser
{
    private Dictionary<string, ICommand> commandDictionary;
    private Graphics graphics;
    private int x;
    private int y;
    private Color penColor = Color.Black;
    private bool fillShapes = false;

    /// <summary>
    /// Initializes a new instance of the CommandParser class.
    /// </summary>
    /// <param name="graphics">The Graphics object for drawing.</param>
    /// <param name="initialX">The initial X-coordinate.</param>
    /// <param name="initialY">The initial Y-coordinate.</param>
    public CommandParser(Graphics graphics, int initialX = 0, int initialY = 0)
    {
        this.graphics = graphics;
        this.x = initialX;
        this.y = initialY;

        // Initialize the command dictionary with command names and their respective ICommand implementations
        commandDictionary = new Dictionary<string, ICommand>
        {
            { "MOVE", new MoveCommand() },
            { "DRAW", new DrawCommand() },
            { "CLEAR", new ClearCommand() },
            { "RESET", new ResetCommand() },
            { "RECTANGLE", new RectangleCommand() },
            { "CIRCLE", new CircleCommand() },
            { "TRIANGLE", new TriangleCommand() },
            { "COLOR", new ColorCommand() },
            { "FILL", new FillCommand() }
            // Can add entries for other commands
        };
    }

    /// <summary>
    /// Executes a single command based on the provided command text.
    /// </summary>
    /// <param name="commandText">The text of the command to execute.</param>
    public void ExecuteCommand(string commandText)
    {
        string[] parts = commandText.Split(' ');

        if (parts.Length == 0)
        {
            return; // Handle empty command
        }

        // First word is the command name
        string commandName = parts[0].ToUpper();

        if (commandDictionary.ContainsKey(commandName))
        {
            ICommand command = commandDictionary[commandName];

            if (command.SyntaxCheck(parts))
            {
                command.Execute(parts, ref x, ref y, ref penColor, ref fillShapes, graphics);
            }
            else
            {
                // Handle syntax error
                MessageBox.Show("Syntax error: " + commandName + " command is not formatted correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            // Handle unsupported command
            MessageBox.Show("Error: Unsupported command - " + commandName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Executes a program consisting of multiple commands.
    /// </summary>
    /// <param name="program">The program to execute.</param>
    public void ExecuteProgram(string program)
    {
        graphics.Clear(Color.LightGray);
        x = 0;
        y = 0;
        penColor = Color.Black;
        fillShapes = false;

        string[] lines = program.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            ExecuteCommand(line);
        }
    }

    /// <summary>
    /// Checks the syntax of a program consisting of multiple commands.
    /// </summary>
    /// <param name="program">The program to check.</param>
    public bool SyntaxCheckProgram(string program)
    {
        string[] lines = program.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            if (!SyntaxCheckLine(lines[i], i + 1))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks the syntax of a single command.
    /// </summary>
    /// <param name="line">The command to check.</param>
    /// <param name="lineNumber">The line number of the command.</param>
    public bool SyntaxCheckLine(string line, int lineNumber = 0)
    {
        // Syntax rules
        string[] validCommands = commandDictionary.Keys.ToArray();

        // Split the line into words
        string[] words = line.Split(' ');

        if (words.Length == 0)
        {
            // Empty line
            return true;
        }

        // Check if the first word is a valid command and it's in uppercase
        string firstWord = words[0].Trim();
        if (!validCommands.Contains(firstWord) || firstWord != firstWord.ToUpper())
        {
            // Invalid command
            SyntaxErrorException(lineNumber, line, "Invalid command: (command must be in uppercase, valid commands are: " + string.Join(", ", validCommands) + ", RUN)");
            return false;
        }

        if (commandDictionary.ContainsKey(firstWord))
        {
            ICommand command = commandDictionary[firstWord];

            if (command.SyntaxCheck(words))
            {
                return true;
            }
            else
            {
                SyntaxErrorException(lineNumber, line, "Syntax error in " + firstWord + " command.");
                return false;
            }
        }
        else
        {
            // Handle unsupported command
            SyntaxErrorException(lineNumber, line, "Unsupported command: " + firstWord);
            return false;
        }
    }

    /// <summary>
    /// Displays a syntax error message.
    /// </summary>
    /// <param name="line">The line number of the command.</param>
    /// <param name="command">The command text.</param>
    /// <param name="message">The error message.</param>
    private void SyntaxErrorException(int line, string command, string message = "")
    {
        MessageBox.Show("Syntax error at line " + line + ": " + command + "\n" + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // utility functions to provide current settings of the resulting drawing
    public Point GetCurrentPosition()
    {
        return new Point(x, y);
    }

    public bool IsFillOn()
    {
        return fillShapes;
    }

    // return the current pen color as a string (e.g. "Black")
    public string GetCurrentColor()
    {
        return penColor.Name;
    }
}
