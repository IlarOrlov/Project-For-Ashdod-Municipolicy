using Client_WPF.Classes;
using Client_WPF.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for Page_Quiz.xaml
    /// </summary>
    public partial class Page_Quiz : Page
    {
        private int questionIndex;             // the index of the current question that is shown in the quiz
        private int correctAnswersNumber;      // the number of correct answers the student answerd
        private int studentID;                 // the ID number of the student who answers the quiz
        private string titleOfQuiz;            // the title of this quiz
        private List<string> studentAnswers;   // the student's answers
        private List<string> itemNumberAnswers;   // the student's answers
        private Quiz myQuiz;                   // the quiz
        private Random rnd;                    // the Random object for sorting the answers randomly
        private Button rightButton;            // the button of the right answer
        private Button wrongButton1;           // the button of the first wrong possible answer
        private Button wrongButton2;           // the button of the second wrong possible answer
        private Button wrongButton3;           // the button of the third wrong possible answer
        private Button[] buttonsArr;           // the array of all the possible answers buttons

        public Page_Quiz(string titleOfQuiz, int studentID)
        {
            InitializeComponent();

            this.questionIndex = 0;
            this.correctAnswersNumber = 0;
            this.studentID = studentID;
            this.titleOfQuiz = titleOfQuiz;
            this.studentAnswers = new List<string>();
            studentName.Content = All_Pages.GetUser().Full_Name;
            SetQuiz();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            All_Pages.GetLogoutMsg().Show();
        }

        // ----------------------------------------------------------------------------------------------
        // sets the quiz that the student is solving now
        // ----------------------------------------------------------------------------------------------

        private void SetQuiz()
        {
            this.myQuiz = All_Pages.GetProjectService().GetQuizByTitleServer(this.titleOfQuiz);

            if (this.myQuiz != null)
            {
                this.quizTitle.Text = this.myQuiz.Title;
                SetQuestion(this.myQuiz.Questions[this.questionIndex]);
            }
        }

        // ----------------------------------------------------------------------------------------------
        // decorating the structure of the current question in the page
        // ----------------------------------------------------------------------------------------------

        private void SetQuestion(Question q)
        {
            this.question.Text = q.QuestionContent;
            this.answers.Children.Clear();

            // -- creating the possible answers buttons --
            rightButton = new Button(); this.rightButton.Tag = 1; this.rightButton.Click += Answer_Click;
            wrongButton1 = new Button(); this.wrongButton1.Tag = 2; this.wrongButton1.Click += Answer_Click;
            wrongButton2 = new Button(); this.wrongButton2.Tag = 3; this.wrongButton2.Click += Answer_Click;
            wrongButton3 = new Button(); this.wrongButton3.Tag = 4; this.wrongButton3.Click += Answer_Click;

            // -- decorating the possible answers buttons --
            this.rightButton.FontFamily = new FontFamily("Assistant ExtraBold");
            this.wrongButton1.FontFamily = new FontFamily("Assistant ExtraBold");
            this.wrongButton2.FontFamily = new FontFamily("Assistant ExtraBold");
            this.wrongButton3.FontFamily = new FontFamily("Assistant ExtraBold");

            this.rightButton.Margin = new Thickness(2); this.rightButton.FontSize = 26;
            this.wrongButton1.Margin = new Thickness(2); this.wrongButton1.FontSize = 26;
            this.wrongButton2.Margin = new Thickness(2); this.wrongButton2.FontSize = 26;
            this.wrongButton3.Margin = new Thickness(2); this.wrongButton3.FontSize = 26;

            this.rightButton.Foreground = Brushes.White; this.rightButton.BorderBrush = Brushes.White;
            this.wrongButton1.Foreground = Brushes.White; this.wrongButton1.BorderBrush = Brushes.White;
            this.wrongButton2.Foreground = Brushes.White; this.wrongButton2.BorderBrush = Brushes.White;
            this.wrongButton3.Foreground = Brushes.White; this.wrongButton3.BorderBrush = Brushes.White;

            this.rightButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            this.wrongButton1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            this.wrongButton2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            this.wrongButton3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));

            RoundButtonEdges();

            // -- setting the buttons' content
            this.rightButton.Content = q.Answer;
            this.wrongButton1.Content = q.WrongAnswers[0];
            this.wrongButton2.Content = q.WrongAnswers[1];
            this.wrongButton3.Content = q.WrongAnswers[2];

            this.buttonsArr = new Button[4];
            this.buttonsArr[0] = this.rightButton; this.buttonsArr[1] = wrongButton1;
            this.buttonsArr[2] = this.wrongButton2; this.buttonsArr[3] = wrongButton3;

            // -- showing the buttons in the UniformGrid randomly --
            this.rnd = new Random();
            while (this.answers.Children.Count < 4)
            {
                int index = rnd.Next(0, 4);
                if (IsExist(this.buttonsArr[index]) == false)
                    this.answers.Children.Add(this.buttonsArr[index]);
            }
        }

        // ----------------------------------------------------------------------------------------------
        // rounding the border of the buttons
        // ----------------------------------------------------------------------------------------------

        private void RoundButtonEdges()
        {
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(50)));
            this.rightButton.Resources.Add(typeof(Border), borderStyle);
            this.wrongButton1.Resources.Add(typeof(Border), borderStyle);
            this.wrongButton2.Resources.Add(typeof(Border), borderStyle);
            this.wrongButton3.Resources.Add(typeof(Border), borderStyle);
        }

        // ----------------------------------------------------------------------------------------------
        // checking if this certain button is already exist in the answers UnifrormGrid
        // ----------------------------------------------------------------------------------------------

        private bool IsExist(Button button)
        {
            for (int i = 0; i < this.answers.Children.Count; i++ )
            {
                if (this.answers.Children[i] == button)
                    return (true);
            }

            return (false);
        }

        // ----------------------------------------------------------------------------------------------
        // checking if the student's answer is correct or not, saving his answer
        // and continue to the next question
        // ----------------------------------------------------------------------------------------------

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            string currentItemNumber = ItemNumberInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(currentItemNumber) || !System.Text.RegularExpressions.Regex.IsMatch(currentItemNumber, @"^\d{1,7}$"))
            {
                MessageBox.Show("יש להזין מספר פריט תקין (1 עד 7 ספרות).", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Button button = sender as Button;
            Question currentQuestion = this.myQuiz.Questions[this.questionIndex];

            bool isCorrectAnswer = button.Tag.Equals(1);
            bool isCorrectItemNumber = currentItemNumber == currentQuestion.ItemNumber?.ToString();

            if (isCorrectAnswer && isCorrectItemNumber)
            {
                this.correctAnswersNumber++;
            }

            this.questionIndex++;
            this.studentAnswers.Add(button.Content.ToString());
            this.itemNumberAnswers.Add(ItemNumberInput.Text);

            // Clear input for next question
            ItemNumberInput.Text = "";

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
            int quizID = myQuiz.ID;
            string answers = "";
            string itemNumbers = "";

            for (int i = 0; i < studentAnswers.Count; i++)
            {
                answers += studentAnswers[i] + "#";
                itemNumbers += itemNumberAnswers[i] + "#";
            }

            answers = answers.Remove(answers.Length - 1);
            itemNumbers = itemNumbers.Remove(itemNumbers.Length - 1);

            // -- checking if the student's answers successfully submitted --
            if (All_Pages.GetProjectService().InsertQuizSubmitServer(this.studentID, this.correctAnswersNumber, quizID, answers, itemNumbers) == true)
            {
                All_Pages.GetQuizFinishedMsg().Show();
                NavigationService navigation = All_Pages.GetFrame().NavigationService;
                navigation.Navigate(All_Pages.GetStudentMenuPage());
            }
        }
    }
}