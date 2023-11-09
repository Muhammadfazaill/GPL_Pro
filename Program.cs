using System;
using System.IO;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    /// <summary>
    /// The main form of the Graphical Programming Language application.
    /// </summary>
    public partial class MainForm : Form
    {
        private TextBox programTextBox = new TextBox();
        private TextBox commandTextBox = new TextBox();
        private Button runButton = new Button();
        private Button syntaxCheckButton = new Button();
        private PictureBox resultBox = new PictureBox();
        private KeyboardControl keyboardControl = new KeyboardControl();
        private Label statusLabel = new Label();
        private Button saveButton = new Button();
        private Button loadButton = new Button();


        /// Initializes the GUI components.
        /// </summary>
        private void InitializeComponent()
        {
            // Create and configure GUI components
            programTextBox.Multiline = true;
            programTextBox.Size = new System.Drawing.Size(400, 200);
            programTextBox.Location = new System.Drawing.Point(10, 10);
            programTextBox.ScrollBars = ScrollBars.Vertical;

            // Set up the keyboard control
            programTextBox.Enter += (sender, e) =>
            {
                keyboardControl.SetActiveTextBox(programTextBox);
            };

            commandTextBox.Size = new System.Drawing.Size(400, 30);
            commandTextBox.Location = new System.Drawing.Point(10, 220);

            commandTextBox.Enter += (sender, e) =>
            {
                keyboardControl.SetActiveTextBox(commandTextBox);
            };

            
            // add different buttons and their properties
            runButton.Text = "Run";
            runButton.Size = new System.Drawing.Size(195, 30);
            runButton.Location = new System.Drawing.Point(10, 260);
            runButton.Click += RunButton_Click;

            syntaxCheckButton.Text = "Syntax Check";
            syntaxCheckButton.Size = new System.Drawing.Size(195, 30);
            syntaxCheckButton.Location = new System.Drawing.Point(215, 260);
           // syntaxCheckButton.Click += SyntaxCheckButton_Click;

            saveButton.Text = "Save";
            saveButton.Size = new System.Drawing.Size(195, 30);
            saveButton.Location = new System.Drawing.Point(10, 300);
           // saveButton.Click += SaveButton_Click;

            loadButton.Text = "Load";
            loadButton.Size = new System.Drawing.Size(195, 30);
            loadButton.Location = new System.Drawing.Point(215, 300);
            //loadButton.Click += LoadButton_Click;

            resultBox.Size = new System.Drawing.Size(400, 400);
            resultBox.Location = new System.Drawing.Point(430, 10);
            resultBox.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(resultBox);
           
            // info label
            statusLabel.Text = "Position: {X=0, Y=0}\nFill: Off\nColor: Black";
            statusLabel.Size = new System.Drawing.Size(400, 50);
            statusLabel.Location = new System.Drawing.Point(10, 340);

            // Set up the form
            this.Text = "Graphical Programming Language";
            this.Size = new System.Drawing.Size(855, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Controls.Add(programTextBox);
            this.Controls.Add(commandTextBox);
            this.Controls.Add(runButton);
            this.Controls.Add(syntaxCheckButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(loadButton);
            this.Controls.Add(resultBox);
            this.Controls.Add(statusLabel);

            keyboardControl = new KeyboardControl();
            this.Controls.Add(keyboardControl);
            keyboardControl.BringToFront();
            keyboardControl.Location = new System.Drawing.Point(10, 420);
            keyboardControl.Visible = true;
        }

        private void RunButton_Click(object? sender, EventArgs e)
        {
            string command = commandTextBox.Text.Trim();

            if (command == "")
            {
                if (programTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a command.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    commandTextBox.Text = "RUN";
                    command = "RUN";
                }
            }

            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}