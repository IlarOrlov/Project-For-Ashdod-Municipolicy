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
    /// Interaction logic for Page_AdminMenu.xaml
    /// </summary>
    public partial class Page_AdminMenu : Page
    {
        public Page_AdminMenu()
        {
            InitializeComponent();
        }

        private void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetCreateQuizPage());
        }

        private void DeleteQuiz_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetDeleteQuizPage());
        }

        private void CheckQuizStatistics_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetStatisticsPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetLoginPage());
        }
    }
}