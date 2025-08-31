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
    /// Interaction logic for Page_DeleteQuiz.xaml
    /// </summary>
    public partial class Page_DeleteQuiz : Page
    {
        public Page_DeleteQuiz()
        {
            InitializeComponent();
            SetQuizTitles();
        }

        // ----------------------------------------------------------------------------------------------
        // deleting the quiz with with the selected title, and his questions
        // ----------------------------------------------------------------------------------------------

        private void DeleteQuiz_Click(object sender, RoutedEventArgs e)
        {
            // -- getting the ID number of the quiz by his title --
            int quizID = All_Pages.GetProjectService().GetQuizIDServer(quizTitle.Text);

            if (quizID > 0)
            {
                if (All_Pages.GetProjectService().DeleteQuestionsServer(quizID) &&
                    All_Pages.GetProjectService().DeleteQuizServer(quizTitle.Text))
                {
                    All_Pages.GetDeleteQuizSucceededMsg().Show();
                }

                else
                    All_Pages.GetDeleteQuizFailedMsg().Show();
            }

            else
                All_Pages.GetDeleteQuizFailedMsg().Show();
        }

        private void quizTitels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.deleteQuiz.Visibility = Visibility.Visible;
        }

        // ----------------------------------------------------------------------------------------------
        // showing all the titles of all the quizzes appears in the database
        // ----------------------------------------------------------------------------------------------

        private void SetQuizTitles()
        {
            string[] quizTitelsArr = All_Pages.GetProjectService().GetAllQuizTitlesServer();

            if (quizTitels != null)
            {
                for (int i = 0; i < quizTitelsArr.Length; i++)
                    quizTitels.Items.Add(quizTitelsArr[i]);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetAdminMenuPage());
        }
    }
}
