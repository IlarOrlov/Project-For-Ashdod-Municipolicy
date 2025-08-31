using Client_WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_WPF
{
    /// <summary>
    /// Interaction logic for Page_CreateQuiz.xaml
    /// </summary>
    public partial class Page_CreateQuiz : Page
    {
        private string title;                            // the title of this Quiz
        private List<QuestionStructure> questionsList;   // the Question Structures of this Quiz's questions

        public Page_CreateQuiz()
        {
            InitializeComponent();
            this.questionsList = new List<QuestionStructure>();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetAdminMenuPage());
        }

        // ----------------------------------------------------------------------------------------------
        // - increasing the number of QuestionStructures in the page for adding a question
        // ( a quiz needs to have at least 1 question and no more then 20 questions )
        // - resizing the rectangle background according to the number of questions
        // ----------------------------------------------------------------------------------------------

        private void QuestionsUp_Click(object sender, RoutedEventArgs e)
        {
            int currentNumber = int.Parse(questionsNumber.Text);

            if (currentNumber < 20)
            {
                if (currentNumber == 0)
                    this.rectangle.Height += 180;

                else
                    this.rectangle.Height += 400;

                this.questionsList.Add(new QuestionStructure(this.newQuestions, currentNumber + 1));
                currentNumber++;
                questionsNumber.Text = currentNumber.ToString();
            }
        }

        // ----------------------------------------------------------------------------------------------
        // - decreasing the number of QuestionStructures in the page for removing a question
        // ( a quiz needs to have at least 1 )
        // - resizing the rectangle background according to the number of questions
        // ----------------------------------------------------------------------------------------------

        private void QuestionsDown_Click(object sender, RoutedEventArgs e)
        {
            int currentNumber = int.Parse(questionsNumber.Text);

            if (currentNumber > 1)
            {
                this.rectangle.Height -= 400;
                this.newQuestions.Children.RemoveAt(currentNumber - 1);
                this.questionsList.RemoveAt(currentNumber - 1);
                currentNumber--;
                questionsNumber.Text = currentNumber.ToString();
            }
        }

        // ----------------------------------------------------------------------------------------------
        // submiting the quiz and checking all the needed issues
        // ( all the needed issues are detailed in the "CheckQuiz" function description )
        // ----------------------------------------------------------------------------------------------

        private void SubmitQuiz_Click(object sender, RoutedEventArgs e)
        {
            int quizID = 0;         // the ID number of the quiz we are creating
            string question = "";   // the current question we are adding to the database
            string answer = "";     // the answer to the current question we are adding to the database
            string[] wrongAnswers = new string[3];   // the 3 wrong possible answers to the
                                                     // current question we are adding to the database
            
            // -- setting this Quiz's title --
            this.title = quizTitle.Text;

            if (CheckQuiz() == false)
            {
                string formattedStartDate = DateTime.Parse(startDate.Text).ToString("yyyy-MM-dd");
                string formattedEndDate = DateTime.Parse(endDate.Text).ToString("yyyy-MM-dd");

                // -- checking the new Quiz successfully added to the database --
                if (All_Pages.GetProjectService().InsertQuizServer
                    (title, formattedStartDate, formattedEndDate) == true)
                {
                    quizID = All_Pages.GetProjectService().GetQuizIDServer(title);

                    if (quizID > 0)
                    {
                        for (int i = 0; i < questionsList.Count(); i++)
                        {
                            // -- setting the fields of the current question we are adding to the database --
                            question = questionsList[i].GetQuestionTitle().Text;
                            answer = questionsList[i].GetRightAnswer();
                            wrongAnswers = questionsList[i].GetWrongAnswers();
                            string itemNumber = questionsList[i].GetItemNumber();

                            // -- checking the new question successfully added to the database --
                            if (All_Pages.GetProjectService().InsertQuestionServer(question, answer, wrongAnswers, quizID, itemNumber) == true)
                                All_Pages.GetQuizUploadSucceededMsg().Show();
                            else
                                All_Pages.GetQuizUploadFailedMsg().Show();

                        }
                    }

                    else
                        All_Pages.GetQuizUploadFailedMsg().Show();
                }

                else
                    All_Pages.GetQuizUploadFailedMsg().Show();
            }
        }

        // ----------------------------------------------------------------------------------------------
        // checking all the needed issues :
        //   * all the needed fields are filled & valid
        //   * the title is uniqe ( don't exist in other quiz in the database )
        // - everytime there is an issue, appears a Red and bold border around the issue &
        // a suitable error message
        // ----------------------------------------------------------------------------------------------

        public bool CheckQuiz()
        {
            bool invalidFields = false;   // shows if there is a problem in the currentUser's Quiz

            // -- checking the Quiz's title --
            if (quizTitle.Text == "     " || All_Pages.GetProjectService().IsQuizExistServer(title) == true)
            {
                if (quizTitle.Text == "     ")
                    All_Pages.GetInvalidQuizMsg().Show();

                else
                    All_Pages.GetQuizExistMsg().Show();

                quizTitle.BorderThickness = new Thickness(2);
                quizTitle.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                quizTitle.BorderThickness = new Thickness(1);
                quizTitle.BorderBrush = Brushes.Black;
            }

            // -- checking the start & end dates --
            if (startDate.SelectedDate.HasValue == false || startDate.SelectedDate < DateTime.Today)
            {
                All_Pages.GetInvalidQuizMsg().Show();
                startDate.BorderThickness = new Thickness(1);
                startDate.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                startDate.BorderThickness = new Thickness(1);
                startDate.BorderBrush = null;
            }

            if (endDate.SelectedDate.HasValue == false || endDate.SelectedDate <= DateTime.Today)
            {
                All_Pages.GetInvalidQuizMsg().Show();
                endDate.BorderThickness = new Thickness(1);
                endDate.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                endDate.BorderThickness = new Thickness(1);
                endDate.BorderBrush = null;
            }

            // -- checking the questions number --
            if (questionsNumber.Text == "0")
            {
                All_Pages.GetInvalidQuizMsg().Show();
                questionsNumber.BorderThickness = new Thickness(2);
                questionsNumber.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                questionsNumber.BorderThickness = new Thickness(1);
                questionsNumber.BorderBrush = Brushes.Black;
            }

            // -- checking the questions fields --
            for (int i = 0; i < questionsList.Count(); i++)
            {
                if (questionsList[i].GetQuestionTitle().Text == "")
                {
                    All_Pages.GetInvalidQuizMsg().Show();
                    questionsList[i].GetQuestionTitle().BorderThickness = new Thickness(2);
                    questionsList[i].GetQuestionTitle().BorderBrush = Brushes.Red;
                    invalidFields = true;
                }

                else
                {
                    questionsList[i].GetQuestionTitle().BorderThickness = new Thickness(1);
                    questionsList[i].GetQuestionTitle().BorderBrush = Brushes.Black;
                }

                for (int j = 0; j < 4; j++)
                {
                    if (questionsList[i].GetAnswers()[j].Text == "")
                    {
                        All_Pages.GetInvalidQuizMsg().Show();
                        questionsList[i].GetAnswers()[j].BorderThickness = new Thickness(2);
                        questionsList[i].GetAnswers()[j].BorderBrush = Brushes.Red;
                        invalidFields = true;
                    }

                    else
                    {
                        questionsList[i].GetAnswers()[j].BorderThickness = new Thickness(1);
                        questionsList[i].GetAnswers()[j].BorderBrush = Brushes.Black;
                    }
                }

                if (questionsList[i].GetCheckBoxes()[0].IsChecked == false && 
                    questionsList[i].GetCheckBoxes()[1].IsChecked == false &&
                    questionsList[i].GetCheckBoxes()[2].IsChecked == false &&
                    questionsList[i].GetCheckBoxes()[3].IsChecked == false)
                {
                    All_Pages.GetInvalidQuizMsg().Show();
                    questionsList[i].GetCheckBoxes()[0].BorderBrush = Brushes.Red;
                    questionsList[i].GetCheckBoxes()[1].BorderBrush = Brushes.Red;
                    questionsList[i].GetCheckBoxes()[2].BorderBrush = Brushes.Red;
                    questionsList[i].GetCheckBoxes()[3].BorderBrush = Brushes.Red;
                    invalidFields = true;
                }

                else
                {
                    questionsList[i].GetCheckBoxes()[0].BorderBrush = Brushes.Black;
                    questionsList[i].GetCheckBoxes()[1].BorderBrush = Brushes.Black;
                    questionsList[i].GetCheckBoxes()[2].BorderBrush = Brushes.Black;
                    questionsList[i].GetCheckBoxes()[3].BorderBrush = Brushes.Black;
                }
            }

            return (invalidFields);
        }
    }
}