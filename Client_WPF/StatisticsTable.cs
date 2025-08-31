using Client_WPF.Classes;
using Client_WPF.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Client_WPF
{
    public class StatisticsTable
    {
        private Question question;                  // the question we are calculating it's statistics
        private List<QuizAnswers> quizSubmitions;   // the students' submitions for this question
        private int questionNumber;                 // the number of this question in the quiz

        public StatisticsTable(Question question, List<QuizAnswers> quizSubmitions, FlowDocument tablesDocument, int questionNumber)
        {
            this.question = question;
            this.quizSubmitions = quizSubmitions;
            this.questionNumber = questionNumber;

            Table table = new Table();   // the table where we will show the question's statistics
            TableRow currentRow;         // the current row we are writing in 
            const int ROWS_NUMBER = 5;   // the final number of rows there are in the table
            int rowNumber = 4;           // the row we are writing in now
            int columnsNumber = 4;       // the columns number in each row

            // -- adding the title & categories to the table --
            AddTableTitles(table);

            while (rowNumber < ROWS_NUMBER)
            {
                // -- creating & filling the row in the table --
                table.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table.RowGroups[0].Rows[rowNumber];
                string[] questionStatistics = new string[columnsNumber];
                CalculateQuestionStatistics(questionStatistics);

                for (int i = 0; i < columnsNumber; i++)
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(questionStatistics[i]))));
                }

                rowNumber++;
            }

            // -- adding the table to the FlowDocument --
            tablesDocument.Blocks.Add(table);
        }

        // ----------------------------------------------------------------------------------------------
        // adding the title & categories of the table to it
        // ----------------------------------------------------------------------------------------------

        private void AddTableTitles(Table table)
        {
            // -- adding to the table the TableRowGroup --
            table.RowGroups.Add(new TableRowGroup());

            // -- adding an empty row in the top of the table so will be a space between the tables --
            table.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table.RowGroups[0].Rows[0];
            currentRow.FontSize = 26;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));

            // -- adding the title of the table --
            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[1];
            currentRow.FontSize = 40;
            currentRow.FontFamily = new FontFamily("Nehama");
            currentRow.Foreground = Brushes.White;
            currentRow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            Paragraph title = new Paragraph(new Run("חישוב סטטיסטיקה - שאלה " + (this.questionNumber + 1) + $" - (מספר פריט: {this.question.ItemNumber})"));
            currentRow.Cells.Add(new TableCell(title));
            title.TextAlignment = TextAlignment.Center;
            currentRow.Cells[0].ColumnSpan = 4;

            // -- adding the question itself under the main title --
            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[2];
            currentRow.FontSize = 26;
            currentRow.FontFamily = new FontFamily("Assistant ExtraBold");
            currentRow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            Paragraph q = new Paragraph(new Run('?' + this.question.QuestionContent));
            currentRow.Cells.Add(new TableCell(q));
            q.TextAlignment = TextAlignment.Center;
            currentRow.Cells[0].ColumnSpan = 4;

            // -- adding the question's possible answers to the table --
            table.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table.RowGroups[0].Rows[3];
            currentRow.FontSize = 26;
            currentRow.FontFamily = new FontFamily("Asakim");
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(this.question.Answer + "\n(תשובה נכונה)"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(this.question.WrongAnswers[0]))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(this.question.WrongAnswers[1]))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(this.question.WrongAnswers[2]))));
        }

        // ----------------------------------------------------------------------------------------------
        // calculating the statistics of the question according to this formula :
        // ( the number of students who chose this answer / the number of students ) * 100
        // ----------------------------------------------------------------------------------------------

        private void CalculateQuestionStatistics(string[] questionStatistics)
        {
            List<string> studentsAnswers = new List<string>();   // list of the students' answers to this question
            double answerResult = 0;                             // the percentage of students who chose a certain answer 

            // -- adding the students' answers --
            for (int i = 0; i < this.quizSubmitions.Count; i++)
            {
                studentsAnswers.Add(this.quizSubmitions[i].Answers[this.questionNumber]);
            }

            // -- calculating the question's statistics according to the formula --
            answerResult = ((studentsAnswers.Count(n => n == this.question.Answer)) / (double)this.quizSubmitions.Count) * 100;
            questionStatistics[0] = "%" + Math.Round(answerResult).ToString();

            for (int i = 0; i < 3; i++)
            {
                answerResult = ((studentsAnswers.Count(n => n == this.question.WrongAnswers[i])) / (double)this.quizSubmitions.Count) * 100;
                questionStatistics[i + 1] = "%" + Math.Round(answerResult).ToString();
            }
        }
    }
}
