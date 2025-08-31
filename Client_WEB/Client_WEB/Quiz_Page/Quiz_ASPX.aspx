<%@ Page Title="חידון ארכיון אשדוד" Language="C#" MasterPageFile="~/Master_Page.Master" AutoEventWireup="true" CodeBehind="Quiz_ASPX.aspx.cs" Inherits="Client_WEB.Quiz_Page.Quiz_ASPX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

    .aspNetHidden {
        display: none !important;
    }

    html, body {
        height: 100%;
        font-size: 16px;
        direction: rtl;
    }

    body {
        font-family: Arial, sans-serif;
        background: linear-gradient(135deg, #6a11cb, #2575fc);
        background-size: 200% 200%;
        animation: gradientBackground 10s ease infinite;
        color: #fff;
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 10px;
    }

    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        text-align: center;
        width: 100%;
        max-width: 1200px;
        padding: 10px;
        direction: rtl;
    }

    .quiz-title, .question, .answer-button, #startQuiz, .logout-button, .user-name, .item-number-input {
        direction: rtl;
        font-size: clamp(1rem, 2.5vw, 3rem);
        word-break: break-word;
        overflow-wrap: break-word;
        text-align: center;
    }

    /* Quiz Title */
    .quiz-title {
        font-family: 'Ts Matka', sans-serif;
        font-weight: bold;
        background: rgba(0, 0, 0, 0.3);
        color: #fff;
        border: 2px solid rgba(255, 255, 255, 0.5);
        border-radius: 15px;
        padding: 15px 30px;
        margin-bottom: 20px;
        width: 90%;
        max-width: 800px;
        box-shadow: 0 6px 15px rgba(0, 0, 0, 0.2);
    }

    /* Question Styling */
    .question {
        font-family: 'Nehama', sans-serif;
        font-weight: bold;
        background: rgba(0, 0, 0, 0.2);
        color: #fff;
        border: 2px solid rgba(255, 255, 255, 0.4);
        border-radius: 15px;
        padding: 10px 20px;
        margin: 20px 0;
        width: 90%;
        max-width: 800px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    }

    /* Buttons Row */
    .buttons-row {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        gap: 10px;
        width: 100%;
        max-width: 800px;
    }

    /* Answer Buttons */
    .answer-button, #startQuiz {
        font-family: 'Nehama', sans-serif;
        color: #fff;
        background-color: #080864;
        border: 2px solid #fff;
        border-radius: 50px;
        padding: 10px 20px;
        cursor: pointer;
        transition: transform 0.2s ease, background-color 0.3s ease;
        min-width: 120px;
        width: calc(50% - 20px);
        max-width: 200px;
    }

    .answer-button:hover, #startQuiz:hover {
        background-color: #2575fc;
    }

    .answer-button:active, #startQuiz:active {
        transform: scale(0.98);
    }

    /* User Info Section */
    .user-info {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 10px 15px;
        background-color: rgba(255, 255, 255, 0.1);
        border-radius: 8px;
        position: absolute;
        top: 50px;
        left: 50%;
        transform: translateX(-50%);
        width: 80%;
        max-width: 800px;
    }

    /* User Name */
    .user-name {
        font-family: 'Asakim', sans-serif;
        font-size: clamp(1rem, 2.5vw, 2rem);
        color: #fff;
        background-color: transparent;
        border: none;
        text-align: center;
        word-break: break-word;
        overflow-wrap: break-word;
        white-space: normal;
        width: 100%;
    }

    /* Logout Button */
    .logout-button {
        font-family: 'Nehama', sans-serif;
        background-color: #080864;
        color: #fff;
        border: 2px solid #fff;
        border-radius: 30px;
        padding: 5px 15px;
        cursor: pointer;
        transition: transform 0.2s ease, background-color 0.3s ease;
    }

    .logout-button:hover {
        background-color: #2575fc;
    }

    .logout-button:active {
        transform: scale(0.98);
    }

    /* Responsive Text Sizes */
    @media (max-width: 768px) {
        .quiz-title, .question, .answer-button, #startQuiz, .logout-button, .user-name {
            font-size: clamp(0.9rem, 2.5vw, 2rem);
        }
    }

    @media (max-width: 480px) {
        .answer-button, #startQuiz {
            width: 100% !important;
            max-width: 100%;
            padding: 10px;
        }
    }

    /* Item Number Input */
    .item-number-input {
        font-family: 'Nehama', sans-serif;
        width: 90%;
        max-width: 300px;
        padding: 10px;
        border-radius: 10px;
        border: 2px solid #fff;
        margin: 15px 0;
        background-color: rgba(255,255,255,0.1);
        color: #fff;
    }

    .item-number-input::placeholder {
        color: #fff;
    }

    .validator-message {
        font-family: 'Nehama', sans-serif;
        font-size: clamp(0.8rem, 2vw, 1rem);
        color: #ff8080;
        margin-bottom: 10px;
    }

    .answer-button {
        font-family: 'Nehama', sans-serif;
        color: #fff;
        background-color: #080864;
        border: 2px solid #fff;
        border-radius: 50px;
        padding: 10px 20px;
        cursor: pointer;
        transition: transform 0.2s ease, background-color 0.3s ease;
        min-width: 120px;
        width: calc(50% - 20px);
        max-width: 200px;

        font-size: clamp(0.9rem, 2vw, 1.5rem); /* <-- More flexible sizing */
        white-space: normal !important;       /* <-- Allow line breaks */
        word-break: break-word !important;    /* <-- Ensure text wraps inside button */
        text-align: center;
        line-height: 1.2;                     /* Optional: better vertical fit */
    }
   
    .popup {
    display: none;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background: #ffffff; /* fully opaque white background */
    color: #000;
    padding: 30px;
    border-radius: 15px;
    box-shadow: 0 8px 20px rgba(0, 0, 0, 0.5);
    z-index: 1000;
    width: 90%;
    max-width: 420px;
    text-align: right;
    direction: rtl;
    font-family: 'Nehama', sans-serif;
    line-height: 1.6;
}

.popup p {
    font-size: 1rem;
    text-align: center;
    white-space: pre-line;
    margin: 10px 0;
}

.popup h2 {
    text-align: center;
    margin-bottom: 10px;
    font-size: 1.5rem;
}

.popup p:last-of-type {
    font-weight: bold;
}

.close-button {
    margin-top: 20px;
    padding: 10px 20px;
    background-color: #080864;
    color: white;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    font-size: 1rem;
}

.close-button:hover {
    background-color: #2575fc;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="user-info">
            <asp:TextBox ID="userName" ReadOnly="true" runat="server" CssClass="user-name" Text="John Doe"></asp:TextBox>
            <asp:Button ID="logout" runat="server" Text="התנתקות" CssClass="logout-button" OnClick="Logout_Click" />
        </div>
        <asp:TextBox ID="title" runat="server" ReadOnly="true" CssClass="quiz-title" Text="Quiz Title"></asp:TextBox>
        <asp:TextBox ID="question" runat="server" ReadOnly="true" CssClass="question" Text="What is the capital of France?"></asp:TextBox>
        <asp:TextBox 
            ID="ItemNumberInput" 
            runat="server" 
            CssClass="item-number-input" 
            placeholder="הזן מספר פריט" 
            MaxLength="7">
        </asp:TextBox>

        <asp:RequiredFieldValidator 
            ID="ItemNumberRequiredValidator" 
            runat="server" 
            ControlToValidate="ItemNumberInput"
            ErrorMessage="יש להזין מספר פריט"
            CssClass="validator-message"
            Display="Dynamic"
            ForeColor="Red">
        </asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator 
            ID="ItemNumberValidator" 
            runat="server" 
            ControlToValidate="ItemNumberInput"
            ErrorMessage="יש להזין מספר בן עד 7 ספרות בלבד (ללא אותיות)"
            CssClass="validator-message"
            ValidationExpression="^\d{1,7}$"
            Display="Dynamic"
            ForeColor="Red">
        </asp:RegularExpressionValidator>

        <div class="buttons-row" id="buttonsRow1" runat="server">
            <asp:Button ID="Answer1" runat="server" Text="Answer 1" CssClass="answer-button" />
            <asp:Button ID="Answer2" runat="server" Text="Answer 2" CssClass="answer-button" />
        </div>
        <div class="buttons-row" id="buttonsRow2" runat="server">
            <asp:Button ID="Answer3" runat="server" Text="Answer 3" CssClass="answer-button" />
            <asp:Button ID="Answer4" runat="server" Text="Answer 4" CssClass="answer-button" />
        </div>

        <!-- Success Popup (hidden by default) -->
        <div class="popup" id="successPopup" style="display:none;">
            <h2><b>תודה על השתתפותך בחידון!</b></h2>
            <p>
                נציגי הארכיון יתקשרו לזוכים על פי פרטי<br />
                ההתקשרות הרשומים.
            </p>
            <p>
                יש לכם הצעה לשאלה לחידון? הצעה לייעול? פנו<br />
                אלינו – 08-9383334 (א'-ה' 09:00-13:00).
            </p>
            <p><b>להתראות בחידונים הבאים!</b></p>
            <button class="close-button" onclick="window.location.href='../StudentMenu_Page/StudentMenu_ASPX.aspx';">סגור</button>
        </div>
    </div>
</asp:Content>