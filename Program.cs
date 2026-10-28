using QuizGameApp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuizGameApp
{
    public class StartForm : Form
    {
        private Label welcomeLbl;
        private Button startBtn;

        public StartForm()
        {
            this.Text = "Start Form";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackgroundImage = Image.FromFile("background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            welcomeLbl = new Label();
            welcomeLbl.Text = "Welcome to the Academic Quiz Game!";
            welcomeLbl.Font = new Font("Arial", 26, FontStyle.Bold);
            welcomeLbl.AutoSize = true;
            welcomeLbl.Location = new Point(200, 130);
            welcomeLbl.BackColor = Color.Transparent;
            this.Controls.Add(welcomeLbl);

            startBtn = new Button();
            startBtn.Text = "Start";
            startBtn.Font = new Font("Arial", 18);
            startBtn.Size = new Size(250, 80);
            startBtn.Location = new Point(373, 400);
            startBtn.BackgroundImage = Image.FromFile("SelectCourseButton.jpg");
            startBtn.BackgroundImageLayout = ImageLayout.Stretch;
            startBtn.Click += OnStartBtnClick;
            this.Controls.Add(startBtn);
        }

        private void OnStartBtnClick(object sender, EventArgs e)
        {
            this.Hide();
            CourseSelectForm courseForm = new CourseSelectForm();
            courseForm.ShowDialog();
            this.Close();
        }
    }

    // COURSE SELECT FORM
    public class CourseSelectForm : Form
    {
        private Label selectLbl;
        private Button it132Btn;
        private Button env075Btn;
        private Button hum011Btn;

        public CourseSelectForm()
        {
            this.Text = "Select Course";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackgroundImage = Image.FromFile("background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            selectLbl = new Label();
            selectLbl.Text = "Select your Course";
            selectLbl.Font = new Font("Arial", 40, FontStyle.Bold);
            selectLbl.AutoSize = true;
            selectLbl.Location = new Point(250, 100);
            selectLbl.BackColor = Color.Transparent;
            selectLbl.ForeColor = Color.White;
            this.Controls.Add(selectLbl);

            it132Btn = new Button();
            it132Btn.Text = "IT132";
            it132Btn.Font = new Font("Arial", 24);
            it132Btn.Size = new Size(300, 120);
            it132Btn.Location = new Point(350, 240);
            it132Btn.BackgroundImage = Image.FromFile("SelectCourseButton.jpg");
            it132Btn.BackgroundImageLayout = ImageLayout.Stretch;
            it132Btn.Click += OnIT132;
            this.Controls.Add(it132Btn);

            env075Btn = new Button();
            env075Btn.Text = "ENV075";
            env075Btn.Font = new Font("Arial", 24);
            env075Btn.Size = new Size(300, 120);
            env075Btn.Location = new Point(350, 380);
            env075Btn.BackgroundImage = Image.FromFile("SelectCourseButton.jpg");
            env075Btn.BackgroundImageLayout = ImageLayout.Stretch;
            env075Btn.Click += OnENV075;
            this.Controls.Add(env075Btn);

            hum011Btn = new Button();
            hum011Btn.Text = "HUM011";
            hum011Btn.Font = new Font("Arial", 24);
            hum011Btn.Size = new Size(300, 120);
            hum011Btn.Location = new Point(350, 520);
            hum011Btn.BackgroundImage = Image.FromFile("SelectCourseButton.jpg");
            hum011Btn.BackgroundImageLayout = ImageLayout.Stretch;
            hum011Btn.Click += OnHUM011;
            this.Controls.Add(hum011Btn);
        }

        private void OnIT132(object sender, EventArgs e)
        {
            this.Hide();
            QnAForm quiz = new QnAForm("IT132");
            quiz.ShowDialog();
            this.Close();
        }

        private void OnENV075(object sender, EventArgs e)
        {
            this.Hide();
            QnAForm quiz = new QnAForm("ENV075");
            quiz.ShowDialog();
            this.Close();
        }

        private void OnHUM011(object sender, EventArgs e)
        {
            this.Hide();
            QnAForm quiz = new QnAForm("HUM011");
            quiz.ShowDialog();
            this.Close();
        }
    }

    // QnA FORM
    public class QnAForm : Form
    {
        // --- COMPONENTS ---
        private Label questionLbl;
        private RadioButton choiceA;
        private RadioButton choiceB;
        private RadioButton choiceC;
        private RadioButton choiceD;
        private Button nextBtn;

        private Label scoreLabel;
        private PictureBox livesPicture;

        // --- QUIZ DATA ---
        private List<Question> questions;
        private int currentIndex;
        private int score;
        private int lives;
        private bool miniGameUsed;
        private string selectedCourse;

        public QnAForm(string course)
        {
            // initialize fields
            questions = new List<Question>();
            currentIndex = 0;
            score = 0;
            lives = 3; // starting lives
            miniGameUsed = false;
            selectedCourse = course;

            // --- FORM PROPERTIES ---
            this.Text = "Quiz - " + course;
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackgroundImage = Image.FromFile("background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // SCORE LABEL (top-left)
            scoreLabel = new Label();
            scoreLabel.Text = "Score: " + score;
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(20, 20);
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.ForeColor = Color.Black;

            // LIVES PICTURE (top-right)
            livesPicture = new PictureBox();
            livesPicture.Size = new Size(150, 100);
            livesPicture.Location = new Point(820, 4);
            livesPicture.BackColor = Color.Transparent;
            livesPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            livesPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // QUESTION LABEL
            questionLbl = new Label();
            questionLbl.Size = new Size(900, 80);
            questionLbl.Location = new Point(60, 100);
            questionLbl.Font = new Font("Arial", 26, FontStyle.Bold);
            questionLbl.BackColor = Color.Transparent;

            // CHOICES
            choiceA = new RadioButton();
            choiceA.Location = new Point(120, 250);
            choiceA.Width = 800;
            choiceA.BackColor = Color.Transparent;
            choiceA.Font = new Font("Arial", 18, FontStyle.Bold);

            choiceB = new RadioButton();
            choiceB.Location = new Point(120, 350);
            choiceB.Width = 800;
            choiceB.BackColor = Color.Transparent;
            choiceB.Font = new Font("Arial", 18, FontStyle.Bold);

            choiceC = new RadioButton();
            choiceC.Location = new Point(120, 450);
            choiceC.Width = 800;
            choiceC.BackColor = Color.Transparent;
            choiceC.Font = new Font("Arial", 18, FontStyle.Bold);

            choiceD = new RadioButton();
            choiceD.Location = new Point(120, 550);
            choiceD.Width = 800;
            choiceD.BackColor = Color.Transparent;
            choiceD.Font = new Font("Arial", 18, FontStyle.Bold);

            // NEXT BUTTON
            nextBtn = new Button();
            nextBtn.Text = "Next";
            nextBtn.Size = new Size(160, 50);
            nextBtn.Location = new Point(820, 650);
            nextBtn.Font = new Font("Arial", 18);
            nextBtn.BackgroundImage = Image.FromFile("PlayAgainButton_ExitButton.jpg");


            nextBtn.Click += NextBtn_Click;

            // ADD CONTROLS TO FORM
            this.Controls.Add(scoreLabel);
            this.Controls.Add(livesPicture);
            this.Controls.Add(questionLbl);
            this.Controls.Add(choiceA);
            this.Controls.Add(choiceB);
            this.Controls.Add(choiceC);
            this.Controls.Add(choiceD);
            this.Controls.Add(nextBtn);

            try
            {
                LoadQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message);
                this.Close();
                return;
            }

            if (questions.Count == 0)
            {
                MessageBox.Show("No questions found for " + selectedCourse + ".txt");
                this.Close();
                return;
            }

            // show first question and lives image
            DisplayQuestion();
            DisplayLives();
        }

        private void LoadQuestions()
        {
            // read all lines directly (no File.Exists check)
            string fileName = selectedCourse + ".txt";
            string[] lines = File.ReadAllLines(fileName);

            for (int i = 0; i + 4 < lines.Length; i += 5)
            {
                string q = lines[i].Trim();
                string a = lines[i + 1].Trim();
                string b = lines[i + 2].Trim();
                string c = lines[i + 3].Trim();
                string d = lines[i + 4].Trim();

                string correct = "";

                if (a.StartsWith(">"))
                {
                    correct = a.Substring(1).Trim();
                    a = a.Substring(1).Trim();
                }

                if (b.StartsWith(">"))
                {
                    correct = b.Substring(1).Trim();
                    b = b.Substring(1).Trim();
                }

                if (c.StartsWith(">"))
                {
                    correct = c.Substring(1).Trim();
                    c = c.Substring(1).Trim();
                }

                if (d.StartsWith(">"))
                {
                    correct = d.Substring(1).Trim();
                    d = d.Substring(1).Trim();
                }

                if (string.IsNullOrEmpty(correct))
                {
                    correct = a; // fallback
                }

                Question question = new Question();
                question.Q = q;
                question.A = a;
                question.B = b;
                question.C = c;
                question.D = d;
                question.Correct = correct;

                questions.Add(question);
            }
        }

        private void DisplayQuestion()
        {
            if (currentIndex < 0 || currentIndex >= questions.Count) return;

            Question question = questions[currentIndex];
            questionLbl.Text = question.Q;

            choiceA.Text = question.A;
            choiceB.Text = question.B;
            choiceC.Text = question.C;
            choiceD.Text = question.D;

            choiceA.Checked = false;
            choiceB.Checked = false;
            choiceC.Checked = false;
            choiceD.Checked = false;
        }

        private void DisplayLives()
        {
            // update score text
            scoreLabel.Text = "Score: " + score;

            // direct image loading (no File.Exists)
            if (lives == 3)
            {
                livesPicture.Image = Image.FromFile("hp_3.png");

            }
            else if (lives == 2)
            {
                livesPicture.Image = Image.FromFile("hp_2.png");
            }
            else if (lives == 1)
            {
                livesPicture.Image = Image.FromFile("hp_1.png");
            }
            else
            {
                livesPicture.Image = null;
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            string selected = "";

            if (choiceA.Checked)
            {
                selected = choiceA.Text;
            }
            else if (choiceB.Checked)
            {
                selected = choiceB.Text;
            }
            else if (choiceC.Checked)
            {
                selected = choiceC.Text;
            }
            else if (choiceD.Checked)
            {
                selected = choiceD.Text;
            }

            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            // check correctness
            if (selected == questions[currentIndex].Correct)
            {
                score = score + 1;
            }
            else
            {
                lives = lives - 1;

                if (lives <= 0)
                {
                    if (!miniGameUsed)
                    {
                        miniGameUsed = true;

                        GuessMiniGameForm mini = new GuessMiniGameForm();
                        mini.ShowDialog();

                        if (mini.WonLife)
                        {
                            lives = 1; // reward one life
                            MessageBox.Show("Correct guess! +1 life!");
                        }
                        else
                        {
                            MessageBox.Show("Mini-game failed. Game Over!");
                            EndQuiz();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No lives left! (Mini-game already used)");
                        EndQuiz();
                        return;
                    }
                }
            }

            currentIndex = currentIndex + 1;

            DisplayLives();

            if (currentIndex >= questions.Count)
            {
                EndQuiz();
                return;
            }

            DisplayQuestion();
        }

        private void EndQuiz()
        {
            DisplayScoreForm display = new DisplayScoreForm(score, questions.Count);

            this.Hide();
            DialogResult result = display.ShowDialog();
            this.Close();

            if (result == DialogResult.Yes)
            {
                StartForm s = new StartForm();
                s.ShowDialog();
            }
            else
            {
                Application.Exit();
            }
        }
    }
    public class Question
    {
        public string Q;
        public string A;
        public string B;
        public string C;
        public string D;
        public string Correct;
    }

    // GUESS MINI-GAME FORM
    public class GuessMiniGameForm : Form
    {
        private Label instructionLbl1;
        private Label instructionLbl2;
        private TextBox guessBox;
        private Button submitBtn;

        public bool WonLife { get; private set; }

        int randomNumber;

        public GuessMiniGameForm()
        {
            this.Text = "Mini Guessing Game";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // direct background
            this.BackgroundImage = Image.FromFile("MiniGameBg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Random rnd = new Random();
            randomNumber = rnd.Next(1, 6); // 

            instructionLbl1 = new Label
            {
                Text = "GUESS THE",
                Font = new Font("Arial", 28, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(110, 30),
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };

            instructionLbl2 = new Label
            {
                Text = "NUMBER (1 - 5)",
                Font = new Font("Arial", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(110, 80),
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };

            guessBox = new TextBox
            {
                Font = new Font("Arial", 20),
                Location = new Point(140, 150),
                Width = 100,
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            submitBtn = new Button
            {
                Text = "Submit",
                Font = new Font("Arial", 12),
                Size = new Size(120, 40),
                Location = new Point(140, 210)
            };

            submitBtn.Click += SubmitBtn_Click;

            this.Controls.Add(instructionLbl1);
            this.Controls.Add(instructionLbl2);
            this.Controls.Add(guessBox);
            this.Controls.Add(submitBtn);
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int guess;
                if (!int.TryParse(guessBox.Text.Trim(), out guess))
                {
                    MessageBox.Show("Invalid input. Please enter a number between 1 and 5.");
                    return;
                }

                if (guess < 1 || guess > 10)
                {
                    MessageBox.Show("Please enter a number between 1 and 5.");
                    return;
                }

                if (guess == randomNumber)
                {
                    WonLife = true;
                    MessageBox.Show("Correct! You gain +1 life!");
                }
                else
                {
                    WonLife = false;
                    MessageBox.Show($"Wrong! The number was {randomNumber}.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter number.");
                return;
            }
        }
    }

    // DISPLAY SCORE FORM
    public class DisplayScoreForm : Form
    {
        private Label scoreLabel;
        private Label questionLabel;
        private Button playAgainButton;
        private Button exitButton;

        public DisplayScoreForm(int score, int totalQuestions)
        {
            this.Text = "Quiz Results";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackgroundImage = Image.FromFile("background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            scoreLabel = new Label();
            scoreLabel.Text = "Score: " + score.ToString();
            scoreLabel.Font = new Font("Arial", 40, FontStyle.Bold);
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(350, 200);
            scoreLabel.BackColor = Color.Transparent;
            scoreLabel.ForeColor = Color.White;


            questionLabel = new Label();
            questionLabel.Text = "Total Questions: " + totalQuestions.ToString();
            questionLabel.Font = new Font("Arial", 30, FontStyle.Bold);
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(320, 270);
            questionLabel.BackColor = Color.Transparent;
            questionLabel.ForeColor = Color.White;


            // PLAY AGAIN BUTTON
            playAgainButton = new Button();
            playAgainButton.Text = "Play Again";
            playAgainButton.Font = new Font("Arial", 14);
            playAgainButton.Size = new Size(160, 50);
            playAgainButton.Location = new Point(320, 500);
            playAgainButton.BackgroundImage = Image.FromFile("PlayAgainButton_ExitButton.jpg");

            // EXIT BUTTON
            exitButton = new Button();
            exitButton.Text = "Exit";
            exitButton.Font = new Font("Arial", 14);
            exitButton.Size = new Size(160, 50);
            exitButton.Location = new Point(520, 500);
            exitButton.BackgroundImage = Image.FromFile("PlayAgainButton_ExitButton.jpg");

            playAgainButton.Click += PlayAgainButton_Click;
            exitButton.Click += ExitButton_Click;

            this.Controls.Add(scoreLabel);
            this.Controls.Add(questionLabel);
            this.Controls.Add(playAgainButton);
            this.Controls.Add(exitButton);
        }

        private void PlayAgainButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm start = new StartForm();
            start.ShowDialog();
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

static class Program
{
    static void Main()
    {
        Application.Run(new StartForm());
    }
}
