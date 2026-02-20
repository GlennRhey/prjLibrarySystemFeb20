using System;
using System.Web.UI;

namespace prjLibrarySystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is already logged in
            if (Session["UserRole"] != null && Session["Username"] != null)
            {
                // Redirect to appropriate page based on role
                string role = Session["UserRole"].ToString();
                if (role == "Admin")
                {
                    Response.Redirect("WebForm1.aspx");
                }
                else
                {
                    Response.Redirect("StudentDashboard.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = hfSelectedRole.Value;

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Please enter both username and password");
                return;
            }

            // Simple authentication (in real app, this would be against a database)
            if (AuthenticateUser(username, password, role))
            {
                // Set session variables
                Session["Username"] = username;
                Session["UserRole"] = role;
                Session["LoginTime"] = DateTime.Now;

                // Redirect based on role
                if (role == "Admin")
                {
                    Response.Redirect("WebForm1.aspx");
                }
                else
                {
                    Response.Redirect("StudentDashboard.aspx");
                }
            }
            else
            {
                ShowError("Invalid username or password");
            }
        }

        private bool AuthenticateUser(string username, string password, string role)
        {
            // Demo authentication - in real app, this would check against database
            if (role == "Student")
            {
                return username == "student" && password == "123456";
            }
            else if (role == "Admin")
            {
                return username == "admin" && password == "admin123";
            }
            return false;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            divError.Visible = true;
        }
    }
}
