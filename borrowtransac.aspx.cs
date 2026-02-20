using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
// using MySql.Data.MySqlClient; // Temporarily commented out
// using prjLibrarySystem.Data; // Temporarily commented out

namespace prjLibrarySystem
{
    public partial class Loans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLoans();
                LoadMembers();
                LoadAvailableBooks();
                
                // Set default dates
                txtLoanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDueDate.Text = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
                txtReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected string GetLoanStatusBadgeClass(object isReturned, object dueDate)
        {
            bool returned = Convert.ToBoolean(isReturned);
            DateTime due = Convert.ToDateTime(dueDate);

            if (returned)
                return "bg-success";
            
            if (due < DateTime.Now)
                return "bg-danger";
            
            return "bg-primary";
        }

        protected string GetLoanStatus(object isReturned, object dueDate)
        {
            bool returned = Convert.ToBoolean(isReturned);
            DateTime due = Convert.ToDateTime(dueDate);

            if (returned)
                return "Returned";
            
            if (due < DateTime.Now)
                return "Overdue";
            
            return "Active";
        }

        private void LoadLoans()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("LoanId");
                dt.Columns.Add("BookTitle");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("LoanDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("ReturnDate");
                dt.Columns.Add("IsReturned");
                dt.Columns.Add("FineAmount");
                
                dt.Rows.Add(1, "Effective Java", "John", "Doe", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(9), DBNull.Value, false, 0.00);
                dt.Rows.Add(2, "Clean Code", "Jane", "Smith", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(11), DBNull.Value, false, 0.00);
                dt.Rows.Add(3, "Code Complete", "Michael", "Johnson", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(13), DBNull.Value, false, 0.00);
                dt.Rows.Add(4, "Design Patterns", "John", "Doe", DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4), true, 0.00);
                
                gvLoans.DataSource = dt;
                gvLoans.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = @"
                    SELECT l.*, b.Title as BookTitle, m.FirstName, m.LastName 
                    FROM Loans l
                    INNER JOIN Books b ON l.BookId = b.BookId
                    INNER JOIN Members m ON l.MemberId = m.MemberId";
                
                List<MySqlParameter> parameters = new List<MySqlParameter>();

                // Add search conditions
                if (!string.IsNullOrEmpty(txtSearchLoan.Text))
                {
                    query += " WHERE (b.Title LIKE @Search OR CONCAT(m.FirstName, ' ', m.LastName) LIKE @Search OR l.LoanId LIKE @Search)";
                    parameters.Add(new MySqlParameter("@Search", "%" + txtSearchLoan.Text + "%"));
                }

                // Add status filter
                if (!string.IsNullOrEmpty(ddlLoanStatus.SelectedValue))
                {
                    if (query.Contains("WHERE"))
                        query += " AND ";
                    else
                        query += " WHERE ";

                    switch (ddlLoanStatus.SelectedValue)
                    {
                        case "Active":
                            query += "l.IsReturned = 0 AND l.DueDate >= CURDATE()";
                            break;
                        case "Returned":
                            query += "l.IsReturned = 1";
                            break;
                        case "Overdue":
                            query += "l.IsReturned = 0 AND l.DueDate < CURDATE()";
                            break;
                    }
                }

                // Add date range filter
                if (!string.IsNullOrEmpty(ddlDateRange.SelectedValue))
                {
                    if (query.Contains("WHERE"))
                        query += " AND ";
                    else
                        query += " WHERE ";

                    switch (ddlDateRange.SelectedValue)
                    {
                        case "Today":
                            query += "DATE(l.LoanDate) = CURDATE()";
                            break;
                        case "ThisWeek":
                            query += "YEARWEEK(l.LoanDate) = YEARWEEK(CURDATE())";
                            break;
                        case "ThisMonth":
                            query += "MONTH(l.LoanDate) = MONTH(CURDATE()) AND YEAR(l.LoanDate) = YEAR(CURDATE())";
                            break;
                        case "ThisYear":
                            query += "YEAR(l.LoanDate) = YEAR(CURDATE())";
                            break;
                    }
                }

                query += " ORDER BY l.LoanDate DESC";

                using (var reader = DatabaseHelper.ExecuteReader(query, parameters.ToArray()))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvLoans.DataSource = dt;
                    gvLoans.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                gvLoans.DataSource = null;
                gvLoans.DataBind();
            }
        }

        private void LoadMembers()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("MemberId");
                dt.Columns.Add("FullName");
                dt.Columns.Add("LibraryCardNumber");
                
                dt.Rows.Add(1, "John Doe", "LC202312180001");
                dt.Rows.Add(2, "Jane Smith", "LC202312180002");
                dt.Rows.Add(3, "Michael Johnson", "LC202312180003");
                
                ddlMember.DataSource = dt;
                ddlMember.DataTextField = "FullName";
                ddlMember.DataValueField = "MemberId";
                ddlMember.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = "SELECT MemberId, CONCAT(FirstName, ' ', LastName) as FullName, LibraryCardNumber FROM Members WHERE IsActive = 1 ORDER BY LastName, FirstName";
                
                using (var reader = DatabaseHelper.ExecuteReader(query))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    
                    ddlMember.DataSource = dt;
                    ddlMember.DataTextField = "FullName";
                    ddlMember.DataValueField = "MemberId";
                    ddlMember.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle error
            }
        }

        private void LoadAvailableBooks()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("BookId");
                dt.Columns.Add("Title");
                dt.Columns.Add("Author");
                
                dt.Rows.Add(1, "Effective Java", "Joshua Bloch");
                dt.Rows.Add(2, "Clean Code", "Robert C. Martin");
                dt.Rows.Add(3, "Code Complete", "Steve McConnell");
                dt.Rows.Add(4, "Design Patterns", "Erich Gamma");
                
                ddlBook.DataSource = dt;
                ddlBook.DataTextField = "Title";
                ddlBook.DataValueField = "BookId";
                ddlBook.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = "SELECT BookId, Title, Author FROM Books WHERE AvailableCopies > 0 ORDER BY Title";
                
                using (var reader = DatabaseHelper.ExecuteReader(query))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    
                    ddlBook.DataSource = dt;
                    ddlBook.DataTextField = "Title";
                    ddlBook.DataValueField = "BookId";
                    ddlBook.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle error
            }
        }

        protected void btnNewLoan_Click(object sender, EventArgs e)
        {
            ClearLoanForm();
            LoadMembers();
            LoadAvailableBooks();
            txtLoanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDueDate.Text = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showLoanModal();", true);
        }

        protected void btnSearchLoan_Click(object sender, EventArgs e)
        {
            LoadLoans();
        }

        protected void ddlLoanStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLoans();
        }

        protected void ddlDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLoans();
        }

        protected void gvLoans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoans.PageIndex = e.NewPageIndex;
            LoadLoans();
        }

        protected void gvLoans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int loanId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ReturnBook":
                    ShowReturnModal(loanId);
                    break;
                case "RenewLoan":
                    RenewLoan(loanId);
                    break;
                case "ViewDetails":
                    ViewLoanDetails(loanId);
                    break;
            }
        }

        private void ShowReturnModal(int loanId)
        {
            try
            {
                hfLoanId.Value = loanId.ToString();
                txtReturnDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFineAmount.Text = "0.00";
                txtReturnNotes.Text = "";
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showReturnModal", "showReturnModal();", true);
            }
            catch (Exception ex)
            {
                // Handle error
            }
        }

        private void RenewLoan(int loanId)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Renew functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                string query = "UPDATE Loans SET DueDate = DATE_ADD(DueDate, INTERVAL 14 DAY), ModifiedDate = NOW() WHERE LoanId = @LoanId";
                var parameters = new MySqlParameter[] { new MySqlParameter("@LoanId", loanId) };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                LoadLoans();
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private void ViewLoanDetails(int loanId)
        {
            // This could open a details modal or navigate to a details page
            // For now, we'll just reload the loans
            LoadLoans();
        }

        protected void btnSaveLoan_Click(object sender, EventArgs e)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Loan functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                if (string.IsNullOrEmpty(ddlMember.SelectedValue) || string.IsNullOrEmpty(ddlBook.SelectedValue))
                {
                    // Show validation error
                    return;
                }

                // Start transaction
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert loan record
                            string loanQuery = @"
                                INSERT INTO Loans (BookId, MemberId, LoanDate, DueDate, Status, Notes, CreatedDate)
                                VALUES (@BookId, @MemberId, @LoanDate, @DueDate, 'Active', @Notes, NOW())";

                            var loanParams = new MySqlParameter[]
                            {
                                new MySqlParameter("@BookId", Convert.ToInt32(ddlBook.SelectedValue)),
                                new MySqlParameter("@MemberId", Convert.ToInt32(ddlMember.SelectedValue)),
                                new MySqlParameter("@LoanDate", DateTime.Parse(txtLoanDate.Text)),
                                new MySqlParameter("@DueDate", DateTime.Parse(txtDueDate.Text)),
                                new MySqlParameter("@Notes", txtNotes.Text)
                            };

                            var cmd = new MySqlCommand(loanQuery, connection, transaction);
                            cmd.Parameters.AddRange(loanParams);
                            cmd.ExecuteNonQuery();

                            // Update book available copies
                            string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookId = @BookId";
                            var updateParams = new MySqlParameter[] { new MySqlParameter("@BookId", Convert.ToInt32(ddlBook.SelectedValue)) };
                            
                            var updateCmd = new MySqlCommand(updateBookQuery, connection, transaction);
                            updateCmd.Parameters.AddRange(updateParams);
                            updateCmd.ExecuteNonQuery();

                            transaction.Commit();
                            LoadLoans();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "hideLoanModal();", true);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        protected void btnProcessReturn_Click(object sender, EventArgs e)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Return functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                int loanId = Convert.ToInt32(hfLoanId.Value);
                decimal fineAmount = string.IsNullOrEmpty(txtFineAmount.Text) ? 0 : Convert.ToDecimal(txtFineAmount.Text);

                // Start transaction
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get book ID for the loan
                            string getBookQuery = "SELECT BookId FROM Loans WHERE LoanId = @LoanId";
                            var getBookCmd = new MySqlCommand(getBookQuery, connection, transaction);
                            getBookCmd.Parameters.AddWithValue("@LoanId", loanId);
                            int bookId = Convert.ToInt32(getBookCmd.ExecuteScalar());

                            // Update loan record
                            string updateLoanQuery = @"
                                UPDATE Loans SET 
                                    ReturnDate = @ReturnDate, 
                                    IsReturned = 1, 
                                    FineAmount = @FineAmount, 
                                    Status = 'Returned',
                                    ModifiedDate = NOW()
                                WHERE LoanId = @LoanId";

                            var loanParams = new MySqlParameter[]
                            {
                                new MySqlParameter("@LoanId", loanId),
                                new MySqlParameter("@ReturnDate", DateTime.Parse(txtReturnDate.Text)),
                                new MySqlParameter("@FineAmount", fineAmount)
                            };

                            var updateLoanCmd = new MySqlCommand(updateLoanQuery, connection, transaction);
                            updateLoanCmd.Parameters.AddRange(loanParams);
                            updateLoanCmd.ExecuteNonQuery();

                            // Update book available copies
                            string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookId = @BookId";
                            var updateParams = new MySqlParameter[] { new MySqlParameter("@BookId", bookId) };
                            
                            var updateBookCmd = new MySqlCommand(updateBookQuery, connection, transaction);
                            updateBookCmd.Parameters.AddRange(updateParams);
                            updateBookCmd.ExecuteNonQuery();

                            transaction.Commit();
                            LoadLoans();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideReturnModal", "hideReturnModal();", true);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private void ClearLoanForm()
        {
            ddlMember.SelectedIndex = 0;
            ddlBook.SelectedIndex = 0;
            txtNotes.Text = "";
        }
    }
}
