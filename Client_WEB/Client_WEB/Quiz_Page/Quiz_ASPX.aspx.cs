using Client_WEB.ProjectService_WEB;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client_WEB.Quiz_Page
{
    public partial class Quiz_ASPX : System.Web.UI.Page
    {
        private int questionIndex;             // the index of the current question that is shown in the quiz
        private int correctAnswersNumber;      // the number of correct answers the student answerd
        private int studentID;                 // the ID number of the student who answers the quiz
        private string titleOfQuiz;            // the title of this quiz
        private Quiz myQuiz;                   // the quiz
        private Random rnd;                    // the Random object for sorting the answers randomly
        private Button rightButton;            // the button of the right answer
        private Button wrongButton1;           // the button of the first wrong possible answer
        private Button wrongButton2;           // the button of the second wrong possible answer
        private Button wrongButton3;           // the button of the third wrong possible answer
        private Button[] buttonsArr;           // the array of all the possible answers buttons
        private List<string> studentAnswers = new List<string>();   // the student's answers

        protected void Page_Load(object sender, EventArgs e)
        {
            bool passedOK = false;   // checks if the user passed by the signup/login pages 

            try
            {
                passedOK = (bool)Session[Master_Page.PASSED];
            }

            catch (Exception ex)
            { }

            // -- checking if the user passed by a signup/login page --
            if (passedOK == true)
            {
                this.titleOfQuiz = Session["quizTitle"].ToString();
                this.studentID = int.Parse(Session["studentID"].ToString());

                if (!IsPostBack)
                {
                    this.questionIndex = 0;
                    this.correctAnswersNumber = 0;
                    ViewState["correctAnswersNumber"] = 0;
                    this.userName.Text = Master_Page.GetUser().Full_Name;
                    ViewState["answeredItems"] = "";
                }

                else
                {
                    this.questionIndex = int.Parse(ViewState["questionIndex"].ToString());
                }

                if (ViewState["submissionDone"] != null && (bool)ViewState["submissionDone"])
                {
                    Response.Redirect("../StudentMenu_Page/StudentMenu_ASPX.aspx");
                    return;
                }

                SetQuiz();
            }

            // -- returning him to the home page --
            else
            {
                Response.Redirect("../Opening_Page/Opening_HTML.html");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Opening_Page/Opening_HTML.html");
        }

        // ----------------------------------------------------------------------------------------------
        // sets the quiz that the student is solving now
        // ----------------------------------------------------------------------------------------------

        private void SetQuiz()
        {
            this.myQuiz = Master_Page.GetServiceProject().GetQuizByTitleServer(this.titleOfQuiz);

            if (this.myQuiz != null)
            {
                this.title.Text = this.myQuiz.Title;

                if (ViewState["submissionDone"] != null && (bool)ViewState["submissionDone"])
                {
                    // Already submitted - skip
                    return;
                }

                if (this.questionIndex < this.myQuiz.Questions.Length)
                {
                    SetQuestion(this.myQuiz.Questions[this.questionIndex]);
                }
                else
                {
                    SubmitStudentAnswers();
                }
            }
        }

        // ----------------------------------------------------------------------------------------------
        // decorating the structure of the current question in the page
        // ----------------------------------------------------------------------------------------------

        private void SetQuestion(Question q)
        {
            this.question.Text = q.QuestionContent;
            this.buttonsRow1.Controls.Clear();
            this.buttonsRow2.Controls.Clear();

            // -- creating, decorating & filling the possible answers buttons --
            rightButton = new Button()
            {
                ID = "1",
                Text = q.Answer,
                CssClass = "answer-button"
            };

            wrongButton1 = new Button()
            {
                ID = "2",
                Text = q.WrongAnswers[0],
                CssClass = "answer-button"
            };

            wrongButton2 = new Button()
            {
                ID = "3",
                Text = q.WrongAnswers[1],
                CssClass = "answer-button"
            };

            wrongButton3 = new Button()
            {
                ID = "4",
                Text = q.WrongAnswers[2],
                CssClass = "answer-button"
            };

            this.rightButton.Click += new EventHandler(Answer_Click);
            this.wrongButton1.Click += new EventHandler(Answer_Click);
            this.wrongButton2.Click += new EventHandler(Answer_Click);
            this.wrongButton3.Click += new EventHandler(Answer_Click);

            this.buttonsArr = new Button[4];
            this.buttonsArr[0] = this.rightButton; this.buttonsArr[1] = wrongButton1;
            this.buttonsArr[2] = this.wrongButton2; this.buttonsArr[3] = wrongButton3;

            ViewState["questionIndex"] = this.questionIndex;

            // -- showing the buttons in the UniformGrid randomly --
            this.rnd = new Random();
            while (this.buttonsRow1.Controls.Count + this.buttonsRow2.Controls.Count < 4)
            {
                int index = rnd.Next(0, 4);
                if (IsExist(this.buttonsArr[index]) == false)
                {
                    if (this.buttonsRow1.Controls.Count < 2)
                        this.buttonsRow1.Controls.Add(this.buttonsArr[index]);

                    else if (this.buttonsRow2.Controls.Count < 2)
                        this.buttonsRow2.Controls.Add(this.buttonsArr[index]);
                }
            }
        }

        // ----------------------------------------------------------------------------------------------
        // checking if this certain button is already exist in the answers UnifrormGrid
        // ----------------------------------------------------------------------------------------------

        private bool IsExist(Button button)
        {
            for (int i = 0; i < this.buttonsRow1.Controls.Count; i++)
            {
                if (this.buttonsRow1.Controls[i] == button)
                    return (true);
            }

            for (int i = 0; i < this.buttonsRow2.Controls.Count; i++)
            {
                if (this.buttonsRow2.Controls[i] == button)
                    return (true);
            }

            return (false);
        }

        // ----------------------------------------------------------------------------------------------
        // checking if the student's answer is correct or not, saving his answer
        // and continue to the next question
        // ----------------------------------------------------------------------------------------------

        protected void Answer_Click(object sender, EventArgs e)
        {
            string currentItemNumber = ItemNumberInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(currentItemNumber) || !Regex.IsMatch(currentItemNumber, @"^\d{1,7}$"))
            {
                // Invalid input – show error or return
                return;
            }

            Button button = sender as Button;
            Question currentQuestion = this.myQuiz.Questions[this.questionIndex];

            bool isCorrectAnswer = button.ID.Equals("1"); // Right answer button
            bool isCorrectItemNumber = currentItemNumber == currentQuestion.ItemNumber.ToString();

            if (isCorrectAnswer && isCorrectItemNumber)
            {
                this.correctAnswersNumber = int.Parse(ViewState["correctAnswersNumber"].ToString());
                this.correctAnswersNumber++;
                ViewState["correctAnswersNumber"] = this.correctAnswersNumber;
            }

            // Save the answer and item number
            this.questionIndex++;
            ViewState["questionIndex"] = this.questionIndex.ToString();
            ViewState["answers"] += button.Text + "#";

            if (string.IsNullOrEmpty(currentItemNumber))
                currentItemNumber = "N/A";

            ViewState["answeredItems"] += currentItemNumber + "#";

            if (this.questionIndex <= this.myQuiz.Questions.Length - 1)
                SetQuestion(this.myQuiz.Questions[this.questionIndex]);
            else
                SubmitStudentAnswers();
        }

        // ----------------------------------------------------------------------------------------------
        // submiting the student's answers after he finished all the questions
        // ----------------------------------------------------------------------------------------------

        private void SubmitStudentAnswers()
        {
            if (ViewState["submissionDone"] != null && (bool)ViewState["submissionDone"])
                return; // Don't submit again if already done

            int quizID = this.myQuiz.ID;
            string answers = ViewState["answers"].ToString().TrimEnd('#');
            string answeredItems = ViewState["answeredItems"].ToString().TrimEnd('#');

            this.correctAnswersNumber = int.Parse(ViewState["correctAnswersNumber"].ToString());

            // Submit to server
            bool success = Master_Page.GetServiceProject().InsertQuizSubmitServer(this.studentID, this.correctAnswersNumber, quizID, answers, answeredItems);

            if (success)
            {
                ViewState["submissionDone"] = true; // <-- Mark submission as done

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup",
                    "setTimeout(function() { document.getElementById('successPopup').style.display = 'block'; }, 200);", true);
            }
        }
    }
}