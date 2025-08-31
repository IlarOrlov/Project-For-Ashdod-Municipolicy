using Client_WPF.Classes;
using Client_WPF.ProjectService;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_WPF
{
    /// <summary>
    /// Interaction logic for Page_QuizStatistics.xaml
    /// </summary>
    public partial class Page_QuizStatistics : Page
    {
        public Page_QuizStatistics()
        {
            ExcelPackage.License.SetNonCommercialOrganization("Ashdod Municipolicy");

            InitializeComponent();
            SetQuizTitles();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigation = All_Pages.GetFrame().NavigationService;
            navigation.Navigate(All_Pages.GetAdminMenuPage());
        }

        // ----------------------------------------------------------------------------------------------
        // showing the Admin all the titles of all the quizzes in the database
        // ----------------------------------------------------------------------------------------------

        private void SetQuizTitles()
        {
            string[] quizTitelsArr = All_Pages.GetProjectService().GetAllQuizTitlesServer();

            for (int i = 0; i < quizTitelsArr.Length; i++)
                quizTitles.Items.Add(quizTitelsArr[i]);
        }

        // ----------------------------------------------------------------------------------------------
        // - the Admin choses the title of the quiz he wants to analyze
        // - calculates the record table of the students who solved this quiz
        // - calculate the statistics of the questions in this quiz
        // ----------------------------------------------------------------------------------------------
        
        private void quizTitels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tablesDocument.Blocks.Clear();
            CalculateRecordsTable(quizTitles.SelectedItem.ToString());
            CalculateQuizStatistics(quizTitles.SelectedItem.ToString());
            if (this.rectangle.Height == 260)
                this.rectangle.Height += 1680;
        }

        // ----------------------------------------------------------------------------------------------
        // - calculating the records table of the students who solved this quiz :
        //   * first priority : maximum correct answers, second priority : minimun time passed
        // ----------------------------------------------------------------------------------------------

        private void CalculateRecordsTable(string quizTitle)
        {
            Table recordsTable;
            List<QuizAnswers> helperList;

            // -- the Table of all the records --
            recordsTable = new Table();

            // -- the list of the students' ID number according to there records --
            List<int> studentsIDs = new List<int>();

            // -- the quiz we are analyzing --
            Quiz quiz = All_Pages.GetProjectService().GetQuizByTitleServer(quizTitle);

            // -- the quiz submitions of the students who solved this quiz --
            QuizAnswers[] quizSubmitions = All_Pages.GetProjectService().GetStudentsQuizSubmitionsServer(quiz.ID);
            
            if (quizSubmitions.Length > 0)
            {
                // -- checking from top to buttom how many correct answers every student have --
                for (int i = quiz.Questions.Length; i >= 0; i--)
                {
                    helperList = new List<QuizAnswers>();

                    for (int j = 0; j < quizSubmitions.Length; j++)
                    {
                        if (quizSubmitions[j].Score == i)
                            helperList.Add(quizSubmitions[j]);
                    }

                    if (helperList.Count() > 0)
                    {
                        while (helperList.Count() != 0)
                        {
                            // -- checking from the students who have the same score who is the minimum time solver --
                            studentsIDs.Add(FindAndRemoveMinTimeSolver(helperList));
                        }
                    }
                }

                // -- filling the records table according to the results --
                FillRecordsTable(studentsIDs, new List<QuizAnswers>(quizSubmitions), recordsTable);

                // -- adding the records table to the Flow Ducument --
                tablesDocument.Blocks.Add(recordsTable);
            }
        }

        // ----------------------------------------------------------------------------------------------
        // filling the records table according to the students' record order
        // ----------------------------------------------------------------------------------------------

        private void FillRecordsTable(List<int> studentsIDs, List<QuizAnswers> quizSubmitions, Table recordsTable)
        {
            TableRow currentRow;      // the current row we are writing in
            Student currentStudent;   // the current student we are adding to the table
            int ROWS_NUMBER = quizSubmitions.Count + 2;   // the final number of rows there are in the table
            int rowNumber = 2;        // the row we are writing in now
            int columnsNumber = 7;    // the columns number in each row

            // -- adding the title & categories to the table --
            AddTableTitles(recordsTable);

            while (rowNumber < ROWS_NUMBER)
            {
                // -- creating & filling the row in the table --
                recordsTable.RowGroups[0].Rows.Add(new TableRow());
                currentRow = recordsTable.RowGroups[0].Rows[rowNumber];

                // -- getting & setting the student's information in the table
                currentStudent = All_Pages.GetProjectService().GetStudentServer(studentsIDs[rowNumber - 2]);
                string[] studentInfo = {(rowNumber - 1).ToString(),
                                         currentStudent.Full_Name,
                                         currentStudent.ID_Number,
                                         currentStudent.PhoneNumber,
                                         currentStudent.School,
                                         ((quizSubmitions.Find(item => item.StudentID == currentStudent.ID)).Score).ToString(),
                                         ((quizSubmitions.Find(item => item.StudentID == currentStudent.ID)).TimePassed).ToString()};

                for (int i = 0; i < columnsNumber; i++)
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(studentInfo[i]))));
                }

                rowNumber++;
            }
        }

        // ----------------------------------------------------------------------------------------------
        // adding the title & categories of the table to it
        // ----------------------------------------------------------------------------------------------

        private void AddTableTitles(Table recordsTable)
        {
            // -- adding the title of the table --
            recordsTable.RowGroups.Add(new TableRowGroup());
            recordsTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = recordsTable.RowGroups[0].Rows[0];
            currentRow.FontSize = 50;
            currentRow.FontFamily = new FontFamily("Nehama");
            currentRow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864"));
            Paragraph title = new Paragraph(new Run("טבלת הישגים"));
            currentRow.Cells.Add(new TableCell(title));
            title.TextAlignment = TextAlignment.Center;
            currentRow.Cells[0].ColumnSpan = 7;

            // -- adding the categories of the table to it --
            recordsTable.RowGroups[0].Rows.Add(new TableRow());
            currentRow = recordsTable.RowGroups[0].Rows[1];
            currentRow.FontSize = 26;
            currentRow.FontFamily = new FontFamily("Asakim");
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("מקום"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("שם מלא"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("תעודת זהות"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("מספר טלפון"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("בית ספר"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ניקוד"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("זמן מיצירת החידון (בדקות)"))));
        }

        // ----------------------------------------------------------------------------------------------
        // - finding the fastest student among all the other students who have the same score as his
        // - removing him from the List to find the next one after him
        // input : list of all the students with the same score
        // output : the ID number of the fastest student [int]
        // ----------------------------------------------------------------------------------------------

        private int FindAndRemoveMinTimeSolver(List<QuizAnswers> sameRightAnswersSolvers)
        {
            double minTime = sameRightAnswersSolvers[0].TimePassed;        // the minimum time passed from all the students
            int minTimeStudentID = sameRightAnswersSolvers[0].StudentID;   // the fastest student ID number who solved the quiz
            QuizAnswers minTimeStudent = sameRightAnswersSolvers[0];       // the submition of the fastest student

            for (int i = 1; i < sameRightAnswersSolvers.Count(); i++)
            {
                // -- if there is a student who solved the quiz faster then the fastest one --
                if (sameRightAnswersSolvers[i].TimePassed < minTime)
                {
                    minTime = sameRightAnswersSolvers[i].TimePassed;
                    minTimeStudentID = sameRightAnswersSolvers[i].ID;
                    minTimeStudent = sameRightAnswersSolvers[i];
                }
            }

            // -- removing the fastest student to find the next one --
            sameRightAnswersSolvers.Remove(minTimeStudent);
            return (minTimeStudentID);
        }

        // ----------------------------------------------------------------------------------------------
        // calculating the statistics of each question in the quiz, by finding the the
        // selection percentage of each possible answer from the possible answers to the question
        // ( the calculation presented in the "StatisticsTable" object )
        // ----------------------------------------------------------------------------------------------

        private void CalculateQuizStatistics(string quizTitle)
        {
            Quiz quiz = All_Pages.GetProjectService().GetQuizByTitleServer(quizTitle);
            QuizAnswers[] quizSubmitions = All_Pages.GetProjectService().GetStudentsQuizSubmitionsServer(quiz.ID);

            if (quizSubmitions.Length > 0)
            {
                for (int i = 0; i < quiz.Questions.Length; i++)
                {
                    // -- creating a "StatisticsTable" object for each question --
                    StatisticsTable statisticsTable = new StatisticsTable(quiz.Questions[i], new List<QuizAnswers>(quizSubmitions), this.tablesDocument, i);
                }
            }
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            string quizTitle = quizTitles.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(quizTitle))
            {
                MessageBox.Show("אנא בחר נושא חידון לייצוא.");
                return;
            }

            var quiz = All_Pages.GetProjectService().GetQuizByTitleServer(quizTitle);
            var submissions = All_Pages.GetProjectService().GetStudentsQuizSubmitionsServer(quiz.ID);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Workbook|*.xlsx";
            dialog.FileName = $"סטטיסטיקה_{quizTitle}.xlsx";

            if (dialog.ShowDialog() == true)
            {
                using (var package = new ExcelPackage())
                {
                    // Sheet 1: Records Table
                    var recordsSheet = package.Workbook.Worksheets.Add("טבלת הישגים");
                    WriteRecordsSheet(recordsSheet, quiz, submissions);

                    // Sheet 2+: Statistics per question
                    for (int i = 0; i < quiz.Questions.Length; i++)
                    {
                        var statSheet = package.Workbook.Worksheets.Add($"שאלה {i + 1}");
                        WriteQuestionStatisticsSheet(statSheet, quiz.Questions[i], submissions, i);
                    }

                    File.WriteAllBytes(dialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("הייצוא הושלם בהצלחה.");
                }
            }
        }

        private void WriteRecordsSheet(ExcelWorksheet sheet, Quiz quiz, QuizAnswers[] submissions)
        {
            var sorted = submissions.OrderByDescending(s => s.Score)
                                    .ThenBy(s => s.TimePassed).ToList();

            sheet.Cells[1, 1].Value = "מקום";
            sheet.Cells[1, 2].Value = "שם מלא";
            sheet.Cells[1, 3].Value = "תעודת זהות";
            sheet.Cells[1, 4].Value = "מספר טלפון";
            sheet.Cells[1, 5].Value = "בית ספר";
            sheet.Cells[1, 6].Value = "ניקוד";
            sheet.Cells[1, 7].Value = "זמן (בדקות)";

            for (int i = 0; i < sorted.Count; i++)
            {
                var student = All_Pages.GetProjectService().GetStudentServer(sorted[i].StudentID);
                sheet.Cells[i + 2, 1].Value = i + 1;
                sheet.Cells[i + 2, 2].Value = student.Full_Name;
                sheet.Cells[i + 2, 3].Value = student.ID_Number;
                sheet.Cells[i + 2, 4].Value = student.PhoneNumber;
                sheet.Cells[i + 2, 5].Value = student.School;
                sheet.Cells[i + 2, 6].Value = sorted[i].Score;
                sheet.Cells[i + 2, 7].Value = sorted[i].TimePassed;
            }

            sheet.Cells.AutoFitColumns();
        }
        private void WriteQuestionStatisticsSheet(ExcelWorksheet sheet, Question question, QuizAnswers[] submissions, int questionIndex)
        {
            sheet.Cells[1, 1].Value = $"שאלה: {question.QuestionContent}";
            sheet.Cells[2, 1].Value = $"מספר פריט: {question.ItemNumber}";
            sheet.Cells[3, 1].Value = $"תשובה נכונה: {question.Answer}";

            // Add header for student answer table
            sheet.Cells[5, 1].Value = "תלמיד";
            sheet.Cells[5, 2].Value = "תשובה";
            sheet.Cells[5, 3].Value = "תשובה נכונה?";
            sheet.Cells[5, 4].Value = "תשובת תלמיד: מספר פריט";

            int row = 6;

            foreach (var sub in submissions)
            {
                var student = All_Pages.GetProjectService().GetStudentServer(sub.StudentID);

                if (sub.Answers == null || sub.AnsweredItems == null)
                    continue;

                if (questionIndex >= sub.Answers.Count() || questionIndex >= sub.AnsweredItems.Count())
                    continue;

                string studentAnswer = sub.Answers[questionIndex];
                string answeredItem = sub.AnsweredItems[questionIndex];

                sheet.Cells[row, 1].Value = student.Full_Name;
                sheet.Cells[row, 2].Value = studentAnswer;
                sheet.Cells[row, 3].Value = studentAnswer == question.Answer ? "כן" : "לא";
                sheet.Cells[row, 4].Value = answeredItem;
                row++;
            }

            sheet.Cells.AutoFitColumns();
        }
    }
}