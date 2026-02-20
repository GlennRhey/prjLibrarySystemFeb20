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
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMembers();
            }
        }

        protected string GetStatusBadgeClass(object isActive, object expiryDate)
        {
            bool active = Convert.ToBoolean(isActive);
            DateTime? expiry = expiryDate as DateTime?;

            if (!active)
                return "bg-secondary";
            
            if (expiry.HasValue && expiry.Value < DateTime.Now)
                return "bg-warning";
            
            return "bg-success";
        }

        protected string GetMemberStatus(object isActive, object expiryDate)
        {
            bool active = Convert.ToBoolean(isActive);
            DateTime? expiry = expiryDate as DateTime?;

            if (!active)
                return "Inactive";
            
            if (expiry.HasValue && expiry.Value < DateTime.Now)
                return "Expired";
            
            return "Active";
        }

        private void LoadMembers()
        {
            try
            {
                // Create sample data for demonstration
                DataTable dt = new DataTable();
                dt.Columns.Add("MemberId");
                dt.Columns.Add("LibraryCardNumber");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("Email");
                dt.Columns.Add("Phone");
                dt.Columns.Add("MembershipType");
                dt.Columns.Add("MembershipDate");
                dt.Columns.Add("ExpiryDate");
                dt.Columns.Add("IsActive");
                
                dt.Rows.Add(1, "LC202312180001", "John", "Doe", "john.doe@email.com", "555-0101", "Student", DateTime.Now.AddDays(-30), DateTime.Now.AddMonths(6), true);
                dt.Rows.Add(2, "LC202312180002", "Jane", "Smith", "jane.smith@email.com", "555-0102", "Faculty", DateTime.Now.AddDays(-60), DateTime.Now.AddMonths(12), true);
                dt.Rows.Add(3, "LC202312180003", "Michael", "Johnson", "michael.j@email.com", "555-0103", "Staff", DateTime.Now.AddDays(-90), DateTime.Now.AddMonths(3), true);
                
                gvMembers.DataSource = dt;
                gvMembers.DataBind();
                
                // Original database code (commented out until MySQL is installed):
                /*
                string query = "SELECT * FROM Members";
                List<MySqlParameter> parameters = new List<MySqlParameter>();

                // Add search conditions
                if (!string.IsNullOrEmpty(txtSearchMember.Text))
                {
                    query += " WHERE (FirstName LIKE @Search OR LastName LIKE @Search OR Email LIKE @Search OR LibraryCardNumber LIKE @Search)";
                    parameters.Add(new MySqlParameter("@Search", "%" + txtSearchMember.Text + "%"));
                }

                // Add membership type filter
                if (!string.IsNullOrEmpty(ddlMembershipType.SelectedValue))
                {
                    if (query.Contains("WHERE"))
                        query += " AND MembershipType = @MembershipType";
                    else
                        query += " WHERE MembershipType = @MembershipType";
                    parameters.Add(new MySqlParameter("@MembershipType", ddlMembershipType.SelectedValue));
                }

                // Add status filter
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    if (ddlStatus.SelectedValue == "Active")
                    {
                        if (query.Contains("WHERE"))
                            query += " AND IsActive = 1 AND (ExpiryDate IS NULL OR ExpiryDate >= CURDATE())";
                        else
                            query += " WHERE IsActive = 1 AND (ExpiryDate IS NULL OR ExpiryDate >= CURDATE())";
                    }
                    else if (ddlStatus.SelectedValue == "Inactive")
                    {
                        if (query.Contains("WHERE"))
                            query += " AND IsActive = 0";
                        else
                            query += " WHERE IsActive = 0";
                    }
                    else if (ddlStatus.SelectedValue == "Expired")
                    {
                        if (query.Contains("WHERE"))
                            query += " AND IsActive = 1 AND ExpiryDate < CURDATE()";
                        else
                            query += " WHERE IsActive = 1 AND ExpiryDate < CURDATE()";
                    }
                }

                query += " ORDER BY LastName, FirstName";

                using (var reader = DatabaseHelper.ExecuteReader(query, parameters.ToArray()))
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvMembers.DataSource = dt;
                    gvMembers.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                gvMembers.DataSource = null;
                gvMembers.DataBind();
            }
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            ClearMemberForm();
            lblMemberModalTitle.Text = "Add New Member";
            hfMemberId.Value = "";
            txtExpiryDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showMemberModal();", true);
        }

        protected void btnSearchMember_Click(object sender, EventArgs e)
        {
            LoadMembers();
        }

        protected void ddlMembershipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMembers();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMembers();
        }

        protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMembers.PageIndex = e.NewPageIndex;
            LoadMembers();
        }

        protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int memberId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditMember":
                    LoadMemberForEdit(memberId);
                    break;
                case "DeleteMember":
                    DeleteMember(memberId);
                    break;
                case "ViewDetails":
                    ViewMemberDetails(memberId);
                    break;
            }
        }

        private void LoadMemberForEdit(int memberId)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Edit functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                string query = "SELECT * FROM Members WHERE MemberId = @MemberId";
                var parameters = new MySqlParameter[] { new MySqlParameter("@MemberId", memberId) };

                using (var reader = DatabaseHelper.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        hfMemberId.Value = reader["MemberId"].ToString();
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtDateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                        ddlMemberType.SelectedValue = reader["MembershipType"].ToString();
                        
                        if (reader["ExpiryDate"] != DBNull.Value)
                        {
                            txtExpiryDate.Text = Convert.ToDateTime(reader["ExpiryDate"]).ToString("yyyy-MM-dd");
                        }
                        
                        chkIsActive.Checked = Convert.ToBoolean(reader["IsActive"]);

                        lblMemberModalTitle.Text = "Edit Member";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showMemberModal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private void DeleteMember(int memberId)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Delete functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                string query = "DELETE FROM Members WHERE MemberId = @MemberId";
                var parameters = new MySqlParameter[] { new MySqlParameter("@MemberId", memberId) };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                LoadMembers();
            }
            catch (Exception ex)
            {
                // Handle error - might need to check for foreign key constraints
            }
            */
        }

        private void ViewMemberDetails(int memberId)
        {
            // This could open a details modal or navigate to a details page
            // For now, we'll just load the member for editing
            LoadMemberForEdit(memberId);
        }

        protected void btnSaveMember_Click(object sender, EventArgs e)
        {
            // Temporarily show a message until database is set up
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Save functionality will be available after database setup.');", true);
            
            // Original database code (commented out until MySQL is installed):
            /*
            try
            {
                if (string.IsNullOrEmpty(hfMemberId.Value))
                {
                    // Add new member
                    string query = @"
                        INSERT INTO Members (FirstName, LastName, Email, Phone, Address, DateOfBirth, 
                                           MembershipType, MembershipDate, ExpiryDate, IsActive, LibraryCardNumber, CreatedDate)
                        VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, 
                                @MembershipType, NOW(), @ExpiryDate, @IsActive, @LibraryCardNumber, NOW())";

                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@FirstName", txtFirstName.Text),
                        new MySqlParameter("@LastName", txtLastName.Text),
                        new MySqlParameter("@Email", txtEmail.Text),
                        new MySqlParameter("@Phone", txtPhone.Text),
                        new MySqlParameter("@Address", txtAddress.Text),
                        new MySqlParameter("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text)),
                        new MySqlParameter("@MembershipType", ddlMemberType.SelectedValue),
                        new MySqlParameter("@ExpiryDate", DateTime.Parse(txtExpiryDate.Text)),
                        new MySqlParameter("@IsActive", chkIsActive.Checked),
                        new MySqlParameter("@LibraryCardNumber", GenerateLibraryCardNumber())
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }
                else
                {
                    // Update existing member
                    string query = @"
                        UPDATE Members SET 
                            FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone,
                            Address = @Address, DateOfBirth = @DateOfBirth, MembershipType = @MembershipType,
                            ExpiryDate = @ExpiryDate, IsActive = @IsActive, ModifiedDate = NOW()
                        WHERE MemberId = @MemberId";

                    var parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@FirstName", txtFirstName.Text),
                        new MySqlParameter("@LastName", txtLastName.Text),
                        new MySqlParameter("@Email", txtEmail.Text),
                        new MySqlParameter("@Phone", txtPhone.Text),
                        new MySqlParameter("@Address", txtAddress.Text),
                        new MySqlParameter("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text)),
                        new MySqlParameter("@MembershipType", ddlMemberType.SelectedValue),
                        new MySqlParameter("@ExpiryDate", DateTime.Parse(txtExpiryDate.Text)),
                        new MySqlParameter("@IsActive", chkIsActive.Checked),
                        new MySqlParameter("@MemberId", Convert.ToInt32(hfMemberId.Value))
                    };

                    DatabaseHelper.ExecuteNonQuery(query, parameters);
                }

                LoadMembers();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "hideMemberModal();", true);
            }
            catch (Exception ex)
            {
                // Handle error
            }
            */
        }

        private string GenerateLibraryCardNumber()
        {
            // Generate a unique library card number
            return "LC" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void ClearMemberForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtDateOfBirth.Text = "";
            ddlMemberType.SelectedIndex = 0;
            txtExpiryDate.Text = "";
            chkIsActive.Checked = true;
        }
    }
}
