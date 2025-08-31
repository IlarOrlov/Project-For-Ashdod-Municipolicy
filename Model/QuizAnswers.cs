using Model;
using System.Collections.Generic;

public class QuizAnswers : BaseEntity
{
    private int studentID;
    private int score;
    private int quizID;
    private List<string> answers;
    private double timePassed;
    private List<string> answeredItems;

    public int StudentID
    {
        get { return this.studentID; }
        set { this.studentID = value; }
    }

    public int Score
    {
        get { return this.score; }
        set { this.score = value; }
    }

    public int QuizID
    {
        get { return this.quizID; }
        set { this.quizID = value; }
    }

    public double TimePassed
    {
        get { return this.timePassed; }
        set { this.timePassed = value; }
    }

    public List<string> Answers
    {
        get { return this.answers; }
    }

    public void SetAnswersList(string answers)
    {
        this.answers = new List<string>(answers.Split('#'));
    }

    public List<string> AnsweredItems
    {
        get { return this.answeredItems; }
    }

    public void SetAnsweredItemsList(string items)
    {
        this.answeredItems = new List<string>(items.Split('#'));
    }
}