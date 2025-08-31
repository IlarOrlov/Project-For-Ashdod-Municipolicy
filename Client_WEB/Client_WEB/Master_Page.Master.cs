using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Client_WEB.ProjectService_WEB;

namespace Client_WEB
{
    public partial class Master_Page : System.Web.UI.MasterPage
    {
        // -- the project's service that connects us to the WCF service --  
        private static Service1Client serviceProject = null;

        // -- the user that's using the program right now --
        private static User currentUser = null;

        // -- the key where we save if the user passed by the login/signup page, or not --
        public const string PASSED = "passed";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            serviceProject = new Service1Client();

            // Register jQuery for unobtrusive validation
            if (ScriptManager.GetCurrent(this.Page) != null && !ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-3.6.0.min.js", // Local path if you have it, otherwise CDN
                    DebugPath = "~/Scripts/jquery-3.6.0.js",
                    CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js", // Use CDN if you prefer
                    CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.js",
                    LoadSuccessExpression = "window.jQuery"
                });
            }
        }

        public static void SetUser(User user) { currentUser = user; }
        public static Service1Client GetServiceProject() { return (serviceProject); }
        public static User GetUser() { return (currentUser); }
    }
}