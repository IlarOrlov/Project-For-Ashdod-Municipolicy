using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Client_WEB.Login_Page
{
    public partial class Login_ASPX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string idNum = IDnumber.Value;        // User ID number
            string userPassword = passWord.Value; // User password

            // Input Validation
            if (string.IsNullOrEmpty(idNum) || !Regex.IsMatch(idNum, "^[0-9]{9}$")) // Example: Israeli ID regex
            {
                DisplayError("Invalid ID number format.");
                return;
            }

            if (string.IsNullOrEmpty(userPassword))
            {
                DisplayError("Invalid password format.");
                return;
            }

            // Use parameterized queries to prevent SQL Injection
            if (Master_Page.GetServiceProject().IsUserExistServer(idNum, userPassword))
            {
                Master_Page.SetUser(Master_Page.GetServiceProject().GetUserServer(idNum));
                Session[Master_Page.PASSED] = true; // Login succeeded
                Response.Redirect("../StudentMenu_Page/StudentMenu_ASPX.aspx", false);
            }
            else
            {
                DisplayError("Incorrect ID number or password.");
            }
        }

        private void DisplayError(string message)
        {
            // Output Encoding to prevent XSS
            string encodedMessage = HttpUtility.HtmlEncode(message);
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{encodedMessage}')", true);

            // Highlight fields with errors
            IDnumber.Attributes["style"] = "border: 3px solid; border-color: red; font-size: 28px;";
            passWord.Attributes["style"] = "border: 3px solid; border-color: red; font-size: 28px;";
        }
    }
}