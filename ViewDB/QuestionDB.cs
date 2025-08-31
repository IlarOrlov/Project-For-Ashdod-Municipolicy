using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDB
{
    public class QuestionDB : BaseEntityDB
    {
        protected override BaseEntity NewEntity()
        {
            return (new Question());
        }

        // -- creating a Question object from a record in the database --
        protected override void CreateModel(BaseEntity entity)
        {
            base.CreateModel(entity);
            Question question = entity as Question;

            string wrongAnswer1, wrongAnswer2, wrongAnswer3;

            if (question != null)
            {
                question.QuestionContent = reader["question"].ToString();
                question.Answer = reader["answer"].ToString();
                wrongAnswer1 = reader["wrong_Answer1"].ToString();
                wrongAnswer2 = reader["wrong_Answer2"].ToString();
                wrongAnswer3 = reader["wrong_Answer3"].ToString();
                question.InsertWrongAnswers(wrongAnswer1, wrongAnswer2, wrongAnswer3);

                if (reader["item_number"] != DBNull.Value)
                    question.ItemNumber = reader["item_number"].ToString();
            }
        }

        // ----------------------------------------------------------------------------------------------
        // Inserting a record of a new Question into the Question table in the database
        // input : the question, the answer to the question, 3 wrong possible answers to the question
        // output : if the record added successfully to the database [bool]
        // ----------------------------------------------------------------------------------------------

        public bool InsertQuestion(string question, string answer, string[] wrongAnswers, int quizID, string itemNumber = "")
        {
            string sqlRequest = @"INSERT INTO [Question] 
                          ([question], [answer], [wrong_Answer1], [wrong_Answer2], [wrong_Answer3], [quiz_ID], [item_number]) 
                          VALUES (@question, @answer, @wrongAnswer1,@wrongAnswer2, @wrongAnswer3, @quizID, @itemNumber)";

            command.CommandText = sqlRequest;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@question", question);
            command.Parameters.AddWithValue("@answer", answer);
            command.Parameters.AddWithValue("@wrongAnswer1", wrongAnswers[0]);
            command.Parameters.AddWithValue("@wrongAnswer2", wrongAnswers[1]);
            command.Parameters.AddWithValue("@wrongAnswer3", wrongAnswers[2]);
            command.Parameters.AddWithValue("@quizID", quizID);
            command.Parameters.AddWithValue("@itemNumber", itemNumber ?? "");

            int result = 0;

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return result > 0;
        }

        // ----------------------------------------------------------------------------------------------
        // Deleting records of Questions from the Question table in the database
        // input : the ID number of the quiz that this Questions belongs to
        // output : if the records deleted successfully from the database [bool]
        // ----------------------------------------------------------------------------------------------

        public bool DeleteQuestions(int quizID)
        {
            string sqlRequest = "DELETE FROM [Question] WHERE [quiz_ID] = @quizID";

            command.CommandText = sqlRequest;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@quizID", quizID);

            int result = 0;

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return result > 0;
        }

        // ----------------------------------------------------------------------------------------------
        // Getting the Questions that belongs to the quiz with this ID number
        // input : the ID number of the quiz
        // output : the Questions list of this quiz [List<Question>]
        // ----------------------------------------------------------------------------------------------

        public List<Question> GetQuizsQuestions(int quizID)
        {
            string sqlRequest = "SELECT * FROM [Question] WHERE [quiz_ID] = @quizID";

            command.CommandText = sqlRequest;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@quizID", quizID);

            List<BaseEntity> questions = new List<BaseEntity>();

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    CreateModel(entity);
                    questions.Add(entity);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                connection.Close();
            }

            List<Question> realQuestions = new List<Question>();

            for (int i = 0; i < questions.Count(); i++)
                realQuestions.Add(questions[i] as Question);

            return realQuestions;
        }
    }
}