using Model;

public class Question : BaseEntity
{
    private int quizID;
    private string question;
    private string answer;
    private string[] wrongAnswers;
    private string itemNumber;

    public int QuizID
    {
        get { return this.quizID; }
        set { this.quizID = value; }
    }

    public string QuestionContent
    {
        get { return this.question; }
        set { this.question = value; }
    }

    public string Answer
    {
        get { return this.answer; }
        set { this.answer = value; }
    }

    public string[] WrongAnswers
    {
        get { return this.wrongAnswers; }
    }

    public void InsertWrongAnswers(string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        this.wrongAnswers = new string[3];
        this.wrongAnswers[0] = wrongAnswer1;
        this.wrongAnswers[1] = wrongAnswer2;
        this.wrongAnswers[2] = wrongAnswer3;
    }

    public string ItemNumber
    {
        get { return this.itemNumber; }
        set { this.itemNumber = value; }
    }
}