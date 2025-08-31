using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student : User
    {
        private int userID;      // the user's ID number of this Student
        private string school;   // the school that this Student belongs to
        private string grade;    // this Student's grade
        private int gradeNum;    // this Student's grade number

        public int UserID
        {
            get { return this.userID; }
            set { this.userID = value; }
        }

        public string School
        {
            get { return this.school; }
            set { this.school = value; }
        }

        public string Grade
        {
            get { return this.grade; }
            set { this.grade = value; }
        }

        public int GradeNumber
        {
            get { return this.gradeNum; }
            set { this.gradeNum = value; }
        }
    }
}