using Model;
using ViewDB;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCF_ProjectService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        // -- USER FUNCTIONS --

        public bool IsUserExistServer(string IDnumber, string password)
        {
            UserDB userDB = new UserDB();
            return (userDB.IsUserExist(IDnumber, password));
        }

        public bool IsUserIsAdminServer(string IDnumber)
        {
            UserDB userDB = new UserDB();
            return (userDB.IsUserIsAdmin(IDnumber));
        }

        public bool IsIDnumberExistServer(string IDnumber)
        {
            UserDB userDB = new UserDB();
            return (userDB.IsIDnumberExist(IDnumber));
        }

        public bool InsertNewUserServer(string name, string password, string email, string gender, string IDnumebr, string phoneNumber)
        {
            UserDB userDB = new UserDB();
            return (userDB.InsertNewUser(name, password, email, gender, IDnumebr, phoneNumber));
        }

        public bool UpdateNewPasswordServer(string IDnumber, string newPassword)
        {
            UserDB userDB = new UserDB();
            return (userDB.UpdateNewPassword(IDnumber, newPassword));
        }

        public User GetUserServer(string IDnumber)
        {
            UserDB userDB = new UserDB();
            return (userDB.GetUser(IDnumber));
        }

        // -- STUDENT FUNCTIONS --

        public bool InsertStudentServer(string IDnumber, string school, string grade, string gradeNum)
        {
            StudentDB studentDB = new StudentDB();
            return (studentDB.InsertStudent(IDnumber, school, grade, gradeNum));
        }

        public int GetStudentIDServer(int userID)
        {
            StudentDB studentDB = new StudentDB();
            return (studentDB.GetStudentID(userID));
        }

        public Student GetStudentServer(int ID)
        {
            StudentDB studentDB = new StudentDB();
            return (studentDB.GetStudent(ID));
        }

        // -- QUIZ FUNCTIONS --

        public bool InsertQuizServer(string quizTitle, string startDate, string endDate)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.InsertQuiz(quizTitle, startDate, endDate));
        }

        public bool IsQuizExistServer(string quizTitle)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.IsQuizExist(quizTitle));
        }

        public int GetQuizIDServer(string quizTitle)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.GetQuizID(quizTitle));
        }

        public List<string> GetAllValidQuizTitlesServer(int studentID)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.GetAllValidQuizTitles(studentID));
        }

        public List<string> GetAllQuizTitlesServer()
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.GetAllQuizTitles());
        }

        public bool DeleteQuizServer(string quizTitle)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.DeleteQuiz(quizTitle));
        }

        public Quiz GetQuizByTitleServer(string quizTitle)
        {
            QuizDB quizDB = new QuizDB();
            return (quizDB.GetQuizByTitle(quizTitle));
        }

        // -- QUESTION FUNCTIONS --

        public bool InsertQuestionServer(string question, string answer, string[] wrongAnswers, int quizID, string itemNumber)
        {
            QuestionDB questionDB = new QuestionDB();
            return (questionDB.InsertQuestion(question, answer, wrongAnswers, quizID, itemNumber));
        }

        public bool DeleteQuestionsServer(int quizID)
        {
            QuestionDB questionDB = new QuestionDB();
            return (questionDB.DeleteQuestions(quizID));
        }

        // -- QUIZ SUBMIT FUNCTIONS --

        public bool InsertQuizSubmitServer(int studentID, int score, int quizID, string answers, string answeredItems)
        {
            QuizAnswersDB quizAnswersDB = new QuizAnswersDB();
            return (quizAnswersDB.InsertQuizSubmit(studentID, score, quizID, answers, answeredItems));
        }

        public List<QuizAnswers> GetStudentsQuizSubmitionsServer(int quizID)
        {
            QuizAnswersDB quizAnswersDB = new QuizAnswersDB();
            return (quizAnswersDB.GetStudentsQuizSubmitions(quizID));
        }

        public bool DeleteQuizSubmissionServer(int studentID, int quizID)
        {
            QuizAnswersDB quizAnswersDB = new QuizAnswersDB();
            return quizAnswersDB.DeleteQuizSubmission(studentID, quizID);
        }
    }
}