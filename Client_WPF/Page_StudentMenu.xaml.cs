using Client_WPF.Classes;
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
    /// Interaction logic for Page_StudentGuest.xaml
    /// </summary>
    public partial class Page_StudentMenu : Page
    {
        private int studentID;   // the ID number of the student

        public Page_StudentMenu()
        {
            InitializeComponent();

            studentName.Content = All_Pages.GetUser().Full_Name;
            this.studentID = All_Pages.GetProjectService().GetStudentIDServer(All_Pages.GetUser().ID);
            SetQuizTitles();
        }
        
        private void Logout_Click(object sender, EventArgs e)
        {
            All_Pages.GetLogoutMsg().Show();
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetQuizPage(quizTitle.Text, this.studentID));
        }

        private void quizTitels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.startQuiz.Visibility = Visibility.Visible;
        }

        // ----------------------------------------------------------------------------------------------
        // showing the student all the titles of all the quizzes he can solve in the database
        // ----------------------------------------------------------------------------------------------

        private void SetQuizTitles()
        {
            string[] quizTitelsArr = All_Pages.GetProjectService().GetAllValidQuizTitlesServer(this.studentID);
            
            for (int i = 0; i < quizTitelsArr.Length; i++)
                quizTitles.Items.Add(quizTitelsArr[i]);
        }
    }
}