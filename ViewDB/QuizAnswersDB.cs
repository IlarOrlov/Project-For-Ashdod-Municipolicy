using Model;
using System;
using System.Collections.Generic;

namespace ViewDB
{
    public class QuizAnswersDB : BaseEntityDB
    {
        protected override BaseEntity NewEntity()
        {
            return (new QuizAnswers());
        }

        protected override void CreateModel(BaseEntity entity)
        {
            base.CreateModel(entity);
            QuizAnswers quizSubmit = entity as QuizAnswers;

            if (quizSubmit != null)
            {
                quizSubmit.StudentID = int.Parse(reader["studentID"].ToString());
                quizSubmit.Score = int.Parse(reader["score"].ToString());
                quizSubmit.QuizID = int.Parse(reader["quizID"].ToString());
                quizSubmit.SetAnswersList(reader["answers"].ToString());
                quizSubmit.TimePassed = double.Parse(reader["quiz_Time"].ToString());

                if (reader["answered_items"] != DBNull.Value)
                    quizSubmit.SetAnsweredItemsList(reader["answered_items"].ToString());
            }
        }

        public bool IsStudentSolvedQuiz(int studentID, string quizTitle)
        {
            QuizDB quizDB = new QuizDB();
            int quizID = quizDB.GetQuizID(quizTitle);

            string sqlRequest = "SELECT COUNT(*) FROM [QuizSubmit] WHERE [studentID] = @studentID AND [quizID] = @quizID";
            var parameters = new Dictionary<string, object>
            {
                { "@studentID", studentID },
                { "@quizID", quizID }
            };

            int result = SelectCountResult(sqlRequest, parameters);

            return result > 0;
        }

        public bool InsertQuizSubmit(int studentID, int score, int quizID, string answers, string answeredItems = "")
        {
            QuizDB quizDB = new QuizDB();
            Quiz quiz = quizDB.GetQuizByID(quizID);

            double timePassed = (DateTime.Now - quiz.StartDate).TotalMinutes;

            string sqlRequest = "INSERT INTO [QuizSubmit] ([studentID], [score], [quizID], [answers], [quiz_Time], [answered_items]) VALUES (@studentID, @score, @quizID, @answers, @quiz_Time, @answeredItems)";
            var parameters = new Dictionary<string, object>
            {
                { "@studentID", studentID },
                { "@score", score },
                { "@quizID", quizID },
                { "@answers", answers },
                { "@quiz_Time", timePassed },
                { "@answeredItems", answeredItems ?? "" }
            };

            int result = SendSqlCommand(sqlRequest, parameters);

            return result > 0;
        }

        public bool DeleteQuizSubmission(int studentID, int quizID)
        {
            string sqlRequest = "DELETE FROM [QuizSubmit] WHERE [studentID] = @studentID AND [quizID] = @quizID";
            var parameters = new Dictionary<string, object>
            {
                { "@studentID", studentID },
                { "@quizID", quizID }
            };

            int result = SendSqlCommand(sqlRequest, parameters);

            return result > 0;
        }

        public List<QuizAnswers> GetStudentsQuizSubmitions(int quizID)
        {
            string sqlRequest = "SELECT * FROM [QuizSubmit] WHERE [quizID] = @quizID";
            var parameters = new Dictionary<string, object>
            {
                { "@quizID", quizID }
            };

            List<BaseEntity> submitions = Select(sqlRequest, parameters);
            List<QuizAnswers> quizSubmitions = new List<QuizAnswers>();

            foreach (var submission in submitions)
            {
                quizSubmitions.Add(submission as QuizAnswers);
            }

            return quizSubmitions;
        }
    }
}