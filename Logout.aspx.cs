using System;
using System.Web.UI;

namespace prjLibrarySystem
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            Session.Abandon();
            
            // Clear session cookie if it exists
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            
            // Redirect to login page after a short delay
            Response.AddHeader("REFRESH", "2;URL=Login.aspx");
        }
    }
}
