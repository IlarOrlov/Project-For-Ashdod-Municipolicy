using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client_WEB.StudentMenu_Page
{
    public partial class StudentMenu_ASPX : Page
    {
        private int studentID;   // the ID number of the student

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
                this.studentID = Master_Page.GetServiceProject().GetStudentIDServer(Master_Page.GetUser().ID);

                if (!IsPostBack)
                {
                    this.userName.Text = Master_Page.GetUser().Full_Name;
                    SetQuizTitles();

                    // Hide the StartQuiz button on page load
                    startQuiz.Attributes["style"] = "display: none;";
                }
            }
            else
            {
                Response.Redirect("../Opening_Page/Opening_HTML.html");
            }
        }

        protected void quizTitels_SelectionChanged(object sender, EventArgs e)
        {
            // If no quiz is selected, hide the Start Quiz button
            if (quizTitles.SelectedValue == "בחר שאלון")
            {
                startQuiz.Attributes["style"] = "display: none;";
            }
            else
            {
                // Show the Start Quiz button when a valid quiz is selected
                startQuiz.Attributes["style"] = "display: inline-block; text-align: center; font-weight: bold; font-size: 32px; font-family: Nehama; height: 60px; width: 120px; color: white; background-color: #080864; border: 1px solid white; border-radius: 36px; margin-top: 15px;";
            }
        }

        protected void StartQuiz_Click(object sender, EventArgs e)
        {
            Session["quizTitle"] = quizTitles.SelectedValue;
            Session["studentID"] = this.studentID.ToString();
            Response.Redirect("../Quiz_Page/Quiz_ASPX.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Opening_Page/Opening_HTML.html");
        }

        // ----------------------------------------------------------------------------------------------
        // showing the student all the titles of all the quizzes he can solve in the database
        // ----------------------------------------------------------------------------------------------
        private void SetQuizTitles()
        {
            string[] quizTitlesArr = Master_Page.GetServiceProject().GetAllValidQuizTitlesServer(this.studentID);

            quizTitles.Items.Add(new ListItem("בחר שאלון"));
            for (int i = 0; i < quizTitlesArr.Length; i++)
                quizTitles.Items.Add(new ListItem(quizTitlesArr[i]));
        }
    }
}