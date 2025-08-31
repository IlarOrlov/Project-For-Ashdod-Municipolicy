using Model;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF_ProjectService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        // -- USER FUNCTIONS --

        [OperationContract]
        bool IsUserExistServer(string IDnumber, string password);

        [OperationContract]
        bool IsUserIsAdminServer(string IDnumber);

        [OperationContract]
        bool IsIDnumberExistServer(string IDnumber);

        [OperationContract]
        bool InsertNewUserServer(string name, string password, string email, string gender, string IDnumebr, string phoneNumber);

        [OperationContract]
        bool UpdateNewPasswordServer(string IDnumber, string newPassword);

        [OperationContract]
        User GetUserServer(string IDnumber);

        // -- STUDENT FUNCTIONS --
        [OperationContract]
        bool InsertStudentServer(string IDnumber, string school, string grade, string gradeNum);

        [OperationContract]
        int GetStudentIDServer(int userID);

        [OperationContract]
        Student GetStudentServer(int ID);

        // -- QUIZ FUNCTIONS --

        [OperationContract]
        bool InsertQuizServer(string quizTitle, string startDate, string endDate);

        [OperationContract]
        bool IsQuizExistServer(string quizTitle);

        [OperationContract]
        int GetQuizIDServer(string quizTitle);

        [OperationContract]
        List<string> GetAllValidQuizTitlesServer(int studentID);

        [OperationContract]
        List<string> GetAllQuizTitlesServer();

        [OperationContract]
        bool DeleteQuizServer(string quizTitle);

        [OperationContract]
        Quiz GetQuizByTitleServer(string quizTitle);

        // -- QUESTION FUNCTIONS --

        [OperationContract]
        bool InsertQuestionServer(string question, string answer, string[] wrongAnswers, int quizID, string itemNumber);

        [OperationContract]
        bool DeleteQuestionsServer(int quizID);

        // -- QUIZ SUBMIT FUNCTIONS --

        [OperationContract]
        bool InsertQuizSubmitServer(int studentID, int score, int quizID, string answers, string answeredItems);

        [OperationContract]
        List<QuizAnswers> GetStudentsQuizSubmitionsServer(int quizID);

        [OperationContract]
        bool DeleteQuizSubmissionServer(int studentID, int quizID);
    }
}