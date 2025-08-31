using Client_WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for Page_ForgotPassword.xaml
    /// </summary>
    public partial class Page_ForgotPassword : Page
    {
        private string verifyCode;   // the verification code that the user gets to change the password

        public Page_ForgotPassword()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------------------------------
        // changing the password of a currentUser who forgot his password
        // ----------------------------------------------------------------------------------------------

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // -- checking if the fields are filled & valid --
            if (CheckUserFields() == false)
            {
                // -- checking if there is a user with this IDnumber in the database --
                if (All_Pages.GetProjectService().IsIDnumberExistServer(IDnumber.Text) == false)
                {
                    All_Pages.GetLoginFailedMsg().Show();
                    IDnumber.BorderThickness = new Thickness(3);
                    IDnumber.BorderBrush = Brushes.Red;
                }

                else
                {
                    IDnumber.BorderThickness = new Thickness(1);
                    IDnumber.BorderBrush = Brushes.Black;
                    All_Pages.SetUser(All_Pages.GetProjectService().GetUserServer(IDnumber.Text));
                    SendVerifyCode();
                }
            }
        }

        private void digit1_Changed(object sender, TextChangedEventArgs e)
        { digit2.Focus(); }

        private void digit2_Changed(object sender, TextChangedEventArgs e)
        { digit3.Focus(); }

        private void digit3_Changed(object sender, TextChangedEventArgs e)
        { digit4.Focus(); }

        private void digit4_Changed(object sender, TextChangedEventArgs e)
        { digit5.Focus(); }

        private void digit5_Changed(object sender, TextChangedEventArgs e)
        { digit6.Focus(); }

        private void digit6_Changed(object sender, TextChangedEventArgs e)
        {
            // -- checking if the user's code and the verification code are similar --
            if (this.verifyCode[0] == digit1.Text[0] && this.verifyCode[1] == digit2.Text[0] &&
                this.verifyCode[2] == digit3.Text[0] && this.verifyCode[3] == digit4.Text[0] &&
                this.verifyCode[4] == digit5.Text[0] && this.verifyCode[5] == digit6.Text[0])
            {
                // -- checking if the new password successfully updated in the database --
                if (All_Pages.GetProjectService().UpdateNewPasswordServer(IDnumber.Text, newPassword.Text) == true)
                {
                    All_Pages.SetUser(All_Pages.GetProjectService().GetUserServer(IDnumber.Text));
                    NavigationService navigation = All_Pages.GetFrame().NavigationService;
                    navigation.Navigate(All_Pages.GetStudentMenuPage());
                    All_Pages.GetLoginSucceededMsg().Show();
                }

                else
                {
                    All_Pages.GetLoginFailedMsg().Show();
                }
            }

            else
            {
                All_Pages.GetWrongVerifyCodeMsg().Show();
                SendVerifyCode();
            }
        }

        private void NewSignup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetSignupPage());
        }

        // ----------------------------------------------------------------------------------------------
        // checks if the currentUser's fields are valid or not
        // ----------------------------------------------------------------------------------------------

        public bool CheckUserFields()
        {
            bool invalidFields = false;   // shows if there is a problem in the currentUser's fields

            // -- checking the IDnumber field --
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

            // -- checking the new password field --
            if (newPassword.Text == "" || verifyNewPassword.Text == "" || newPassword.Text != verifyNewPassword.Text)
            {
                All_Pages.GetInvalidFieldsMsg().Show();
                newPassword.BorderThickness = new Thickness(3);
                newPassword.BorderBrush = Brushes.Red;
                verifyNewPassword.BorderThickness = new Thickness(3);
                verifyNewPassword.BorderBrush = Brushes.Red;
                invalidFields = true;
            }

            else
            {
                newPassword.BorderThickness = new Thickness(1);
                newPassword.BorderBrush = Brushes.Black;
                verifyNewPassword.BorderThickness = new Thickness(1);
                verifyNewPassword.BorderBrush = Brushes.Black;
            }

            return (invalidFields);
        }

        // ----------------------------------------------------------------------------------------------
        // sending an email to the user with a verifcation code to make sure it's him
        // ----------------------------------------------------------------------------------------------

        private void SendVerifyCode()
        {
            // -- Sender's email --
            string senderEmail = "linorrav@gmail.com";
            string senderPassword = "vmaw fgch seau wflo";

            // -- User's email --
            string userEmail = All_Pages.GetUser().Email;

            // -- setting the verification code --
            this.verifyCode = RandomCode();

            // -- Mail message --
            MailMessage mail = new MailMessage(senderEmail, userEmail);
            mail.Subject = "אימות סיסמה - " + All_Pages.GetUser().Full_Name;
            mail.Body = "קוד האימות :\n" + this.verifyCode;

            // -- Setup SMTP client --
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            try
            {
                // -- Send the email --
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            // -- showing the needed elements for the verification code message --
            this.codeTitle.Visibility = Visibility.Visible;
            this.rectangle.Visibility = Visibility.Visible;
            this.digit1.Visibility = Visibility.Visible; this.digit2.Visibility = Visibility.Visible; this.digit3.Visibility = Visibility.Visible;
            this.digit4.Visibility = Visibility.Visible; this.digit5.Visibility = Visibility.Visible; this.digit6.Visibility = Visibility.Visible;
        }

        // ----------------------------------------------------------------------------------------------
        // creating a string with 6 different & random digits
        // ----------------------------------------------------------------------------------------------

        private string RandomCode()
        {
            int index = 0;               // the current index we are taking his value from the digits list
            string randomCode = "";      // the 6 digits code that will sent to the user
            Random rnd = new Random();   // Random object to choose randomly the digits
            List<int> digits = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };   // digits list

            for (int i = 0; i < 6; i ++)
            {
                index = rnd.Next(0, digits.Count);
                randomCode += digits[index];
                digits.RemoveAt(index);
            }

            return (randomCode);
        }
    }
}