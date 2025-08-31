using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewDB
{
    public class QuizDB : BaseEntityDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Quiz();
        }

        protected override void CreateModel(BaseEntity entity)
        {
            base.CreateModel(entity);
            Quiz quiz = entity as Quiz;
            QuestionDB questionDB = new QuestionDB();

            if (quiz != null)
            {
                quiz.Title = reader["title"].ToString();
                quiz.StartDate = DateTime.Parse(reader["start_Date"].ToString());
                quiz.EndDate = DateTime.Parse(reader["end_Date"].ToString());
                quiz.InsertQuestions(questionDB.GetQuizsQuestions(quiz.ID));
            }
        }

        public bool IsQuizExist(string quizTitle)
        {
            string sqlRequest = "SELECT COUNT(*) FROM [Quiz] WHERE [title] = @Title";
            var parameters = new Dictionary<string, object>
            {
                { "@Title", quizTitle }
            };
            int result = SelectCountResult(sqlRequest, parameters);

            return result > 0;
        }

        public bool InsertQuiz(string quizTitle, string startDate, string endDate)
        {
            string sqlRequest = "INSERT INTO [Quiz] ([start_Date], [end_Date], [title]) VALUES (@StartDate, @EndDate, @Title)";
            var parameters = new Dictionary<string, object>
            {
                { "@StartDate", startDate },
                { "@EndDate", endDate },
                { "@Title", quizTitle }
            };
            int result = SendSqlCommand(sqlRequest, parameters);

            return result > 0;
        }

        public int GetQuizID(string quizTitle)
        {
            string sqlRequest = "SELECT * FROM [Quiz] WHERE [title] = @Title";
            var parameters = new Dictionary<string, object>
            {
                { "@Title", quizTitle }
            };
            List<BaseEntity> result = Select(sqlRequest, parameters);
            if (result.Count > 0 && result[0] is Quiz quiz)
            {
                return quiz.ID;
            }

            return 0; // Or handle this case appropriately
        }

        public List<string> GetAllValidQuizTitles(int studentID)
        {
            string sqlRequest = "SELECT * FROM [Quiz]";
            var quizzes = Select(sqlRequest, new Dictionary<string, object>());
            var quizTitles = new List<string>();

            var today = DateTime.Today;
            QuizAnswersDB quizAnswersDB = new QuizAnswersDB();

            foreach (var entity in quizzes)
            {
                if (entity is Quiz quiz)
                {
                    bool isWithinDateRange = quiz.StartDate <= today && quiz.EndDate >= today;
                    bool isAlreadySolved = quizAnswersDB.IsStudentSolvedQuiz(studentID, quiz.Title);

                    if (isWithinDateRange && !isAlreadySolved)
                    {
                        quizTitles.Add(quiz.Title);
                    }
                }
            }

            return quizTitles;
        }

        public List<string> GetAllQuizTitles()
        {
            string sqlRequest = "SELECT * FROM [Quiz]";
            var quizzes = Select(sqlRequest, new Dictionary<string, object>());
            return quizzes.OfType<Quiz>().Select(quiz => quiz.Title).ToList();
        }

        public bool DeleteQuiz(string quizTitle)
        {
            string sqlRequest = "DELETE FROM [Quiz] WHERE [title] = @Title";
            var parameters = new Dictionary<string, object>
            {
                { "@Title", quizTitle }
            };
            int result = SendSqlCommand(sqlRequest, parameters);

            return result > 0;
        }

        public Quiz GetQuizByTitle(string quizTitle)
        {
            string sqlRequest = "SELECT * FROM [Quiz] WHERE [title] = @Title";
            var parameters = new Dictionary<string, object>
            {
                { "@Title", quizTitle }
            };
            var result = Select(sqlRequest, parameters);
            return result.FirstOrDefault() as Quiz;
        }

        public Quiz GetQuizByID(int quizID)
        {
            string sqlRequest = "SELECT * FROM [Quiz] WHERE [ID] = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", quizID }
            };
            var result = Select(sqlRequest, parameters);
            return result.FirstOrDefault() as Quiz;
        }
    }
}