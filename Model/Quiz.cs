using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Quiz : BaseEntity
    {
        private string title;               // the title of this Quiz
        private DateTime startDate;         // the start date of this Quiz
        private DateTime endDate;           // the end date of this Quiz
        private List<Question> questions;   // the questions in this Quiz

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public List<Question> Questions
        {
            get { return this.questions; }
        }

        public void InsertQuestions(List<Question> questions)
        {
            this.questions = new List<Question>();
            for (int i = 0; i < questions.Count(); i++)
                this.questions.Add(questions[i]);
        }
    }
}