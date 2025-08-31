using Client_WPF.Messages;
using Client_WPF.ProjectService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client_WPF.Classes
{
    public class All_Pages
    {
        // --- PAGES ---
        private static Frame myFrame = null;                      // the frame of all this pages
        private static User currentUser = null;                   // the current currentUser who uses the program
        private static Service1Client projectService = null;      // the service of this project
        private static Page1_Registration page1_Registartion;     // the first page after the opening - the page where the currentUser signing up / loging in
        private static Page_Login page_Login;                     // the page where the currentUser loging in
        private static Page_Signup page_Signup;                   // the page where the currentUser signing in
        private static Page_ForgotPassword page_ForgotPassword;   // the page where the currentUser creating a new password
        private static Page_StudentMenu page_StudentMenu;         // the page where the student / guest are choosing their myQuiz
        private static Page_AdminMenu page_AdminMenu;             // the admin's menu page 
        private static Page_Quiz page_Quiz;                       // the page where the myQuiz is created
        private static Page_CreateQuiz page_CreateQuiz;           // the page where ths admin is creating the myQuiz 
        private static Page_DeleteQuiz page_DeleteQuiz;           // the page where the admin is deleting the myQuiz
        private static Page_QuizStatistics page_QuizStatistics;   // the page where the admin is calculating the quiz's statistics 

        // --- MESSAGES WINDOWS ---
        private static Login_Succeeded_Msg login_Succeeded_Msg;               // the message that says the login succeeded
        private static Login_Failed_Msg login_Failed_Msg;                     // the message that says the login failed
        private static Signup_Failed_Msg signup_Failed_Msg;                   // the message that says the signup failed
        private static Invalid_Fields_Msg invalid_Fields_Msg;                 // the message that says the signup is invalid
        private static Signup_Succeeded_Msg signup_Succeeded_Msg;             // the message that says the signup succeeded
        private static Invalid_Quiz_Msg invalid_Quiz_Msg;                     // the message that says the myQuiz isn't completed
        private static Quiz_Upload_Failed_Msg quiz_Upload_Failed_Msg;         // the message that says the myQuiz's upload failed
        private static Quiz_Exist_Msg quiz_Exist_Msg;                         // the message that says there is a myQuiz with the same subject already
        private static Quiz_Upload_Succeeded_Msg quiz_Upload_Succeeded_Msg;   // the message that says the myQuiz's upload succeeded
        private static Delete_Quiz_Failed_Msg delete_Quiz_Failed_Msg;         // the message that says the myQuiz deleted
        private static Delete_Quiz_Succeeded_Msg delete_Quiz_Succeeded_Msg;   // the message that says the myQuiz didn't deleted
        private static Quiz_Finished_Msg quiz_Finished_Msg;                   // the message that says the quiz is over
        private static Logout_Msg logout_Msg;                                 // the message that asks the currentUser if he wants to logout
        private static Wrong_Verify_Code_Msg wrong_Verify_Code_Msg;           // the message that says the user enterd wrong verification code

        public static void CreateMessagesAndPages()
        {
            // --------------------- MESSAGES ---------------------
            login_Succeeded_Msg = new Login_Succeeded_Msg();
            login_Failed_Msg = new Login_Failed_Msg();
            signup_Failed_Msg = new Signup_Failed_Msg();
            invalid_Fields_Msg = new Invalid_Fields_Msg();
            signup_Succeeded_Msg = new Signup_Succeeded_Msg();
            invalid_Quiz_Msg = new Invalid_Quiz_Msg();
            quiz_Upload_Failed_Msg = new Quiz_Upload_Failed_Msg();
            quiz_Exist_Msg = new Quiz_Exist_Msg();
            quiz_Upload_Succeeded_Msg = new Quiz_Upload_Succeeded_Msg();
            delete_Quiz_Failed_Msg = new Delete_Quiz_Failed_Msg();
            delete_Quiz_Succeeded_Msg = new Delete_Quiz_Succeeded_Msg();
            quiz_Finished_Msg = new Quiz_Finished_Msg();
            logout_Msg = new Logout_Msg();
            wrong_Verify_Code_Msg = new Wrong_Verify_Code_Msg();

            // ---------------------- PAGES -----------------------
            page1_Registartion = new Page1_Registration();
            page_AdminMenu = new Page_AdminMenu();
        }

        public static void SetFrame(Frame frame) { myFrame = frame; }
        public static void SetUser(User user) { currentUser = user; }
        public static void SetProjectService(Service1Client service) { projectService = service; }
        public static Frame GetFrame() { return (myFrame); }
        public static User GetUser() { return (currentUser); }
        public static Service1Client GetProjectService() { return (projectService); }
        public static Page1_Registration GetRegisterPage() { return (page1_Registartion); }
        public static Page_Login GetLoginPage() { return (page_Login = new Page_Login()); }
        public static Page_Signup GetSignupPage() { return (page_Signup = new Page_Signup()); }
        public static Page_ForgotPassword GetForgotPasswordPage() { return (page_ForgotPassword = new Page_ForgotPassword()); }
        public static Page_AdminMenu GetAdminMenuPage() { return (page_AdminMenu); }
        public static Page_StudentMenu GetStudentMenuPage() { return (page_StudentMenu = new Page_StudentMenu()); }
        public static Page_Quiz GetQuizPage(string quizTitle, int studentID) { return (page_Quiz = new Page_Quiz(quizTitle, studentID)); }
        public static Page_CreateQuiz GetCreateQuizPage() { return (page_CreateQuiz = new Page_CreateQuiz()); }
        public static Page_DeleteQuiz GetDeleteQuizPage() { return (page_DeleteQuiz = new Page_DeleteQuiz()); }
        public static Page_QuizStatistics GetStatisticsPage() { return (page_QuizStatistics = new Page_QuizStatistics()); }


        // -------------------------------------- MESSAGES WINDOWS --------------------------------------


        public static Login_Succeeded_Msg GetLoginSucceededMsg() { return (login_Succeeded_Msg); }
        public static Login_Failed_Msg GetLoginFailedMsg() { return (login_Failed_Msg); }
        public static Signup_Failed_Msg GetSignupFailedMsg() { return (signup_Failed_Msg); }
        public static Invalid_Fields_Msg GetInvalidFieldsMsg() { return (invalid_Fields_Msg); }
        public static Signup_Succeeded_Msg GetSignupSucceededMsg() { return (signup_Succeeded_Msg); }
        public static Invalid_Quiz_Msg GetInvalidQuizMsg() { return (invalid_Quiz_Msg); }
        public static Quiz_Upload_Failed_Msg GetQuizUploadFailedMsg() { return (quiz_Upload_Failed_Msg); }
        public static Quiz_Exist_Msg GetQuizExistMsg() { return (quiz_Exist_Msg); }
        public static Quiz_Upload_Succeeded_Msg GetQuizUploadSucceededMsg() { return (quiz_Upload_Succeeded_Msg); }
        public static Delete_Quiz_Failed_Msg GetDeleteQuizFailedMsg() { return (delete_Quiz_Failed_Msg); }
        public static Delete_Quiz_Succeeded_Msg GetDeleteQuizSucceededMsg() { return (delete_Quiz_Succeeded_Msg); }
        public static Quiz_Finished_Msg GetQuizFinishedMsg() { return (quiz_Finished_Msg); }
        public static Logout_Msg GetLogoutMsg() { return (logout_Msg); }
        public static Wrong_Verify_Code_Msg GetWrongVerifyCodeMsg() { return (wrong_Verify_Code_Msg); }
    }
}