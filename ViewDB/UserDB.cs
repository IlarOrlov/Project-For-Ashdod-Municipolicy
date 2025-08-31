using Model;
using System;
using System.Collections.Generic;

namespace ViewDB
{
    public class UserDB : BaseEntityDB
    {
        protected override BaseEntity NewEntity()
        {
            return new User();
        }

        protected override void CreateModel(BaseEntity entity)
        {
            base.CreateModel(entity);
            User user = entity as User;

            if (user != null)
            {
                user.Full_Name = reader["full_Name"].ToString();
                user.Password = reader["passWord"].ToString();
                user.Email = reader["email"].ToString();
                user.ID_Number = reader["ID_Number"].ToString();
                user.PhoneNumber = reader["phoneNumber"].ToString();
                user.Gender = reader["gender"].ToString()[0];
            }
        }

        public bool IsUserExist(string IDnumber, string password)
        {
            string sqlRequest = "SELECT COUNT(*) FROM [User] WHERE [ID_Number] = @ID_Number AND [passWord] = @password";
            var parameters = new Dictionary<string, object>
            {
                { "@ID_Number", IDnumber },
                { "@password", password }
            };
            int result = SelectCountResult(sqlRequest, parameters);
            return result > 0;
        }

        public bool IsUserIsAdmin(string IDnumber)
        {
            string sqlRequest = "SELECT COUNT(*) FROM [User] WHERE [ID_Number] = @ID_Number AND [is_Admin] = 1";
            var parameters = new Dictionary<string, object>
            {
                { "@ID_Number", IDnumber }
            };
            int result = SelectCountResult(sqlRequest, parameters);
            return result > 0;
        }

        public bool IsIDnumberExist(string IDnumber)
        {
            string sqlRequest = "SELECT COUNT(*) FROM [User] WHERE [ID_Number] = @ID_Number";
            var parameters = new Dictionary<string, object>
            {
                { "@ID_Number", IDnumber }
            };
            int result = SelectCountResult(sqlRequest, parameters);
            return result > 0;
        }

        public bool InsertNewUser(string name, string password, string email, string gender, string IDnumber, string phoneNumber)
        {
            string sqlRequest = "INSERT INTO [User] ([full_Name], [passWord], [email], [gender], [ID_Number], [is_Admin], [phoneNumber]) " +
                                "VALUES (@full_Name, @passWord, @email, @gender, @ID_Number, 0, @phoneNumber)";
            var parameters = new Dictionary<string, object>
            {
                { "@full_Name", name },
                { "@passWord", password },
                { "@email", email },
                { "@gender", gender[0] },
                { "@ID_Number", IDnumber },
                { "@phoneNumber", phoneNumber }
            };
            int result = SendSqlCommand(sqlRequest, parameters);
            return result > 0;
        }

        public bool UpdateNewPassword(string IDnumber, string newPassword)
        {
            string sqlRequest = "UPDATE [User] SET [passWord] = @newPassword WHERE [ID_Number] = @ID_Number";
            var parameters = new Dictionary<string, object>
            {
                { "@newPassword", newPassword },
                { "@ID_Number", IDnumber }
            };
            int result = SendSqlCommand(sqlRequest, parameters);
            return result > 0;
        }

        public User GetUser(string IDnumber)
        {
            string sqlRequest = "SELECT * FROM [User] WHERE [ID_Number] = @ID_Number";
            var parameters = new Dictionary<string, object>
            {
                { "@ID_Number", IDnumber }
            };
            List<BaseEntity> users = Select(sqlRequest, parameters);
            return users.Count > 0 ? users[0] as User : null;
        }

        public int GetUserID(string IDnumber)
        {
            string sqlRequest = "SELECT * FROM [User] WHERE [ID_Number] = @ID_Number";
            var parameters = new Dictionary<string, object>
            {
                { "@ID_Number", IDnumber }
            };
            return SelectCountResult(sqlRequest, parameters);
        }

        public User GetUser(int ID)
        {
            string sqlRequest = "SELECT * FROM [User] WHERE [ID] = @ID";
            var parameters = new Dictionary<string, object>
            {
                { "@ID", ID }
            };
            List<BaseEntity> users = Select(sqlRequest, parameters);
            return users.Count > 0 ? users[0] as User : null;
        }
    }
}