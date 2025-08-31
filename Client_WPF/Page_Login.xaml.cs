using Client_WPF.Classes;
using Client_WPF.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for Page_Login.xaml
    /// </summary>
    public partial class Page_Login : Page
    {
        private string password;   // the currentUser's password

        public Page_Login()
        {
            InitializeComponent();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetForgotPasswordPage());
        }

        // ----------------------------------------------------------------------------------------------
        // checking if you are allowed to log in to the program
        // ----------------------------------------------------------------------------------------------

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // -- converting the PasswordBox content to string --
            ConvertPasswordToString();

            // -- checking if the fields you fiiled are valid --
            if (CheckUserFields() == false)
            {
                // -- checking if the currentUser exist --
                if (All_Pages.GetProjectService().IsUserExistServer(IDnumber.Text, this.password) == true)
                {
                    All_Pages.GetLoginSucceededMsg().Show();
                    All_Pages.SetUser(All_Pages.GetProjectService().GetUserServer(IDnumber.Text));

                    // -- checking if the currentUser is Admin or not --
                    if (All_Pages.GetProjectService().IsUserIsAdminServer(IDnumber.Text) == true)
                    {
                        NavigationService navigation = All_Pages.GetFrame().NavigationService;
                        navigation.Navigate(All_Pages.GetAdminMenuPage());
                    }

                    else
                    {
                        NavigationService navigation = All_Pages.GetFrame().NavigationService;
                        navigation.Navigate(All_Pages.GetStudentMenuPage());
                    }
                }

                else
                {
                    All_Pages.GetLoginFailedMsg().Show();
                    IDnumber.BorderThickness = new Thickness(3);
                    IDnumber.BorderBrush = Brushes.Red;
                    passWord.BorderThickness = new Thickness(3);
                    passWord.BorderBrush = Brushes.Red;
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetRegisterPage());
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the currentUser's fields are valid or not
        // ----------------------------------------------------------------------------------------------

        public bool CheckUserFields()
        {
            bool invalidFields = false;   // shows if there is a problem in the currentUser's fields

            // -- checking the ID number field --
            if (IDnumber.Text == "")
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                IDnumber.BorderThickness = new Thickness(3);
                IDnumber.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                IDnumber.BorderThickness = new Thickness(1);
                IDnumber.BorderBrush = Brushes.Black;
            }

            // -- checking the password field --
            if (this.password == "")
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                passWord.BorderThickness = new Thickness(3);
                passWord.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                passWord.BorderThickness = new Thickness(1);
                passWord.BorderBrush = Brushes.Black;
            }

            return (invalidFields);
        }

        public void ConvertPasswordToString()
        {
            SecureString securePassword = passWord.SecurePassword;
            this.password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;
        }
    }
}