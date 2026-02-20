using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace prjLibrarySystem
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["Username"] == null || Session["UserRole"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Check if user is admin
            if (Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("StudentDashboard.aspx");
                return;
            }

            // Set admin name
            lblAdminName.Text = "Welcome, " + Session["Username"].ToString();

            if (!IsPostBack)
            {
                LoadDashboardStatistics();
                LoadRecentLoans();
                LoadPopularBooks();
            }
        }

        private void LoadDashboardStatistics()
        {
            try
            {
                // Temporary hardcoded values until MySQL is properly installed
                lblTotalBooks.Text = "8";
                lblTotalMembers.Text = "5";
                lblActiveLoans.Text = "3";
                lblOverdueBooks.Text = "1";
                
                // Original database code (commented out until MySQL is installed):
                /*
                // Get total books count
                string booksQuery = "SELECT COUNT(*) FROM Books";
                lblTotalBooks.Text = DatabaseHelper.ExecuteScalar(booksQuery)?.ToString() ?? "0";

                // Get total members count
                string membersQuery = "SELECT COUNT(*) FROM Members WHERE IsActive = 1";
                lblTotalMembers.Text = DatabaseHelper.ExecuteScalar(membersQuery)?.ToString() ?? "0";

                // Get active loans count
                string activeLoansQuery = "SELECT COUNT(*) FROM Loans WHERE IsReturned = 0";
                lblActiveLoans.Text = DatabaseHelper.ExecuteScalar(activeLoansQuery)?.ToString() ?? "0";

                // Get overdue books count
                string overdueQuery = "SELECT COUNT(*) FROM Loans WHERE IsReturned = 0 AND DueDate < CURDATE()";
                lblOverdueBooks.Text = DatabaseHelper.ExecuteScalar(overdueQuery)?.ToString() ?? "0";
                */
            }
            catch (Exception ex)
            {
                // Handle database connection errors gracefully
                lblTotalBooks.Text = "N/A";
                lblTotalMembers.Text = "N/A";
                lblActiveLoans.Text = "N/A";
                lblOverdueBooks.Text = "N/A";
            }
        }

        private void LoadRecentLoans()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("BookTitle");
                dt.Columns.Add("MemberName");
                dt.Columns.Add("LoanDate");
                
                dt.Rows.Add("Effective Java", "John Doe", DateTime.Now.AddDays(-5));
                dt.Rows.Add("Clean Code", "Jane Smith", DateTime.Now.AddDays(-3));
                dt.Rows.Add("Code Complete", "Michael Johnson", DateTime.Now.AddDays(-1));
                
                gvRecentLoans.DataSource = dt;
                gvRecentLoans.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = @"
                    SELECT l.LoanId, b.Title as BookTitle, 
                           CONCAT(m.FirstName, ' ', m.LastName) as MemberName, 
                           l.LoanDate
                    FROM Loans l
                    INNER JOIN Books b ON l.BookId = b.BookId
                    INNER JOIN Members m ON l.MemberId = m.MemberId
                    ORDER BY l.LoanDate DESC
                    LIMIT 10";

                using (var reader = DatabaseHelper.ExecuteReader(query))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvRecentLoans.DataSource = dt;
                    gvRecentLoans.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                gvRecentLoans.DataSource = null;
                gvRecentLoans.DataBind();
            }
        }

        private void LoadPopularBooks()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("Title");
                dt.Columns.Add("Author");
                dt.Columns.Add("LoanCount");
                
                dt.Rows.Add("Effective Java", "Joshua Bloch", "5");
                dt.Rows.Add("Clean Code", "Robert C. Martin", "4");
                dt.Rows.Add("Code Complete", "Steve McConnell", "3");
                
                gvPopularBooks.DataSource = dt;
                gvPopularBooks.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = @"
                    SELECT b.Title, b.Author, COUNT(l.LoanId) as LoanCount
                    FROM Books b
                    LEFT JOIN Loans l ON b.BookId = l.BookId
                    GROUP BY b.BookId, b.Title, b.Author
                    ORDER BY LoanCount DESC
                    LIMIT 10";

                using (var reader = DatabaseHelper.ExecuteReader(query))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvPopularBooks.DataSource = dt;
                    gvPopularBooks.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                gvPopularBooks.DataSource = null;
                gvPopularBooks.DataBind();
            }
        }
    }
}