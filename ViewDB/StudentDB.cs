using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewDB
{
    public class StudentDB : UserDB
    {   
        protected override BaseEntity NewEntity()
        {
            return (new Student());
        }

        // -- creating a Student object from a record in the database --
        protected override void CreateModel(BaseEntity entity)
        {
            Student student = entity as Student;

            if (student != null)
            {
                student.ID = int.Parse(reader["ID"].ToString());
                student.UserID = int.Parse(reader["userID"].ToString());
                student.School = reader["school"].ToString();
                student.Grade = reader["grade"].ToString();
                student.GradeNumber = int.Parse(reader["grade_Num"].ToString());

                // -- creating the User that this Student is for getting his fields --
                UserDB userDB = new UserDB();
                User user = userDB.GetUser(student.UserID);

                student.Full_Name = user.Full_Name;
                student.Password = user.Password;
                student.Email = user.Email;
                student.ID_Number = user.ID_Number;
                student.PhoneNumber = user.PhoneNumber;
                student.Gender = user.Gender;
            }
        }

        // ----------------------------------------------------------------------------------------------
        // Inserting a record of a new Student into the Student table in the database
        // input : the Student's IDnumber, school, grade, grade number
        // output : if the record added successfully to the database [bool]
        // ----------------------------------------------------------------------------------------------
        public bool InsertStudent(string IDnumber, string school, string grade, string gradeNum)
        {
            UserDB userDB = new UserDB();
            int userID = userDB.GetUserID(IDnumber);

            if (userID > 0)
            {
                string sqlRequest = "INSERT INTO [Student] ([userID], [school], [grade], [grade_Num]) VALUES (@UserID, @School, @Grade, @GradeNum)";
                var parameters = new Dictionary<string, object>
                {
                    { "@UserID", userID },
                    { "@School", school },
                    { "@Grade", grade },
                    { "@GradeNum", int.Parse(gradeNum) }
                };

                int result = SendSqlCommand(sqlRequest, parameters);

                if (result > 0)
                    return true;
            }

            return false;
        }

        public int GetStudentID(int userID)
        {
            string sqlRequest = "SELECT * FROM [Student] WHERE [userID] = @UserID";
            var parameters = new Dictionary<string, object>
            {
                { "@UserID", userID }
            };

            int result = SelectCountResult(sqlRequest, parameters);
            return result;
        }

        public Student GetStudent(int ID)
        {
            string sqlRequest = "SELECT * FROM [Student] WHERE [ID] = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", ID }
            };

            List<BaseEntity> students = Select(sqlRequest, parameters);

            if (students.Count > 0)
            {
                Student realStudent = students[0] as Student;
                return realStudent;
            }

            return null;
        }
    }
}