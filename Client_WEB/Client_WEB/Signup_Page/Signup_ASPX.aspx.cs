using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client_WEB.Signup_Page
{
    public partial class Signup_ASPX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // ----------------------------------------------------------------------------------------------
        // Signing up the new currentUser and adding them to the database
        // ----------------------------------------------------------------------------------------------

        protected void Signup_Click(object sender, EventArgs e)
        {
            if (!CheckUserFields())
            {
                if (Master_Page.GetServiceProject().IsIDnumberExistServer(HttpUtility.HtmlEncode(IDnumber.Value)))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('משתמש עם מספר תעודת זהות זה כבר קיים במערכת :(')", true);
                    IDnumber.Value = "";
                }
                else
                {
                    string gender = male.Checked ? "M" : "F";

                    string sanitizedFullName = HttpUtility.HtmlEncode(fullName.Value);
                    string sanitizedPassword = HttpUtility.HtmlEncode(userPassword.Value);
                    string sanitizedEmail = HttpUtility.HtmlEncode(email.Value);
                    string sanitizedID = HttpUtility.HtmlEncode(IDnumber.Value);
                    string sanitizedPhone = HttpUtility.HtmlEncode(phoneNumber.Value);

                    if (Master_Page.GetServiceProject().InsertNewUserServer(sanitizedFullName, sanitizedPassword, sanitizedEmail, gender, sanitizedID, sanitizedPhone) &&
                        Master_Page.GetServiceProject().InsertStudentServer(sanitizedID, school.Text, grade.Text, gradeNum.Text))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('ההרשמה בוצעה בהצלחה :)')", true);
                        Master_Page.SetUser(Master_Page.GetServiceProject().GetUserServer(sanitizedID));
                        Session[Master_Page.PASSED] = true;   // The user passed by a Signup page
                        Response.Redirect("../StudentMenu_Page/StudentMenu_ASPX.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('ההרשמה נכשלה :(')", true);
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('חלק מהשדות לא תקינים :(')", true);
            }
        }

        // ----------------------------------------------------------------------------------------------
        // Checks if the currentUser's fields are valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckUserFields()
        {
            bool invalidFields = false;

            // -- Checking the ID number --
            if (!CheckIDnumber())
            {
                IDnumber.Attributes["style"] = "border: 3px solid; border-color: red;";
                invalidFields = true;
            }
            else
            {
                IDnumber.Attributes["style"] = "border: 1px solid;";
            }

            // -- Checking the password & password verification --
            if (string.IsNullOrWhiteSpace(userPassword.Value) ||
                string.IsNullOrWhiteSpace(verifyPassword.Value) ||
                verifyPassword.Value != userPassword.Value)
            {
                userPassword.Attributes["style"] = "border: 3px solid; border-color: red;";
                verifyPassword.Attributes["style"] = "border: 3px solid; border-color: red;";
                invalidFields = true;
            }
            else
            {
                userPassword.Attributes["style"] = "border: 1px solid;";
                verifyPassword.Attributes["style"] = "border: 1px solid;";
            }

            // -- Checking the full name --
            if (!CheckFullName())
            {
                fullName.Attributes["style"] = "border: 3px solid; border-color: red;";
                invalidFields = true;
            }
            else
            {
                fullName.Attributes["style"] = "border: 1px solid;";
            }

            // -- Checking the email --
            if (!CheckEmail())
            {
                email.Attributes["style"] = "border: 3px solid; border-color: red;";
                invalidFields = true;
            }
            else
            {
                email.Attributes["style"] = "border: 1px solid;";
            }

            // -- Checking the phone number --
            if (!CheckPhoneNumber())
            {
                phoneNumber.Attributes["style"] = "border: 3px solid; border-color: red;";
                invalidFields = true;
            }
            else
            {
                phoneNumber.Attributes["style"] = "border: 1px solid;";
            }

            // -- Checking the gender --
            if (!male.Checked && !female.Checked)
            {
                genderEmpty.Attributes["style"] = "visibility: visible;";
                invalidFields = true;
            }
            else
            {
                genderEmpty.Attributes["style"] = "visibility: hidden;";
            }

            // -- Checking the school --
            if (school.SelectedItem == null || school.SelectedValue == "בחר בית ספר")
            {
                invalidFields = true;
            }

            // -- Checking the grade --
            if (grade.SelectedItem == null || grade.SelectedValue == "בחר שכבה")
            {
                invalidFields = true;
            }

            // -- Checking the grade number --
            if (gradeNum.SelectedItem == null || gradeNum.SelectedValue == "בחר מספר כיתה")
            {
                invalidFields = true;
            }

            return invalidFields;
        }

        // ----------------------------------------------------------------------------------------------
        // Checks if the ID number of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckIDnumber()
        {
            string IDnum = IDnumber.Value;
            int sum = 0;

            if (string.IsNullOrWhiteSpace(IDnum) || IDnum.Length != 9 || !Regex.IsMatch(IDnum, "^\\d{9}$"))
            {
                return false;
            }

            for (int i = 0; i < 8; i++)
            {
                int digit = IDnum[i] - '0';
                sum += (i % 2 == 0) ? digit : (digit * 2 > 9 ? (digit * 2) - 9 : digit * 2);
            }

            return ((10 - (sum % 10)) % 10) == (IDnum[8] - '0');
        }

        // ----------------------------------------------------------------------------------------------
        // Checks if the full name of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckFullName()
        {
            string name = fullName.Value;
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^([א-תA-Za-z\\""'\-]+ ){1,3}[א-תA-Za-z\\""'\-]+$");
        }

        // ----------------------------------------------------------------------------------------------
        // Checks if the email address of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckEmail()
        {
            string emailAddress = email.Value;
            string emailPattern = @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return !string.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress, emailPattern);
        }

        // ----------------------------------------------------------------------------------------------
        // Checks if the phone number of the currentUser is valid
        // ----------------------------------------------------------------------------------------------

        public bool CheckPhoneNumber()
        {
            string userPhoneNumber = phoneNumber.Value;
            return !string.IsNullOrWhiteSpace(userPhoneNumber) && Regex.IsMatch(userPhoneNumber, "^05\\d{8}$");
        }
    }
}