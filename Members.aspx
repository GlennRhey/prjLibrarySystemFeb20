<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="prjLibrarySystem.Members" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Management - Library System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css?v=2.0" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css?v=2.0" rel="stylesheet">
    <style>
        .sidebar {
            min-height: 100vh;
            background: linear-gradient(135deg, #8b0000 0%, #b11226 100%);
        }
        .sidebar .nav-link {
            color: white;
            padding: 15px 20px;
            border-radius: 0;
        }
        .sidebar .nav-link:hover {
            background-color: rgba(255, 255, 255, 0.1);
            color: white;
        }
        .sidebar .nav-link.active {
            background: rgba(255, 255, 255, 0.2);
            border-left: 4px solid white;
        }
        .main-content {
            padding: 20px;
        }
        .action-buttons .btn {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <!-- Sidebar -->
                <nav class="col-md-3 col-lg-2 d-md-block sidebar collapse">
                    <div class="position-sticky pt-3">
                        <h4 class="text-white text-center mb-4">Library System</h4>
                        <ul class="nav flex-column">
                            <li class="nav-item">
                                <a class="nav-link" href="WebForm1.aspx">
                                    <i class="fas fa-tachometer-alt me-2"></i> Dashboard
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="Books.aspx">
                                    <i class="fas fa-book me-2"></i> Books
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" href="Members.aspx">
                                    <i class="fas fa-users me-2"></i> Members
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="borrowtransac.aspx">
                                    <i class="fas fa-hand-holding me-2"></i> Borrow Taransaction
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="reports.aspx">
                                    <i class="fas fa-chart-bar me-2"></i> Reports
                                </a>
                            </li>
                        </ul>
                    </div>
                </nav>

                <!-- Main Content -->
                <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4 main-content">
                    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                        <h1 class="h2">Member Management</h1>
                        <div class="btn-toolbar mb-2 mb-md-0">
                            <div class="btn-group me-2">
                                <asp:Button ID="btnAddMember" runat="server" Text="Add New Member" 
                                    CssClass="btn btn-primary" OnClick="btnAddMember_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Search and Filter -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearchMember" runat="server" CssClass="form-control" 
                                    placeholder="Search by name, email, or card number..."></asp:TextBox>
                                <asp:Button ID="btnSearchMember" runat="server" Text="Search" 
                                    CssClass="btn btn-outline-secondary" OnClick="btnSearchMember_Click" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlMembershipType" runat="server" CssClass="form-select" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMembershipType_SelectedIndexChanged">
                                <asp:ListItem Value="">All Types</asp:ListItem>
                                <asp:ListItem Value="Student">Student</asp:ListItem>
                                <asp:ListItem Value="Faculty">Faculty</asp:ListItem>
                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                <asp:ListItem Value="Public">Public</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="">All Status</asp:ListItem>
                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                <asp:ListItem Value="Expired">Expired</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Members Grid -->
                    <div class="card shadow">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Members Directory</h6>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvMembers" runat="server" CssClass="table table-hover" 
                                AutoGenerateColumns="false" GridLines="None" AllowPaging="true" 
                                PageSize="10" OnPageIndexChanging="gvMembers_PageIndexChanging"
                                OnRowCommand="gvMembers_RowCommand" DataKeyNames="MemberId">
                                <Columns>
                                    <asp:BoundField DataField="MemberId" HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                                    <asp:BoundField DataField="LibraryCardNumber" HeaderText="Card Number" />
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <%# Eval("FirstName") + " " + Eval("LastName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                    <asp:BoundField DataField="MembershipType" HeaderText="Type" />
                                    <asp:BoundField DataField="MembershipDate" HeaderText="Member Since" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <span class='badge <%# GetStatusBadgeClass(Eval("IsActive"), Eval("ExpiryDate")) %>'>
                                                <%# GetMemberStatus(Eval("IsActive"), Eval("ExpiryDate")) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="action-buttons">
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditMember" 
                                                    CommandArgument='<%# Eval("MemberId") %>' CssClass="btn btn-sm btn-warning">
                                                    <i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteMember" 
                                                    CommandArgument='<%# Eval("MemberId") %>' CssClass="btn btn-sm btn-danger"
                                                    OnClientClick="return confirm('Are you sure you want to delete this member?');">
                                                    <i class="fas fa-trash"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDetails" runat="server" CommandName="ViewDetails" 
                                                    CommandArgument='<%# Eval("MemberId") %>' CssClass="btn btn-sm btn-info">
                                                    <i class="fas fa-eye"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination" />
                            </asp:GridView>
                        </div>
                    </div>
                </main>
            </div>
        </div>

        <!-- Add/Edit Member Modal -->
        <div class="modal fade" id="memberModal" tabindex="-1" aria-labelledby="memberModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="memberModalLabel">
                            <asp:Label ID="lblMemberModalTitle" runat="server" Text="Add New Member"></asp:Label>
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="txtFirstName" class="form-label">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="txtLastName" class="form-label">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                                    TextMode="Email" required></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="txtPhone" class="form-label">Phone</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mb-3">
                                <label for="txtAddress" class="form-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label for="txtDateOfBirth" class="form-label">Date of Birth</label>
                                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" 
                                    TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="ddlMemberType" class="form-label">Membership Type</label>
                                <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Select Type</asp:ListItem>
                                    <asp:ListItem Value="Student">Student</asp:ListItem>
                                    <asp:ListItem Value="Faculty">Faculty</asp:ListItem>
                                    <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                    <asp:ListItem Value="Public">Public</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="txtExpiryDate" class="form-label">Expiry Date</label>
                                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" 
                                    TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <div class="form-check">
                                    <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-check-input" Checked="true" />
                                    <label class="form-check-label" for="chkIsActive">
                                        Active Member
                                    </label>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfMemberId" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnSaveMember" runat="server" Text="Save Member" 
                            CssClass="btn btn-primary" OnClick="btnSaveMember_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showMemberModal() {
            var modal = new bootstrap.Modal(document.getElementById('memberModal'));
            modal.show();
        }

        function hideMemberModal() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('memberModal'));
            if (modal) {
                modal.hide();
            }
        }
    </script>
</body>
</html>
