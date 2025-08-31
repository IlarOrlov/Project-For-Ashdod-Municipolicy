using Client_WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Page_Signup.xaml
    /// </summary>
    public partial class Page_Signup : Page
    {
        public Page_Signup()
        {
            InitializeComponent();
        }

        private void female_Checked(object sender, RoutedEventArgs e)
        {
            male.IsChecked = false;
        }

        private void male_Checked(object sender, RoutedEventArgs e)
        {
            female.IsChecked = false;
        }

        // ----------------------------------------------------------------------------------------------
        // signing up the new currentUser and adding him to the database
        // ----------------------------------------------------------------------------------------------

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            // -- checking if the currentUser fields are filled & valid --
            if (CheckUserFields() == false)
            {
                // -- checking if there is a currentUser with the same IDnumber --
                if (All_Pages.GetProjectService().IsIDnumberExistServer(IDnumber.Text) == true)
                {
                    All_Pages.GetSignupFailedMsg().Show();
                    IDnumber.Clear();
                }

                else
                {
                    // -- setting the gender of the new currentUser --
                    string gender = "";
                    if (male.IsChecked == true)
                        gender = "M";

                    else
                        gender = "F";

                    // -- checking if the new currentUser & new student successfully added to the database --
                    if (All_Pages.GetProjectService().InsertNewUserServer(fullName.Text, passWord.Text, email.Text, gender, IDnumber.Text, phoneNumber.Text) == true &&
                        All_Pages.GetProjectService().InsertStudentServer(IDnumber.Text, school.Text, grade.Text, gradeNum.Text) == true)
                    {
                        All_Pages.GetSignupSucceededMsg().Show();
                        All_Pages.SetUser(All_Pages.GetProjectService().GetUserServer(IDnumber.Text));
                        NavigationService navigation = All_Pages.GetFrame().NavigationService;
                        navigation.Navigate(All_Pages.GetStudentMenuPage());
                    }

                    else
                    {
                        All_Pages.GetSignupFailedMsg().Show();
                    }
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

            // -- checking the IDnumber --
            if (CheckIDnumber() == false)
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

            // -- checking the password & the password verification --
            if (passWord.Text == "" || verifyPass.Text == "" || verifyPass.Text != passWord.Text)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                passWord.BorderThickness = new Thickness(3);
                passWord.BorderBrush = Brushes.Red;
                verifyPass.BorderThickness = new Thickness(3);
                verifyPass.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                passWord.BorderThickness = new Thickness(1);
                passWord.BorderBrush = Brushes.Black;
                verifyPass.BorderThickness = new Thickness(1);
                verifyPass.BorderBrush = Brushes.Black;
            }

            // -- checking the full name --
            if (CheckFullName() == false)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                fullName.BorderThickness = new Thickness(3);
                fullName.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                fullName.BorderThickness = new Thickness(1);
                fullName.BorderBrush = Brushes.Black;
            }

            // -- checking the email --
            if (CheckEmail() == false)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                email.BorderThickness = new Thickness(3);
                email.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                email.BorderThickness = new Thickness(1);
                email.BorderBrush = Brushes.Black;
            }

            // -- checking the phone number --
            if (CheckPhoneNumber() == false)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                phoneNumber.BorderThickness = new Thickness(3);
                phoneNumber.BorderBrush = Brushes.Red;
                invalidFields = true;
            }
            else
            {
                phoneNumber.BorderThickness = new Thickness(1);
                phoneNumber.BorderBrush = Brushes.Black;
            }

            // -- checking the gender --
            if (male.IsChecked == false && female.IsChecked == false)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                female.BorderBrush = Brushes.Red;
                male.BorderBrush = Brushes.Red;
                invalidFields = true;
            }
            else
            {
                female.BorderBrush = Brushes.Blue;
                male.BorderBrush = Brushes.Blue;
            }

            // -- checking the school --
            if (school.SelectedItem == null)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                school.BorderBrush = Brushes.Red;
                invalidFields = true;
            }
            else
            {
                school.BorderBrush = Brushes.Blue;
            }

            // -- checkimg the grade --
            if (grade.SelectedItem == null)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                grade.BorderBrush = Brushes.Red;
                invalidFields = true;
            }
            else
            {
                grade.BorderBrush = Brushes.Blue;
            }

            // -- checking the grade number --
            if (gradeNum.SelectedItem == null)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                gradeNum.BorderBrush = Brushes.Red;
                invalidFields = true;
            }
            else
            {
                gradeNum.BorderBrush = Brushes.Blue;
            }

            return (invalidFields);
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the ID number of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckIDnumber()
        {
            string IDnum = IDnumber.Text;   // the ID number the currentUser wrote
            int sum = 0;                    // the sum of the ID number digits to calculate the check digit
            int digit = 0;                  // the current digit we are checking
            int checkDigit = 0;             // the check digit of the ID number

            // --- checks if the currentUser filled the ID number ---
            if (IDnum == "")
                return (false);

            // --- checks if the whole ID number contains ONLY digits ---
            for (int i = 0; i < IDnum.Length; i++)
            {
                if (IDnum[i] < '0' || IDnum[i] > '9')
                {
                    return (false);
                }
            }

            // --- checks if the ID number is valid ---

            if (IDnum.Length != 9)
                return (false);

            for (int i = 0; i < 8; i++)
            {
                digit = IDnum[i] - '0';

                if (i % 2 == 0)
                    sum += digit;

                else
                {
                    if (digit < 5)
                        sum += (digit * 2);

                    else
                        sum += (digit * 2) - 9;
                }
            }

            checkDigit = (10 - (sum % 10)) % 10;

            if (checkDigit != IDnum[8] - '0')
                return (false);

            return (true);
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the full name of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckFullName()
        {
            string name = fullName.Text;

            if (name == "")
                return (false);

            for (int i = 0; i < name.Length; i++)
            {
                if ((name[i] < 'א' || name[i] > 'ת') && name[i] != ' ' && name[i] != '\'' && name[i] != '-')
                    return (false);
            }

            return (true);
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the email address of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckEmail()
        {
            string emailAddress = email.Text;
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (emailAddress == "")
                return (false);

            if (Regex.IsMatch(emailAddress, emailPattern) == false)
                return (false);

            return (true);
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the phone number of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckPhoneNumber()
        {
            string userPhoneNumber = phoneNumber.Text;

            if (userPhoneNumber == "")
                return (false);

            if (userPhoneNumber.Length != 10)
                return (false);

            for (int i = 0; i < userPhoneNumber.Length; i++)
            {
                if (userPhoneNumber[i] < '0' || userPhoneNumber[i] > '9')
                {
                    return (false);
                }
            }

            if (userPhoneNumber[0] != '0' || userPhoneNumber[1] != '5')
                return (false);

            return (true);
        }
    }
}