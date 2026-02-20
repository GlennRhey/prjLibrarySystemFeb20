using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
// using MySql.Data.MySqlClient; // Temporarily commented out
// using prjLibrarySystem.Data; // Temporarily commented out
// using prjLibrarySystem.Models; // Temporarily commented out

namespace prjLibrarySystem
{
    public partial class Books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("BookId");
                dt.Columns.Add("ISBN");
                dt.Columns.Add("Title");
                dt.Columns.Add("Author");
                dt.Columns.Add("Category");
                dt.Columns.Add("TotalCopies");
                dt.Columns.Add("AvailableCopies");
                
                dt.Rows.Add(1, "978-0134685991", "Effective Java", "Joshua Bloch", "Technology", 5, 5);
                dt.Rows.Add(2, "978-1491904244", "Clean Code", "Robert C. Martin", "Technology", 3, 2);
                dt.Rows.Add(3, "978-0735619678", "Code Complete", "Steve McConnell", "Technology", 4, 4);
                
                gvBooks.DataSource = dt;
                gvBooks.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = "SELECT * FROM Books";
                List<MySqlParameter> parameters = new List<MySqlParameter>();

                // Add search conditions
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    query += " WHERE Title LIKE @Search OR Author LIKE @Search OR ISBN LIKE @Search";
                    parameters.Add(new MySqlParameter("@Search", "%" + txtSearch.Text + "%"));
                }

                // Add category filter
                if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                {
                    if (query.Contains("WHERE"))
                        query += " AND Category = @Category";
                    else
                        query += " WHERE Category = @Category";
                    parameters.Add(new MySqlParameter("@Category", ddlCategory.SelectedValue));
                }

                // Add availability filter
                if (!string.IsNullOrEmpty(ddlAvailability.SelectedValue))
                {
                    if (ddlAvailability.SelectedValue == "Available")
                    {
                        if (query.Contains("WHERE"))
                            query += " AND AvailableCopies > 0";
                        else
                            query += " WHERE AvailableCopies > 0";
                    }
                    else if (ddlAvailability.SelectedValue == "Borrowed")
                    {
                        if (query.Contains("WHERE"))
                            query += " AND AvailableCopies = 0";
                        else
                            query += " WHERE AvailableCopies = 0";
                    }
                }

                query += " ORDER BY Title";

                using (var reader = DatabaseHelper.ExecuteReader(query, parameters.ToArray()))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvBooks.DataSource = dt;
                    gvBooks.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                gvBooks.DataSource = null;
                gvBooks.DataBind();
            }
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            ClearBookForm();
            lblModalTitle.Text = "Add New Book";
            hfBookId.Value = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showBookModal();", true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }

        protected void ddlAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooks();
        }

        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;
            LoadBooks();
        }

        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int bookId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditBook":
                    LoadBookForEdit(bookId);
                    break;
                case "DeleteBook":
                    DeleteBook(bookId);
                    break;
                case "ViewDetails":
                    ViewBookDetails(bookId);
                    break;
            }
        }

        private void LoadBookForEdit(int bookId)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Edit functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                string query = "SELECT * FROM Books WHERE BookId = @BookId";
                var parameters = new MySqlParameter[] { new MySqlParameter("@BookId", bookId) };

                using (var reader = DatabaseHelper.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        hfBookId.Value = reader["BookId"].ToString();
                        txtISBN.Text = reader["ISBN"].ToString();
                        txtTitle.Text = reader["Title"].ToString();
                        txtAuthor.Text = reader["Author"].ToString();
                        txtPublisher.Text = reader["Publisher"].ToString();
                        txtPublicationYear.Text = Convert.ToDateTime(reader["PublicationYear"]).ToString("yyyy-MM-dd");
                        ddlBookCategory.SelectedValue = reader["Category"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        txtTotalCopies.Text = reader["TotalCopies"].ToString();
                        txtDescription.Text = reader["Description"].ToString();

                        lblModalTitle.Text = "Edit Book";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showBookModal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private void DeleteBook(int bookId)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Delete functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                var parameters = new MySqlParameter[] { new MySqlParameter("@BookId", bookId) };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                LoadBooks();
            }
            catch (Exception ex)
            {
                // Handle error - might need to check for foreign key constraints
            }
            */
        }

        private void ViewBookDetails(int bookId)
        {
            // This could open a details modal or navigate to a details page
            // For now, we'll just load the book for editing
            LoadBookForEdit(bookId);
        }

        protected void btnSaveBook_Click(object sender, EventArgs e)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                if (string.IsNullOrEmpty(hfBookId.Value))
                {
                    // Add new book
                    string query = @"
                        INSERT INTO Books (ISBN, Title, Author, Publisher, PublicationYear, 
                                        Category, TotalCopies, AvailableCopies, Price, Description, CreatedDate)
                        VALUES (@ISBN, @Title, @Author, @Publisher, @PublicationYear, 
                                @Category, @TotalCopies, @TotalCopies, @Price, @Description, NOW())";

                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@ISBN", txtISBN.Text),
                        new MySqlParameter("@Title", txtTitle.Text),
                        new MySqlParameter("@Author", txtAuthor.Text),
                        new MySqlParameter("@Publisher", txtPublisher.Text),
                        new MySqlParameter("@PublicationYear", DateTime.Parse(txtPublicationYear.Text)),
                        new MySqlParameter("@Category", ddlBookCategory.SelectedValue),
                        new MySqlParameter("@TotalCopies", Convert.ToInt32(txtTotalCopies.Text)),
                        new MySqlParameter("@Price", Convert.ToDecimal(txtPrice.Text)),
                        new MySqlParameter("@Description", txtDescription.Text)
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }
                else
                {
                    // Update existing book
                    string query = @"
                        UPDATE Books SET 
                            ISBN = @ISBN, Title = @Title, Author = @Author, Publisher = @Publisher,
                            PublicationYear = @PublicationYear, Category = @Category, 
                            TotalCopies = @TotalCopies, Price = @Price, Description = @Description,
                            ModifiedDate = NOW()
                        WHERE BookId = @BookId";

                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@ISBN", txtISBN.Text),
                        new MySqlParameter("@Title", txtTitle.Text),
                        new MySqlParameter("@Author", txtAuthor.Text),
                        new MySqlParameter("@Publisher", txtPublisher.Text),
                        new MySqlParameter("@PublicationYear", DateTime.Parse(txtPublicationYear.Text)),
                        new MySqlParameter("@Category", ddlBookCategory.SelectedValue),
                        new MySqlParameter("@TotalCopies", Convert.ToInt32(txtTotalCopies.Text)),
                        new MySqlParameter("@Price", Convert.ToDecimal(txtPrice.Text)),
                        new MySqlParameter("@Description", txtDescription.Text),
                        new MySqlParameter("@BookId", Convert.ToInt32(hfBookId.Value))
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

                LoadBooks();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "hideBookModal();", true);
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private void ClearBookForm()
        {
            txtISBN.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPublisher.Text = "";
            txtPublicationYear.Text = "";
            ddlBookCategory.SelectedIndex = 0;
            txtPrice.Text = "";
            txtTotalCopies.Text = "";
            txtDescription.Text = "";
        }
    }
}
