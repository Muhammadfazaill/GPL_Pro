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


            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
