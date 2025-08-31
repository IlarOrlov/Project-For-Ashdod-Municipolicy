using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Client_WPF
{
    public class QuestionStructure
    {
        private Grid questionGrid;                                // the Grid that will contain the whole needed content for this question
        private TextBlock questionTitle, answersTitle, dividor;   // the needed TextBlocks
        private TextBox[] answers;                                // the TextBoxes for the possible answers
        private CheckBox[] checkBoxes;                            // the CheckBoxes for checking what's the correct answer
        private TextBox question;                                 // the TextBox for the question's content
        private TextBox itemNumberTextBox; // for item number

        public QuestionStructure(StackPanel questionsStack, int questionNum)
        {
            // Create the main grid
            questionGrid = new Grid
            {
                Margin = new Thickness(10)
            };

            for (int i = 0; i < 7; i++)
                questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Divider line
            dividor = new TextBlock
            {
                Text = new string('-', 100),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetRow(dividor, 0);
            questionGrid.Children.Add(dividor);

            // Question title
            questionTitle = new TextBlock
            {
                Text = $"[{questionNum}] שאלה",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF080864")),
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetRow(questionTitle, 1);
            questionGrid.Children.Add(questionTitle);

            // Question TextBox
            question = new TextBox
            {
                FontSize = 22,
                FontFamily = new FontFamily("Assistant ExtraBold"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(20),
                MinWidth = 300
            };
            RoundCorners(question);
            Grid.SetRow(question, 2);
            questionGrid.Children.Add(question);

            // Answers title
            answersTitle = new TextBlock
            {
                Text = "תשובות אפשריות",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetRow(answersTitle, 3);
            questionGrid.Children.Add(answersTitle);

            // Answer rows (2 rows x 2 columns)
            answers = new TextBox[4];
            checkBoxes = new CheckBox[4];

            for (int i = 0; i < 2; i++)
                questionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            for (int i = 0; i < 2; i++)
            {
                var rowIndex = 4 + i;
                var answerGrid = new Grid();
                answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
                answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

                for (int j = 0; j < 2; j++)
                {
                    int answerIndex = i * 2 + j;

                    var answerBox = new TextBox
                    {
                        FontSize = 20,
                        FontFamily = new FontFamily("Assistant ExtraBold"),
                        Margin = new Thickness(10),
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
                    RoundCorners(answerBox);

                    var checkBox = new CheckBox
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        ToolTip = "סמן כתשובה נכונה"
                    };
                    checkBox.Checked += IsCheckBox_Checked;

                    answers[answerIndex] = answerBox;
                    checkBoxes[answerIndex] = checkBox;

                    Grid.SetColumn(answerBox, j * 2);
                    Grid.SetColumn(checkBox, j * 2 + 1);

                    answerGrid.Children.Add(answerBox);
                    answerGrid.Children.Add(checkBox);
                }

                Grid.SetRow(answerGrid, rowIndex);
                questionGrid.Children.Add(answerGrid);
            }

            // Item number TextBox
            itemNumberTextBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment= HorizontalAlignment.Center,
                FontSize = 18,
                FontFamily = new FontFamily("Assistant ExtraBold"),
                MaxLength = 7,
                ToolTip = "Enter item number (up to 7 digits)",
                Margin = new Thickness(10),
                Width = 200
            };
            itemNumberTextBox.PreviewTextInput += OnlyAllowDigits;
            RoundCorners(itemNumberTextBox);
            Grid.SetRow(itemNumberTextBox, 6);
            questionGrid.Children.Add(itemNumberTextBox);

            // Add to parent container
            questionsStack.Children.Add(questionGrid);
        }

        private void OnlyAllowDigits(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        // ----------------------------------------------------------------------------------------------
        // making sure that always only ONE CheckBox will be checked
        // ----------------------------------------------------------------------------------------------

        public string GetItemNumber()
        {
            return this.itemNumberTextBox.Text;
        }

        private void IsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox clickedCheckBox = (CheckBox)sender;

            for (int i = 0; i < 4; i++)
            {
                if (this.checkBoxes[i] != clickedCheckBox)
                    this.checkBoxes[i].IsChecked = false;
            }
        }

        // ----------------------------------------------------------------------------------------------
        // rounding the border of the buttons
        // ----------------------------------------------------------------------------------------------

        private void RoundCorners (TextBox textBox)
        {
            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(30)));
            textBox.Resources.Add(typeof(Border), borderStyle);
        }

        // ----------------------------------------------------------------------------------------------
        // returning the question's content
        // ----------------------------------------------------------------------------------------------

        public TextBox GetQuestionTitle() { return (this.question); }

        // ----------------------------------------------------------------------------------------------
        // returning the possible answers for this question
        // ----------------------------------------------------------------------------------------------

        public TextBox[] GetAnswers() { return (this.answers); }

        // ----------------------------------------------------------------------------------------------
        // returning the CheckBoxes of this question
        // ----------------------------------------------------------------------------------------------

        public CheckBox[] GetCheckBoxes() { return (this.checkBoxes); }

        // ----------------------------------------------------------------------------------------------
        // returning the right answer to this question
        // ----------------------------------------------------------------------------------------------

        public string GetRightAnswer()
        {
            string rightAnswer = "";   // the right answer for this question

            for (int i = 0; i < 4; i++)
            {
                if (this.checkBoxes[i].IsChecked == true)
                    rightAnswer = this.answers[i].Text;
            }

            return (rightAnswer);
        }

        // ----------------------------------------------------------------------------------------------
        // returning the wrong possible answers to this question
        // ----------------------------------------------------------------------------------------------

        public string[] GetWrongAnswers()
        {
            string[] wrongAnswers = new string[3];   // the wrong answers' array
            int index = 0;                           // the index we are adding the wrong answer into

            for (int i = 0; i < 4; i ++)
            {
                if (this.checkBoxes[i].IsChecked == false)
                {
                    wrongAnswers[index] = this.answers[i].Text;
                    index++;
                }
            }

            return (wrongAnswers);
        }
    }
}