using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : BaseEntity
    {
        private string fullName;      // this User's full name
        private string password;      // this User's password
        private string email;         // this User's email address
        private string IDnumber;      // this User's ID number
        private string phoneNumber;   // this User's phone number
        private char gender;          // this User's gender

        public string Full_Name
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string ID_Number
        {
            get { return this.IDnumber; }
            set { this.IDnumber = value; }
        }

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }
    }
}