<%@ Page Title="חידון ארכיון אשדוד - בחירת שאלון" Language="C#" MasterPageFile="~/Master_Page.Master" AutoEventWireup="true" CodeBehind="StudentMenu_ASPX.aspx.cs" Inherits="Client_WEB.StudentMenu_Page.StudentMenu_ASPX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General Reset */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        .aspNetHidden {
            display: none !important;
        }

        html, body {
            height: 100%; /* Ensure body spans full height of viewport */
            overflow: hidden; /* Prevent scrollbars */
        }

        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #6a11cb, #2575fc);
            background-size: 200% 200%;
            animation: gradientBackground 10s ease infinite;
            color: #fff;
            display: flex;
            justify-content: center; /* Center the content horizontally */
            align-items: center; /* Center the content vertically */
        }

        /* Container */
        .container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100%; /* Match the height of the viewport */
            text-align: center;
            width: 100%; /* Prevent content from overflowing horizontally */
        }

        /* Main Title */
        h1 {
            font-family: 'Ts Matka', sans-serif;
            font-size: 4rem;
            font-weight: bold;
            margin-bottom: 20px;
            color: #fff;
        }

        /* Subtitle */
        h2 {
            font-family: 'Nehama', sans-serif;
            font-size: 2.5rem;
            margin-bottom: 20px;
            color: #fff;
        }

        /* Dropdown and Button Styling */
        .quiz-titles, #startQuiz {
            width: 100%;
            max-width: 500px;
            height: 60px;
            font-size: 1.5rem;
            font-family: 'Asakim', sans-serif;
            color: #080864;
            border: 2px solid #fff;
            border-radius: 30px;
            padding: 0 20px;
            text-align: center;
            outline: none;
            background-color: #fff;
            transition: box-shadow 0.3s ease, border-color 0.3s ease;
        }

        .quiz-titles:focus, #startQuiz:hover {
            border-color: #6a11cb;
            box-shadow: 0 6px 15px rgba(106, 17, 203, 0.3);
        }

        #startQuiz {
            margin-top: 20px;
            background-color: #2575fc;
            color: #fff;
            font-family: 'Nehama', sans-serif;
            font-weight: bold;
            cursor: pointer;
            transition: transform 0.2s ease, background-color 0.3s ease;
        }

        #startQuiz:active {
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
            z-index: 1000;
        }

        .user-name {
            font-family: 'Asakim', sans-serif;
            font-size: 1rem;
            color: #fff;
            background-color: transparent;
            border: none;
            text-align: center;
            max-width: 120px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .logout-button {
            font-family: 'Nehama', sans-serif;
            font-size: 1rem;
            color: #fff;
            background-color: #080864;
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

        /* Responsive Design */
        @media (max-width: 768px) {
            h1 {
                font-size: 3rem;
            }

            h2 {
                font-size: 2rem;
            }

            .quiz-titles, #startQuiz {
                max-width: 90%;
                font-size: 1.2rem;
            }

            .logout-button {
                font-size: 0.9rem;
                padding: 5px 10px;
            }

            .user-name {
                font-size: 0.9rem;
            }
        }

        @media (max-width: 480px) {
            h1 {
                font-size: 2.5rem;
            }

            h2 {
                font-size: 1.8rem;
            }

            .quiz-titles, #startQuiz {
                max-width: 100%;
                font-size: 1rem;
            }

            .logout-button {
                font-size: 0.8rem;
                padding: 4px 8px;
            }

            .user-name {
                font-size: 0.8rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="user-info">
            <asp:TextBox ID="userName" ReadOnly="true" runat="server" CssClass="user-name" Text="John Doe"></asp:TextBox>
            <asp:Button ID="logout" runat="server" Text="התנתקות" CssClass="logout-button" OnClick="Logout_Click" />
        </div>

        <!-- Removed nested container div -->
        <h1>בחר שאלון</h1>
        <h2>נושא השאלון</h2>
        <asp:DropDownList ID="quizTitles" runat="server" CssClass="quiz-titles" 
            AutoPostBack="true" OnSelectedIndexChanged="quizTitels_SelectionChanged">
        </asp:DropDownList>
        <asp:Button ID="startQuiz" runat="server" Text="התחלה" CssClass="start-button" OnClick="StartQuiz_Click" />
    </div>
</asp:Content>