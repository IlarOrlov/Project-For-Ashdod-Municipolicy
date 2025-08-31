<%@ Page Title="חידון ארכיון אשדוד - התחברות" Language="C#" MasterPageFile="~/Master_Page.Master" AutoEventWireup="true" CodeBehind="Login_ASPX.aspx.cs" Inherits="Client_WEB.Login_Page.Login_ASPX" %>

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
    height: 100%; /* Ensure full height */
    overflow: hidden; /* Prevent scrollbars */
}

body {
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
    background: linear-gradient(135deg, #6a11cb, #2575fc);
    background-size: 200% 200%;
    animation: gradientBackground 10s ease infinite;
    color: #080864;
    display: flex;
    align-items: center;
    justify-content: center;
}

/* Background Animation */
@keyframes gradientBackground {
    0% { background-position: 0% 50%; }
    50% { background-position: 100% 50%; }
    100% { background-position: 0% 50%; }
}

/* Container Styles */
.container {
    width: 100%;
    max-width: 500px; /* Limit container size */
    padding: 20px;
    text-align: center;
}

/* Page Title */
h1 {
    font-family: 'Ts Matka', sans-serif;
    font-weight: bold;
    color: #ffffff;
    font-size: 6vw;
    margin-bottom: 40px;
}

/* Input Fields */
.input-group {
    width: 100%;
    margin-bottom: 30px;
}

input {
    width: 100%;
    height: 60px;
    font-size: 1.5rem;
    font-family: 'Assistant ExtraBold', sans-serif;
    color: #080864;
    text-align: center;
    border: 2px solid #ffffff;
    border-radius: 40px;
    outline: none;
    padding: 0 20px;
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.1);
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

input:focus {
    border-color: #2575fc;
    box-shadow: 0 6px 15px rgba(37, 117, 252, 0.3);
}

input::placeholder {
    color: #a0a0a0;
}

/* Button Styles */
.login-button {
    display: inline-block;
    font-weight: bold;
    font-size: 1.8rem;
    font-family: Nehama, sans-serif;
    color: #fff;
    background-color: #2575fc;
    border: none;
    border-radius: 50px;
    padding: 20px 50px;
    cursor: pointer;
    transition: background-color 0.3s ease, transform 0.2s ease;
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.1);
}

.login-button:hover {
    background-color: #1e63d0;
    transform: scale(1.05);
}

.login-button:active {
    transform: scale(1);
}

/* Responsive Design */
@media (max-width: 768px) {
    h1 {
        font-size: 8vw;
    }

    input {
        font-size: 1.3rem;
    }

    .login-button {
        font-size: 1.5rem;
        padding: 15px 40px;
    }
}

@media (max-width: 480px) {
    h1 {
        font-size: 10vw;
    }

    input {
        font-size: 1.2rem;
    }

    .login-button {
        font-size: 1.2rem;
        padding: 12px 30px;
    }
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>התחברות</h1>

        <div class="input-group">
            <input type="text" id="IDnumber" runat="server" placeholder="תעודת זהות" />
        </div>

        <div class="input-group">
            <input type="password" id="passWord" runat="server" placeholder="סיסמה" />
        </div>

        <asp:Button runat="server" Text="התחבר" OnClick="Login_Click" CssClass="login-button" />
    </div>
</asp:Content>