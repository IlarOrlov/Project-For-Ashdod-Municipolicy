<%@ Page Title="חידון ארכיון אשדוד - הרשמה" Language="C#" MasterPageFile="~/Master_Page.Master" AutoEventWireup="true" CodeBehind="Signup_ASPX.aspx.cs" Inherits="Client_WEB.Signup_Page.Signup_ASPX" %>

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

        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(135deg, #6a11cb, #2575fc);
            background-size: 200% 200%;
            animation: gradientBackground 10s ease infinite;
            color: #080864;
        }

        @keyframes gradientBackground {
            0% { background-position: 0% 50%; }
            50% { background-position: 100% 50%; }
            100% { background-position: 0% 50%; }
        }

        /* Container Styles */
        .container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
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
            max-width: 500px;
            margin-bottom: 30px;
        }

        input, select {
            width: 100%;
            max-width: 450px;
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

        input:focus, select:focus {
            border-color: #2575fc;
            box-shadow: 0 6px 15px rgba(37, 117, 252, 0.3);
        }

        input::placeholder {
            color: #a0a0a0;
        }

        /* Radio Buttons and Labels */
        .radio-group {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 20px;
            margin-bottom: 30px;
            max-width: 500px;
        }

        .radio-group input {
            width: auto;
            height: 30px;
        }

        .radio-group label {
            font-size: 1.5rem;
            font-family: 'Assistant ExtraBold', sans-serif;
            color: #ffffff;
        }

        /* Button Styles */
        .signup-button {
            display: flex;
            justify-content: center;
            align-items: center;
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
            margin-top: 20px;
        }


        .signup-button:hover {
            background-color: #1e63d0;
            transform: scale(1.05);
        }

        .signup-button:active {
            transform: scale(1);
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            h1 {
                font-size: 8vw;
            }

            input, select {
                font-size: 1.3rem;
                max-width: 95%;
            }

            .signup-button {
                font-size: 1.5rem;
                padding: 15px 40px;
                width: 95%;
            }
        }

        @media (max-width: 480px) {
            h1 {
                font-size: 10vw;
            }

            input, select {
                font-size: 1.2rem;
                max-width: 95%;
            }

            .signup-button {
                font-size: 1.2rem;
                padding: 12px 30px;
                width: 95%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>הרשמה</h1>

        <div class="input-group">
            <input type="text" id="fullName" runat="server" placeholder="שם מלא" />
        </div>
        <div class="input-group">
            <input type="text" id="IDnumber" runat="server" placeholder="תעודת זהות" />
        </div>
        <div class="input-group">
            <input type="password" id="userPassword" runat="server" placeholder="סיסמה" />
        </div>
        <div class="input-group">
            <input type="password" id="verifyPassword" runat="server" placeholder="אימות סיסמה" />
        </div>
        <div class="input-group">
            <input type="text" id="email" runat="server" placeholder="כתובת מייל" />
        </div>
        <div class="input-group">
            <input type="text" id="phoneNumber" runat="server" placeholder="מספר טלפון" />
        </div>

        <div class="radio-group">
            <Label ID="genderEmpty" runat="server" Text="Please select a gender" Visible="false" ForeColor="Red"></Label>
            <label for="female">נקבה</label>
            <input type="radio" id="female" runat="server" />
            <label for="male">זכר</label>
            <input type="radio" id="male" runat="server" />
        </div>

        <div class="input-group">
            <asp:DropDownList ID="school" runat="server" CssClass="drop-down">
                <asp:ListItem>בחר בית ספר</asp:ListItem>
                <asp:ListItem>אין</asp:ListItem>
                <asp:ListItem>אופק</asp:ListItem>
                <asp:ListItem>אורות</asp:ListItem>
                <asp:ListItem>אורט ימי</asp:ListItem>
                <asp:ListItem>אחדות</asp:ListItem>
                <asp:ListItem>אילנות</asp:ListItem>
                <asp:ListItem>אמירים</asp:ListItem>
                <asp:ListItem>אנקורי</asp:ListItem>
                <asp:ListItem>ארזים</asp:ListItem>
                <asp:ListItem>אריאל</asp:ListItem>
                <asp:ListItem>אשכול</asp:ListItem>
                <asp:ListItem>בית יעקב גור</asp:ListItem>
                <asp:ListItem>גאולים</asp:ListItem>
                <asp:ListItem>גוונים</asp:ListItem>
                <asp:ListItem>דביר</asp:ListItem>
                <asp:ListItem>היובל</asp:ListItem>
                <asp:ListItem>המגינים</asp:ListItem>
                <asp:ListItem>הקריה</asp:ListItem>
                <asp:ListItem>הרא"ה</asp:ListItem>
                <asp:ListItem>הראל</asp:ListItem>
                <asp:ListItem>חב"ד</asp:ListItem>
                <asp:ListItem>חופית</asp:ListItem>
                <asp:ListItem>חורב</asp:ListItem>
                <asp:ListItem>חזון יעקב</asp:ListItem>
                <asp:ListItem>חניכי הישיבות</asp:ListItem>
                <asp:ListItem>יד שבתאי</asp:ListItem>
                <asp:ListItem>יהל"ם</asp:ListItem>
                <asp:ListItem>יח"ד</asp:ListItem>
                <asp:ListItem>יצחק רבין</asp:ListItem>
                <asp:ListItem>ישיבה תיכונית אמי"ת</asp:ListItem>
                <asp:ListItem>לב שמחה</asp:ListItem>
                <asp:ListItem>מאיר עיני ישראל</asp:ListItem>
                <asp:ListItem>מדרשת אמי"ת לבנות</asp:ListItem>
                <asp:ListItem>מוסדות עמוד החסד</asp:ListItem>
                <asp:ListItem>מוריה</asp:ListItem>
                <asp:ListItem>מח"ט</asp:ListItem>
                <asp:ListItem>מחזיקי הדת</asp:ListItem>
                <asp:ListItem>מירון</asp:ListItem>
                <asp:ListItem>מכללה</asp:ListItem>
                <asp:ListItem>מע"ר חדש</asp:ListItem>
                <asp:ListItem>מעלות</asp:ListItem>
                <asp:ListItem>מפתן חרמון</asp:ListItem>
                <asp:ListItem>מקיף א</asp:ListItem>
                <asp:ListItem>מקיף אמי"ת ב</asp:ListItem>
                <asp:ListItem>מקיף אמי"ת י</asp:ListItem>
                <asp:ListItem>מקיף ג</asp:ListItem>
                <asp:ListItem>מקיף ד</asp:ListItem>
                <asp:ListItem>מקיף ה</asp:ListItem>
                <asp:ListItem>מקיף ו</asp:ListItem>
                <asp:ListItem>מקיף ז</asp:ListItem>
                <asp:ListItem>מקיף ח</asp:ListItem>
                <asp:ListItem>מקיף ט</asp:ListItem>
                <asp:ListItem>מקיף י"א</asp:ListItem>
                <asp:ListItem>נאות אסתר בנות</asp:ListItem>
                <asp:ListItem>נווה הרצוג</asp:ListItem>
                <asp:ListItem>נועם</asp:ListItem>
                <asp:ListItem>נוף ים</asp:ListItem>
                <asp:ListItem>ניר</asp:ListItem>
                <asp:ListItem>עוז והדר</asp:ListItem>
                <asp:ListItem>עלומים</asp:ListItem>
                <asp:ListItem>עמל טכנולוגי</asp:ListItem>
                <asp:ListItem>פרי עץ חיים</asp:ListItem>
                <asp:ListItem>צמח א</asp:ListItem>
                <asp:ListItem>צפרירים</asp:ListItem>
                <asp:ListItem>קשת</asp:ListItem>
                <asp:ListItem>קשת ראובן</asp:ListItem>
                <asp:ListItem>רימונים</asp:ListItem>
                <asp:ListItem>רננים</asp:ListItem>
                <asp:ListItem>רעים</asp:ListItem>
                <asp:ListItem>רתמים</asp:ListItem>
                <asp:ListItem>שז"ר</asp:ListItem>
                <asp:ListItem>שח"ר</asp:ListItem>
                <asp:ListItem>שחקים</asp:ListItem>
                <asp:ListItem>שילה</asp:ListItem>
                <asp:ListItem>שירת הים</asp:ListItem>
                <asp:ListItem>שערי ציון</asp:ListItem>
                <asp:ListItem>שקד</asp:ListItem>
                <asp:ListItem>שקמים</asp:ListItem>
                <asp:ListItem>ת"ת אורות התורה</asp:ListItem>
                <asp:ListItem>ת"ת אור מאיר</asp:ListItem>
                <asp:ListItem>ת"ת מורשה</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-group">
            <asp:DropDownList ID="grade" runat="server" CssClass="drop-down">
                <asp:ListItem>בחר שכבה</asp:ListItem>
                <asp:ListItem>אין</asp:ListItem>
                <asp:ListItem>א</asp:ListItem>
                <asp:ListItem>ב</asp:ListItem>
                <asp:ListItem>ג</asp:ListItem>
                <asp:ListItem>ד</asp:ListItem>
                <asp:ListItem>ה</asp:ListItem>
                <asp:ListItem>ו</asp:ListItem>
                <asp:ListItem>ז</asp:ListItem>
                <asp:ListItem>ח</asp:ListItem>
                <asp:ListItem>ט</asp:ListItem>
                <asp:ListItem>י</asp:ListItem>
                <asp:ListItem>י"א</asp:ListItem>
                <asp:ListItem>י"ב</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="input-group">
            <asp:DropDownList ID="gradeNum" runat="server" CssClass="drop-down">
                <asp:ListItem>בחר מספר כיתה</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:Button runat="server" Text="הרשם" OnClick="Signup_Click" CssClass="signup-button" />
    </div>
</asp:Content>